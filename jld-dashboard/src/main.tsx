import { StrictMode, Suspense, lazy, useState } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { LoginPage } from './components/LoginPage';

const Workspace = lazy(() => import('./App'));

function RootApp() {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(() => {
    const params = new URLSearchParams(window.location.search);
    return params.get('view') === 'workspace' || !!sessionStorage.getItem('jld_auth_user');
  });

  const handleLoginSuccess = (email: string) => {
    sessionStorage.setItem('jld_auth_user', email);
    const url = new URL(window.location.href);
    url.searchParams.set('view', 'workspace');
    window.history.pushState({}, '', url.toString());
    setIsAuthenticated(true);
  };

  if (isAuthenticated) {
    return (
      <Suspense fallback={<div className="login-loading" role="status">Opening your workspace…</div>}>
        <Workspace />
      </Suspense>
    );
  }

  return <LoginPage onLoginSuccess={handleLoginSuccess} />;
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RootApp />
  </StrictMode>,
);

