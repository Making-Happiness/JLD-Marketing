import http from 'node:http';
import { readFile, stat } from 'node:fs/promises';
import { resolve, extname, sep } from 'node:path';
import { handleApi, json } from './api.mjs';

const dev=process.argv.includes('--dev');
const port=Number(process.env.PORT||5173);
const host=process.env.HOST||'127.0.0.1';
const origin=process.env.JLD_ORIGIN||`http://${host}:${port}`;
const allowedOrigins=new Set([origin,`http://localhost:${port}`,`http://127.0.0.1:${port}`,`http://[::1]:${port}`]);
if(!dev&&!origin.startsWith('https://')&&!/^http:\/\/(127\.0\.0\.1|localhost)(:|$)/.test(origin))throw new Error('Production JLD_ORIGIN must use HTTPS.');
const secure=origin.startsWith('https://');
const vite=dev?await (await import('vite')).createServer({server:{middlewareMode:true},appType:'spa'}):null;
const publicRoot=resolve('dist');
const server=http.createServer(async(req,res)=>{
 try{
  res.setHeader('X-Content-Type-Options','nosniff');res.setHeader('Referrer-Policy','same-origin');res.setHeader('X-Frame-Options','DENY');
  if(secure)res.setHeader('Strict-Transport-Security','max-age=31536000');
  const path=new URL(req.url,origin).pathname;
  if(path.startsWith('/api/')){
   await handleApi(req,res,{origin,allowedOrigins,secure,dev});
   return;
  }
  // Never serve source datasets or server files as static downloads.
  if(path.startsWith('/data/')||path.startsWith('/server/'))return json(res,404,{error:'Not found.'});
  if(vite){vite.middlewares(req,res);return;}
  if(!['GET','HEAD'].includes(req.method))return json(res,405,{error:'Method not allowed.'});
  let filename=resolve(publicRoot,`.${decodeURIComponent(path)}`);
  if(filename!==publicRoot&&!filename.startsWith(publicRoot+sep))return json(res,404,{error:'Not found.'});
  if(!(await stat(filename).catch(()=>null))?.isFile())filename=resolve(publicRoot,'index.html');
  const types={'.html':'text/html','.js':'text/javascript','.css':'text/css','.svg':'image/svg+xml','.png':'image/png','.ico':'image/x-icon','.woff2':'font/woff2'};
  res.writeHead(200,{'Content-Type':types[extname(filename)]||'application/octet-stream','Cache-Control':'no-cache'});
  res.end(req.method==='HEAD'?undefined:await readFile(filename));
 }catch(error){console.error(error.message);if(!res.headersSent)json(res,400,{error:'The request could not be saved. Check the entries and try again.'});else res.end();}
});
server.listen(port,host,()=>console.log(`JLD workspace: ${origin}`));
