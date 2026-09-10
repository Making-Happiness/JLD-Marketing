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
    <ModalFrame onClose={onClose} title="Amortization Calculator">
      <div className="calculator-modal-shell form-shell">
        {/* Header */}
        <header className="calculator-modal-header form-header">
          <div className="calculator-modal-icon-wrapper form-heading-icon">
            <Calculator size={22}/>
          </div>
          <div className="calculator-modal-title-group">
            <span className="calculator-modal-eyebrow form-eyebrow">FINANCIAL TOOLS</span>
            <h2 className="calculator-modal-title">Amortization Calculator</h2>
            <p className="calculator-modal-subtitle">
              Simulate lot financing terms, downpayments, and monthly cash flow.
            </p>
          </div>
          <button
            onClick={onClose}
            aria-label="Close calculator"
            className="calculator-modal-close-button form-close"
          >
            <X size={18}/>
          </button>
        </header>

        {/* Calculator Body */}
        <div className="calculator-modal-body form-body">
          {/* Inputs */}
          <fieldset className="calculator-fieldset">
            <legend className="calculator-legend">Financial Parameters</legend>
            <div className="space-y-4">
              <div className="calculator-field-group">
                <div className="flex justify-between items-center mb-1">
                  <label className="form-field-label">Total Lot Price (₱)</label>
                  <span className="text-[#145f49] font-bold text-sm">{formatCurrency(price)}</span>
                </div>
                <input
                  type="number"
                  step="5000"
                  value={price}
                  onChange={(e) => setPrice(Number(e.target.value))}
                  className="calculator-number-input"
                  style={{ fontWeight: 600 }}
                />
                <input
                  type="range"
                  min="50000"
                  max="2000000"
                  step="10000"
                  value={price}
                  onChange={(e) => setPrice(Number(e.target.value))}
                  className="w-full mt-2 accent-[#145f49] cursor-pointer"
                />
              </div>

              <div className="calculator-field-group">
                <div className="flex justify-between items-center mb-1">
                  <label className="form-field-label">Estimated Downpayment (₱)</label>
                  <span className="text-[#145f49] font-bold text-sm">{formatCurrency(downpayment)}</span>
                </div>
                <input
                  type="number"
                  step="2500"
                  value={downpayment}
                  onChange={(e) => setDownpayment(Number(e.target.value))}
                  className="calculator-number-input"
                  style={{ fontWeight: 600 }}
                />
                <input
                  type="range"
                  min="0"
                  max={price}
                  step="5000"
                  value={downpayment}
                  onChange={(e) => setDownpayment(Number(e.target.value))}
                  className="w-full mt-2 accent-[#145f49] cursor-pointer"
                />
              </div>

              <div className="calculator-field-group">
                <div className="flex justify-between items-center mb-1.5">
                  <label className="form-field-label">Payment Term</label>
                  <span className="text-[#1e3b2b] font-bold text-xs">{terms} Year{terms > 1 ? 's' : ''} ({terms * 12} Mos)</span>
                </div>
                <div className="grid grid-cols-5 gap-2">
                  {[1, 2, 3, 5, 10].map((t) => (
                    <button
                      key={t}
                      type="button"
                      onClick={() => setTerms(t)}
                      className={`py-2 text-xs font-bold rounded-lg border transition-all cursor-pointer ${
                        terms === t
                          ? 'bg-[#145f49] text-white border-[#145f49] shadow-xs'
                          : 'bg-white text-[#4a6355] border-[#d5e0d8] hover:bg-[#f8faf9]'
                      }`}
                    >
                      {t} {t === 1 ? 'Yr' : 'Yrs'}
                    </button>
                  ))}
                </div>
              </div>
            </div>
          </fieldset>

          {/* Result Card */}
          <div className="calculator-result-card bg-gradient-to-br from-[#0c382b] to-[#06241b] text-white p-5 rounded-xl shadow-xs mt-4">
            <span className="text-[11px] uppercase tracking-wider text-emerald-300 font-semibold block">
              Estimated Monthly Fee
            </span>
            <div className="text-3xl font-extrabold text-white mt-1 tracking-tight">
              {formatCurrency(monthlyFee)}
              <span className="text-xs font-normal text-emerald-300 ml-1.5">/ month</span>
            </div>

            {/* Financial breakdown */}
            <div className="mt-4 pt-3 border-t border-emerald-800/80 grid grid-cols-3 gap-2 text-[11px] text-emerald-200">
              <div>
                <span className="text-emerald-400 block text-[10px]">Net Principal:</span>
                <span className="font-semibold text-white">{formatCurrency(principal)}</span>
              </div>
              <div>
                <span className="text-emerald-400 block text-[10px]">Total 15% Interest:</span>
                <span className="font-semibold text-white">{formatCurrency(interestTotal)}</span>
              </div>
              <div>
                <span className="text-emerald-400 block text-[10px]">Total Contract:</span>
                <span className="font-semibold text-white">{formatCurrency(totalContractPayable)}</span>
              </div>
            </div>
          </div>

          {/* Formula Note */}
          <div className="form-note" style={{ marginTop: '16px', marginBottom: '0' }}>
            <strong>Standard Formula: </strong>
            (Principal × 15% × Terms + Principal) ÷ (12 × Terms). All calculations are rounded to the nearest peso.
          </div>
        </div>

        {/* Actions */}
        <div className="calculator-actions-bar form-actions">
          <button
            type="button"
            onClick={onClose}
            className="calculator-cancel-button secondary-button"
          >
            Close
          </button>
          {onApply && (
            <button
              type="button"
              onClick={() => {
                onApply(price, downpayment, terms, monthlyFee);
                onClose();
              }}
              className="calculator-apply-button primary-button"
            >
              <span>Apply to Contract</span>
              <ArrowRight size={14}/>
            </button>
          )}
        </div>
      </div>
    </ModalFrame>
  );
};



