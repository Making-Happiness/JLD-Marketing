import { ModalFrame } from './ModalFrame';
import React, { useState, useMemo } from 'react';
import { 
  History, 
  Archive, 
  ArchiveRestore, 
  Trash2, 
  Plus, 
  Edit3, 
  Search, 
  X,
  User,
  Calendar
} from 'lucide-react';
import { AuditLogEntry, AuditAction, EntityTypeName } from '../../types';

interface ActivityHistoryModalProps {
  isOpen: boolean;
  onClose: () => void;
  auditLogs: AuditLogEntry[];
  selectedRecord?: {
    entityType: EntityTypeName;
    recordId: string | number;
    recordLabel: string;
  } | null;
}

export const ActivityHistoryModal: React.FC<ActivityHistoryModalProps> = ({
  isOpen,
  onClose,
  auditLogs,
  selectedRecord
}) => {
  const [filterAction, setFilterAction] = useState<AuditAction | 'ALL'>('ALL');
  const [searchTerm, setSearchTerm] = useState('');
  const [scope, setScope] = useState<'RECORD' | 'ALL'>(selectedRecord ? 'RECORD' : 'ALL');



  const relevantLogs = useMemo(() => {
    let logs = [...auditLogs];

    // Filter by record scope
    if (scope === 'RECORD' && selectedRecord) {
      logs = logs.filter(
        (l) =>
          l.entityType === selectedRecord.entityType &&
          String(l.recordId) === String(selectedRecord.recordId)
      );
    }

    // Filter by action
    if (filterAction !== 'ALL') {
      logs = logs.filter((l) => l.action === filterAction);
    }

    // Filter by search query
    if (searchTerm.trim()) {
      const q = searchTerm.toLowerCase();
      logs = logs.filter(
        (l) =>
          l.recordLabel.toLowerCase().includes(q) ||
          l.performedBy.toLowerCase().includes(q) ||
          l.entityType.toLowerCase().includes(q) ||
          (l.reason && l.reason.toLowerCase().includes(q)) ||
          (l.details && l.details.toLowerCase().includes(q))
      );
    }

    // Sort newest first
    return logs.sort(
      (a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()
    );
  }, [auditLogs, selectedRecord, scope, filterAction, searchTerm]);

  if (!isOpen) return null;

  const getActionBadge = (action: AuditAction) => {
    switch (action) {
      case 'ARCHIVE':
        return (
          <span className="audit-badge audit-badge-archive inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-amber-50 text-amber-800 border border-amber-200">
            <Archive className="w-3 h-3 text-amber-600" />
            <span>ARCHIVED</span>
          </span>
        );
      case 'RESTORE':
        return (
          <span className="audit-badge audit-badge-restore inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-emerald-50 text-emerald-800 border border-emerald-200">
            <ArchiveRestore className="w-3 h-3 text-emerald-600" />
            <span>RESTORED</span>
          </span>
        );
      case 'PERMANENT_DELETE':
        return (
          <span className="audit-badge audit-badge-delete inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-rose-50 text-rose-800 border border-rose-200">
            <Trash2 className="w-3 h-3 text-rose-600" />
            <span>HARD DELETED</span>
          </span>
        );
      case 'CREATE':
        return (
          <span className="audit-badge audit-badge-create inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-blue-50 text-blue-800 border border-blue-200">
            <Plus className="w-3 h-3 text-blue-600" />
            <span>CREATED</span>
          </span>
        );
      case 'EDIT':
        return (
          <span className="audit-badge audit-badge-edit inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-indigo-50 text-indigo-800 border border-indigo-200">
            <Edit3 className="w-3 h-3 text-indigo-600" />
            <span>UPDATED</span>
          </span>
        );
    }
  };

  const formatLogDate = (iso: string) => {
    try {
      const d = new Date(iso);
      return d.toLocaleString('en-US', {
        month: 'short',
        day: 'numeric',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      });
    } catch {
      return iso;
    }
  };

  return (
    <ModalFrame onClose={onClose} title="Activity History">
      <div className="audit-history-modal-shell form-shell form-shell-wide">
        {/* Header */}
        <header className="audit-history-modal-header form-header">
          <div className="audit-history-icon-badge form-heading-icon">
            <History size={22} />
          </div>
          <div className="audit-history-title-group">
            <span className="audit-history-eyebrow form-eyebrow">AUDIT LEDGER</span>
            <div style={{ display: 'flex', alignItems: 'center', gap: '8px', flexWrap: 'wrap' }}>
              <h2 className="audit-history-title">ERP Audit Trail &amp; Activity History</h2>
              {selectedRecord && scope === 'RECORD' && (
                <span style={{ fontSize: '11px', fontWeight: 600, padding: '2px 8px', borderRadius: '9999px', background: '#e8f2ec', color: '#175f46', border: '1px solid #c8dfd0' }}>
                  {selectedRecord.entityType}: {selectedRecord.recordLabel}
                </span>
              )}
            </div>
            <p className="audit-history-subtitle">
              Immutable audit ledger recording all creations, edits, soft-deletions, and restorations.
            </p>
          </div>
          <button
            onClick={onClose}
            className="audit-history-close-button form-close"
            aria-label="Close dialog"
          >
            <X size={18} />
          </button>
        </header>

        {/* Filter Controls Bar */}
        <div style={{ padding: '14px 28px', borderBottom: '1px solid #e7eeea', background: '#ffffff', display: 'flex', flexWrap: 'wrap', alignItems: 'center', justifyContent: 'space-between', gap: '12px' }}>
          {/* Scope Toggle if record selected */}
          {selectedRecord && (
            <div style={{ display: 'inline-flex', padding: '3px', background: '#f0f5f2', borderRadius: '8px', border: '1px solid #d5e0d8', gap: '4px' }}>
              <button
                type="button"
                onClick={() => setScope('RECORD')}
                style={{
                  padding: '5px 12px',
                  borderRadius: '6px',
                  fontSize: '12px',
                  fontWeight: 600,
                  cursor: 'pointer',
                  border: 0,
                  background: scope === 'RECORD' ? '#ffffff' : 'transparent',
                  color: scope === 'RECORD' ? '#173b28' : '#5f7568',
                  boxShadow: scope === 'RECORD' ? '0 1px 3px rgba(0,0,0,0.08)' : 'none'
                }}
              >
                This Record Only
              </button>
              <button
                type="button"
                onClick={() => setScope('ALL')}
                style={{
                  padding: '5px 12px',
                  borderRadius: '6px',
                  fontSize: '12px',
                  fontWeight: 600,
                  cursor: 'pointer',
                  border: 0,
                  background: scope === 'ALL' ? '#ffffff' : 'transparent',
                  color: scope === 'ALL' ? '#173b28' : '#5f7568',
                  boxShadow: scope === 'ALL' ? '0 1px 3px rgba(0,0,0,0.08)' : 'none'
                }}
              >
                All ERP Events
              </button>
            </div>
          )}

          {/* Action Filter Pills */}
          <div style={{ display: 'flex', alignItems: 'center', gap: '6px', flexWrap: 'wrap' }}>
            {(['ALL', 'ARCHIVE', 'RESTORE', 'PERMANENT_DELETE', 'CREATE', 'EDIT'] as const).map(
              (act) => {
                const isSelected = filterAction === act;
                return (
                  <button
                    key={act}
                    type="button"
                    onClick={() => setFilterAction(act)}
                    style={{
                      padding: '5px 11px',
                      borderRadius: '6px',
                      fontSize: '11.5px',
                      fontWeight: 600,
                      cursor: 'pointer',
                      transition: 'all 0.15s',
                      background: isSelected ? '#145f49' : '#ffffff',
                      color: isSelected ? '#ffffff' : '#5f7568',
                      border: isSelected ? '1px solid #145f49' : '1px solid #d5e0d8',
                      boxShadow: isSelected ? '0 1px 2px rgba(20,95,73,0.2)' : 'none'
                    }}
                  >
                    {act === 'ALL' ? 'All Actions' : act.replace('_', ' ')}
                  </button>
                );
              }
            )}
          </div>

          {/* Search bar */}
          <div style={{ position: 'relative', minWidth: '220px', flex: 1, maxWidth: '320px' }}>
            <Search size={14} style={{ color: '#7a8e82', position: 'absolute', left: '11px', top: '50%', transform: 'translateY(-50%)', pointerEvents: 'none' }} />
            <input
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              placeholder="Search audit trail..."
              style={{
                width: '100%',
                padding: '8px 12px 8px 32px',
                background: '#ffffff',
                border: '1px solid #d5e0d8',
                borderRadius: '8px',
                fontSize: '12.5px',
                color: '#1e3628',
                outline: 'none',
                boxSizing: 'border-box'
              }}
            />
          </div>
        </div>

        {/* Logs Table / List */}
        <div className="form-body" style={{ maxHeight: '55vh', overflowY: 'auto', padding: '20px 28px' }}>
          {relevantLogs.length === 0 ? (
            <div style={{ padding: '48px 0', textAlign: 'center', color: '#7a8e82', fontSize: '13px' }}>
              <History size={36} style={{ margin: '0 auto 12px', color: '#a4b8ad' }} />
              <p style={{ margin: 0, fontWeight: 500 }}>No audit log events found matching the selected filter criteria.</p>
            </div>
          ) : (
            <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
              {relevantLogs.map((log) => (
                <div
                  key={log.id}
                  style={{
                    padding: '16px 18px',
                    background: '#ffffff',
                    border: '1px solid #e2eae4',
                    borderRadius: '10px',
                    boxShadow: '0 1px 2px rgba(18,58,34,0.03)',
                    display: 'flex',
                    flexDirection: 'column',
                    gap: '10px'
                  }}
                >
                  <div style={{ display: 'flex', flexWrap: 'wrap', alignItems: 'center', justifyContent: 'space-between', gap: '8px' }}>
                    <div style={{ display: 'flex', alignItems: 'center', gap: '10px', flexWrap: 'wrap' }}>
                      {getActionBadge(log.action)}
                      <span style={{ fontWeight: 700, color: '#173b28', fontSize: '13.5px' }}>{log.recordLabel}</span>
                      <span style={{ fontSize: '11.5px', color: '#7a8e82', fontFamily: 'monospace' }}>
                        ({log.entityType} #{log.recordId})
                      </span>
                    </div>
                    <div style={{ display: 'flex', alignItems: 'center', gap: '14px', fontSize: '11.5px', color: '#66816f' }}>
                      <span style={{ display: 'flex', alignItems: 'center', gap: '4px' }}>
                        <User size={13} style={{ color: '#7a8e82' }} />
                        <strong style={{ color: '#274233' }}>{log.performedBy}</strong>
                      </span>
                      <span style={{ display: 'flex', alignItems: 'center', gap: '4px', fontFamily: 'monospace' }}>
                        <Calendar size={13} style={{ color: '#7a8e82' }} />
                        {formatLogDate(log.timestamp)}
                      </span>
                    </div>
                  </div>

                  {log.reason && (
                    <div style={{ padding: '8px 12px', background: '#f5f8f6', borderRadius: '6px', border: '1px solid #e5ede7', fontSize: '12px', color: '#3f604d' }}>
                      <strong style={{ color: '#173b28' }}>Reason:</strong> {log.reason}
                    </div>
                  )}

                  {log.details && (
                    <div style={{ fontSize: '12px', color: '#5f7568', lineHeight: 1.5 }}>
                      {log.details}
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>

        {/* Footer */}
        <div className="audit-modal-footer form-actions" style={{ justifyContent: 'space-between' }}>
          <span style={{ fontSize: '12.5px', color: '#5f7568' }}>
            Total: <strong style={{ color: '#173b28' }}>{relevantLogs.length}</strong> logged event(s)
          </span>
          <button
            type="button"
            onClick={onClose}
            className="audit-close-button secondary-button"
          >
            Close
          </button>
        </div>
      </div>
    </ModalFrame>
  );
};



