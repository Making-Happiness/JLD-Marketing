import { History, ChevronRight } from 'lucide-react';
import { NAV_SECTIONS, NavigationTab } from './Sidebar';

export function TopHeader({
  currentTab,
  onOpenNotifications,
}: {
  currentTab: NavigationTab;
  onOpenNotifications?: () => void;
}) {
  const section = NAV_SECTIONS.find((s) => s.items.some((i) => i.id === currentTab));
  const title = section?.items.find((i) => i.id === currentTab)?.label || 'Overview';

  return (
    <header className="top-header-bar erp-header">
      <div className="header-breadcrumbs breadcrumb">
        <span className="breadcrumb-root">Workspace</span>
        <ChevronRight className="breadcrumb-separator" size={15} />
        <strong className="breadcrumb-current">{title}</strong>
      </div>
      <div className="header-utility-tools header-tools">
        <span className="header-session-tag session-label">Demo · session data</span>
        <button
          className="header-history-trigger"
          onClick={onOpenNotifications}
          aria-label="Open activity history"
          title="Activity history"
        >
          <History size={19} />
        </button>
        <div className="header-user-avatar workspace-avatar">AC</div>
        <span className="header-account-profile account-label">
          Accountant
          <small className="account-company">JLD Subdivision</small>
        </span>
      </div>
    </header>
  );
}

