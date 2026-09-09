import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Product } from '../../types';
import { X, Building2, MapPin, Hash, Layers, Maximize2, Tag, CheckCircle2 } from 'lucide-react';

interface ProductModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (product: Product | Omit<Product, 'idproduct'>) => void;
  productToEdit?: Product | null;
}

export const ProductModal: React.FC<ProductModalProps> = ({
  isOpen,
  onClose,
  onSave,
  productToEdit
}) => {
  const isEditing = Boolean(productToEdit);

  const [code, setCode] = useState('');
  const [location, setLocation] = useState('');
  const [totalBlock, setTotalBlock] = useState<number | ''>(1);
  const [totalLot, setTotalLot] = useState<number | ''>(10);
  const [totalArea, setTotalArea] = useState<number | ''>(1000);
  const [cashPrice, setCashPrice] = useState<number | ''>(100000);
  const [errorMsg, setErrorMsg] = useState('');
  const [successMsg, setSuccessMsg] = useState('');

  useEffect(() => {
    if (isOpen) {
      if (productToEdit) {
        setCode(productToEdit.code);
        setLocation(productToEdit.location);
        setTotalBlock(productToEdit.totalblockno);
        setTotalLot(productToEdit.totallotno);
        setTotalArea(productToEdit.totalarea);
        setCashPrice(productToEdit.cashprice);
      } else {
        setCode('');
        setLocation('');
        setTotalBlock(1);
        setTotalLot(10);
        setTotalArea(1000);
        setCashPrice(100000);
      }
      setErrorMsg('');
      setSuccessMsg('');
    }
  }, [isOpen, productToEdit]);

  if (!isOpen) return null;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!code.trim()) {
      setErrorMsg('Product Code is required.');
      return;
    }
    if (!location.trim()) {
      setErrorMsg('Location / Address is required.');
      return;
    }
    if (!totalBlock || Number(totalBlock) <= 0) {
      setErrorMsg('Total Block must be greater than 0.');
      return;
    }
    if (!totalLot || Number(totalLot) <= 0) {
      setErrorMsg('Total Lot must be greater than 0.');
      return;
    }
    if (!totalArea || Number(totalArea) <= 0) {
      setErrorMsg('Total Area (sq.m.) must be greater than 0.');
      return;
    }
    if (cashPrice === '' || Number(cashPrice) < 0) {
      setErrorMsg('Spot/Cash Price Per 100sqm must be a valid number.');
      return;
    }

    const payload = {
      ...(productToEdit ? { idproduct: productToEdit.idproduct } : {}),
      code: code.trim(),
      location: location.trim(),
      totalblockno: Number(totalBlock),
      totallotno: Number(totalLot),
      totalarea: Number(totalArea),
      cashprice: Number(cashPrice),
      projectPhase: productToEdit?.projectPhase ?? 'Open',
      status: productToEdit?.status ?? 'active',
      deleted_at: productToEdit?.deleted_at ?? null
    };

    onSave(payload as Product);
    onClose();
  };

  return <ModalFrame onClose={onClose} title={isEditing?'Edit property':'Add property'}><div className="product-modal-shell form-shell">
    <header className="product-modal-header form-header"><div className="product-modal-icon-wrapper form-heading-icon"><Building2 size={22}/></div><div className="product-modal-title-group"><span className="product-modal-eyebrow form-eyebrow">PROPERTY & SALES</span><h2 className="product-modal-title">{isEditing?'Edit property':'Add property'}</h2><p className="product-modal-subtitle">Register a subdivision and its inventory details.</p></div><button onClick={onClose} aria-label="Close form" className="product-modal-close-btn form-close"><X size={20}/></button></header>
    <form className="product-modal-form" onSubmit={handleSubmit}><div className="product-modal-body form-body"><p className="product-required-note required-note">All fields are required.</p>{errorMsg&&<p role="alert" className="product-error-alert form-error">{errorMsg}</p>}
      <fieldset className="product-fieldset"><legend className="product-legend">Property details</legend><p className="product-fieldset-help fieldset-help">Use a short code to identify this project in contracts and reports.</p><div className="product-form-grid form-grid"><label className="product-form-label">Property code<input className="product-form-input" autoFocus required value={code} onChange={e=>setCode(e.target.value)} placeholder="e.g. JLD-STN"/></label><label className="product-form-label">Location / project name<input className="product-form-input" required value={location} onChange={e=>setLocation(e.target.value)} placeholder="e.g. Sto. Niño, Tupaz Subdivision"/></label></div></fieldset>
      <fieldset className="product-fieldset"><legend className="product-legend">Land inventory</legend><div className="product-form-grid form-grid"><label className="product-form-label">Total blocks<input className="product-form-input" required type="number" min="1" step="1" value={totalBlock} onChange={e=>setTotalBlock(e.target.value===''?'':Number(e.target.value))}/></label><label className="product-form-label">Total lots<input className="product-form-input" required type="number" min="1" step="1" value={totalLot} onChange={e=>setTotalLot(e.target.value===''?'':Number(e.target.value))}/></label><label className="product-form-label">Total land area (m²)<input className="product-form-input" required type="number" min="0.01" step="0.01" value={totalArea} onChange={e=>setTotalArea(e.target.value===''?'':Number(e.target.value))}/></label><label className="product-form-label">Cash price per 100 m² (PHP)<input className="product-form-input" required type="number" min="0" step="0.01" value={cashPrice} onChange={e=>setCashPrice(e.target.value===''?'':Number(e.target.value))}/></label></div></fieldset>
    </div><footer className="product-modal-actions form-actions"><button type="button" className="product-cancel-btn secondary-button" onClick={onClose}>Cancel</button><button type="submit" className="product-submit-btn primary-button">{isEditing?'Save changes':'Save property'}</button></footer></form></div></ModalFrame>;
};
