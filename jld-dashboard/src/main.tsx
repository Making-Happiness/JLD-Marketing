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

  if (!dbData) {
    return (
      <div className="flex items-center justify-center min-h-screen bg-[#F8FAFC]">
        <div className="relative flex items-center justify-center w-24 h-24">
          <div className="absolute inset-0 border-4 border-blue-100 rounded-full"></div>
          <div className="absolute inset-0 border-4 border-blue-600 rounded-full border-t-transparent animate-spin"></div>
          <span className="text-xl font-bold text-slate-800 tracking-wider">JLD</span>
        </div>
      </div>
    );
  }

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
