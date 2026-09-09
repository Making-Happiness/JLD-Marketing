import { History, ChevronRight } from 'lucide-react';
import { NAV_MODULES, NavigationTab } from './Sidebar';
export function TopHeader({currentTab,onOpenNotifications}:{currentTab:NavigationTab;onOpenNotifications?:()=>void}) {
 const module=NAV_MODULES.find(m=>m.items.some(i=>i.id===currentTab));
 const title=module?.items.find(i=>i.id===currentTab)?.label || 'Overview';
 return <header className="erp-header"><div className="breadcrumb"><span>Workspace</span><ChevronRight size={15}/><strong>{title}</strong></div><div className="header-tools"><span className="session-label">Demo · session data</span><button onClick={onOpenNotifications} aria-label="Open activity history" title="Activity history"><History size={19}/></button><div className="workspace-avatar">AC</div><span className="account-label">Accountant<small>JLD Subdivision</small></span></div></header>
}

