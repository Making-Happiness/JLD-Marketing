import { useState } from 'react';
import { Printer } from 'lucide-react';
import type { PayrollRecord,PayslipRecord } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { payslipFromPayroll } from '../../utils/workflows';
import { RecordTable } from './RecordTable';
import { ModalFrame } from '../modals/ModalFrame';
interface PayrollTableProps {initialMode?:'payroll'|'payslips';payrollRecords:PayrollRecord[];payslips?:PayslipRecord[];onProcessPayroll?:()=>void;onGeneratePayslip?:()=>void;onArchivePayroll?:(r:PayrollRecord)=>void;onRestorePayroll?:(r:PayrollRecord)=>void;onPermanentDeletePayroll?:(r:PayrollRecord)=>void;onArchivePayslip?:(r:PayslipRecord)=>void;onRestorePayslip?:(r:PayslipRecord)=>void;onPermanentDeletePayslip?:(r:PayslipRecord)=>void;onViewHistory?:(r:PayrollRecord|PayslipRecord,type:'Payroll'|'Payslip')=>void;}
export function PayrollTable(p:PayrollTableProps){
  const [mode,setMode]=useState(p.initialMode||'payroll');
  const [selectedSlip,setSelectedSlip]=useState<PayslipRecord|null>(null);
  const slips=p.payslips||[];
  const tabs=(
    <div className="payroll-mode-tabs workspace-tabs">
      <button className="payroll-tab-button" aria-pressed={mode==='payroll'} onClick={()=>setMode('payroll')}>Payroll records</button>
      <button className="payroll-tab-button" aria-pressed={mode==='payslips'} onClick={()=>setMode('payslips')}>Employee payslips</button>
    </div>
  );

  return (
    <div className="payroll-table-view">
      {mode==='payroll' ? (
        <RecordTable
          rows={p.payrollRecords}
          rowKey={r=>r.id}
          searchText={r=>`${r.employeeName} ${r.designation} ${r.period}`}
          addLabel="Prepare payroll"
          onAdd={p.onProcessPayroll}
          actions={{onArchive:p.onArchivePayroll,onRestore:p.onRestorePayroll,onDelete:p.onPermanentDeletePayroll,onHistory:r=>p.onViewHistory?.(r,'Payroll')}}
          columns={[
            {label:'Employee',render:r=><strong className="payroll-employee-name">{r.employeeName}</strong>},
            {label:'Period',render:r=><span className="payroll-period-text">{r.period}</span>},
            {label:'Days',numeric:true,render:r=><span className="payroll-days-count">{r.daysWorked}</span>},
            {label:'Gross pay',numeric:true,render:r=><span className="payroll-gross-pay">{formatCurrency(r.grossPay)}</span>},
            {label:'Deductions',numeric:true,render:r=><details className="payroll-deductions-expander"><summary className="payroll-deductions-summary">{formatCurrency(r.totalDeductions)}</summary><div className="payroll-deductions-breakdown deduction-detail">{[['Cash advance',r.cashAdvance],['Merienda',r.merienda],['Egg',r.egg],['Rice',r.rice],['Emergency fund',r.emergencyFund],['Undertime',r.undertime],['Other',r.otherDeductions]].map(([name,amount])=><p key={name} className="payroll-deduction-line">{name}: {formatCurrency(Number(amount))}</p>)}</div></details>},
            {label:'Net pay',numeric:true,render:r=><strong className="payroll-net-pay">{formatCurrency(r.netPay)}</strong>},
            {label:'Status',render:r=><span className="payroll-status-badge source-pill neutral">{r.approvalStatus}</span>}
          ]}
          primaryAction={r=><button className="payroll-view-slip-btn table-action" onClick={()=>setSelectedSlip(slips.find(s=>s.idemployee===r.idemployee&&s.month===r.period)||payslipFromPayroll(r))}>View slip</button>}
          summary={rows=><span className="payroll-summary-total">Net pay <strong>{formatCurrency(rows.reduce((s,r)=>s+r.netPay,0))}</strong></span>}
        >
          {tabs}
        </RecordTable>
      ) : (
        <RecordTable
          rows={slips}
          rowKey={r=>r.id}
          searchText={r=>`${r.employeeName} ${r.designation} ${r.month}`}
          addLabel="Generate payslips"
          onAdd={p.onGeneratePayslip}
          actions={{onArchive:p.onArchivePayslip,onRestore:p.onRestorePayslip,onDelete:p.onPermanentDeletePayslip,onHistory:r=>p.onViewHistory?.(r,'Payslip')}}
          columns={[
            {label:'Employee',render:r=><strong className="payslip-employee-name">{r.employeeName}</strong>},
            {label:'Period',render:r=><span className="payslip-period-text">{r.month}</span>},
            {label:'Designation',render:r=><span className="payslip-designation-text">{r.designation}</span>},
            {label:'Basic pay',numeric:true,render:r=><span className="payslip-basic-pay">{formatCurrency(r.basicSalary)}</span>},
            {label:'Benefits',numeric:true,render:r=><span className="payslip-benefits-amount">{formatCurrency(r.benefits)}</span>},
            {label:'Net pay',numeric:true,render:r=><strong className="payslip-net-pay">{formatCurrency(r.netPay)}</strong>},
            {label:'Status',render:r=><span className="payslip-status-tag">{r.payoutStatus}</span>}
          ]}
          primaryAction={r=><button className="payslip-view-print-btn table-action" onClick={()=>setSelectedSlip(r)}>View / print</button>}
        >
          {tabs}
        </RecordTable>
      )}

      {selectedSlip && (
        <ModalFrame onClose={()=>setSelectedSlip(null)} title="Payslip">
          <div className="payslip-preview-modal-card bg-white rounded-2xl max-w-lg w-full p-6 border border-slate-200 shadow-2xl space-y-4 text-xs">
            <div className="payslip-modal-header text-center pb-3 border-b border-slate-200">
              <h3 className="payslip-company-title text-base font-bold text-slate-900">JLD SUBDIVISION &amp; REAL PROPERTY</h3>
              <p className="payslip-doc-subtitle text-slate-500 text-[11px]">Employee payslip · {selectedSlip.payoutStatus}</p>
              <div className="payslip-period-banner mt-1 font-bold text-slate-800">{selectedSlip.month}</div>
            </div>

            <div className="payslip-metadata-grid grid grid-cols-2 gap-2 text-slate-700">
              <div className="payslip-meta-item"><span className="payslip-meta-label text-slate-400">Employee:</span> <strong className="payslip-meta-val text-slate-900">{selectedSlip.employeeName}</strong></div>
              <div className="payslip-meta-item"><span className="payslip-meta-label text-slate-400">Designation:</span> <span className="payslip-meta-val">{selectedSlip.designation}</span></div>
            </div>

            <div className="payslip-financial-breakdown border-t border-b border-slate-200 py-3 space-y-2">
              <div className="payslip-breakdown-row flex justify-between">
                <span className="payslip-breakdown-label">Basic Salary:</span>
                <span className="payslip-breakdown-value font-semibold">{formatCurrency(selectedSlip.basicSalary)}</span>
              </div>
              <div className="payslip-breakdown-row flex justify-between text-emerald-700">
                <span className="payslip-breakdown-label">Overtime &amp; Special Pay:</span>
                <span className="payslip-breakdown-value font-semibold">+{formatCurrency(selectedSlip.overtimeEarnings)}</span>
              </div>
              <div className="payslip-breakdown-row flex justify-between text-emerald-700">
                <span className="payslip-breakdown-label">Allowances &amp; Benefits:</span>
                <span className="payslip-breakdown-value font-semibold">+{formatCurrency(selectedSlip.benefits)}</span>
              </div>
              <div className="payslip-breakdown-row flex justify-between text-rose-600">
                <span className="payslip-breakdown-label">Cash Advance Deduction:</span>
                <span className="payslip-breakdown-value font-semibold">-{formatCurrency(selectedSlip.cashAdvanceDeduction)}</span>
              </div>
              <div className="payslip-breakdown-row flex justify-between text-rose-600">
                <span className="payslip-breakdown-label">Other Deductions (CA/Egg/Rice/Merienda):</span>
                <span className="payslip-breakdown-value font-semibold">-{formatCurrency(selectedSlip.otherDeductions)}</span>
              </div>
            </div>

            <div className="payslip-total-row flex justify-between items-center text-sm font-bold pt-1">
              <span className="payslip-total-label">NET SALARY PAYABLE:</span>
              <span className="payslip-total-amount text-emerald-800 text-base">{formatCurrency(selectedSlip.netPay)}</span>
            </div>

            <div className="payslip-modal-actions flex justify-end gap-2 pt-4">
              <button
                onClick={() => window.print()}
                className="payslip-action-print px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl flex items-center gap-1.5 cursor-pointer"
              >
                <Printer className="w-3.5 h-3.5" />
                <span>Print Slip</span>
              </button>
              <button
                onClick={() => setSelectedSlip(null)}
                className="payslip-action-close px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl cursor-pointer"
              >
                Close
              </button>
            </div>
          </div>
        </ModalFrame>
      )}
    </div>
  );
}
export default PayrollTable;

