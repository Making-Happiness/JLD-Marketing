import { useState } from 'react';
import { Printer } from 'lucide-react';
import type { PayrollRecord,PayslipRecord } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { payslipFromPayroll } from '../../utils/workflows';
import { RecordTable } from './RecordTable';
import { ModalFrame } from '../modals/ModalFrame';
interface PayrollTableProps {initialMode?:'payroll'|'payslips';payrollRecords:PayrollRecord[];payslips?:PayslipRecord[];onProcessPayroll?:()=>void;onGeneratePayslip?:()=>void;onArchivePayroll?:(r:PayrollRecord)=>void;onRestorePayroll?:(r:PayrollRecord)=>void;onPermanentDeletePayroll?:(r:PayrollRecord)=>void;onArchivePayslip?:(r:PayslipRecord)=>void;onRestorePayslip?:(r:PayslipRecord)=>void;onPermanentDeletePayslip?:(r:PayslipRecord)=>void;onViewHistory?:(r:PayrollRecord|PayslipRecord,type:'Payroll'|'Payslip')=>void;}
export function PayrollTable(p:PayrollTableProps){const [mode,setMode]=useState(p.initialMode||'payroll');const [selectedSlip,setSelectedSlip]=useState<PayslipRecord|null>(null);const slips=p.payslips||[];
 const tabs=<div className="module-tabs"><button aria-pressed={mode==='payroll'} onClick={()=>setMode('payroll')}>Payroll records</button><button aria-pressed={mode==='payslips'} onClick={()=>setMode('payslips')}>Employee payslips</button></div>;
 return <>{mode==='payroll'?<RecordTable rows={p.payrollRecords} rowKey={r=>r.id} searchText={r=>`${r.employeeName} ${r.designation} ${r.period}`} addLabel="Prepare payroll" onAdd={p.onProcessPayroll} actions={{onArchive:p.onArchivePayroll,onRestore:p.onRestorePayroll,onDelete:p.onPermanentDeletePayroll,onHistory:r=>p.onViewHistory?.(r,'Payroll')}} columns={[
 {label:'Employee',render:r=><strong>{r.employeeName}</strong>},{label:'Period',render:r=>r.period},{label:'Days',numeric:true,render:r=>r.daysWorked},{label:'Gross pay',numeric:true,render:r=>formatCurrency(r.grossPay)},{label:'Deductions',numeric:true,render:r=><details><summary>{formatCurrency(r.totalDeductions)}</summary><div className="deduction-detail">{[['Cash advance',r.cashAdvance],['Merienda',r.merienda],['Egg',r.egg],['Rice',r.rice],['Emergency fund',r.emergencyFund],['Undertime',r.undertime],['Other',r.otherDeductions]].map(([name,amount])=><p key={name}>{name}: {formatCurrency(Number(amount))}</p>)}</div></details>},{label:'Net pay',numeric:true,render:r=><strong>{formatCurrency(r.netPay)}</strong>},{label:'Status',render:r=><span className="source-pill neutral">{r.approvalStatus}</span>}
 ]} primaryAction={r=><button className="table-action" onClick={()=>setSelectedSlip(slips.find(s=>s.idemployee===r.idemployee&&s.month===r.period)||payslipFromPayroll(r))}>View slip</button>} summary={rows=><>Net pay <strong>{formatCurrency(rows.reduce((s,r)=>s+r.netPay,0))}</strong></>}>{tabs}</RecordTable>:<RecordTable rows={slips} rowKey={r=>r.id} searchText={r=>`${r.employeeName} ${r.designation} ${r.month}`} addLabel="Generate payslips" onAdd={p.onGeneratePayslip} actions={{onArchive:p.onArchivePayslip,onRestore:p.onRestorePayslip,onDelete:p.onPermanentDeletePayslip,onHistory:r=>p.onViewHistory?.(r,'Payslip')}} columns={[{label:'Employee',render:r=><strong>{r.employeeName}</strong>},{label:'Period',render:r=>r.month},{label:'Designation',render:r=>r.designation},{label:'Basic pay',numeric:true,render:r=>formatCurrency(r.basicSalary)},{label:'Benefits',numeric:true,render:r=>formatCurrency(r.benefits)},{label:'Net pay',numeric:true,render:r=><strong>{formatCurrency(r.netPay)}</strong>},{label:'Status',render:r=>r.payoutStatus}]} primaryAction={r=><button className="table-action" onClick={()=>setSelectedSlip(r)}>View / print</button>}>{tabs}</RecordTable>}      {selectedSlip && (
        <ModalFrame onClose={()=>setSelectedSlip(null)} title="Payslip">
          <div className="bg-white rounded-2xl max-w-lg w-full p-6 border border-slate-200 shadow-2xl space-y-4 text-xs">
            <div className="text-center pb-3 border-b border-slate-200">
              <h3 className="text-base font-bold text-slate-900">JLD SUBDIVISION &amp; REAL PROPERTY</h3>
              <p className="text-slate-500 text-[11px]">Employee payslip · {selectedSlip.payoutStatus}</p>
              <div className="mt-1 font-bold text-slate-800">{selectedSlip.month}</div>
            </div>

            <div className="grid grid-cols-2 gap-2 text-slate-700">
              <div><span className="text-slate-400">Employee:</span> <strong className="text-slate-900">{selectedSlip.employeeName}</strong></div>
              <div><span className="text-slate-400">Designation:</span> {selectedSlip.designation}</div>
            </div>

            <div className="border-t border-b border-slate-200 py-3 space-y-2">
              <div className="flex justify-between">
                <span>Basic Salary:</span>
                <span className="font-semibold">{formatCurrency(selectedSlip.basicSalary)}</span>
              </div>
              <div className="flex justify-between text-emerald-700">
                <span>Overtime &amp; Special Pay:</span>
                <span className="font-semibold">+{formatCurrency(selectedSlip.overtimeEarnings)}</span>
              </div>
              <div className="flex justify-between text-emerald-700">
                <span>Allowances &amp; Benefits:</span>
                <span className="font-semibold">+{formatCurrency(selectedSlip.benefits)}</span>
              </div>
              <div className="flex justify-between text-rose-600">
                <span>Cash Advance Deduction:</span>
                <span className="font-semibold">-{formatCurrency(selectedSlip.cashAdvanceDeduction)}</span>
              </div>
              <div className="flex justify-between text-rose-600">
                <span>Other Deductions (CA/Egg/Rice/Merienda):</span>
                <span className="font-semibold">-{formatCurrency(selectedSlip.otherDeductions)}</span>
              </div>
            </div>

            <div className="flex justify-between items-center text-sm font-bold pt-1">
              <span>NET SALARY PAYABLE:</span>
              <span className="text-emerald-800 text-base">{formatCurrency(selectedSlip.netPay)}</span>
            </div>

            <div className="flex justify-end gap-2 pt-4">
              <button
                onClick={() => window.print()}
                className="px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl flex items-center gap-1.5 cursor-pointer"
              >
                <Printer className="w-3.5 h-3.5" />
                <span>Print Slip</span>
              </button>
              <button
                onClick={() => setSelectedSlip(null)}
                className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl cursor-pointer"
              >
                Close
              </button>
            </div>
          </div>
        </ModalFrame>
      )}
</>;}
export default PayrollTable;

