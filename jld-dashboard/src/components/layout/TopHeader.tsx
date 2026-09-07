import React from 'react';
import { Bell, ChevronDown } from 'lucide-react';
import { NavigationTab } from './Sidebar';

interface TopHeaderProps {
  currentTab: NavigationTab;
  onOpenNotifications?: () => void;
}

export const TopHeader: React.FC<TopHeaderProps> = ({ currentTab, onOpenNotifications }) => {
  const getHeaderInfo = () => {
    switch (currentTab) {
      case 'products':
        return {
          module: 'Module 1: Sales & Properties',
          title: 'Products (Inventory & Lots)',
          subtitle: 'Manage subdivision project locations, phases, total blocks, and lot allocations.'
        };
      case 'stakeholder':
        return {
          module: 'Module 1: Sales & Properties',
          title: 'Stakeholders & Contracts',
          subtitle: 'Registered buyers, personal contact information, spouse details, and acquired lots with 1-click application.'
        };
      case 'payment':
        return {
          module: 'Module 1: Sales & Properties',
          title: 'Client Payments',
          subtitle: 'Process reservations, downpayments, and monthly amortizations with official receipts.'
        };
      case 'agents-commissions':
        return {
          module: 'Module 2: Agents & Commissions',
          title: 'Unified Agent & Commission Center',
          subtitle: 'Sales directory of accredited agents & dicers, performance quotas, and commission claim vouchers.'
        };
      case 'employees':
        return {
          module: 'Module 3: HR & Payroll',
          title: 'Employee Directory',
          subtitle: 'Employee masterlist, designations, daily rates, and personnel employment records.'
        };
      case 'loans-benefits':
        return {
          module: 'Module 3: HR & Payroll',
          title: 'Employee Adjustments (Loans & Benefits)',
          subtitle: 'Track employee cash advances, emergency loans, and performance allowances with in-page toggle.'
        };
      case 'payroll':
        return {
          module: 'Module 3: HR & Payroll',
          title: 'Payroll & Payslips',
          subtitle: 'Semi-monthly payroll cutoff processing with integrated batch payslip generation and printing.'
        };
      case 'expenses':
        return {
          module: 'Module 4: Finance & Reports',
          title: 'Company Expenses',
          subtitle: 'Track operational disbursements, engineering surveys, documentation fees, and permits.'
        };
      case 'reports':
        return {
          module: 'Module 4: Finance & Reports',
          title: 'Financial Reports & SOA',
          subtitle: 'Generate Statements of Account (SOA), cash inflow receipts, and financial summaries.'
        };
      default:
        return {
          module: 'JLD Subdivision',
          title: 'Real Property System',
          subtitle: 'Integrated subdivision management and accounting system.'
        };
    }
  };

  const { module, title, subtitle } = getHeaderInfo();

  return (
    <header className="px-8 pt-5 pb-4 flex items-center justify-between border-b border-slate-200/70 bg-white/80 backdrop-blur-xs sticky top-0 z-10">
      <div>
        <div className="flex items-center gap-2 mb-0.5">
          <span className="text-[11px] font-bold text-emerald-800 tracking-wide uppercase">
            {module}
          </span>
          <span className="text-slate-300">&bull;</span>
          <span className="text-[11px] font-medium text-slate-400">JLD Real Property</span>
        </div>
        <h1 className="text-xl font-bold text-slate-900 tracking-tight">{title}</h1>
        <p className="text-xs text-slate-500 mt-0.5 font-normal">{subtitle}</p>
      </div>

      <div className="flex items-center gap-3.5">
        {/* Notification Bell */}
        <button 
          onClick={onOpenNotifications}
          className="relative w-9 h-9 rounded-xl border border-slate-200/80 bg-white hover:bg-slate-50 flex items-center justify-center text-slate-600 transition-colors shadow-2xs cursor-pointer"
        >
          <Bell className="w-4 h-4 text-slate-600" />
          <span className="absolute top-2 right-2 w-2 h-2 bg-emerald-600 rounded-full ring-2 ring-white"></span>
        </button>

        {/* Profile Card */}
        <div className="flex items-center gap-2.5 pl-2 py-1 pr-3 rounded-xl border border-slate-200/80 bg-white hover:bg-slate-50/70 transition-all cursor-pointer shadow-2xs">
          <div className="w-8 h-8 rounded-lg bg-emerald-900 flex items-center justify-center text-white font-bold text-xs shadow-2xs">
            JD
          </div>
          <div className="flex flex-col text-left">
            <span className="text-xs font-bold text-slate-900 leading-tight">Admin User</span>
            <span className="text-[10px] text-emerald-700 font-semibold">Administrator</span>
          </div>
          <ChevronDown className="w-3.5 h-3.5 text-slate-400 ml-1" />
        </div>
      </div>
    </header>
  );
};

export default TopHeader;
