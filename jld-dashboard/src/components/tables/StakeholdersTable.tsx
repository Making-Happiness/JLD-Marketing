import type { Client } from '../../types';
import { RecordTable } from './RecordTable';
interface StakeholdersTableProps{clients:Client[];onNewRecord:()=>void;onEditClient:(r:Client)=>void;onArchiveClient:(r:Client)=>void;onRestoreClient?:(r:Client)=>void;onPermanentDeleteClient?:(r:Client)=>void;onViewHistory?:(r:Client)=>void;onApply:(r:Client)=>void;onSelectClient:(r:Client)=>void;}
export function StakeholdersTable(p:StakeholdersTableProps){
  return (
    <div className="stakeholders-table-view">
      <RecordTable
        rows={p.clients}
        rowKey={r=>r.idclients}
        searchText={r=>`${r.idclients} ${r.fullname} ${r.firstname} ${r.lastname} ${r.contactno} ${r.email||''}`}
        addLabel="Add stakeholder"
        onAdd={p.onNewRecord}
        actions={{onEdit:p.onEditClient,onArchive:p.onArchiveClient,onRestore:p.onRestoreClient,onDelete:p.onPermanentDeleteClient,onHistory:p.onViewHistory}}
        primaryAction={r=><div className="buyer-actions-group"><button className="buyer-contracts-btn table-action" onClick={()=>p.onSelectClient(r)}>View contracts</button><button className="buyer-new-contract-btn table-action" onClick={()=>p.onApply(r)}>New contract</button></div>}
        columns={[
          {label:'Buyer ID',render:r=><span className="buyer-id-badge">{`BUY-${r.idclients}`}</span>},
          {label:'Stakeholder name',render:r=><strong className="buyer-fullname-text">{r.fullname||`${r.firstname} ${r.lastname}`}</strong>},
          {label:'Contact number',render:r=><span className="buyer-contact-text">{r.contactno||'—'}</span>},
          {label:'Email',render:r=><span className="buyer-email-text">{r.email||'—'}</span>},
          {label:'Spouse',render:r=><span className="buyer-spouse-text">{r.spousename||'—'}</span>}
        ]}
      />
    </div>
  );
}
