import type { Agent } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface AgentsTableProps{agents:Agent[];onReleaseClaim?:(r:Agent)=>void;onAddAgent?:()=>void;onArchiveAgent?:(r:Agent)=>void;onRestoreAgent?:(r:Agent)=>void;onPermanentDeleteAgent?:(r:Agent)=>void;onViewHistory?:(r:Agent)=>void;}
export function AgentsTable(p:AgentsTableProps){return <RecordTable rows={p.agents} rowKey={r=>r.id} searchText={r=>`${r.fullname} ${r.role} ${r.contactno}`} addLabel="Add agent" onAdd={p.onAddAgent} actions={{onArchive:p.onArchiveAgent,onRestore:p.onRestoreAgent,onDelete:p.onPermanentDeleteAgent,onHistory:p.onViewHistory}} columns={[
 {label:'Agent / dicer',render:r=><strong>{r.fullname}</strong>},{label:'Designation',render:r=>r.role},{label:'Contact number',render:r=>r.contactno||'—'},{label:'Rate',numeric:true,render:r=>`${r.commissionRate}%`},{label:'Sales volume',numeric:true,render:r=>formatCurrency(r.totalSales)},{label:'Earned',numeric:true,render:r=>formatCurrency(r.totalEarned)},{label:'Released',numeric:true,render:r=>formatCurrency(r.totalClaimed)},{label:'Balance',numeric:true,render:r=><strong className="positive">{formatCurrency(r.balance)}</strong>}
 ]} primaryAction={r=><button className="table-action" disabled={r.balance<=0} onClick={()=>p.onReleaseClaim?.(r)}>Release claim</button>} summary={rows=><>Balance <strong>{formatCurrency(rows.reduce((s,r)=>s+r.balance,0))}</strong></>}/>}

