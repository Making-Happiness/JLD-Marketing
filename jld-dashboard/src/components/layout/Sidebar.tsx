import React, { useState, useEffect } from 'react';
import { 
  Building2, 
  Users, 
  Receipt, 
  Wallet, 
  FileSpreadsheet, 
  UserCheck, 
  HandCoins, 
  Calculator, 
  ChevronDown, 
  ChevronRight,
  Award,
  GitBranch
} from 'lucide-react';

export type NavigationTab = 
  | 'products'
  | 'stakeholder'
  | 'payment'
  | 'agents-commissions'
  | 'employees'
  | 'loans-benefits'
  | 'payroll'
  | 'expenses'
  | 'reports';

export interface NavSubItem {
  id: NavigationTab;
  label: string;
  icon: React.ComponentType<{ className?: string }>;
  badge?: string;
}

export interface NavModule {
  id: string;
  title: string;
  subtitle: string;
  icon: React.ComponentType<{ className?: string }>;
  items: NavSubItem[];
}

export const NAV_MODULES: NavModule[] = [
  {
    id: 'sales-properties',
    title: 'Sales & Properties',
    subtitle: 'Inventory, buyers & collections',
    icon: Building2,
    items: [
      { id: 'products', label: 'Products (Inventory & Lots)', icon: Building2 },
      { id: 'stakeholder', label: 'Stakeholders & Contracts', icon: Users },
      { id: 'payment', label: 'Client Payments', icon: Receipt },
    ]
  },
  {
    id: 'agents-commissions-mod',
    title: 'Agents & Commissions',
    subtitle: 'Unified sales & claims center',
    icon: Award,
    items: [
      { id: 'agents-commissions', label: 'Agent & Commission Center', icon: GitBranch, badge: 'Claims' },
    ]
  },
  {
    id: 'hr-payroll-mod',
    title: 'HR & Payroll',
    subtitle: 'Personnel, adjustments & salary',
    icon: UserCheck,
    items: [
      { id: 'employees', label: 'Employee Directory', icon: UserCheck },
      { id: 'loans-benefits', label: 'Loans & Benefits', icon: HandCoins },
      { id: 'payroll', label: 'Payroll & Payslips', icon: Calculator },
    ]
  },
  {
    id: 'finance-reports-mod',
    title: 'Finance & Reports',
    subtitle: 'Cash inflows & expenditures',
    icon: FileSpreadsheet,
    items: [
      { id: 'expenses', label: 'Company Expenses', icon: Wallet },
      { id: 'reports', label: 'Financial Reports & SOA', icon: FileSpreadsheet },
    ]
  }
];

interface SidebarProps {
  currentTab: NavigationTab;
  onTabChange: (tab: NavigationTab) => void;
}

