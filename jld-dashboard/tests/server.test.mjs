import test from 'node:test';
import assert from 'node:assert/strict';
import {mkdtempSync,rmSync} from 'node:fs';
import {tmpdir} from 'node:os';
import {join,resolve} from 'node:path';
import {spawn} from 'node:child_process';
import {collections} from '../server/validation.mjs';
const folder=mkdtempSync(join(tmpdir(),'jld-integration-'));
process.env.JLD_DATABASE=join(folder,'test.sqlite');
const {createAccount,db}=await import('../server/store.mjs');
const password='Isolated-test-password-264!';
await createAccount('qa@example.invalid',password);
const origin='http://127.0.0.1:5187';
const server=spawn(process.execPath,['server/index.mjs'],{cwd:resolve('.'),env:{...process.env,PORT:'5187',JLD_ORIGIN:origin},stdio:'ignore'});
for(let i=0;i<100;i++){try{await fetch(origin);break;}catch{await new Promise(r=>setTimeout(r,50));}}
const request=(path,method='GET',body,cookie)=>fetch(origin+path,{method,headers:{Origin:origin,'Content-Type':'application/json',...(cookie?{Cookie:cookie}:{})},...(body?{body:JSON.stringify(body)}:{})});
let cookie;
test.after(async()=>{const exited=new Promise(r=>server.once('exit',r));server.kill();await exited;db.close();rmSync(folder,{recursive:true,force:true});});
test('data requires server session; query and forged cookie cannot authenticate',async()=>{
 assert.equal((await request('/api/workspace?view=workspace')).status,401);
 assert.equal((await request('/api/workspace','GET',undefined,'jld_session=forged')).status,401);
 assert.equal((await request('/data/dim_accounts.csv')).status,404);
});
test('email/password uses HttpOnly cookie; bad credentials and foreign origins rejected',async()=>{
 assert.equal((await request('/api/auth/login','POST',{email:'qa@example.invalid',password:'incorrect'})).status,401);
 const cross=await fetch(origin+'/api/auth/login',{method:'POST',headers:{Origin:'https://untrusted.invalid','Content-Type':'application/json'},body:JSON.stringify({email:'qa@example.invalid',password})});assert.equal(cross.status,403);
 const response=await request('/api/auth/login','POST',{email:'qa@example.invalid',password});assert.equal(response.status,200);
 assert.match(response.headers.get('set-cookie'),/HttpOnly/);assert.match(response.headers.get('set-cookie'),/SameSite=Strict/);cookie=response.headers.get('set-cookie').split(';')[0];
 assert.equal((await request('/api/auth/session','GET',undefined,cookie)).status,200);
});
test('buyer → agent → contract → receipt persists; invalid links and stale updates rejected',async()=>{
 const state=Object.fromEntries(collections.map(k=>[k,[]]));
 state.products=[{idproduct:1,status:'active',code:'TEST-LOT'}];state.clients=[{idclients:1,status:'active',fullname:'Test Buyer'}];state.agents=[{id:1,status:'active',fullname:'Test Agent'}];
 state.leads=[{id:1,status:'active',idclients:1,idproducts:1,idagent:1,lotprice:160400,downpayment:20000,agentpercentage:7,blockno:1,lotno:1}];
 state.payments=[{id:1,status:'active',orderreceipt:'TEST-OR-1',paidby:1,totalamount:23900,items:[{id:1,idpayment:1,idpurchasedetails:1,amount:20000,paymentfor:'DOWN PAYMENT'},{id:2,idpayment:1,idpurchasedetails:1,amount:3900,paymentfor:'INSTALLMENT'}]}];
 const save=await request('/api/workspace','PUT',{revision:0,state},cookie);assert.equal(save.status,200);assert.equal((await save.json()).revision,1);
 const reloaded=await(await request('/api/workspace','GET',undefined,cookie)).json();assert.deepEqual(reloaded.state,state);
 assert.equal((await request('/api/workspace','PUT',{revision:0,state},cookie)).status,409);
 const broken=structuredClone(state);broken.clients=[];assert.equal((await request('/api/workspace','PUT',{revision:1,state:broken},cookie)).status,400);
 const duplicate=structuredClone(state);duplicate.leads.push({...duplicate.leads[0],id:2});assert.equal((await request('/api/workspace','PUT',{revision:1,state:duplicate},cookie)).status,400);
 const overpaid=structuredClone(state);overpaid.payments[0].items[1].amount=160400;overpaid.payments[0].totalamount=180400;assert.equal((await request('/api/workspace','PUT',{revision:1,state:overpaid},cookie)).status,400);
});
test('logout invalidates server session',async()=>{
 assert.equal((await request('/api/auth/logout','POST',{},cookie)).status,200);
 assert.equal((await request('/api/workspace','GET',undefined,cookie)).status,401);
});
