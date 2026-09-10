const test = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const ts = require('typescript');
const vm = require('node:vm');
const compiled=ts.transpileModule(fs.readFileSync('src/utils/statement.ts','utf8'),{compilerOptions:{module:ts.ModuleKind.CommonJS,target:ts.ScriptTarget.ES2022}}).outputText;
const ctx={exports:{}};vm.runInNewContext(compiled,ctx);
const {calculateStatement,validateReceipt}=ctx.exports;
const contract={id:1,lotprice:160400,downpayment:20000};
const receipt=(id,date,amount,type='INSTALLMENT',contractId=1,status='active')=>({id,dateofpayment:date,orderreceipt:`TEST-${id}`,status,items:[{id:id+10,idpayment:id,idpurchasedetails:contractId,amount,paymentfor:type}]});
test('legacy formula and chronological running balances',()=>{
 const s=calculateStatement(contract,[receipt(2,'2026-09-10',3900),receipt(1,'2026-09-01',10000)]);
 assert.equal(s.openingBalance,140400);assert.equal(s.rows[0].balance,130400);assert.equal(s.rows[1].balance,126500);assert.equal(s.balance,126500);assert.equal(s.totalPayments,13900);
});
test('DP receipt confirms opening credit without double deduction',()=>{
 const s=calculateStatement(contract,[receipt(1,'2026-09-01',20000,'DOWN PAYMENT'),receipt(2,'2026-09-02',3900)]);
 assert.equal(s.balance,136500);assert.equal(s.totalPayments,23900);assert.equal(s.downPaymentOutstanding,0);
});
test('unreceipted DP exposed, unrelated contracts excluded, archived cash retained',()=>{
 const s=calculateStatement(contract,[receipt(1,'2026-09-01',3900,'INSTALLMENT',1,'archived'),receipt(2,'2026-09-02',99999,'INSTALLMENT',2)]);
 assert.equal(s.downPaymentOutstanding,20000);assert.equal(s.balance,136500);assert.equal(s.totalPayments,3900);
});
test('reservation unapplied, full settlement reduces principal, cents exact',()=>{
 const s=calculateStatement({id:1,lotprice:0.3,downpayment:0.1},[receipt(1,'2026-09-01',500,'RESERVED'),receipt(2,'2026-09-02',0.2,'FULL PAYMENT')]);
 assert.equal(s.balance,0);assert.equal(s.unappliedReservations,500);assert.equal(s.totalPayments,500.2);
});
test('overpayment and excessive DP blocked, negatives not concealed',()=>{
 assert.match(validateReceipt(contract,[],receipt(1,'2026-09-01',140400.01)),/exceeds/);
 assert.match(validateReceipt(contract,[],receipt(1,'2026-09-01',20001,'DOWN PAYMENT')),/exceed/);
 assert.equal(calculateStatement(contract,[receipt(1,'2026-09-01',140401)]).balance,-1);
});
test('reference sheet ending balance reconciles exactly',()=>{
 assert.equal(calculateStatement({id:1,lotprice:160400,downpayment:0},[receipt(1,'2024-10-10',146915)]).balance,13485);
});
