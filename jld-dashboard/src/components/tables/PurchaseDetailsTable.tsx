import { calculateStatement } from '../../utils/statement';
import type { PaymentTransaction } from '../../types';
import type { PurchaseDetail } from '../../types';
import { formatCurrency,formatDate } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface PurchaseDetailsTableProps{purchases:PurchaseDetail[];payments:PaymentTransaction[];onNewPurchase:()=>void;onEditPurchase:(r:PurchaseDetail)=>void;onArchivePurchase?:(r:PurchaseDetail)=>void;onRestorePurchase?:(r:PurchaseDetail)=>void;onPermanentDeletePurchase?:(r:PurchaseDetail)=>void;onViewHistory?:(r:PurchaseDetail)=>void;onViewDetails:(r:PurchaseDetail)=>void;}
export function PurchaseDetailsTable(p:PurchaseDetailsTableProps){
  return (
    <div className="contracts-table-view">
      <RecordTable
        rows={p.purchases}
        rowKey={r=>r.id}
        searchText={r=>`${r.clientName} ${r.productCode} ${r.location} ${r.blockno} ${r.lotno} ${r.agentName}`}
        addLabel="Add contract"
        onAdd={p.onNewPurchase}
        actions={{onEdit:p.onEditPurchase,onArchive:p.onArchivePurchase,onRestore:p.onRestorePurchase,onDelete:p.onPermanentDeletePurchase,onHistory:p.onViewHistory}}
        primaryAction={r=><button className="contract-soa-btn table-action" onClick={()=>p.onViewDetails(r)}>Statement</button>}
        columns={[
          {label:'Stakeholder',render:r=><strong className="contract-buyer-name">{r.clientName}</strong>},
          {label:'Property / lot',render:r=><span className="contract-lot-wrapper">{r.productCode}<small className="contract-lot-spec cell-secondary">Block {r.blockno} · Lot {r.lotno}</small></span>},
          {label:'Agent',render:r=><span className="contract-agent-name">{r.agentName}</span>},
          {label:'Contract price',numeric:true,render:r=><span className="contract-price-value">{formatCurrency(r.lotprice)}</span>},
          {label:'Lot balance',numeric:true,render:r=><span>{formatCurrency(calculateStatement(r,p.payments).balance)}</span>},
          {label:'Monthly payment',numeric:true,render:r=><span className="contract-amortization-value">{formatCurrency(r.amortization)}</span>},
          {label:'Term',render:r=><span className="contract-term-badge">{`${r.terms} years`}</span>},
          {label:'Due date',render:r=><span className="contract-due-date">{formatDate(r.duedate)}</span>}
        ]}
      />
    </div>
  );
}
