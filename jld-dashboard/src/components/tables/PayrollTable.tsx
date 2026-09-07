import React, { useState, useMemo } from 'react';
import { PayrollRecord, PayslipRecord } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { Calculator, FileText, Printer, Plus, Search } from 'lucide-react';

interface PayrollTableProps {
  payrollRecords: PayrollRecord[];
  payslips?: PayslipRecord[];
  onProcessPayroll?: () => void;
  onGeneratePayslip?: () => void;
}

export const PayrollTable: React.FC<PayrollTableProps> = ({
  payrollRecords,
  payslips = [],
  onProcessPayroll,
  onGeneratePayslip
}) => {
  // Tab mode: Cutoff Batches vs Employee Payslips Directory
  const [activeTab, setActiveTab] = useState<'BATCH' | 'PAYSLIPS'>('BATCH');
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedSlip, setSelectedSlip] = useState<PayslipRecord | null>(null);

  // Financial calculations
  const totalGross = payrollRecords.reduce((acc, curr) => acc + curr.grossPay, 0);
  const totalNet = payrollRecords.reduce((acc, curr) => acc + curr.netPay, 0);

  // Filtered payslips
  const filteredPayslips = useMemo(() => {
    if (!searchTerm.trim()) return payslips;
    const q = searchTerm.toLowerCase();
    return payslips.filter(s => 
      s.employeeName.toLowerCase().includes(q) ||
      s.designation.toLowerCase().includes(q) ||
      s.month.toLowerCase().includes(q)
    );
  }, [payslips, searchTerm]);

  // Filtered payroll records
  const filteredPayroll = useMemo(() => {
    if (!searchTerm.trim()) return payrollRecords;
    const q = searchTerm.toLowerCase();
    return payrollRecords.filter(p => 
      p.employeeName.toLowerCase().includes(q) ||
      p.designation.toLowerCase().includes(q)
    );
  }, [payrollRecords, searchTerm]);

  // Helper to open slip modal from a payroll record
  const handleOpenSlipFromPayroll = (p: PayrollRecord) => {
    const existing = payslips.find(s => s.idemployee === p.idemployee);
    if (existing) {
      setSelectedSlip(existing);
    } else {
      setSelectedSlip({
        id: p.id,
        idemployee: p.idemployee,
        employeeName: p.employeeName,
        designation: p.designation,
        month: p.period || 'Current Cutoff',
        basicSalary: p.grossPay,
        overtimeEarnings: 0,
        benefits: 0,
        cashAdvanceDeduction: p.cashAdvance,
        otherDeductions: p.merienda + p.egg + p.rice + p.emergencyFund + p.otherDeductions,
        netPay: p.netPay,
        dateGenerated: new Date().toISOString().split('T')[0],
        status: p.status === 'Approved' ? 'Paid' : 'Draft'
      });
    }
  };

  return (
    <div className="px-8 py-5 space-y-4">
      {/* Top Segmented Tab Switcher */}
      <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs flex flex-col md:flex-row items-stretch md:items-center justify-between gap-4">
        {/* Tab Controls */}
        <div className="inline-flex p-1 bg-slate-100/80 rounded-xl border border-slate-200/60 self-start md:self-auto">
          <button
            type="button"
            onClick={() => {
              setActiveTab('BATCH');
              setSearchTerm('');
            }}
            className={`flex items-center gap-2 px-4 py-2 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              activeTab === 'BATCH'
                ? 'bg-[#00593B] text-white shadow-xs'
                : 'text-slate-600 hover:text-slate-900 hover:bg-slate-200/60'
            }`}
          >
            <Calculator className="w-3.5 h-3.5" />
            <span>Semi-Monthly Cutoff Batches</span>
            <span className={`text-[10px] px-1.5 py-0.2 rounded-full font-bold ml-1 ${
              activeTab === 'BATCH' ? 'bg-emerald-900/60 text-emerald-200' : 'bg-slate-200 text-slate-700'
            }`}>
              {payrollRecords.length}
            </span>
          </button>

          <button
            type="button"
            onClick={() => {
              setActiveTab('PAYSLIPS');
              setSearchTerm('');
            }}
            className={`flex items-center gap-2 px-4 py-2 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              activeTab === 'PAYSLIPS'
                ? 'bg-[#00593B] text-white shadow-xs'
                : 'text-slate-600 hover:text-slate-900 hover:bg-slate-200/60'
            }`}
          >
            <FileText className="w-3.5 h-3.5" />
            <span>Employee Payslips Directory</span>
            <span className={`text-[10px] px-1.5 py-0.2 rounded-full font-bold ml-1 ${
              activeTab === 'PAYSLIPS' ? 'bg-emerald-900/60 text-emerald-200' : 'bg-slate-200 text-slate-700'
            }`}>
              {payslips.length}
            </span>
          </button>
        </div>

        {/* Action Buttons */}
        <div className="flex items-center gap-2 self-end md:self-auto">
          {activeTab === 'BATCH' ? (
            <>
              <button
                type="button"
                onClick={() => window.print()}
                className="px-3.5 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 text-xs font-semibold rounded-xl transition-all cursor-pointer flex items-center gap-1.5"
                title="Print batch cutoff summary"
              >
                <Printer className="w-3.5 h-3.5" />
                <span>1-Click Print Cutoff</span>
              </button>
              <button
                type="button"
                onClick={onProcessPayroll}
                className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
              >
                <Plus className="w-3.5 h-3.5" />
                <span>Process Cutoff Batch</span>
              </button>
            </>
          ) : (
            <button
              type="button"
              onClick={onGeneratePayslip}
              className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
            >
              <Plus className="w-3.5 h-3.5" />
              <span>Generate Monthly Payslips</span>
            </button>
          )}
        </div>
      </div>

      {/* Main Table Card */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        {/* Header inside Card */}
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <h3 className="text-sm font-bold text-slate-900">
              {activeTab === 'BATCH' ? 'Semi-Monthly Payroll Cutoff Processing' : 'Employee Master Payslips Directory'}
            </h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmPayroll.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmPaySlip.cs</code>
            </p>
          </div>

          <div className="flex items-center gap-3">
            {/* Search Input */}
            <div className="relative">
              <Search className="w-3.5 h-3.5 text-slate-400 absolute left-3 top-1/2 -translate-y-1/2" />
              <input
                type="text"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search employee..."
                className="pl-8 pr-3 py-1.5 rounded-xl border border-slate-200 text-xs focus:outline-hidden focus:ring-2 focus:ring-emerald-500 w-52"
              />
            </div>

            {/* Metric Box */}
            <div className="bg-emerald-50/80 border border-emerald-200/80 px-3.5 py-1.5 rounded-xl text-right">
              <span className="text-[10px] uppercase font-bold text-emerald-800/70 block">
                {activeTab === 'BATCH' ? 'Total Net Payout' : 'Total Net Payable'}
              </span>
              <span className="text-sm font-bold text-emerald-950">{formatCurrency(totalNet)}</span>
            </div>
          </div>
        </div>

        {/* View 1: Semi-Monthly Cutoff Batches */}
        {activeTab === 'BATCH' && (
          <div className="overflow-x-auto">
            <table className="w-full text-left border-collapse text-xs">
              <thead>
                <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                  <th className="py-3 px-6">Employee</th>
                  <th className="py-3 px-3">Days</th>
                  <th className="py-3 px-3 text-right">Gross Pay</th>
                  <th className="py-3 px-3 text-right">CA Advance</th>
                  <th className="py-3 px-3 text-right">Merienda</th>
                  <th className="py-3 px-3 text-right">Egg</th>
                  <th className="py-3 px-3 text-right">Rice</th>
                  <th className="py-3 px-3 text-right">Emerg. Fund</th>
                  <th className="py-3 px-3 text-right">Total Ded.</th>
                  <th className="py-3 px-4 text-right">Net Take Home</th>
                  <th className="py-3 px-4 text-center">Status</th>
                  <th className="py-3 px-6 text-right">Action</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-slate-100">
                {filteredPayroll.length === 0 ? (
                  <tr>
                    <td colSpan={12} className="py-8 text-center text-slate-400 text-xs">
                      No payroll cutoff records found.
                    </td>
                  </tr>
                ) : (
                  filteredPayroll.map((p) => (
                    <tr key={p.id} className="hover:bg-slate-50/60 transition-colors">
                      <td className="py-3.5 px-6">
                        <div className="font-bold text-slate-900">{p.employeeName}</div>
                        <span className="text-[11px] text-slate-400">{p.designation}</span>
                      </td>
                      <td className="py-3.5 px-3 font-semibold text-slate-700">
                        {p.daysWorked} d
                      </td>
                      <td className="py-3.5 px-3 text-right font-semibold text-slate-900">
                        {formatCurrency(p.grossPay)}
                      </td>
                      <td className="py-3.5 px-3 text-right text-rose-600 font-medium">
                        {p.cashAdvance > 0 ? `-${formatCurrency(p.cashAdvance)}` : '-'}
                      </td>
                      <td className="py-3.5 px-3 text-right text-slate-600">
                        {p.merienda > 0 ? `-${formatCurrency(p.merienda)}` : '-'}
                      </td>
                      <td className="py-3.5 px-3 text-right text-slate-600">
                        {p.egg > 0 ? `-${formatCurrency(p.egg)}` : '-'}
                      </td>
                      <td className="py-3.5 px-3 text-right text-slate-600">
                        {p.rice > 0 ? `-${formatCurrency(p.rice)}` : '-'}
                      </td>
                      <td className="py-3.5 px-3 text-right text-slate-600">
                        {p.emergencyFund > 0 ? `-${formatCurrency(p.emergencyFund)}` : '-'}
                      </td>
                      <td className="py-3.5 px-3 text-right font-semibold text-rose-700">
                        -{formatCurrency(p.totalDeductions)}
                      </td>
                      <td className="py-3.5 px-4 text-right font-bold text-emerald-950 text-sm">
                        {formatCurrency(p.netPay)}
                      </td>
                      <td className="py-3.5 px-4 text-center">
                        <span className="px-2.5 py-0.5 rounded-full text-[10px] font-semibold bg-emerald-50 text-emerald-700 border border-emerald-200">
                          {p.status}
                        </span>
                      </td>
                      <td className="py-3.5 px-6 text-right">
                        <button
                          onClick={() => handleOpenSlipFromPayroll(p)}
                          className="px-2.5 py-1 bg-white border border-slate-200 text-slate-700 hover:bg-slate-50 rounded-lg text-xs font-semibold shadow-2xs transition-colors cursor-pointer"
                        >
                          View Slip
                        </button>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        )}

        {/* View 2: Employee Payslips Directory */}
        {activeTab === 'PAYSLIPS' && (
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
                  <th className="py-3 px-4 text-center">Status</th>
                  <th className="py-3 px-6 text-right">Action</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-slate-100">
                {filteredPayslips.length === 0 ? (
                  <tr>
                    <td colSpan={11} className="py-8 text-center text-slate-400 text-xs">
                      No payslips found matching your search.
                    </td>
                  </tr>
                ) : (
                  filteredPayslips.map((slip) => (
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
                      <td className="py-3.5 px-4 text-center">
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
                  ))
                )}
              </tbody>
            </table>
          </div>
        )}
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
                <span>Other Deductions (CA/Egg/Rice/Merienda):</span>
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

export default PayrollTable;
