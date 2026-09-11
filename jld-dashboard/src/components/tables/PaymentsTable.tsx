import type { PaymentTransaction } from '../../types';
import { formatCurrency,formatDate } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface PaymentsTableProps{payments:PaymentTransaction[];onNewPayment:()=>void;onArchivePayment?:(r:PaymentTransaction)=>void;onRestorePayment?:(r:PaymentTransaction)=>void;onPermanentDeletePayment?:(r:PaymentTransaction)=>void;onViewHistory?:(r:PaymentTransaction)=>void;}
export function PaymentsTable(p:PaymentsTableProps){
  return (
    <div className="payments-table-view">
      <RecordTable
        rows={p.payments}
        rowKey={r=>r.id}
        searchText={r=>`${r.paymentref} ${r.orderreceipt} ${r.referenceno} ${r.paidbyName}`}
        addLabel="Record payment"
        onAdd={p.onNewPayment}
        actions={{onArchive:p.onArchivePayment,onRestore:p.onRestorePayment,onDelete:p.onPermanentDeletePayment,onHistory:p.onViewHistory}}
        columns={[
          {label:'Receipt / reference',render:r=><span className="payment-receipt-group"><strong className="payment-receipt-number">{r.orderreceipt}</strong><small className="payment-ref-code cell-secondary">{r.paymentref}</small></span>},
          {label:'Date',render:r=><span className="payment-date-cell">{formatDate(r.dateofpayment)}</span>},
          {label:'Buyer',render:r=><span className="payment-buyer-name">{r.paidbyName}</span>},
          {label:'Method',render:r=><span className="payment-method-badge source-pill neutral">{r.paymenttype}</span>},
          {label:'Items',render:r=><details className="payment-items-expander"><summary className="payment-items-summary">{r.items.length} payment items</summary><div className="payment-items-breakdown deduction-detail">{r.items.map(i=><p key={i.id} className="payment-item-row">{i.paymentfor}: {formatCurrency(i.amount)}<small className="payment-item-desc cell-secondary">{i.description}</small></p>)}</div></details>},
          {label:'Cashier',render:r=><span className="payment-cashier-name">{r.inchargebyName}</span>},
          {label:'Amount',numeric:true,render:r=><strong className="payment-total-amount">{formatCurrency(r.totalamount)}</strong>}
        ]}
      />
    </div>
  );
}
