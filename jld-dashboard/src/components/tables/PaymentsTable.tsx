import React from 'react';
import { PaymentTransaction } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { Receipt, CreditCard, CheckCircle2, Search } from 'lucide-react';

interface PaymentsTableProps {
  payments: PaymentTransaction[];
  onNewPayment: () => void;
}

export const PaymentsTable: React.FC<PaymentsTableProps> = ({
  payments,
  onNewPayment
}) => {
  const totalCollections = payments.reduce((acc, curr) => acc + curr.totalamount, 0);

  return (
    <div className="px-8 py-4">
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        {/* Header Strip */}
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Payment Collection Ledger</h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmpayments.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">payments</code> database
            </p>
          </div>
          <div className="flex items-center gap-3">
            <div className="bg-emerald-50 border border-emerald-200 px-3 py-1.5 rounded-xl text-right">
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">Total Ledger</span>
              <span className="text-sm font-bold text-emerald-950">{formatCurrency(totalCollections)}</span>
            </div>
            <button
              onClick={onNewPayment}
              className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer"
            >
              + Record Payment
            </button>
          </div>
        </div>

        {/* Ledger Table */}
        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">Payment Ref</th>
                <th className="py-3 px-4">Receipt (OR #)</th>
                <th className="py-3 px-4">Date</th>
                <th className="py-3 px-4">Paid By Client</th>
                <th className="py-3 px-4">Payment Method</th>
                <th className="py-3 px-4">Transaction Items</th>
                <th className="py-3 px-4">Cashier / In-Charge</th>
                <th className="py-3 px-6 text-right">Total Amount</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {payments.map((p) => (
                <tr key={p.id} className="hover:bg-slate-50/60 transition-colors">
                  <td className="py-3.5 px-6 font-mono font-bold text-emerald-900">
                    <span className="bg-emerald-50 border border-emerald-200 px-2 py-0.5 rounded-md">
                      {p.paymentref}
                    </span>
                  </td>
                  <td className="py-3.5 px-4 font-mono font-semibold text-slate-900">
                    {p.orderreceipt}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {formatDate(p.dateofpayment)}
                  </td>
                  <td className="py-3.5 px-4 font-semibold text-slate-900">
                    {p.paidbyName}
                  </td>
                  <td className="py-3.5 px-4">
                    <span className="px-2 py-0.5 rounded-full bg-slate-100 text-slate-700 text-[10px] font-semibold">
                      {p.paymenttype} {p.referenceno ? `(${p.referenceno})` : ''}
                    </span>
                  </td>
                  <td className="py-3.5 px-4 text-slate-600 text-[11px]">
                    {p.items.map((it, idx) => (
                      <span key={idx} className="inline-block mr-1">
                        • {it.paymentfor}: {formatCurrency(it.amount)}
                      </span>
                    ))}
                  </td>
                  <td className="py-3.5 px-4 text-slate-500">
                    {p.inchargebyName}
                  </td>
                  <td className="py-3.5 px-6 text-right font-bold text-slate-900 text-sm">
                    {formatCurrency(p.totalamount)}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

