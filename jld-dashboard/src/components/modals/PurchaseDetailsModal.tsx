import type { Client,PurchaseDetail } from '../../types';
import { X,FileText } from 'lucide-react';
import { ModalFrame } from './ModalFrame';
import { PurchaseDetailsTable } from '../tables/PurchaseDetailsTable';
interface PurchaseDetailsModalProps{isOpen:boolean;onClose:()=>void;client:Client|null;purchases:PurchaseDetail[];onEditPurchase:(p:PurchaseDetail)=>void;onViewHistory:(p:PurchaseDetail)=>void;onAddNewPurchase:(c:Client)=>void;onArchivePurchase?:(p:PurchaseDetail)=>void;}
export function PurchaseDetailsModal(p:PurchaseDetailsModalProps){
  if(!p.isOpen||!p.client) return null;
  const client=p.client;
  return (
    <ModalFrame title="Buyer contracts" onClose={p.onClose}>
      <div className="contract-details-modal-shell form-shell">
        <header className="contract-details-modal-header form-header">
          <div className="contract-details-icon-wrapper form-heading-icon"><FileText size={22}/></div>
          <div className="contract-details-title-group">
            <span className="contract-details-eyebrow form-eyebrow">PROPERTY & SALES</span>
            <h2 className="contract-details-title">Buyer contracts</h2>
            <p className="contract-details-subtitle">{client.fullname||`${client.firstname} ${client.lastname}`} · Buyer #{client.idclients}</p>
          </div>
          <button className="contract-details-close-btn form-close" onClick={p.onClose} aria-label="Close contracts"><X size={20}/></button>
        </header>
        <div className="contract-details-body">
          <PurchaseDetailsTable purchases={p.purchases} onNewPurchase={()=>p.onAddNewPurchase(client)} onEditPurchase={p.onEditPurchase} onViewDetails={p.onViewHistory} onArchivePurchase={p.onArchivePurchase}/>
        </div>
      </div>
    </ModalFrame>
  );
}
