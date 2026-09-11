import type { Client,PurchaseDetail,PaymentTransaction } from '../../types';
import { X,FileText } from 'lucide-react';
import { ModalFrame } from './ModalFrame';
import { PurchaseDetailsTable } from '../tables/PurchaseDetailsTable';
interface PurchaseDetailsModalProps{isOpen:boolean;onClose:()=>void;client:Client|null;purchases:PurchaseDetail[];payments:PaymentTransaction[];onEditPurchase:(p:PurchaseDetail)=>void;onViewHistory:(p:PurchaseDetail)=>void;onAddNewPurchase:(c:Client)=>void;onArchivePurchase?:(p:PurchaseDetail)=>void;}
export function PurchaseDetailsModal(p:PurchaseDetailsModalProps){
  if(!p.isOpen||!p.client) return null;
  const client=p.client;
  return (
    <ModalFrame title="Buyer contracts" onClose={p.onClose}>
      <div className="contract-details-modal-shell form-shell form-shell-wide">
        <header className="contract-details-modal-header form-header">
          <div className="contract-details-icon-wrapper form-heading-icon">
            <FileText size={22}/>
          </div>
          <div className="contract-details-title-group">
            <span className="contract-details-eyebrow form-eyebrow">PURCHASE CONTRACTS</span>
            <h2 className="contract-details-title">Buyer Contracts</h2>
            <p className="contract-details-subtitle">
              {client.fullname || `${client.firstname} ${client.lastname}`} · Stakeholder #{client.idclients}
            </p>
          </div>
          <button className="contract-details-close-btn form-close" onClick={p.onClose} aria-label="Close contracts">
            <X size={18}/>
          </button>
        </header>
        <div className="contract-details-body form-body" style={{ padding: '20px 24px' }}>
          <PurchaseDetailsTable payments={p.payments}
            purchases={p.purchases}
            onNewPurchase={() => p.onAddNewPurchase(client)}
            onEditPurchase={p.onEditPurchase}
            onViewDetails={p.onViewHistory}
            onArchivePurchase={p.onArchivePurchase}
          />
        </div>
      </div>
    </ModalFrame>
  );
}
