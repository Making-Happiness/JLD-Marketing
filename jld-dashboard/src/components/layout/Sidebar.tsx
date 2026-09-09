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
 const item=(id:string,label:string,Icon:typeof Building2)=><button key={id} title={label} aria-current={currentTab===id?'page':undefined} onClick={()=>onTabChange(id as NavigationTab)} className={`nav-item ${currentTab===id?'selected':''}`}><Icon size={19}/><span>{label}</span></button>;
 return <aside className={`erp-sidebar ${collapsed?'collapsed':''}`}><div className="brand"><div className="brand-icon"><Building2 size={24}/></div><div><strong>JLD<span> workspace</span></strong><small>PROPERTY MANAGEMENT</small></div></div><nav aria-label="Main navigation">{item('overview','Overview',LayoutDashboard)}{NAV_MODULES.map(m=><section key={m.id}><h2>{m.title}</h2>{m.items.map(i=>item(i.id,i.label,i.icon))}</section>)}</nav><div className="sidebar-footer"><div className="workspace-avatar">JL</div><div><strong>JLD Subdivision</strong><small>Accounting workspace</small></div><button aria-label={collapsed?'Expand navigation':'Collapse navigation'} onClick={()=>setCollapsed(!collapsed)}>{collapsed?<PanelLeftOpen size={18}/>:<PanelLeftClose size={18}/>}</button></div></aside>
}

