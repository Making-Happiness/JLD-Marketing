import React, { useState } from 'react';
import { PayslipRecord } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { FileText, Printer, CheckCircle, Plus } from 'lucide-react';

interface PayslipTableProps {
  payslips: PayslipRecord[];
  onGeneratePayslip?: () => void;
}

export const PayslipTable: React.FC<PayslipTableProps> = ({
  payslips,
  onGeneratePayslip
}) => {
  const [selectedSlip, setSelectedSlip] = useState<PayslipRecord | null>(null);

  return (
    <div className="px-8 py-4 space-y-4">
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Employee Master Payslips</h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmPaySlip.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">payslips</code> table
            </p>
          </div>
          <button
            onClick={onGeneratePayslip}
            className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
          >
            <Plus className="w-3.5 h-3.5" />
            <span>Generate Monthly Payslips</span>
          </button>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">Slip #</th>
                <th className="py-3 px-4">Employee</th>
                <th className="py-3 px-4">Designation</th>
                <th className="py-3 px-4">Payroll Month</th>
                <th className="py-3 px-4 text-right">Basic Pay</th>
                <th className="py-3 px-4 text-right">Overtime / Addl</th>
                <th className="py-3 px-4 text-right">Benefits</th>
                <th className="py-3 px-4 text-right">Deductions</th>
                <th className="py-3 px-4 text-right">Net Take Home</th>
                <th className="py-3 px-4">Status</th>
                <th className="py-3 px-6 text-right">Action</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {payslips.map((slip) => (
                <tr key={slip.id} className="hover:bg-slate-50/60 transition-colors">
                  <td className="py-3.5 px-6 font-mono text-slate-400">
                    PS-{slip.id.toString().padStart(4, '0')}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-slate-900">
                    {slip.employeeName}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {slip.designation}
                  </td>
                  <td className="py-3.5 px-4 font-medium text-slate-800">
                    {slip.month}
                  </td>
                  <td className="py-3.5 px-4 text-right text-slate-700">
                    {formatCurrency(slip.basicSalary)}
                  </td>
                  <td className="py-3.5 px-4 text-right text-emerald-700 font-medium">
                    +{formatCurrency(slip.overtimeEarnings)}
                  </td>
                  <td className="py-3.5 px-4 text-right text-emerald-700 font-medium">
                    +{formatCurrency(slip.benefits)}
                  </td>
                  <td className="py-3.5 px-4 text-right text-rose-600 font-medium">
                    -{formatCurrency(slip.cashAdvanceDeduction + slip.otherDeductions)}
                  </td>
                  <td className="py-3.5 px-4 text-right font-bold text-emerald-950 text-sm">
                    {formatCurrency(slip.netPay)}
                  </td>
                  <td className="py-3.5 px-4">
                    <span className="px-2.5 py-0.5 rounded-full text-[10px] font-semibold bg-emerald-50 text-emerald-700 border border-emerald-200">
                      {slip.status}
                    </span>
                  </td>
                  <td className="py-3.5 px-6 text-right">
                    <button
                      onClick={() => setSelectedSlip(slip)}
                      className="px-2.5 py-1 bg-white border border-slate-200 text-slate-700 hover:bg-slate-50 rounded-lg text-xs font-semibold shadow-2xs transition-colors cursor-pointer"
                    >
                      View Slip
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {/* Printable Payslip Modal */}
      {selectedSlip && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/40 backdrop-blur-xs p-4">
          <div className="bg-white rounded-2xl max-w-lg w-full p-6 border border-slate-200 shadow-2xl space-y-4 text-xs">
            <div className="text-center pb-3 border-b border-slate-200">
              <h3 className="text-base font-bold text-slate-900">JLD SUBDIVISION &amp; REAL PROPERTY</h3>
              <p className="text-slate-500 text-[11px]">Employee Official Monthly Payslip</p>
              <div className="mt-1 font-bold text-slate-800">{selectedSlip.month}</div>
            </div>

            <div className="grid grid-cols-2 gap-2 text-slate-700">
              <div><span className="text-slate-400">Employee:</span> <strong className="text-slate-900">{selectedSlip.employeeName}</strong></div>
              <div><span className="text-slate-400">Designation:</span> {selectedSlip.designation}</div>
            </div>

            <div className="border-t border-b border-slate-200 py-3 space-y-2">
              <div className="flex justify-between">
                <span>Basic Salary:</span>
                <span className="font-semibold">{formatCurrency(selectedSlip.basicSalary)}</span>
              </div>
              <div className="flex justify-between text-emerald-700">
                <span>Overtime &amp; Special Pay:</span>
                <span className="font-semibold">+{formatCurrency(selectedSlip.overtimeEarnings)}</span>
              </div>
              <div className="flex justify-between text-emerald-700">
                <span>Allowances &amp; Benefits:</span>
                <span className="font-semibold">+{formatCurrency(selectedSlip.benefits)}</span>
              </div>
              <div className="flex justify-between text-rose-600">
                <span>Cash Advance Deduction:</span>
                <span className="font-semibold">-{formatCurrency(selectedSlip.cashAdvanceDeduction)}</span>
              </div>
              <div className="flex justify-between text-rose-600">
                <span>Other Deductions (SSS/PhilHealth/Taxes):</span>
                <span className="font-semibold">-{formatCurrency(selectedSlip.otherDeductions)}</span>
              </div>
            </div>

            <div className="flex justify-between items-center text-sm font-bold pt-1">
              <span>NET SALARY PAYABLE:</span>
              <span className="text-emerald-800 text-base">{formatCurrency(selectedSlip.netPay)}</span>
            </div>

            <div className="flex justify-end gap-2 pt-4">
              <button
                onClick={() => window.print()}
                className="px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl flex items-center gap-1.5 cursor-pointer"
              >
                <Printer className="w-3.5 h-3.5" />
                <span>Print Slip</span>
              </button>
              <button
                onClick={() => setSelectedSlip(null)}
                className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl cursor-pointer"
              >
                Close
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};
