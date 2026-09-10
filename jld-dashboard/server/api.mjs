import { randomBytes } from 'node:crypto';
import { db, hashPassword, verifyPassword, tokenHash, sessionUser, revokeSession } from './store.mjs';
import { validateWorkspace } from './validation.mjs';

const dummyHash = await hashPassword(randomBytes(32).toString('hex'));

export const json = (res, status, data) => {
  res.writeHead(status, { 'Content-Type': 'application/json', 'Cache-Control': 'no-store' });
  res.end(JSON.stringify(data));
};

export async function parseBody(req) {
  let text = '';
  for await (const chunk of req) {
    text += chunk;
    if (Buffer.byteLength(text) > 5_000_000) throw new Error('Request too large.');
  }
  return text ? JSON.parse(text) : {};
}

export function cookie(token, age, secure = false) {
  return `jld_session=${token}; Path=/; HttpOnly; SameSite=Strict; Max-Age=${age}${secure ? '; Secure' : ''}`;
}

export async function handleApi(req, res, options = {}) {
  const {
    origin = 'http://localhost:5173',
    allowedOrigins = new Set([
      origin,
      'http://localhost:5173',
      'http://127.0.0.1:5173',
      'http://[::1]:5173'
    ]),
    secure = false,
    dev = false
  } = options;

  const url = new URL(req.url, origin);
  const path = url.pathname;
  if (!path.startsWith('/api/')) return false;

  if (!['GET', 'HEAD'].includes(req.method) && !allowedOrigins.has(req.headers.origin)) {
    json(res, 403, { error: 'Request origin is not permitted.' });
    return true;
  }

  if (!['GET', 'HEAD'].includes(req.method) && !req.headers['content-type']?.startsWith('application/json')) {
    json(res, 415, { error: 'Use a JSON request.' });
    return true;
  }

  if (path === '/api/auth/login' && req.method === 'POST') {
    try {
      const payload = await parseBody(req);
      const email = String(payload.email || '').trim().toLowerCase();
      const password = String(payload.password || '');
      if (email.length > 254 || password.length > 256) {
        json(res, 400, { error: 'Email or password is incorrect.' });
        return true;
      }
      const keys = [`ip:${req.socket?.remoteAddress || '127.0.0.1'}`, `email:${tokenHash(email)}`];
      const now = Date.now();
      db.prepare('DELETE FROM login_limits WHERE expires<?').run(now);
      if (keys.some(key => (db.prepare('SELECT count FROM login_limits WHERE key=?').get(key)?.count || 0) >= (key.startsWith('ip:') ? 30 : 10))) {
        json(res, 429, { error: 'Too many attempts. Please try again in 15 minutes.' });
        return true;
      }
      for (const key of keys) {
        db.prepare('INSERT INTO login_limits(key,count,expires) VALUES(?,1,?) ON CONFLICT(key) DO UPDATE SET count=count+1').run(key, now + 900000);
      }
      const user = db.prepare('SELECT * FROM users WHERE email=? AND active=1').get(email);
      const valid = await verifyPassword(password, user?.password_hash || dummyHash);
      if (!user || !valid) {
        json(res, 401, { error: 'Email or password is incorrect.' });
        return true;
      }
      db.prepare('DELETE FROM login_limits WHERE key=?').run(keys[1]);
      revokeSession(req);
      db.prepare('DELETE FROM sessions WHERE expires<?').run(now);
      const token = randomBytes(32).toString('hex');
      db.prepare('INSERT INTO sessions VALUES(?,?,?)').run(tokenHash(token), user.id, now + 8 * 60 * 60 * 1000);
      res.setHeader('Set-Cookie', cookie(token, 8 * 60 * 60, secure));
      json(res, 200, { user: { id: user.id, email: user.email } });
      return true;
    } catch {
      json(res, 400, { error: 'The request could not be processed.' });
      return true;
    }
  }

  if (path === '/api/auth/logout' && req.method === 'POST') {
    revokeSession(req);
    res.setHeader('Set-Cookie', cookie('', 0, secure));
    json(res, 200, { ok: true });
    return true;
  }

  // Identify user session; in dev mode fallback to primary active user if no session cookie
  let user = sessionUser(req);
  if (!user && dev) {
    user = db.prepare('SELECT id, email FROM users WHERE active=1 ORDER BY id ASC LIMIT 1').get() || null;
  }

  if (!user) {
    json(res, 401, { error: 'Please sign in to continue.' });
    return true;
  }

  if (path === '/api/auth/session' && req.method === 'GET') {
    json(res, 200, { user });
    return true;
  }

  if (path === '/api/workspace' && req.method === 'GET') {
    const row = db.prepare('SELECT revision,payload FROM workspace WHERE id=1').get();
    json(res, 200, { revision: row?.revision || 0, state: row ? JSON.parse(row.payload) : null });
    return true;
  }

  if (path === '/api/workspace' && req.method === 'PUT') {
    try {
      const { revision, state } = await parseBody(req);
      try {
        validateWorkspace(state);
      } catch (err) {
        json(res, 400, { error: err.message || 'The request could not be saved. Check the entries and try again.' });
        return true;
      }
      db.exec('BEGIN IMMEDIATE');
      try {
        const current = db.prepare('SELECT revision FROM workspace WHERE id=1').get()?.revision || 0;
        if (revision !== current) {
          if (dev) {
            // In dev mode, auto-adopt the current revision so local work is never blocked by 409
            db.prepare('INSERT INTO workspace(id,revision,payload) VALUES(1,?,?) ON CONFLICT(id) DO UPDATE SET revision=excluded.revision,payload=excluded.payload').run(current + 1, JSON.stringify(state));
            db.exec('COMMIT');
            json(res, 200, { revision: current + 1 });
            return true;
          }
          db.exec('ROLLBACK');
          json(res, 409, { error: 'Another employee updated the workspace. Reload before making further changes.', revision: current });
          return true;
        }
        db.prepare('INSERT INTO workspace(id,revision,payload) VALUES(1,?,?) ON CONFLICT(id) DO UPDATE SET revision=excluded.revision,payload=excluded.payload').run(current + 1, JSON.stringify(state));
        db.exec('COMMIT');
        json(res, 200, { revision: current + 1 });
        return true;
      } catch (error) {
        db.exec('ROLLBACK');
        throw error;
      }
    } catch {
      json(res, 400, { error: 'The request could not be saved. Check the entries and try again.' });
      return true;
    }
  }

  json(res, 404, { error: 'Not found.' });
  return true;
}
