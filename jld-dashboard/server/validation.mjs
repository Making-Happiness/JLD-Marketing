export const collections=['leads','products','agents','clients','payments','expenses','employees','loans','benefits','payslips','payrollRecords','auditLogs'];
export function validateWorkspace(state) {
 if(!state||collections.some(key=>!Array.isArray(state[key])))throw new Error('Invalid workspace data.');
 const ids={products:'idproduct',clients:'idclients',employees:'idemployee'};
 for(const key of collections){
  const seen=new Set();
  for(const row of state[key]){
   const id=row[ids[key]||'id'];
   if(!id||seen.has(id))throw new Error(`Duplicate or missing ${key} reference.`);
   seen.add(id);
   if(key!=='auditLogs'&&!['active','archived'].includes(row.status))throw new Error(`Invalid ${key} status.`);
  }
 }
 const has=(key,field,value)=>state[key].some(row=>row[field]===value);
 const usedLots=new Set();
 for(const c of state.leads){
  if(!has('products','idproduct',c.idproducts)||!has('clients','idclients',c.idclients)||!has('agents','id',c.idagent))throw new Error('Contract references a missing property, buyer or agent.');
  if(![c.lotprice,c.downpayment,c.agentpercentage].every(Number.isFinite)||c.lotprice<=0||c.downpayment<0||c.downpayment>c.lotprice||c.agentpercentage<0||c.agentpercentage>100)throw new Error('Invalid contract amounts.');
  const key=`${c.idproducts}/${c.blockno}/${c.lotno}`;
  if(c.status==='active'){if(usedLots.has(key))throw new Error('This lot already has an active contract.');usedLots.add(key);}
 }
 const receipts=new Set();
 for(const p of state.payments){
  const ref=String(p.orderreceipt||'').trim().toLowerCase();
  if(!ref||receipts.has(ref))throw new Error('Receipt number must be unique.');receipts.add(ref);
  if(!Array.isArray(p.items)||!p.items.length||!Number.isFinite(p.totalamount))throw new Error('Invalid payment.');
  let total=0;
  for(const item of p.items){
   const contract=state.leads.find(c=>c.id===item.idpurchasedetails);
   if(!contract||contract.idclients!==p.paidby||item.idpayment!==p.id)throw new Error('Payment must belong to its buyer and contract.');
   if(!Number.isFinite(item.amount)||item.amount<=0||!['DOWN PAYMENT','INSTALLMENT','FULL PAYMENT','RESERVED'].includes(item.paymentfor))throw new Error('Invalid payment item.');
   total+=Math.round(item.amount*100);
  }
  if(total!==Math.round(p.totalamount*100))throw new Error('Receipt total does not match its items.');
 }
 for(const c of state.leads){
  const items=state.payments.flatMap(p=>p.items).filter(i=>i.idpurchasedetails===c.id);
  const amount=kind=>items.filter(i=>kind.includes(i.paymentfor)).reduce((sum,i)=>sum+Math.round(i.amount*100),0);
  if(amount(['DOWN PAYMENT'])>Math.round(c.downpayment*100)||amount(['INSTALLMENT','FULL PAYMENT'])>Math.round(c.lotprice*100)-Math.round(c.downpayment*100))throw new Error('Payments exceed the contract balance.');
 }
 for(const key of ['loans','benefits','payrollRecords','payslips'])for(const row of state[key])if(!has('employees','idemployee',row.idemployee))throw new Error('Employee record references a missing employee.');
}
