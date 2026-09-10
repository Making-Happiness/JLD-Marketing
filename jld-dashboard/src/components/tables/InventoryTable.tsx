import type { Product } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface InventoryTableProps{products:Product[];onAddProduct:()=>void;onEditProduct:(r:Product)=>void;onArchiveProduct:(r:Product)=>void;onRestoreProduct?:(r:Product)=>void;onPermanentDeleteProduct?:(r:Product)=>void;onViewHistory?:(r:Product)=>void;}
export function InventoryTable(p:InventoryTableProps){
  return (
    <div className="inventory-table-view">
      <RecordTable
        rows={p.products}
        rowKey={r=>r.idproduct}
        searchText={r=>`${r.code} ${r.location}`}
        addLabel="Add property"
        onAdd={p.onAddProduct}
        actions={{onArchive:p.onArchiveProduct,onRestore:p.onRestoreProduct,onDelete:p.onPermanentDeleteProduct,onHistory:p.onViewHistory}}
        primaryAction={r=><button className="inventory-edit-action-btn table-action" onClick={()=>p.onEditProduct(r)}>Edit property</button>}
        columns={[
          
          {label:'Location',render:r=><span className="inventory-location-cell">{r.location}</span>},
          {label:'Blocks',numeric:true,render:r=><span className="inventory-blocks-cell">{r.totalblockno}</span>},
          {label:'Lots',numeric:true,render:r=>{
            const avail = r.availableLots ?? r.totallotno;
            return (
              <span className="inventory-lots-cell" title={`${avail} available out of ${r.totallotno} total`}>
                {r.totallotno}
                {avail < r.totallotno && <small style={{display:'block',fontSize:'10px',color:'#718679'}}>{avail} avail</small>}
              </span>
            );
          }},
          {label:'Land area (m²)',numeric:true,render:r=><span className="inventory-area-cell">{r.totalarea.toLocaleString()}</span>},
          {label:'Cash price / 100 m²',numeric:true,render:r=><span className="inventory-price-cell">{formatCurrency(r.cashprice)}</span>},
          {label:'Phase',render:r=>{
            const phase = r.projectPhase || 'Open';
            const pillClass = phase === 'Nearly Sold' ? 'amber' : phase === 'Open' ? 'green' : 'neutral';
            return <span className={`inventory-phase-tag source-pill ${pillClass}`}>{phase}</span>;
          }}
        ]}
        summary={rows=><span className="inventory-summary-text">{rows.reduce((s,r)=>s+r.totallotno,0)} total lots</span>}
      />
    </div>
  );
}
