import type { FullDatabaseState } from './guardRails';
import type { PayrollRecord, PayslipRecord } from '../types';
export function preparePayroll(db:FullDatabaseState,start:string,end:string,days:number):PayrollRecord[]{
 if(!start||!end||end<start||!Number.isInteger(days)||days<1||days>31)throw new Error('Enter a valid period and 1–31 whole working days.');
 if(days>Math.floor((Date.parse(end)-Date.parse(start))/86400000)+1)throw new Error('Working days cannot exceed the selected period.');
 const active=db.employees.filter(e=>e.status==='active');if(!active.length)throw new Error('Add an active employee first.');
 const period=`${start} – ${end}`;
 if(db.payrollRecords.some(p=>p.period===period))throw new Error('Payroll already exists for this period, including archived records.');
 return active.map((e,index)=>{
 const eligible=(date:string)=>date.slice(0,10)<=end;
 const cashAdvance=db.loans.filter(l=>l.status==='active'&&l.idemployee===e.idemployee&&eligible(l.dateapplied)).reduce((s,l)=>s+Math.min(l.amount,l.amortization),0);
 const earnings=db.benefits.filter(b=>b.status==='active'&&b.idemployee===e.idemployee&&b.dateapplied.slice(0,10)>=start&&eligible(b.dateapplied)).reduce((s,b)=>s+b.amount,0);
 const grossPay=Math.round((e.salary*days+earnings)*100)/100;
 if(cashAdvance>grossPay)throw new Error(`Deductions exceed gross pay for ${e.fullname}. Review adjustments first.`);
 return {id:Date.now()+index,idemployee:e.idemployee,employeeName:e.fullname,designation:e.designation,dailyRate:e.salary,daysWorked:days,grossPay,cashAdvance,merienda:0,egg:0,rice:0,emergencyFund:0,undertime:0,otherDeductions:0,totalDeductions:cashAdvance,netPay:Math.round((grossPay-cashAdvance)*100)/100,period,approvalStatus:'Pending',status:'active'};
 });
}
export function payslipFromPayroll(p:PayrollRecord):PayslipRecord{return {id:p.id,idemployee:p.idemployee,employeeName:p.employeeName,designation:p.designation,month:p.period,basicSalary:p.dailyRate*p.daysWorked,overtimeEarnings:0,benefits:Math.max(0,p.grossPay-p.dailyRate*p.daysWorked),cashAdvanceDeduction:p.cashAdvance,otherDeductions:p.totalDeductions-p.cashAdvance,netPay:p.netPay,dateGenerated:new Date().toISOString().slice(0,10),payoutStatus:'Draft',status:'active'};}
