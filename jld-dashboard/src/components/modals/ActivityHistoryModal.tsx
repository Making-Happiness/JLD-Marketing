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
  Calendar, 
  User, 
  Filter 
} from 'lucide-react';
import { AuditLogEntry, AuditAction, EntityTypeName } from '../../types';
import { formatDate } from '../../utils/calculations';

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
          <span className="inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-amber-50 text-amber-800 border border-amber-200">
            <Archive className="w-3 h-3 text-amber-600" />
            <span>ARCHIVED</span>
          </span>
        );
      case 'RESTORE':
        return (
          <span className="inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-emerald-50 text-emerald-800 border border-emerald-200">
            <ArchiveRestore className="w-3 h-3 text-emerald-600" />
            <span>RESTORED</span>
          </span>
        );
      case 'PERMANENT_DELETE':
        return (
          <span className="inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-rose-50 text-rose-800 border border-rose-200">
            <Trash2 className="w-3 h-3 text-rose-600" />
            <span>HARD DELETED</span>
          </span>
        );
      case 'CREATE':
        return (
          <span className="inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-blue-50 text-blue-800 border border-blue-200">
            <Plus className="w-3 h-3 text-blue-600" />
            <span>CREATED</span>
          </span>
        );
      case 'EDIT':
        return (
          <span className="inline-flex items-center gap-1 px-2.5 py-0.5 rounded-md text-[10px] font-bold bg-indigo-50 text-indigo-800 border border-indigo-200">
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
    <ModalFrame onClose={onClose} title='ActivityHistory'>
      <div className="bg-white rounded-2xl shadow-2xl border border-slate-200 max-w-3xl w-full flex flex-col max-h-[90vh] overflow-hidden animate-in zoom-in-95 duration-150">
        {/* Header */}
        <div className="px-6 py-4.5 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-xl bg-emerald-50 text-emerald-800 border border-emerald-200 flex items-center justify-center shadow-2xs">
              <History className="w-5 h-5" />
            </div>
            <div>
              <div className="flex items-center gap-2">
                <h3 className="text-sm font-bold text-slate-900">
                  ERP Audit Trail &amp; Activity History
                </h3>
                {selectedRecord && scope === 'RECORD' && (
                  <span className="px-2 py-0.5 rounded-full text-[10px] font-semibold bg-emerald-100 text-emerald-900 border border-emerald-200">
                    {selectedRecord.entityType}: {selectedRecord.recordLabel}
                  </span>
                )}
              </div>
              <p className="text-xs text-slate-500 mt-0.5">
                Immutable audit ledger recording all creations, edits, soft-deletions, and restorations.
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-1.5 text-slate-400 hover:text-slate-600 rounded-lg hover:bg-white transition-colors cursor-pointer"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Filter Controls Bar */}
        <div className="px-6 py-3 border-b border-slate-100 bg-white flex flex-wrap items-center justify-between gap-3 text-xs">
          {/* Scope Toggle if record selected */}
          {selectedRecord && (
            <div className="inline-flex p-0.5 bg-slate-100 rounded-lg border border-slate-200">
              <button
                type="button"
                onClick={() => setScope('RECORD')}
                className={`px-3 py-1 rounded-md font-semibold transition-all cursor-pointer ${
                  scope === 'RECORD'
                    ? 'bg-white text-slate-900 shadow-2xs'
                    : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                This Record Only
              </button>
              <button
                type="button"
                onClick={() => setScope('ALL')}
                className={`px-3 py-1 rounded-md font-semibold transition-all cursor-pointer ${
                  scope === 'ALL'
                    ? 'bg-white text-slate-900 shadow-2xs'
                    : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                All ERP Events
              </button>
            </div>
          )}

          {/* Action Filter Pills */}
          <div className="flex items-center gap-1 overflow-x-auto">
            {(['ALL', 'ARCHIVE', 'RESTORE', 'PERMANENT_DELETE', 'CREATE', 'EDIT'] as const).map(
              (act) => (
                <button
                  key={act}
                  type="button"
                  onClick={() => setFilterAction(act)}
                  className={`px-2.5 py-1 rounded-lg font-semibold text-[11px] transition-all cursor-pointer ${
                    filterAction === act
                      ? 'bg-[#00593B] text-white shadow-2xs'
                      : 'text-slate-600 hover:bg-slate-100'
                  }`}
                >
                  {act === 'ALL' ? 'All Actions' : act}
                </button>
              )
            )}
          </div>

          {/* Search bar */}
          <div className="relative min-w-[200px]">
            <Search className="w-3.5 h-3.5 text-slate-400 absolute left-2.5 top-1/2 -translate-y-1/2" />
            <input
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              placeholder="Search audit trail..."
              className="w-full pl-8 pr-3 py-1 bg-slate-50 border border-slate-200 rounded-lg text-xs text-slate-800 focus:bg-white focus:outline-none focus:ring-1 focus:ring-emerald-500"
            />
          </div>
        </div>

        {/* Logs Table / List */}
        <div className="flex-1 overflow-y-auto p-6 space-y-3">
          {relevantLogs.length === 0 ? (
            <div className="py-16 text-center text-slate-400 text-xs">
              <History className="w-8 h-8 mx-auto text-slate-300 mb-2" />
              <p>No audit log events found matching the selected filter criteria.</p>
            </div>
          ) : (
            <div className="space-y-3">
              {relevantLogs.map((log) => (
                <div
                  key={log.id}
                  className="p-4 bg-white border border-slate-200/80 rounded-xl hover:border-slate-300 transition-colors shadow-2xs text-xs space-y-2"
                >
                  <div className="flex flex-wrap items-center justify-between gap-2">
                    <div className="flex items-center gap-2">
                      {getActionBadge(log.action)}
                      <span className="font-bold text-slate-900">{log.recordLabel}</span>
                      <span className="text-[11px] text-slate-400 font-mono">
                        ({log.entityType} #{log.recordId})
                      </span>
                    </div>
                    <div className="flex items-center gap-3 text-slate-500 text-[11px]">
                      <span className="flex items-center gap-1">
                        <User className="w-3 h-3 text-slate-400" />
                        <strong>{log.performedBy}</strong>
                      </span>
                      <span className="flex items-center gap-1 font-mono">
                        <Calendar className="w-3 h-3 text-slate-400" />
                        {formatLogDate(log.timestamp)}
                      </span>
                    </div>
                  </div>

                  {log.reason && (
                    <div className="text-slate-700 bg-slate-50 p-2.5 rounded-lg border border-slate-100 text-[11px]">
                      <strong className="text-slate-900">Reason:</strong> {log.reason}
                    </div>
                  )}

                  {log.details && (
                    <div className="text-slate-500 text-[11px]">
                      {log.details}
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>

        {/* Footer */}
        <div className="px-6 py-3.5 border-t border-slate-100 bg-slate-50/70 flex items-center justify-between text-xs text-slate-500">
          <span>
            Total: <strong>{relevantLogs.length}</strong> logged event(s)
          </span>
          <button
            onClick={onClose}
            className="px-4 py-1.5 bg-white hover:bg-slate-100 text-slate-700 font-semibold border border-slate-200 rounded-lg cursor-pointer"
          >
            Close
          </button>
        </div>
      </div>
    </ModalFrame>
  );
};



