import { useState } from 'react';
import type { LoanRecord } from '../../types';
import { formatCurrency,formatDate } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface LoansTableProps{loans:LoanRecord[];benefits:LoanRecord[];onAddLoan?:()=>void;onAddBenefit?:()=>void;onArchiveLoan?:(r:LoanRecord)=>void;onRestoreLoan?:(r:LoanRecord)=>void;onPermanentDeleteLoan?:(r:LoanRecord)=>void;onViewHistory?:(r:LoanRecord)=>void;}
export function LoansTable(p:LoansTableProps){
  const [mode,setMode]=useState<'loans'|'benefits'>('loans');
  return (
    <div className="loans-table-view">
      <RecordTable
        key={mode}
        rows={p[mode]}
        rowKey={r=>r.id}
        searchText={r=>`${r.employeeName} ${r.description} ${r.category} ${r.remarks}`}
        addLabel={mode==='loans'?'Add loan':'Add benefit'}
        onAdd={mode==='loans'?p.onAddLoan:p.onAddBenefit}
        actions={{onArchive:p.onArchiveLoan,onRestore:p.onRestoreLoan,onDelete:p.onPermanentDeleteLoan,onHistory:p.onViewHistory}}
        columns={[
          {label:'Employee',render:r=><strong className="loan-employee-name">{r.employeeName}</strong>},
          {label:'Date',render:r=><span className="loan-date-cell">{formatDate(r.dateapplied)}</span>},
          {label:'Description',render:r=><span className="loan-description-cell">{r.description}</span>},
          {label:'Category',render:r=><span className="loan-category-badge source-pill neutral">{r.category}</span>},
          {label:'Amount',numeric:true,render:r=><span className="loan-amount-value">{formatCurrency(r.amount)}</span>},
          ...(mode==='loans'?[{label:'Per-cutoff deduction',numeric:true,render:(r:LoanRecord)=><span className="loan-amortization-value">{formatCurrency(r.amortization)}</span>}]:[])
        ]}
        summary={rows=><span className="loans-summary-total">Total <strong>{formatCurrency(rows.reduce((s,r)=>s+r.amount,0))}</strong></span>}
      >
        <div className="loans-tabs-container module-tabs">
          <button className="loans-tab-button" aria-pressed={mode==='loans'} onClick={()=>setMode('loans')}>Loans & cash advances</button>
          <button className="loans-tab-button" aria-pressed={mode==='benefits'} onClick={()=>setMode('benefits')}>Benefits & earnings</button>
        </div>
      </RecordTable>
    </div>
  );
}
