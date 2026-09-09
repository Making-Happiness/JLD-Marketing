import { useState } from 'react';
import { Building2, Users, Receipt, Wallet, FileSpreadsheet, UserCheck, HandCoins, Calculator, Award, LayoutDashboard, FileText, PanelLeftClose, PanelLeftOpen } from 'lucide-react';
export type NavigationTab = 'overview' | 'contracts' | 'products' | 'stakeholder' | 'payment' | 'agents-commissions' | 'employees' | 'loans-benefits' | 'payroll' | 'expenses' | 'reports';
export const NAV_MODULES = [
 {id:'sales',title:'PROPERTY & SALES',items:[{id:'products',label:'Properties & lots',icon:Building2},{id:'stakeholder',label:'Buyers',icon:Users},{id:'contracts',label:'Sales contracts',icon:FileText},{id:'payment',label:'Collections',icon:Receipt}]},
 {id:'agents',title:'SALES PARTNERS',items:[{id:'agents-commissions',label:'Agents & commissions',icon:Award}]},
 {id:'people',title:'PEOPLE & PAYROLL',items:[{id:'employees',label:'Employees',icon:UserCheck},{id:'loans-benefits',label:'Loans & benefits',icon:HandCoins},{id:'payroll',label:'Payroll & payslips',icon:Calculator}]},
 {id:'finance',title:'FINANCE',items:[{id:'expenses',label:'Expenses',icon:Wallet},{id:'reports',label:'Cash flow & reports',icon:FileSpreadsheet}]}
];
export function Sidebar({currentTab,onTabChange}:{currentTab:NavigationTab;onTabChange:(tab:NavigationTab)=>void}) {
  const [collapsed,setCollapsed]=useState(false);
  const item=(id:string,label:string,Icon:typeof Building2)=><button key={id} title={label} aria-current={currentTab===id?'page':undefined} onClick={()=>onTabChange(id as NavigationTab)} className={`sidebar-nav-item nav-item ${currentTab===id?'selected sidebar-nav-item-active':''}`}><Icon className="sidebar-nav-icon" size={19}/><span className="sidebar-nav-label">{label}</span></button>;
  return <aside className={`sidebar-nav-panel erp-sidebar ${collapsed?'collapsed':''}`}><div className="sidebar-brand-container brand"><div className="sidebar-brand-icon brand-icon"><Building2 size={24}/></div><div className="sidebar-brand-details"><strong className="sidebar-brand-title">JLD<span className="sidebar-brand-subtitle"> workspace</span></strong><small className="sidebar-brand-tagline">PROPERTY MANAGEMENT</small></div></div><nav className="sidebar-nav-menu" aria-label="Main navigation">{item('overview','Overview',LayoutDashboard)}{NAV_MODULES.map(m=><section className="sidebar-module-section" key={m.id}><h2 className="sidebar-section-heading">{m.title}</h2>{m.items.map(i=>item(i.id,i.label,i.icon))}</section>)}</nav><div className="sidebar-footer-panel sidebar-footer"><div className="sidebar-user-avatar workspace-avatar">JL</div><div className="sidebar-user-details"><strong className="sidebar-user-name">JLD Subdivision</strong><small className="sidebar-user-role">Accounting workspace</small></div><button className="sidebar-toggle-button" aria-label={collapsed?'Expand navigation':'Collapse navigation'} onClick={()=>setCollapsed(!collapsed)}>{collapsed?<PanelLeftOpen size={18}/>:<PanelLeftClose size={18}/>}</button></div></aside>;
}

