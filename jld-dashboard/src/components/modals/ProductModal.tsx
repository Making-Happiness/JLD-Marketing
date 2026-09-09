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

  return <ModalFrame onClose={onClose} title={isEditing?'Edit property':'Add property'}><div className="form-shell">
    <header className="form-header"><div className="form-heading-icon"><Building2 size={22}/></div><div><span className="form-eyebrow">PROPERTY & SALES</span><h2>{isEditing?'Edit property':'Add property'}</h2><p>Register a subdivision and its inventory details.</p></div><button onClick={onClose} aria-label="Close form" className="form-close"><X size={20}/></button></header>
    <form onSubmit={handleSubmit}><div className="form-body"><p className="required-note">All fields are required.</p>{errorMsg&&<p role="alert" className="form-error">{errorMsg}</p>}
      <fieldset><legend>Property details</legend><p className="fieldset-help">Use a short code to identify this project in contracts and reports.</p><div className="form-grid"><label>Property code<input autoFocus required value={code} onChange={e=>setCode(e.target.value)} placeholder="e.g. JLD-STN"/></label><label>Location / project name<input required value={location} onChange={e=>setLocation(e.target.value)} placeholder="e.g. Sto. Niño, Tupaz Subdivision"/></label></div></fieldset>
      <fieldset><legend>Land inventory</legend><div className="form-grid"><label>Total blocks<input required type="number" min="1" step="1" value={totalBlock} onChange={e=>setTotalBlock(e.target.value===''?'':Number(e.target.value))}/></label><label>Total lots<input required type="number" min="1" step="1" value={totalLot} onChange={e=>setTotalLot(e.target.value===''?'':Number(e.target.value))}/></label><label>Total land area (m²)<input required type="number" min="0.01" step="0.01" value={totalArea} onChange={e=>setTotalArea(e.target.value===''?'':Number(e.target.value))}/></label><label>Cash price per 100 m² (PHP)<input required type="number" min="0" step="0.01" value={cashPrice} onChange={e=>setCashPrice(e.target.value===''?'':Number(e.target.value))}/></label></div></fieldset>
    </div><footer className="form-actions"><button type="button" className="secondary-button" onClick={onClose}>Cancel</button><button type="submit" className="primary-button">{isEditing?'Save changes':'Save property'}</button></footer></form></div></ModalFrame>;
};
