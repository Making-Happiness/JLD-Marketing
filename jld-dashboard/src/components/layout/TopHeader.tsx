import { useSession } from '../../utils/session';
import { History, ChevronRight, LogOut } from 'lucide-react';
import { NAV_SECTIONS, NavigationTab } from './Sidebar';

export function TopHeader({
  currentTab,
  onOpenNotifications,
}: {
  currentTab: NavigationTab;
  onOpenNotifications?: () => void;
}) {
  const user = useSession();
  const section = NAV_SECTIONS.find((s) => s.items.some((i) => i.id === currentTab));
  const title = section?.items.find((i) => i.id === currentTab)?.label || 'Overview';

  const handleSignOut = async () => {
    await fetch('/api/auth/logout', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: '{}' }).catch(() => null);
    sessionStorage.removeItem('jld_auth_user');
    window.location.assign('/');
  };

  return (
    <header className="top-header-bar erp-header">
      <div className="header-breadcrumbs breadcrumb">
        <span className="breadcrumb-root">Workspace</span>
        <ChevronRight className="breadcrumb-separator" size={15} />
        <strong className="breadcrumb-current">{title}</strong>
      </div>
      <div className="header-utility-tools header-tools">
        <span className="header-session-tag session-label">Employee workspace</span>
        <button
          className="header-history-trigger"
          onClick={onOpenNotifications}
          aria-label="Open activity history"
          title="Activity history"
        >
          <History size={19} />
        </button>
        <button
          type="button"
          className="header-signout"
          onClick={handleSignOut}
          aria-label="Sign out"
          title="Sign out"
        >
          <LogOut size={15} />
          <span>Sign out</span>
        </button>
        <div className="header-user-avatar workspace-avatar" title={user?.email}>JLD</div>
        <span className="header-account-profile account-label">
          {user?.email}
          <small className="account-company">JLD Subdivision</small>
        </span>
      </div>
    </header>
  );
}


