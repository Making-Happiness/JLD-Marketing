import { StrictMode, useState, useEffect } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import { SessionContext, type SessionUser } from './utils/session';
import { App } from './App';
import { fetchAllData } from './utils/supabase';

function RootApp() {
  const [dbData, setDbData] = useState<any>(null);

  useEffect(() => {
    fetchAllData().then(setDbData);
  }, []);

  if (!dbData) return <div className="login-loading" role="status">Loading database...</div>;

  const dummyUser: SessionUser = { id: 'admin-1', email: 'admin@jldsubdivision.com' };

  return (
    <SessionContext.Provider value={dummyUser}>
      <App initialState={dbData} />
    </SessionContext.Provider>
  );
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RootApp />
  </StrictMode>
);
