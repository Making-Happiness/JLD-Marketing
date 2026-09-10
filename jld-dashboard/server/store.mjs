import { DatabaseSync } from 'node:sqlite';
import { mkdirSync } from 'node:fs';
import { dirname, resolve } from 'node:path';
import { randomBytes, scrypt as derive, timingSafeEqual, createHash } from 'node:crypto';
import { promisify } from 'node:util';
const scrypt = promisify(derive);
export const databasePath = resolve(process.env.JLD_DATABASE || 'server/private/jld.sqlite');
mkdirSync(dirname(databasePath), { recursive: true });
export const db = new DatabaseSync(databasePath);
db.exec(`PRAGMA journal_mode=WAL; PRAGMA foreign_keys=ON;
 CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, email TEXT UNIQUE NOT NULL, password_hash TEXT NOT NULL, active INTEGER NOT NULL DEFAULT 1);
 CREATE TABLE IF NOT EXISTS sessions(token_hash TEXT PRIMARY KEY, user_id INTEGER NOT NULL REFERENCES users(id), expires INTEGER NOT NULL);
 CREATE TABLE IF NOT EXISTS workspace(id INTEGER PRIMARY KEY CHECK(id=1), revision INTEGER NOT NULL, payload TEXT NOT NULL);
 CREATE TABLE IF NOT EXISTS login_limits(key TEXT PRIMARY KEY, count INTEGER NOT NULL, expires INTEGER NOT NULL);`);
export async function hashPassword(password) {
 const salt=randomBytes(16).toString('hex');
 const hash=await scrypt(password,salt,64);
 return `${salt}:${hash.toString('hex')}`;
}
export async function verifyPassword(password,encoded) {
 const [salt,hex]=encoded.split(':');
 const actual=await scrypt(password,salt,64);
 const expected=Buffer.from(hex,'hex');
 return actual.length===expected.length && timingSafeEqual(actual,expected);
}
export const tokenHash = value => createHash('sha256').update(value).digest('hex');
export function sessionUser(req) {
 const token=(req.headers.cookie||'').split(';').map(v=>v.trim()).find(v=>v.startsWith('jld_session='))?.slice(12);
 if(!token)return null;
 return db.prepare('SELECT users.id,users.email FROM sessions JOIN users ON users.id=sessions.user_id WHERE token_hash=? AND expires>? AND users.active=1').get(tokenHash(token),Date.now()) || null;
}
export function revokeSession(req) {
 const token=(req.headers.cookie||'').split(';').map(v=>v.trim()).find(v=>v.startsWith('jld_session='))?.slice(12);
 if(token)db.prepare('DELETE FROM sessions WHERE token_hash=?').run(tokenHash(token));
}
export async function createAccount(email,password) {
 email=email.trim().toLowerCase();
 if(!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email))throw new Error('Enter a valid employee email.');
 if(password.length<6||password.length>256)throw new Error('Use a password between 6 and 256 characters.');
 const encoded=await hashPassword(password);
 db.prepare('INSERT INTO users(email,password_hash) VALUES(?,?) ON CONFLICT(email) DO UPDATE SET password_hash=excluded.password_hash,active=1').run(email,encoded);
 db.prepare('DELETE FROM sessions WHERE user_id=(SELECT id FROM users WHERE email=?)').run(email);
}

// Auto-seed primary administrator account if not present
const defaultAdmin = db.prepare('SELECT id FROM users WHERE email=?').get('kayeencampana@gmail.com');
if (!defaultAdmin) {
 await createAccount('kayeencampana@gmail.com', 'admin123');
}

