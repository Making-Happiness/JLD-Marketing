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
      availableLots: productToEdit?.availableLots ?? Number(totalLot),
      status: productToEdit?.status ?? 'Open'
    };

    setSuccessMsg(isEditing ? 'Product successfully updated!' : 'Product successfully added!');
    setTimeout(() => {
      onSave(payload as Product);
      setSuccessMsg('');
      onClose();
    }, 350);
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/40 backdrop-blur-xs p-4 animate-in fade-in duration-200">
      <div className="bg-white rounded-2xl shadow-2xl border border-slate-100 w-full max-w-xl overflow-hidden flex flex-col max-h-[90vh]">
        {/* Header */}
        <div className="px-6 py-5 border-b border-slate-100 flex items-center justify-between bg-slate-50/50">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-xl bg-emerald-50 border border-emerald-200/80 flex items-center justify-center text-emerald-800 shadow-2xs">
              <Building2 className="w-5 h-5" />
            </div>
            <div>
              <h2 className="text-base font-bold text-slate-900">
                {isEditing ? `Edit Product (ID: #${productToEdit?.idproduct})` : 'Add New Product'}
              </h2>
              <p className="text-xs text-slate-500">
                {isEditing 
                  ? 'Update subdivision project details and land specifications.' 
                  : 'Enter the details to register a new subdivision project.'}
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-1.5 text-slate-400 hover:text-slate-600 rounded-lg hover:bg-white transition-colors cursor-pointer"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="p-6 overflow-y-auto space-y-4">
          {errorMsg && (
            <div className="p-3 bg-rose-50 border border-rose-200 rounded-xl text-xs text-rose-700 font-medium">
              {errorMsg}
            </div>
          )}

          {successMsg && (
            <div className="p-3 bg-emerald-50 border border-emerald-200 rounded-xl text-xs text-emerald-800 font-semibold flex items-center gap-2">
              <CheckCircle2 className="w-4 h-4 text-emerald-600" />
              <span>{successMsg}</span>
            </div>
          )}

          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {/* Code */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Tag className="w-3.5 h-3.5 text-slate-400" />
                <span>Code</span>
              </label>
              <input
                type="text"
                value={code}
                onChange={(e) => setCode(e.target.value)}
                placeholder="e.g. JLD-STN"
                className="w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all font-mono"
              />
            </div>

            {/* Location / Address */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <MapPin className="w-3.5 h-3.5 text-slate-400" />
                <span>Location / Address</span>
              </label>
              <input
                type="text"
                value={location}
                onChange={(e) => setLocation(e.target.value)}
                placeholder="e.g. Sto. Niño (Tupaz)"
                className="w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Total Block */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Layers className="w-3.5 h-3.5 text-slate-400" />
                <span>Total Block</span>
              </label>
              <input
                type="number"
                min="1"
                value={totalBlock}
                onChange={(e) => setTotalBlock(e.target.value === '' ? '' : Number(e.target.value))}
                placeholder="e.g. 4"
                className="w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Total Lot */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Hash className="w-3.5 h-3.5 text-slate-400" />
                <span>Total Lot</span>
              </label>
              <input
                type="number"
                min="1"
                value={totalLot}
                onChange={(e) => setTotalLot(e.target.value === '' ? '' : Number(e.target.value))}
                placeholder="e.g. 120"
                className="w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Total Area (sq.m.) */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Maximize2 className="w-3.5 h-3.5 text-slate-400" />
                <span>Total Area (sq.m.)</span>
              </label>
              <input
                type="number"
                min="1"
                step="any"
                value={totalArea}
                onChange={(e) => setTotalArea(e.target.value === '' ? '' : Number(e.target.value))}
                placeholder="e.g. 15000"
                className="w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Spot/Cash Price Per 100sqm */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Tag className="w-3.5 h-3.5 text-emerald-600" />
                <span>Spot/Cash Price Per 100sqm</span>
              </label>
              <div className="relative">
                <span className="absolute left-3.5 top-1/2 -translate-y-1/2 text-xs font-bold text-slate-400">₱</span>
                <input
                  type="number"
                  min="0"
                  step="any"
                  value={cashPrice}
                  onChange={(e) => setCashPrice(e.target.value === '' ? '' : Number(e.target.value))}
                  placeholder="e.g. 108000"
                  className="w-full pl-8 pr-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-bold text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
                />
              </div>
            </div>
          </div>

          {/* Action Buttons */}
          <div className="pt-4 flex items-center justify-end gap-2.5 border-t border-slate-100">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2.5 text-xs font-semibold text-slate-600 hover:text-slate-800 hover:bg-slate-100 rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-5 py-2.5 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-sm shadow-emerald-950/20 transition-all cursor-pointer"
            >
              {isEditing ? 'Update Product' : 'Save Product'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

