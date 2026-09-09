import { ModalFrame } from './ModalFrame';
import React from 'react';
import { PurchaseDetail, PaymentTransaction } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { X, Printer, FileText, CheckCircle, AlertTriangle } from 'lucide-react';

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
    <ModalFrame onClose={onClose} title='SOA'>
      <div className="bg-white rounded-2xl max-w-3xl w-full border border-slate-200 shadow-2xl overflow-hidden my-6 animate-in fade-in zoom-in-95 duration-150">
        {/* Header */}
        <div className="px-6 py-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/80">
          <div className="flex items-center gap-2.5">
            <div className="w-8 h-8 rounded-xl bg-emerald-100 text-emerald-800 flex items-center justify-center">
              <FileText className="w-4 h-4" />
            </div>
            <div>
              <h2 className="text-base font-bold text-slate-900">Statement of Account (SOA)</h2>
              <p className="text-[11px] text-slate-500">
                Linked workspace records
              </p>
            </div>
          </div>
          <div className="flex items-center gap-2">
            <button
              onClick={() => window.print()}
              className="px-3 py-1.5 bg-white hover:bg-slate-100 border border-slate-200 text-slate-700 font-semibold rounded-lg flex items-center gap-1.5 text-xs shadow-2xs cursor-pointer transition-colors"
            >
              <Printer className="w-3.5 h-3.5" />
              <span>Print SOA</span>
            </button>
            <button
              onClick={onClose}
              className="p-1.5 rounded-lg text-slate-400 hover:text-slate-600 hover:bg-slate-100 transition-colors"
            >
              <X className="w-5 h-5" />
            </button>
          </div>
        </div>

        {/* Printable SOA Document Container */}
        <div className="p-8 space-y-6 text-xs print:p-0">
          {/* Document Title Header */}
          <div className="flex justify-between items-start border-b border-slate-200 pb-5">
            <div>
              <div className="flex items-center gap-2">
                <div className="w-7 h-7 rounded-lg bg-emerald-900 text-white flex items-center justify-center font-bold text-xs">
                  JLD
                </div>
                <span className="text-sm font-bold text-slate-900 uppercase tracking-tight">
                  JLD Subdivision &amp; Real Property
                </span>
              </div>
              <p className="text-[11px] text-slate-500 mt-1">
                South Cotabato Subdivision Operations • Statement of Account
              </p>
            </div>
            <div className="text-right">
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider">Account ID</span>
              <span className="text-xs font-mono font-bold text-slate-800">SOA-{application.id.toString().padStart(6, '0')}</span>
              <span className="text-[10px] text-slate-400 block mt-0.5">Issued: {formatDate(new Date().toISOString())}</span>
            </div>
          </div>

          {/* Client & Lot Metadata Grid */}
          <div className="grid grid-cols-2 gap-6 bg-slate-50/70 p-4 rounded-xl border border-slate-100">
            <div>
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider mb-1.5">
                Stakeholder / Buyer
              </span>
              <div className="font-bold text-slate-900 text-sm">{application.clientName}</div>
              <div className="text-slate-600 mt-0.5">{application.clientRole}</div>
              <div className="text-slate-500 text-[11px] mt-1">Assigned Agent: {application.agentName} ({application.agentpercentage}%)</div>
            </div>

            <div>
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider mb-1.5">
                Property Particulars
              </span>
              <div className="font-bold text-slate-900 text-sm">{application.location}</div>
              <div className="text-slate-700 font-medium mt-0.5">
                Block {application.blockno}, Lot {application.lotno} • {application.area} sq.m
              </div>
              <div className="text-slate-500 text-[11px] mt-1">
                Terms: {application.terms} Year{application.terms > 1 ? 's' : ''} • Monthly Amortization: {formatCurrency(application.amortization)}
              </div>
            </div>
          </div>

          {/* Payment Progress Bar */}
          <div>
            <div className="flex justify-between items-center text-xs mb-1.5">
              <span className="font-semibold text-slate-700">Payment Amortization Progress</span>
              <span className="font-bold text-emerald-800">{progressPercent}% Settled</span>
            </div>
            <div className="w-full bg-slate-100 h-2.5 rounded-full overflow-hidden">
              <div
                className="bg-emerald-700 h-full rounded-full transition-all"
                style={{ width: `${progressPercent}%` }}
              />
            </div>
          </div>

          {/* Payment Ledger Table (frmpaymenthistory.cs) */}
          <div>
            <div className="flex justify-between items-center mb-2">
              <h4 className="font-bold text-slate-800 text-xs uppercase tracking-wider">
                Historical Payment Ledger
              </h4>
              <span className="text-[11px] text-slate-400">{paymentHistory.length} transaction(s) recorded</span>
            </div>

            <div className="border border-slate-200 rounded-xl overflow-hidden">
              <table className="w-full text-left text-xs">
                <thead className="bg-slate-100/70 text-[11px] font-semibold text-slate-600 border-b border-slate-200">
                  <tr>
                    <th className="py-2.5 px-3">Date</th>
                    <th className="py-2.5 px-3">Receipt (OR)</th>
                    <th className="py-2.5 px-3">Type</th>
                    <th className="py-2.5 px-3">Particulars</th>
                    <th className="py-2.5 px-3 text-right">Amount Paid</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                  {paymentHistory.length === 0 ? (
                    <tr>
                      <td colSpan={5} className="py-6 text-center text-slate-400">
                        No payments recorded yet for this purchase.
                      </td>
                    </tr>
                  ) : (
                    paymentHistory.map((item, i) => (
                      <tr key={i} className="hover:bg-slate-50/80">
                        <td className="py-2.5 px-3 text-slate-600">{formatDate(item.date)}</td>
                        <td className="py-2.5 px-3 font-mono font-semibold text-slate-900">{item.or}</td>
                        <td className="py-2.5 px-3">
                          <span className="px-2 py-0.5 rounded-full bg-slate-100 text-slate-700 text-[10px] font-semibold">
                            {item.for}
                          </span>
                        </td>
                        <td className="py-2.5 px-3 text-slate-600 text-[11px]">{item.desc || item.method}</td>
                        <td className="py-2.5 px-3 text-right font-bold text-slate-900">{formatCurrency(item.amount)}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </div>

          {/* Financial Summary Box */}
          <div className="p-4 bg-emerald-50/50 border border-emerald-200/80 rounded-2xl grid grid-cols-2 sm:grid-cols-4 gap-3 text-xs">
            <div>
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">Total Lot Price</span>
              <span className="text-sm font-bold text-slate-900">{formatCurrency(application.lotprice)}</span>
            </div>
            <div>
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">Total Paid to Date</span>
              <span className="text-sm font-bold text-emerald-900">{formatCurrency(totalPaid)}</span>
            </div>
            <div>
              <span className="text-[10px] uppercase font-bold text-rose-800/70 block">Remaining Balance</span>
              <span className="text-sm font-bold text-rose-900">{formatCurrency(remainingBalance)}</span>
            </div>
            <div>
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">Next Due Date</span>
              <span className="text-sm font-bold text-slate-900">{formatDate(application.duedate)}</span>
            </div>
          </div>

          {/* Signatures Footer for Print */}
          <div className="pt-6 border-t border-slate-200 grid grid-cols-2 gap-8 text-[11px] text-slate-500">
            <div>
              <div className="border-b border-slate-300 pb-1 mb-1 font-semibold text-slate-700">
                Prepared by: Ralph Edwards (Cashier / Bookkeeper)
              </div>
              <span>JLD Operations Accounting</span>
            </div>
            <div>
              <div className="border-b border-slate-300 pb-1 mb-1 font-semibold text-slate-700">
                Confirmed by: {application.clientName}
              </div>
              <span>Buyer / Stakeholder Signature</span>
            </div>
          </div>
        </div>

        {/* Modal Actions */}
        <div className="px-6 py-3.5 bg-slate-50 border-t border-slate-100 flex items-center justify-end">
          <button
            onClick={onClose}
            className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl transition-all cursor-pointer"
          >
            Close Statement
          </button>
        </div>
      </div>
    </ModalFrame>
  );
};



