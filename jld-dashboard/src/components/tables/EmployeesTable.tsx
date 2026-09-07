import React from 'react';
import { Employee } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { UserCheck, Phone, Plus } from 'lucide-react';

interface EmployeesTableProps {
  employees: Employee[];
  onAddEmployee?: () => void;
}

export const EmployeesTable: React.FC<EmployeesTableProps> = ({
  employees,
  onAddEmployee
}) => {
  return (
    <div className="px-8 py-4">
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-5 border-b border-slate-100 flex items-center justify-between">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Employees &amp; Staff Masterlist</h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmEmployees.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">employees</code> table
            </p>
          </div>
          <button
            onClick={onAddEmployee}
            className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
          >
            <Plus className="w-3.5 h-3.5" />
            <span>+ Add Employee</span>
          </button>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">ID #</th>
                <th className="py-3 px-4">Employee Full Name</th>
                <th className="py-3 px-4">Designation / Role</th>
                <th className="py-3 px-4">Gender</th>
                <th className="py-3 px-4">Contact #</th>
                <th className="py-3 px-4">Civil Status</th>
                <th className="py-3 px-4">Base Monthly Salary</th>
                <th className="py-3 px-6 text-right">Status</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {employees.map((emp) => (
                <tr key={emp.idemployee} className="hover:bg-slate-50/60 transition-colors">
                  <td className="py-3.5 px-6 font-mono text-slate-400">
                    EMP-{emp.idemployee.toString().padStart(4, '0')}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-slate-900">
                    <div className="flex items-center gap-2">
                      <div className="w-7 h-7 rounded-full bg-emerald-100 text-emerald-800 flex items-center justify-center font-bold text-xs">
                        {emp.firstname.charAt(0)}
                      </div>
                      <span>{emp.fullname}</span>
                    </div>
                  </td>
                  <td className="py-3.5 px-4 font-semibold text-slate-700">
                    {emp.designation}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {emp.gender}
                  </td>
                  <td className="py-3.5 px-4 text-slate-700">
                    <div className="flex items-center gap-1">
                      <Phone className="w-3 h-3 text-slate-400" />
                      <span>{emp.contactno}</span>
                    </div>
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {emp.civilstatus}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-slate-900">
                    {formatCurrency(emp.salary)}
                  </td>
                  <td className="py-3.5 px-6 text-right">
                    <span className="px-2.5 py-0.5 rounded-full text-[10px] font-semibold bg-emerald-50 text-emerald-700 border border-emerald-200">
                      {emp.recordstatus}
                    </span>
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
