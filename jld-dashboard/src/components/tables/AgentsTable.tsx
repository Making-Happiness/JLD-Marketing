import type { Agent } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface AgentsTableProps{agents:Agent[];onReleaseClaim?:(r:Agent)=>void;onAddAgent?:()=>void;onArchiveAgent?:(r:Agent)=>void;onRestoreAgent?:(r:Agent)=>void;onPermanentDeleteAgent?:(r:Agent)=>void;onViewHistory?:(r:Agent)=>void;}
export function AgentsTable(p:AgentsTableProps){
  return (
    <div className="agents-table-view">
      <RecordTable
        rows={p.agents}
        rowKey={r=>r.id}
        searchText={r=>`${r.fullname} ${r.role} ${r.contactno}`}
        addLabel="Add agent"
        onAdd={p.onAddAgent}
        actions={{onArchive:p.onArchiveAgent,onRestore:p.onRestoreAgent,onDelete:p.onPermanentDeleteAgent,onHistory:p.onViewHistory}}
        columns={[
          {label:'Agent / dicer',render:r=><strong className="agent-fullname-text">{r.fullname}</strong>},
          {label:'Designation',render:r=><span className="agent-role-text">{r.role}</span>},
          {label:'Contact number',render:r=><span className="agent-contact-text">{r.contactno||'—'}</span>},
          {label:'Rate',numeric:true,render:r=><span className="agent-commission-rate">{`${r.commissionRate}%`}</span>},
          {label:'Sales volume',numeric:true,render:r=><span className="agent-sales-volume">{formatCurrency(r.totalSales)}</span>},
          {label:'Earned',numeric:true,render:r=><span className="agent-earned-amount">{formatCurrency(r.totalEarned)}</span>},
          {label:'Released',numeric:true,render:r=><span className="agent-claimed-amount">{formatCurrency(r.totalClaimed)}</span>},
          {label:'Balance',numeric:true,render:r=><strong className="agent-balance-amount positive">{formatCurrency(r.balance)}</strong>}
        ]}
        primaryAction={r=><button className="agent-release-voucher-btn table-action" disabled={r.balance<=0} onClick={()=>p.onReleaseClaim?.(r)}>Release claim</button>}
      />
    </div>
  );
}