export const Sidebar: React.FC<SidebarProps> = ({ currentTab, onTabChange }) => {
  // Collapsible accordion state for the 4 modules
  const [expandedModules, setExpandedModules] = useState<Record<string, boolean>>({
    'sales-properties': true,
    'agents-commissions-mod': true,
    'hr-payroll-mod': true,
    'finance-reports-mod': true,
  });

  // Auto-expand module containing the active tab
  useEffect(() => {
    const parentModule = NAV_MODULES.find(m => m.items.some(i => i.id === currentTab));
    if (parentModule && !expandedModules[parentModule.id]) {
      setExpandedModules(prev => ({ ...prev, [parentModule.id]: true }));
    }
  }, [currentTab]);

  const toggleModule = (moduleId: string) => {
    setExpandedModules(prev => ({
      ...prev,
      [moduleId]: !prev[moduleId]
    }));
  };

  return (
    <aside className="w-72 bg-white border-r border-slate-200/80 flex flex-col shrink-0 select-none min-h-screen shadow-2xs">
      {/* Brand Header */}
      <div className="flex items-center gap-3 px-6 py-5 border-b border-slate-100 bg-slate-50/50">
        <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-emerald-800 to-emerald-950 flex items-center justify-center text-white font-bold text-lg shadow-sm shadow-emerald-900/20">
          <span className="tracking-tight text-white font-black">J</span>
        </div>
        <div className="flex flex-col min-w-0">
          <span className="font-bold text-slate-900 text-base tracking-tight leading-tight">JLD Subdivision</span>
          <span className="text-[10px] uppercase font-bold text-emerald-800 tracking-wider">Property Management</span>
        </div>
      </div>

      {/* 4 Business Modules with Sub-navigation */}
      <nav className="px-3.5 py-4 space-y-3 flex-1 overflow-y-auto">
        {NAV_MODULES.map((module, idx) => {
          const isExpanded = expandedModules[module.id] ?? true;
          const hasActiveChild = module.items.some(i => i.id === currentTab);
          const ModuleIcon = module.icon;

          return (
            <div key={module.id} className="rounded-2xl border border-slate-200/60 overflow-hidden bg-white shadow-2xs">
              {/* Module Header Button (Accordion Trigger) */}
              <button
                type="button"
                onClick={() => toggleModule(module.id)}
                className={`w-full flex items-center justify-between px-3.5 py-3 transition-colors cursor-pointer text-left ${
                  hasActiveChild
                    ? 'bg-emerald-950 text-white'
                    : 'bg-slate-50 hover:bg-slate-100/80 text-slate-800'
                }`}
              >
                <div className="flex items-center gap-2.5 min-w-0">
                  <div className={`w-7 h-7 rounded-lg flex items-center justify-center shrink-0 ${
                    hasActiveChild ? 'bg-emerald-800 text-emerald-100' : 'bg-white text-slate-600 border border-slate-200/60'
                  }`}>
                    <ModuleIcon className="w-4 h-4" />
                  </div>
                  <div className="min-w-0">
                    <div className="flex items-center gap-1.5">
                      <span className={`text-[10px] font-bold uppercase tracking-wider px-1 rounded ${
                        hasActiveChild ? 'bg-emerald-800/80 text-emerald-200' : 'bg-slate-200 text-slate-700'
                      }`}>
                        M{idx + 1}
                      </span>
                      <span className="font-bold text-xs tracking-tight truncate block">
                        {module.title}
                      </span>
                    </div>
                  </div>
                </div>

                <div className="flex items-center gap-1 shrink-0 ml-2">
                  <span className={`text-[10px] font-semibold px-1.5 py-0.5 rounded-full ${
                    hasActiveChild ? 'bg-emerald-800/60 text-emerald-200' : 'bg-slate-200/80 text-slate-600'
                  }`}>
                    {module.items.length}
                  </span>
                  {isExpanded ? (
                    <ChevronDown className={`w-4 h-4 ${hasActiveChild ? 'text-emerald-300' : 'text-slate-400'}`} />
                  ) : (
                    <ChevronRight className={`w-4 h-4 ${hasActiveChild ? 'text-emerald-300' : 'text-slate-400'}`} />
                  )}
                </div>
              </button>

              {/* Sub-navigation Items */}
              {isExpanded && (
                <div className="py-1.5 px-2 bg-slate-50/40 divide-y divide-slate-100/60 space-y-0.5">
                  {module.items.map((subItem) => {
                    const SubIcon = subItem.icon;
                    const isActive = currentTab === subItem.id;

                    return (
                      <button
                        key={subItem.id}
                        type="button"
                        onClick={() => onTabChange(subItem.id)}
                        className={`w-full flex items-center justify-between px-3 py-2 rounded-xl text-xs font-medium transition-all cursor-pointer ${
                          isActive
                            ? 'bg-emerald-100/80 text-emerald-950 font-bold border border-emerald-300/80 shadow-2xs'
                            : 'text-slate-600 hover:text-slate-900 hover:bg-slate-100/70 border border-transparent'
                        }`}
                      >
                        <div className="flex items-center gap-2.5 min-w-0">
                          <SubIcon className={`w-3.5 h-3.5 shrink-0 ${isActive ? 'text-emerald-800' : 'text-slate-400'}`} />
                          <span className="truncate">{subItem.label}</span>
                        </div>

                        {subItem.badge && (
                          <span className={`text-[9px] px-1.5 py-0.2 rounded-full font-bold uppercase tracking-wider ${
                            isActive ? 'bg-emerald-800 text-white' : 'bg-emerald-50 text-emerald-800 border border-emerald-200'
                          }`}>
                            {subItem.badge}
                          </span>
                        )}
                      </button>
                    );
                  })}
                </div>
              )}
            </div>
          );
        })}
      </nav>

      {/* Footer info */}
      <div className="p-4 border-t border-slate-100 text-[11px] text-slate-400 text-center bg-slate-50/30">
        <span className="font-semibold text-slate-600">JLD Real Property</span> &bull; 4 Business Modules
      </div>
    </aside>
  );
};

export default Sidebar;
