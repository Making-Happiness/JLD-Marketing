import { useCallback, useEffect, useRef, useState } from 'react';
import { App } from './App';
import type { FullDatabaseState } from './utils/guardRails';
import type { AuditLogEntry } from './types';
import { getDefaultFullState } from './data/initialData';

export type SavedWorkspace = FullDatabaseState & { auditLogs: AuditLogEntry[] };

export default function ConnectedWorkspace() {
  // 1. Instant, synchronous boot from localStorage
  const [initial] = useState<SavedWorkspace>(() => {
    const DEMO_VERSION = 'clean_demo_v2';
    const currentVersion = (() => {
      try {
        return localStorage.getItem('jld_workspace_version');
      } catch {
        return null;
      }
    })();

    if (currentVersion === DEMO_VERSION) {
      try {
        const raw = localStorage.getItem('jld_workspace_state');
        if (raw) {
          const parsed = JSON.parse(raw);
          if (parsed && Array.isArray(parsed.products)) {
            return parsed;
          }
        }
      } catch {}
    }

    const cleanInitial = getDefaultFullState();
    try {
      localStorage.setItem('jld_workspace_version', DEMO_VERSION);
      localStorage.setItem('jld_workspace_state', JSON.stringify(cleanInitial));
    } catch {}
    return cleanInitial;
  });

  const [status, setStatus] = useState('All changes saved');
  const revision = useRef(0);
  const last = useRef('');
  const pending = useRef<string | null>(null);
  const saving = useRef(false);

  // Background server sync (purely opportunistic, never blocks or alerts user)
  const flush = useCallback(async () => {
    if (saving.current) return;
    saving.current = true;

    while (pending.current) {
      const payload = pending.current;
      pending.current = null;

      try {
        const response = await fetch('/api/workspace', {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ revision: revision.current, state: JSON.parse(payload) }),
        }).catch(() => null);

        if (response && response.ok) {
          const result = await response.json().catch(() => null);
          revision.current = result?.revision ?? (revision.current + 1);
        }
      } catch {
        // Silent background fallback: localStorage is already saved
      }
    }
    saving.current = false;
  }, []);

  // Synchronous, immediate save to local storage on ANY user action
  const onChange = useCallback((state: SavedWorkspace) => {
    const next = JSON.stringify(state);
    if (last.current === next) return;
    last.current = next;

    // IMMEDIATE SYNCHRONOUS LOCALSTORAGE PERSISTENCE
    try {
      localStorage.setItem('jld_workspace_state', next);
    } catch (e) {
      console.warn('[LocalStorage] Save warning:', e);
    }
    setStatus('All changes saved');

    // Opportunistic background SQLite sync
    pending.current = next;
    void flush();
  }, [flush]);

  useEffect(() => {
    const handler = (e: BeforeUnloadEvent) => {
      if (saving.current || pending.current) {
        e.preventDefault();
        e.returnValue = '';
      }
    };
    window.addEventListener('beforeunload', handler);
    return () => window.removeEventListener('beforeunload', handler);
  }, []);

  const handleReset = useCallback(() => {
    if (window.confirm('Reset demo state back to clean products only?')) {
      const clean = getDefaultFullState();
      try {
        localStorage.setItem('jld_workspace_version', 'clean_demo_v2');
        localStorage.setItem('jld_workspace_state', JSON.stringify(clean));
      } catch {}
      void fetch('/api/workspace', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ revision: revision.current, state: clean }),
      }).catch(() => null);
      location.reload();
    }
  }, []);

  return (
    <>
      <div
        className="workspace-save-status"
        role="status"
        style={{
          pointerEvents: 'auto',
          display: 'flex',
          alignItems: 'center',
          gap: '10px',
        }}
      >
        <span>{status}</span>
        <span style={{ color: '#9bb2a5' }}>•</span>
        <button
          type="button"
          onClick={handleReset}
          style={{
            background: 'none',
            border: 'none',
            color: '#1a5e3f',
            textDecoration: 'underline',
            cursor: 'pointer',
            fontSize: '11px',
            padding: 0,
            fontFamily: 'inherit',
          }}
          title="Reset to clean state (products only)"
        >
          Reset to clean state
        </button>
      </div>
      <App initialState={initial} onStateChange={onChange} />
    </>
  );
}
