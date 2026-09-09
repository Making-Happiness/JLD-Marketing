import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { calculateAmortization, formatCurrency } from '../../utils/calculations';
import { X, Calculator, ArrowRight } from 'lucide-react';

interface CalculatorModalProps {
  isOpen: boolean;
  onClose: () => void;
  onApply?: (price: number, downpayment: number, terms: number, monthlyFee: number) => void;
  initialPrice?: number;
  initialDownpayment?: number;
  initialTerms?: number;
}

export const CalculatorModal: React.FC<CalculatorModalProps> = ({
  isOpen,
  onClose,
  onApply,
  initialPrice = 108000,
  initialDownpayment = 20000,
  initialTerms = 2
}) => {
  const [price, setPrice] = useState<number>(initialPrice);
  const [downpayment, setDownpayment] = useState<number>(initialDownpayment);
  const [terms, setTerms] = useState<number>(initialTerms);
  const [monthlyFee, setMonthlyFee] = useState<number>(0);

  useEffect(() => {
    setPrice(initialPrice);
    setDownpayment(initialDownpayment);
    setTerms(initialTerms);
  }, [initialPrice, initialDownpayment, initialTerms, isOpen]);

  useEffect(() => {
    const fee = calculateAmortization(price, downpayment, terms);
    setMonthlyFee(fee);
  }, [price, downpayment, terms]);

  if (!isOpen) return null;

  const principal = Math.max(0, price - downpayment);
  const interestTotal = principal * 0.15 * terms;
  const totalContractPayable = downpayment + principal + interestTotal;

  return (
    <ModalFrame onClose={onClose} title='Calculator'>
      <div className="calculator-modal-card bg-white rounded-2xl max-w-lg w-full border border-slate-200 shadow-xl overflow-hidden animate-in fade-in zoom-in-95 duration-150">
        {/* Header */}
        <div className="calculator-modal-header px-6 py-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="calculator-modal-header-brand flex items-center gap-2.5">
            <div className="calculator-modal-icon-badge w-8 h-8 rounded-xl bg-emerald-700 text-white flex items-center justify-center shadow-xs">
              <Calculator className="w-4 h-4" />
            </div>
            <div className="calculator-modal-title-group">
              <h2 className="calculator-modal-title text-base font-bold text-slate-900">Amortization Calculator</h2>
              <p className="calculator-modal-subtitle text-[11px] text-slate-500">
                Linked workspace records
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="calculator-modal-close-button p-1.5 rounded-lg text-slate-400 hover:text-slate-600 hover:bg-slate-100 transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Calculator Body */}
        <div className="calculator-modal-body p-6 space-y-5 text-xs">
          {/* Inputs */}
          <div className="calculator-inputs-stack space-y-3.5">
            <div className="calculator-field-group">
              <div className="calculator-field-header flex justify-between items-center mb-1">
                <label className="calculator-field-label font-semibold text-slate-700">Total Lot Price (₱)</label>
                <span className="calculator-field-display text-emerald-700 font-bold">{formatCurrency(price)}</span>
              </div>
              <input
                type="number"
                step="5000"
                value={price}
                onChange={(e) => setPrice(Number(e.target.value))}
                className="calculator-number-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs font-semibold text-slate-900 focus:border-emerald-600 outline-none"
              />
              <input
                type="range"
                min="50000"
                max="2000000"
                step="10000"
                value={price}
                onChange={(e) => setPrice(Number(e.target.value))}
                className="calculator-range-input w-full mt-2 accent-emerald-700 cursor-pointer"
              />
            </div>

            <div className="calculator-field-group">
              <div className="calculator-field-header flex justify-between items-center mb-1">
                <label className="calculator-field-label font-semibold text-slate-700">Estimated Downpayment (₱)</label>
                <span className="calculator-field-display text-emerald-700 font-bold">{formatCurrency(downpayment)}</span>
              </div>
              <input
                type="number"
                step="2500"
                value={downpayment}
                onChange={(e) => setDownpayment(Number(e.target.value))}
                className="calculator-number-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs font-semibold text-slate-900 focus:border-emerald-600 outline-none"
              />
              <input
                type="range"
                min="0"
                max={price}
                step="5000"
                value={downpayment}
                onChange={(e) => setDownpayment(Number(e.target.value))}
                className="calculator-range-input w-full mt-2 accent-emerald-700 cursor-pointer"
              />
            </div>

            <div className="calculator-field-group">
              <div className="calculator-field-header flex justify-between items-center mb-1">
                <label className="calculator-field-label font-semibold text-slate-700">Payment Term (Years)</label>
                <span className="calculator-field-display text-slate-900 font-bold">{terms} Year{terms > 1 ? 's' : ''} ({terms * 12} Mos)</span>
              </div>
              <div className="calculator-term-buttons-grid grid grid-cols-5 gap-2">
                {[1, 2, 3, 5, 10].map((t) => (
                  <button
                    key={t}
                    type="button"
                    onClick={() => setTerms(t)}
                    className={`calculator-term-select-button py-2 text-xs font-bold rounded-xl border transition-all cursor-pointer ${
                      terms === t
                        ? 'bg-emerald-800 text-white border-emerald-800 shadow-xs'
                        : 'bg-white text-slate-700 border-slate-200 hover:bg-slate-50'
                    }`}
                  >
                    {t} {t === 1 ? 'Yr' : 'Yrs'}
                  </button>
                ))}
              </div>
            </div>
          </div>

          {/* Result Card */}
          <div className="calculator-result-card bg-gradient-to-br from-emerald-900 to-emerald-950 text-white p-5 rounded-2xl shadow-sm">
            <span className="calculator-result-label text-[11px] uppercase tracking-wider text-emerald-300 font-medium block">
              Estimated Monthly Fee
            </span>
            <div className="calculator-result-value text-3xl font-extrabold text-white mt-1 tracking-tight">
              {formatCurrency(monthlyFee)}
              <span className="calculator-result-period text-xs font-normal text-emerald-300 ml-1.5">/ month</span>
            </div>

            {/* Financial breakdown */}
            <div className="calculator-breakdown-grid mt-4 pt-3 border-t border-emerald-800/80 grid grid-cols-3 gap-2 text-[11px] text-emerald-200">
              <div className="calculator-breakdown-col">
                <span className="calculator-breakdown-label text-emerald-400 block text-[10px]">Net Principal:</span>
                <span className="calculator-breakdown-value font-semibold text-white">{formatCurrency(principal)}</span>
              </div>
              <div className="calculator-breakdown-col">
                <span className="calculator-breakdown-label text-emerald-400 block text-[10px]">Total 15% Interest:</span>
                <span className="calculator-breakdown-value font-semibold text-white">{formatCurrency(interestTotal)}</span>
              </div>
              <div className="calculator-breakdown-col">
                <span className="calculator-breakdown-label text-emerald-400 block text-[10px]">Total Contract:</span>
                <span className="calculator-breakdown-value font-semibold text-white">{formatCurrency(totalContractPayable)}</span>
              </div>
            </div>
          </div>

          {/* Formula Note */}
          <div className="calculator-formula-note p-3 bg-slate-50 border border-slate-200 rounded-xl text-[11px] text-slate-500 leading-relaxed">
            <span className="font-semibold text-slate-700">Formula standard: </span>
            (Principal × 15% × Terms + Principal) ÷ (12 × Terms). All calculations rounded to nearest whole peso.
          </div>

          {/* Actions */}
          <div className="calculator-actions-bar flex items-center justify-end gap-2.5 pt-2">
            <button
              onClick={onClose}
              className="calculator-cancel-button px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl transition-colors cursor-pointer"
            >
              Close
            </button>
            {onApply && (
              <button
                onClick={() => {
                  onApply(price, downpayment, terms, monthlyFee);
                  onClose();
                }}
                className="calculator-apply-button px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl shadow-sm transition-all cursor-pointer flex items-center gap-1.5"
              >
                <span>Use These Numbers</span>
                <ArrowRight className="w-3.5 h-3.5" />
              </button>
            )}
          </div>
        </div>
      </div>
    </ModalFrame>
  );
};



