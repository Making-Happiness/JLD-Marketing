import React, { useState, useMemo } from 'react';
import { LoanRecord } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { HandCoins, Gift, Plus, Search } from 'lucide-react';

interface LoansTableProps {
  loans: LoanRecord[];
  benefits: LoanRecord[];
  onAddLoan?: () => void;
  onAddBenefit?: () => void;
}

export const LoansTable: React.FC<LoansTableProps> = ({
  loans,
  benefits,
  onAddLoan,
  onAddBenefit
}) => {
  // In-page toggle selector between Deductions and Earnings matching frmLoan.cs
  const [activeMode, setActiveMode] = useState<'DEDUCTIONS' | 'EARNINGS'>('DEDUCTIONS');
  const [searchTerm, setSearchTerm] = useState('');

  const isEarnings = activeMode === 'EARNINGS';
  const rawRecords = isEarnings ? benefits : loans;

  const filteredRecords = useMemo(() => {
    if (!searchTerm.trim()) return rawRecords;
    const q = searchTerm.toLowerCase();
    return rawRecords.filter(r => 
      r.employeeName.toLowerCase().includes(q) ||
      r.description.toLowerCase().includes(q) ||
      r.category.toLowerCase().includes(q) ||
      (r.remarks && r.remarks.toLowerCase().includes(q))
    );
  }, [rawRecords, searchTerm]);

  const totalAmount = rawRecords.reduce((acc, curr) => acc + curr.amount, 0);
  const totalAmort = rawRecords.reduce((acc, curr) => acc + curr.amortization, 0);

  return (
    <div className="px-8 py-5 space-y-4">
      {/* Top Segmented Toggle Switch */}
      <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs flex flex-col md:flex-row items-stretch md:items-center justify-between gap-4">
        {/* Toggle Pills */}
        <div className="inline-flex p-1 bg-slate-100/80 rounded-xl border border-slate-200/60 self-start md:self-auto">
          <button
            type="button"
            onClick={() => {
              setActiveMode('DEDUCTIONS');
              setSearchTerm('');
            }}
            className={`flex items-center gap-2 px-4 py-2 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              !isEarnings
                ? 'bg-[#00593B] text-white shadow-xs'
                : 'text-slate-600 hover:text-slate-900 hover:bg-slate-200/60'
            }`}
          >
            <HandCoins className="w-3.5 h-3.5" />
            <span>Loans &amp; Cash Advances (Deductions)</span>
            <span className={`text-[10px] px-1.5 py-0.2 rounded-full font-bold ml-1 ${
              !isEarnings ? 'bg-emerald-900/60 text-emerald-200' : 'bg-slate-200 text-slate-700'
            }`}>
              {loans.length}
            </span>
          </button>

          <button
            type="button"
            onClick={() => {
              setActiveMode('EARNINGS');
              setSearchTerm('');
            }}
            className={`flex items-center gap-2 px-4 py-2 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              isEarnings
                ? 'bg-[#00593B] text-white shadow-xs'
                : 'text-slate-600 hover:text-slate-900 hover:bg-slate-200/60'
            }`}
          >
            <Gift className="w-3.5 h-3.5" />
            <span>Other Earnings &amp; Benefits (Earnings)</span>
            <span className={`text-[10px] px-1.5 py-0.2 rounded-full font-bold ml-1 ${
              isEarnings ? 'bg-emerald-900/60 text-emerald-200' : 'bg-slate-200 text-slate-700'
            }`}>
              {benefits.length}
            </span>
          </button>
        </div>

        {/* Action Button */}
        <button
          type="button"
          onClick={isEarnings ? onAddBenefit : onAddLoan}
          className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5 self-end md:self-auto"
        >
          <Plus className="w-3.5 h-3.5" />
          <span>{isEarnings ? '+ Add Benefit / Incentive' : '+ New Cash Advance'}</span>
        </button>
      </div>

      {/* Main Table Card */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <div className="flex items-center gap-2">
              <h3 className="text-sm font-bold text-slate-900">
                {isEarnings ? 'Other Earnings & Employee Benefits' : 'Employee Loans & Cash Advances'}
              </h3>
              <span className={`px-2 py-0.5 rounded-full text-[10px] font-bold ${
                isEarnings ? 'bg-purple-50 text-purple-700 border border-purple-200' : 'bg-amber-50 text-amber-700 border border-amber-200'
              }`}>
                {isEarnings ? 'EARNINGS MODE' : 'DEDUCTIONS MODE'}
              </span>
            </div>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmLoan.cs</code> (Transaction = "{activeMode}")
            </p>
          </div>

          <div className="flex items-center gap-4">
            {/* Search Input */}
            <div className="relative">
              <Search className="w-3.5 h-3.5 text-slate-400 absolute left-3 top-1/2 -translate-y-1/2" />
              <input
                type="text"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search employee or description..."
                className="pl-8 pr-3 py-1.5 rounded-xl border border-slate-200 text-xs focus:outline-hidden focus:ring-2 focus:ring-emerald-500 w-56"
              />
            </div>

            {/* Total Metric Card */}
            <div className="bg-emerald-50/80 border border-emerald-200/80 px-3.5 py-1.5 rounded-xl text-right">
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">
                Total {isEarnings ? 'Benefits Disbursed' : 'Advances Issued'}
              </span>
              <span className="text-sm font-bold text-emerald-950">{formatCurrency(totalAmount)}</span>
            </div>
          </div>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">Record ID</th>
                <th className="py-3 px-4">Employee</th>
                <th className="py-3 px-4">Date Applied</th>
                <th className="py-3 px-4">Description</th>
                <th className="py-3 px-4">Category</th>
                <th className="py-3 px-4 text-right">Total Amount</th>
                <th className="py-3 px-4 text-right">Amortization / Mo</th>
                <th className="py-3 px-4">Remarks</th>
                <th className="py-3 px-6 text-right">Status</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {filteredRecords.length === 0 ? (
                <tr>
                  <td colSpan={9} className="py-8 text-center text-slate-400 text-xs">
                    No {isEarnings ? 'benefit' : 'loan/cash advance'} records found matching your filter.
                  </td>
                </tr>
              ) : (
                filteredRecords.map((rec) => (
                  <tr key={rec.id} className="hover:bg-slate-50/60 transition-colors">
                    <td className="py-3.5 px-6 font-mono text-slate-400 font-semibold">
                      {isEarnings ? 'BEN' : 'LN'}-{rec.id.toString().padStart(4, '0')}
                    </td>
                    <td className="py-3.5 px-4 font-bold text-slate-900">
                      {rec.employeeName}
                    </td>
                    <td className="py-3.5 px-4 text-slate-600">
                      {formatDate(rec.dateapplied)}
                    </td>
                    <td className="py-3.5 px-4 text-slate-700 font-medium">
                      {rec.description}
                    </td>
                    <td className="py-3.5 px-4">
                      <span className={`px-2 py-0.5 rounded-full text-[10px] font-semibold ${
                        isEarnings ? 'bg-purple-50 text-purple-700 border border-purple-200' : 'bg-amber-50 text-amber-700 border border-amber-200'
                      }`}>
                        {rec.category}
                      </span>
                    </td>
                    <td className="py-3.5 px-4 text-right font-bold text-slate-900">
                      {formatCurrency(rec.amount)}
                    </td>
                    <td className="py-3.5 px-4 text-right font-semibold text-slate-600">
                      {formatCurrency(rec.amortization)}
                    </td>
                    <td className="py-3.5 px-4 text-slate-500 text-[11px]">
                      {rec.remarks}
                    </td>
                    <td className="py-3.5 px-6 text-right">
                      <span className="px-2.5 py-0.5 rounded-full text-[10px] font-semibold bg-emerald-50 text-emerald-700 border border-emerald-200">
                        {rec.recordstatus}
                      </span>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default LoansTable;
