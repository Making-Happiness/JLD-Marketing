import { useState } from 'react';
import { Printer, X, ReceiptText } from 'lucide-react';
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
        <ModalFrame onClose={()=>setSelectedSlip(null)} title={`Payslip - ${selectedSlip.employeeName}`}>
          <div className="payslip-modal-shell form-shell form-shell-compact">
            {/* Header */}
            <header className="payslip-modal-header form-header">
              <div className="payslip-icon-badge form-heading-icon">
                <ReceiptText size={22} />
              </div>
              <div className="payslip-title-group">
                <span className="payslip-eyebrow form-eyebrow">PAYSLIP PREVIEW</span>
                <h2 className="payslip-title">{selectedSlip.employeeName}</h2>
                <p className="payslip-subtitle">
                  {selectedSlip.designation} · {selectedSlip.month} · {selectedSlip.payoutStatus}
                </p>
              </div>
              <button
                onClick={() => setSelectedSlip(null)}
                className="payslip-close-button form-close"
                aria-label="Close dialog"
              >
                <X size={18} />
              </button>
            </header>

            {/* Content */}
            <div className="payslip-body form-body">
              {/* Note / Metadata */}
              <div className="form-note">
                Official employee salary distribution voucher for <strong>{selectedSlip.month}</strong>. Payout status is currently recorded as <strong style={{ textTransform: 'uppercase' }}>{selectedSlip.payoutStatus}</strong>.
              </div>

              {/* Financial Breakdown Table / Card */}
              <div style={{ background: '#ffffff', border: '1px solid #e2eae4', borderRadius: '10px', overflow: 'hidden', boxShadow: '0 1px 2px rgba(18,58,34,0.03)' }}>
                <div style={{ padding: '12px 16px', background: '#f8fbf9', borderBottom: '1px solid #e7eeea', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <span style={{ fontSize: '12px', fontWeight: 700, color: '#173b28', textTransform: 'uppercase', letterSpacing: '0.05em' }}>Earnings &amp; Deductions Breakdown</span>
                  <span style={{ fontSize: '11px', color: '#7a8e82', fontFamily: 'monospace' }}>VOUCHER #{selectedSlip.id}</span>
                </div>

                <div style={{ padding: '14px 16px', display: 'flex', flexDirection: 'column', gap: '10px', fontSize: '13px' }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <span style={{ color: '#4a6355' }}>Basic Salary</span>
                    <strong style={{ color: '#173b28' }}>{formatCurrency(selectedSlip.basicSalary)}</strong>
                  </div>

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', color: '#15803d' }}>
                    <span>Overtime &amp; Special Pay</span>
                    <strong>+{formatCurrency(selectedSlip.overtimeEarnings)}</strong>
                  </div>

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', color: '#15803d' }}>
                    <span>Allowances &amp; Benefits</span>
                    <strong>+{formatCurrency(selectedSlip.benefits)}</strong>
                  </div>

                  <div style={{ height: '1px', background: '#edf2ee', margin: '4px 0' }} />

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', color: '#dc2626' }}>
                    <span>Cash Advance Deduction</span>
                    <strong>-{formatCurrency(selectedSlip.cashAdvanceDeduction)}</strong>
                  </div>

                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', color: '#dc2626' }}>
                    <span>Other Deductions (CA/Egg/Rice/Merienda)</span>
                    <strong>-{formatCurrency(selectedSlip.otherDeductions)}</strong>
                  </div>
                </div>

                {/* Net Salary Payable Banner */}
                <div style={{ padding: '14px 16px', background: '#eaf4ee', borderTop: '1px solid #d4e5d9', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <div>
                    <span style={{ fontSize: '11px', fontWeight: 700, color: '#1a5e3f', textTransform: 'uppercase', letterSpacing: '0.06em', display: 'block' }}>Net Take-Home Pay</span>
                    <span style={{ fontSize: '11px', color: '#527b65' }}>Calculated after statutory &amp; ledger deductions</span>
                  </div>
                  <strong style={{ fontSize: '20px', fontWeight: 700, color: '#11634d', fontVariantNumeric: 'tabular-nums' }}>
                    {formatCurrency(selectedSlip.netPay)}
                  </strong>
                </div>
              </div>
            </div>

            {/* Actions Footer */}
            <div className="payslip-modal-actions form-actions">
              <button
                type="button"
                onClick={() => window.print()}
                className="secondary-button"
              >
                <Printer size={15} />
                <span>Print Payslip</span>
              </button>
              <button
                type="button"
                onClick={() => setSelectedSlip(null)}
                className="primary-button"
              >
                Done
              </button>
            </div>
          </div>
        </ModalFrame>
      )}
    </div>
  );
}
export default PayrollTable;

