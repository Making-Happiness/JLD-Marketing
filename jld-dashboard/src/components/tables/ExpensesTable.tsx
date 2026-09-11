import type { Expense } from '../../types';
import { formatCurrency,formatDate } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface ExpensesTableProps{expenses:Expense[];onAddExpense?:()=>void;onArchiveExpense?:(r:Expense)=>void;onRestoreExpense?:(r:Expense)=>void;onPermanentDeleteExpense?:(r:Expense)=>void;onViewHistory?:(r:Expense)=>void;}
export function ExpensesTable(p:ExpensesTableProps){
  return (
    <div className="expenses-table-view">
      <RecordTable
        rows={p.expenses}
        rowKey={r=>r.id}
        searchText={r=>`${r.id} ${r.description} ${r.purpose} ${r.receivebyName} ${r.releasebyName}`}
        addLabel="Add expense"
        onAdd={p.onAddExpense}
        actions={{onArchive:p.onArchiveExpense,onRestore:p.onRestoreExpense,onDelete:p.onPermanentDeleteExpense,onHistory:p.onViewHistory}}
        columns={[
          {label:'Voucher',render:r=><span className="expense-voucher-badge">{`EXP-${r.id}`}</span>},
          {label:'Date',render:r=><span className="expense-date-cell">{formatDate(r.daterelease)}</span>},
          {label:'Description',render:r=><strong className="expense-description-text">{r.description}</strong>},
          {label:'Purpose',render:r=><span className="expense-purpose-text">{r.purpose}</span>},
          {label:'Received by',render:r=><span className="expense-received-by">{r.receivebyName}</span>},
          {label:'Released by',render:r=><span className="expense-released-by">{r.releasebyName}</span>},
          {label:'Amount',numeric:true,render:r=><span className="expense-amount-value">{formatCurrency(r.amount)}</span>}
        ]}
      />
    </div>
  );
}
