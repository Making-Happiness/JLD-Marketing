import { StrictMode, Suspense, lazy, useState, useEffect } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { LoginPage } from './components/LoginPage';
import { SessionContext, type SessionUser } from './utils/session';
const Workspace = lazy(() => import('./ConnectedWorkspace'));
function RootApp() {
 const [user,setUser]=useState<SessionUser|null>(null);
 const [checking,setChecking]=useState(true);
 useEffect(() => {
  fetch('/api/auth/session')
    .then((r) => (r.ok ? r.json() : null))
    .then((data) => {
      if (data?.user) {
        setUser(data.user);
      } else {
        const stored = sessionStorage.getItem('jld_auth_user');
        if (stored) {
          try { setUser(JSON.parse(stored)); } catch { setUser(null); }
        } else {
          setUser(null);
        }
      }
    })
    .catch(() => {
      const stored = sessionStorage.getItem('jld_auth_user');
      if (stored) {
        try { setUser(JSON.parse(stored)); } catch { setUser(null); }
      } else {
        setUser(null);
      }
    })
    .finally(() => setChecking(false));
 }, []);
 if(checking)return <div className="login-loading" role="status">Checking your session…</div>;
 return user?<SessionContext.Provider value={user}><Suspense fallback={<div className="login-loading" role="status">Opening your workspace…</div>}><Workspace /></Suspense></SessionContext.Provider>:<LoginPage onLoginSuccess={setUser}/>;
}
createRoot(document.getElementById('root')!).render(<StrictMode><RootApp/></StrictMode>);
