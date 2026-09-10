import { defineConfig, type Plugin } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

function apiDevServer(): Plugin {
  return {
    name: 'jld-api-dev-server',
    configureServer(server) {
      server.middlewares.use(async (req, res, next) => {
        if (!req.url?.startsWith('/api/')) return next();
        try {
          const { handleApi } = await import('./server/api.mjs');
          const port = server.config.server.port || 5173;
          const origin = `http://localhost:${port}`;
          const handled = await handleApi(req, res, {
            origin,
            allowedOrigins: new Set([
              origin,
              `http://127.0.0.1:${port}`,
              `http://localhost:${port}`,
              `http://[::1]:${port}`,
            ]),
            secure: false,
            dev: true,
          });
          if (!handled) next();
        } catch (err: any) {
          console.error('[API Server Error]', err);
          if (!res.headersSent) {
            res.writeHead(500, { 'Content-Type': 'application/json' });
            res.end(JSON.stringify({ error: err?.message || 'Internal server error' }));
          }
        }
      });
    },
  };
}

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
    apiDevServer(),
  ],
})
