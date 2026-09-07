import React, { useState } from 'react';
import { Expense } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { Wallet, Plus, Receipt } from 'lucide-react';

interface ExpensesTableProps {
  expenses: Expense[];
  onAddExpense?: () => void;
}

export const ExpensesTable: React.FC<ExpensesTableProps> = ({
  expenses,
  onAddExpense
}) => {
  const totalExpenses = expenses.reduce((acc, curr) => acc + curr.amount, 0);

  return (
    <div className="px-8 py-4 space-y-4">
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Operating Expenses &amp; Disbursements</h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmExpenses.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">expenses</code> database
            </p>
          </div>
          <div className="flex items-center gap-3">
            <div className="bg-emerald-50 border border-emerald-200 px-3 py-1.5 rounded-xl text-right">
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">Total Disbursed</span>
              <span className="text-sm font-bold text-emerald-950">{formatCurrency(totalExpenses)}</span>
            </div>
            <button
              onClick={onAddExpense}
              className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
            >
              <Plus className="w-3.5 h-3.5" />
              <span>Record Expense</span>
            </button>
          </div>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">ID #</th>
                <th className="py-3 px-4">Date Released</th>
                <th className="py-3 px-4">Description</th>
                <th className="py-3 px-4">Purpose / Category</th>
                <th className="py-3 px-4">Received By</th>
                <th className="py-3 px-4">Released By</th>
                <th className="py-3 px-4">Remarks</th>
                <th className="py-3 px-6 text-right">Amount</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {expenses.map((exp) => (
                <tr key={exp.id} className="hover:bg-slate-50/60 transition-colors">
                  <td className="py-3.5 px-6 font-mono text-slate-400">
                    EXP-{exp.id.toString().padStart(4, '0')}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {formatDate(exp.daterelease)}
                  </td>
                  <td className="py-3.5 px-4 font-semibold text-slate-900">
                    {exp.description}
                  </td>
                  <td className="py-3.5 px-4">
                    <span className="px-2 py-0.5 rounded-full bg-slate-100 text-slate-700 text-[10px] font-semibold">
                      {exp.purpose}
                    </span>
                  </td>
                  <td className="py-3.5 px-4 text-slate-800 font-medium">
                    {exp.receivebyName}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {exp.releasebyName}
                  </td>
                  <td className="py-3.5 px-4 text-slate-500 text-[11px]">
                    {exp.remarks}
                  </td>
                  <td className="py-3.5 px-6 text-right font-bold text-slate-900 text-sm">
                    {formatCurrency(exp.amount)}
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
