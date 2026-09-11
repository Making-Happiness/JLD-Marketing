import { ModalFrame } from './ModalFrame';
import React from 'react';
import { PurchaseDetail, PaymentTransaction } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { X, Printer, FileText } from 'lucide-react';

interface SOAModalProps {
  isOpen: boolean;
  onClose: () => void;
  application: PurchaseDetail | null;
  payments: PaymentTransaction[];
}

export const SOAModal: React.FC<SOAModalProps> = ({
  isOpen,
  onClose,
  application,
  payments
}) => {
  if (!isOpen || !application) return null;

  // Filter payments for this specific purchase
  const appPayments = payments.filter(p => 
    p.items.some(item => item.idpurchasedetails === application.id)
  );

  // Flatten matching payment items
  const paymentHistory = appPayments.flatMap(p => 
    p.items
      .filter(item => item.idpurchasedetails === application.id)
      .map(item => ({
        date: p.dateofpayment,
        or: p.orderreceipt,
        method: p.paymenttype,
        ref: p.paymentref,
        for: item.paymentfor,
        amount: item.amount,
        desc: item.description
      }))
  );

  const totalPaid = paymentHistory.reduce((acc, curr) => acc + curr.amount, 0);
  const remainingBalance = Math.max(0, (application.lotprice + (application.otherfees || 0) + (application.penalty || 0)) - totalPaid);
  const progressPercent = Math.min(100, Math.round((totalPaid / (application.lotprice || 1)) * 100));

  return (
    <ModalFrame onClose={onClose} title="Statement of Account">
      <div className="soa-modal-shell form-shell form-shell-wide">
        {/* Header */}
        <header className="soa-modal-header form-header">
          <div className="soa-modal-icon-wrapper form-heading-icon">
            <FileText size={22}/>
          </div>
          <div className="soa-modal-title-group">
            <span className="soa-modal-eyebrow form-eyebrow">CUSTOMER ACCOUNTING</span>
            <h2 className="soa-modal-title">Statement of Account (SOA)</h2>
            <p className="soa-modal-subtitle">
              Official payment ledger and amortization settlement summary.
            </p>
          </div>
          <div className="flex items-center gap-2 ml-auto">
            <button
              onClick={() => window.print()}
              className="secondary-button"
              style={{ minHeight: '34px', padding: '6px 12px', fontSize: '12px' }}
            >
              <Printer size={14}/>
              <span>Print SOA</span>
            </button>
            <button
              onClick={onClose}
              className="soa-modal-close-button form-close"
              aria-label="Close statement"
            >
              <X size={18}/>
            </button>
          </div>
        </header>

        {/* Printable SOA Document Container */}
        <div className="soa-modal-body p-8 space-y-6 text-xs print:p-0">
          {/* Document Title Header */}
          <div className="soa-doc-header flex justify-between items-start border-b border-slate-200 pb-5">
            <div className="soa-company-brand">
              <div className="soa-company-logo flex items-center gap-2">
                <div className="soa-logo-badge w-7 h-7 rounded-lg bg-emerald-900 text-white flex items-center justify-center font-bold text-xs">
                  JLD
                </div>
                <span className="soa-company-name text-sm font-bold text-slate-900 uppercase tracking-tight">
                  JLD Subdivision &amp; Real Property
                </span>
              </div>
              <p className="soa-company-subtext text-[11px] text-slate-500 mt-1">
                South Cotabato Subdivision Operations • Statement of Account
              </p>
            </div>
            <div className="soa-doc-meta text-right">
              <span className="soa-meta-label text-[10px] uppercase font-bold text-slate-400 block tracking-wider">Account ID</span>
              <span className="soa-account-id text-xs font-mono font-bold text-slate-800">SOA-{application.id.toString().padStart(6, '0')}</span>
              <span className="soa-issue-date text-[10px] text-slate-400 block mt-0.5">Issued: {formatDate(new Date().toISOString())}</span>
            </div>
          </div>

          {/* Client & Lot Metadata Grid */}
          <div className="soa-particulars-grid grid grid-cols-2 gap-6 bg-slate-50/70 p-4 rounded-xl border border-slate-100">
            <div className="soa-buyer-card">
              <span className="soa-section-label text-[10px] uppercase font-bold text-slate-400 block tracking-wider mb-1.5">
                Stakeholder / Buyer
              </span>
              <div className="soa-buyer-name font-bold text-slate-900 text-sm">{application.clientName}</div>
              <div className="soa-buyer-role text-slate-600 mt-0.5">{application.clientRole}</div>
              <div className="soa-agent-note text-slate-500 text-[11px] mt-1">Assigned Agent: {application.agentName} ({application.agentpercentage}%)</div>
            </div>

            <div className="soa-property-card">
              <span className="soa-section-label text-[10px] uppercase font-bold text-slate-400 block tracking-wider mb-1.5">
                Property Particulars
              </span>
              <div className="soa-property-location font-bold text-slate-900 text-sm">{application.location}</div>
              <div className="soa-property-blocklot text-slate-700 font-medium mt-0.5">
                Block {application.blockno}, Lot {application.lotno} • {application.area} sq.m
              </div>
              <div className="soa-property-terms text-slate-500 text-[11px] mt-1">
                Terms: {application.terms} Year{application.terms > 1 ? 's' : ''} • Monthly Amortization: {application.amortization == null ? '—' : formatCurrency(application.amortization)}
              </div>
            </div>
          </div>

          {/* Payment Progress Bar */}
          <div className="soa-progress-section">
            <div className="soa-progress-header flex justify-between items-center text-xs mb-1.5">
              <span className="soa-progress-label font-semibold text-slate-700">Payment Amortization Progress</span>
              <span className="soa-progress-percent font-bold text-emerald-800">{progressPercent}% Settled</span>
            </div>
            <div className="soa-progress-bar-track w-full bg-slate-100 h-2.5 rounded-full overflow-hidden">
              <div
                className="soa-progress-bar-fill bg-emerald-700 h-full rounded-full transition-all"
                style={{ width: `${progressPercent}%` }}
              />
            </div>
          </div>

          {/* Payment Ledger Table (frmpaymenthistory.cs) */}
          <div className="soa-ledger-section">
            <div className="soa-ledger-header flex justify-between items-center mb-2">
              <h4 className="soa-ledger-title font-bold text-slate-800 text-xs uppercase tracking-wider">
                Historical Payment Ledger
              </h4>
              <span className="soa-ledger-count text-[11px] text-slate-400">{paymentHistory.length} transaction(s) recorded</span>
            </div>

            <div className="soa-ledger-table-container border border-slate-200 rounded-xl overflow-hidden">
              <table className="soa-ledger-table w-full text-left text-xs">
                <thead className="soa-ledger-thead bg-slate-100/70 text-[11px] font-semibold text-slate-600 border-b border-slate-200">
                  <tr>
                    <th className="soa-ledger-th py-2.5 px-3">Date</th>
                    <th className="soa-ledger-th py-2.5 px-3">Receipt (OR)</th>
                    <th className="soa-ledger-th py-2.5 px-3">Type</th>
                    <th className="soa-ledger-th py-2.5 px-3">Particulars</th>
                    <th className="soa-ledger-th py-2.5 px-3 text-right">Amount Paid</th>
                  </tr>
                </thead>
                <tbody className="soa-ledger-tbody divide-y divide-slate-100">
                  {paymentHistory.length === 0 ? (
                    <tr className="soa-ledger-empty-row">
                      <td colSpan={5} className="soa-ledger-empty-cell py-6 text-center text-slate-400">
                        No payments recorded yet for this purchase.
                      </td>
                    </tr>
                  ) : (
                    paymentHistory.map((item, i) => (
                      <tr key={i} className="soa-ledger-row hover:bg-slate-50/80">
                        <td className="soa-ledger-cell py-2.5 px-3 text-slate-600">{formatDate(item.date)}</td>
                        <td className="soa-ledger-cell py-2.5 px-3 font-mono font-semibold text-slate-900">{item.or}</td>
                        <td className="soa-ledger-cell py-2.5 px-3">
                          <span className="soa-payment-for-badge px-2 py-0.5 rounded-full bg-slate-100 text-slate-700 text-[10px] font-semibold">
                            {item.for}
                          </span>
                        </td>
                        <td className="soa-ledger-cell py-2.5 px-3 text-slate-600 text-[11px]">{item.desc || item.method}</td>
                        <td className="soa-ledger-cell py-2.5 px-3 text-right font-bold text-slate-900">{formatCurrency(item.amount)}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </div>

          {/* Financial Summary Box */}
          <div className="soa-summary-metrics-grid p-4 bg-emerald-50/50 border border-emerald-200/80 rounded-2xl grid grid-cols-2 sm:grid-cols-4 gap-3 text-xs">
            <div className="soa-summary-metric-card">
              <span className="soa-summary-metric-label text-[10px] uppercase font-bold text-emerald-800/70 block">Total Lot Price</span>
              <span className="soa-summary-metric-value text-sm font-bold text-slate-900">{formatCurrency(application.lotprice)}</span>
            </div>
            <div className="soa-summary-metric-card">
              <span className="soa-summary-metric-label text-[10px] uppercase font-bold text-emerald-800/70 block">Total Paid to Date</span>
              <span className="soa-summary-metric-value text-sm font-bold text-emerald-900">{formatCurrency(totalPaid)}</span>
            </div>
            <div className="soa-summary-metric-card">
              <span className="soa-summary-metric-label text-[10px] uppercase font-bold text-rose-800/70 block">Remaining Balance</span>
              <span className="soa-summary-metric-value text-sm font-bold text-rose-900">{formatCurrency(remainingBalance)}</span>
            </div>
            <div className="soa-summary-metric-card">
              <span className="soa-summary-metric-label text-[10px] uppercase font-bold text-emerald-800/70 block">Next Due Date</span>
              <span className="soa-summary-metric-value text-sm font-bold text-slate-900">{formatDate(application.duedate)}</span>
            </div>
          </div>

          {/* Signatures Footer for Print */}
          <div className="soa-signatures-grid pt-6 border-t border-slate-200 grid grid-cols-2 gap-8 text-[11px] text-slate-500">
            <div className="soa-signature-col">
              <div className="soa-signature-line border-b border-slate-300 pb-1 mb-1 font-semibold text-slate-700">
                Prepared by: Ralph Edwards (Cashier / Bookkeeper)
              </div>
              <span className="soa-signature-role">JLD Operations Accounting</span>
            </div>
            <div className="soa-signature-col">
              <div className="soa-signature-line border-b border-slate-300 pb-1 mb-1 font-semibold text-slate-700">
                Confirmed by: {application.clientName}
              </div>
              <span className="soa-signature-role">Buyer / Stakeholder Signature</span>
            </div>
          </div>
        </div>

        {/* Modal Actions */}
        <div className="soa-modal-footer form-actions">
          <button
            type="button"
            onClick={onClose}
            className="soa-close-footer-button primary-button"
          >
            Close Statement
          </button>
        </div>
      </div>
    </ModalFrame>
  );
};



