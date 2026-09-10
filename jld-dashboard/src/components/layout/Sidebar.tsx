import { useState } from 'react';
import { 
  Building2, 
  Users, 
  Receipt, 
  Wallet, 
  FileSpreadsheet, 
  UserCheck, 
  HandCoins, 
  Calculator, 
  Award, 
  LayoutDashboard, 
  FileText, 
  PanelLeftClose, 
  PanelLeftOpen,
  Files
} from 'lucide-react';

export type NavigationTab = 
  | 'overview' 
  | 'contracts' 
  | 'products' 
  | 'stakeholder' 
  | 'payment' 
  | 'agents-commissions' 
  | 'employees' 
  | 'loans-benefits' 
  | 'payroll' 
  | 'expenses' 
  | 'reports'
  | 'printable-accounts';

export const NAV_SECTIONS = [
  {
    id: 'sales',
    title: 'PROPERTY & SALES',
    items: [
      { id: 'products', label: 'Properties', icon: Building2 },
      { id: 'stakeholder', label: 'Stakeholders', icon: Users },
      { id: 'contracts', label: 'Sales contracts', icon: FileText },
      { id: 'payment', label: 'Payments', icon: Receipt },
    ],
  },
  {
    id: 'agents',
    title: 'SALES PARTNERS',
    items: [
      { id: 'agents-commissions', label: 'Agents & commissions', icon: Award },
    ],
  },
  {
    id: 'people',
    title: 'PEOPLE & PAYROLL',
    items: [
      { id: 'employees', label: 'Employees', icon: UserCheck },
      { id: 'loans-benefits', label: 'Loans & benefits', icon: HandCoins },
      { id: 'payroll', label: 'Payroll & payslips', icon: Calculator },
    ],
  },
  {
    id: 'finance',
    title: 'FINANCE',
    items: [
      { id: 'expenses', label: 'Expenses', icon: Wallet },
      { id: 'reports', label: 'Cash flow & reports', icon: FileSpreadsheet },
    ],
  },
  {
    id: 'printables',
    title: 'DATA & PRINTABLES',
    items: [
      { id: 'printable-accounts', label: 'Printable accounts', icon: Files },
    ],
  },
];

export function Sidebar({
  currentTab,
  onTabChange,
}: {
  currentTab: NavigationTab;
  onTabChange: (tab: NavigationTab) => void;
}) {
  const [collapsed, setCollapsed] = useState(false);

  const renderItem = (id: string, label: string, Icon: typeof Building2) => (
    <button
      key={id}
      title={label}
      aria-current={currentTab === id ? 'page' : undefined}
      onClick={() => onTabChange(id as NavigationTab)}
      className={`sidebar-nav-item nav-item ${currentTab === id ? 'selected sidebar-nav-item-active' : ''}`}
    >
      <Icon className="sidebar-nav-icon" size={19} />
      <span className="sidebar-nav-label">{label}</span>
    </button>
  );

  return (
    <aside className={`sidebar-nav-panel erp-sidebar ${collapsed ? 'collapsed' : ''}`}>
      <div className="sidebar-brand-container brand">
        <div className="sidebar-brand-icon brand-icon">
          <Building2 size={24} />
        </div>
        <div className="sidebar-brand-details">
          <strong className="sidebar-brand-title">
            JLD<span className="sidebar-brand-subtitle"> workspace</span>
          </strong>
          <small className="sidebar-brand-tagline">PROPERTY MANAGEMENT</small>
        </div>
      </div>

      <nav className="sidebar-nav-menu" aria-label="Main navigation">
        {renderItem('overview', 'Overview', LayoutDashboard)}
        {NAV_SECTIONS.map((section) => (
          <section className="sidebar-nav-section" key={section.id}>
            <h2 className="sidebar-section-heading">{section.title}</h2>
            {section.items.map((item) => renderItem(item.id, item.label, item.icon))}
          </section>
        ))}
      </nav>

      <div className="sidebar-footer-panel sidebar-footer">
        <button
          className="sidebar-toggle-button"
          aria-label={collapsed ? 'Expand navigation' : 'Collapse navigation'}
          title={collapsed ? 'Expand navigation' : 'Collapse navigation'}
          onClick={() => setCollapsed(!collapsed)}
        >
          {collapsed ? <PanelLeftOpen size={18} /> : <PanelLeftClose size={18} />}
          {!collapsed && <span className="sidebar-toggle-text">Collapse navigation</span>}
        </button>
      </div>
    </aside>
  );
}

