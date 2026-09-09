import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Archive, AlertCircle, X, ShieldAlert } from 'lucide-react';
import { GuardRailResult } from '../../types';

interface ArchiveConfirmModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: (reason: string) => void;
  entityName: string;
  recordTitle: string;
  guardRailResult?: GuardRailResult;
}

export const ArchiveConfirmModal: React.FC<ArchiveConfirmModalProps> = ({
  isOpen,
  onClose,
  onConfirm,
  entityName,
  recordTitle,
  guardRailResult = { allowed: true }
}) => {
  const [reason, setReason] = useState('');

  useEffect(() => {
    if (isOpen) {
      setReason('');
    }
  }, [isOpen]);

  if (!isOpen) return null;

  const isBlocked = !guardRailResult.allowed;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (isBlocked) return;
    onConfirm(reason.trim() || 'Archived by user');
  };

  return (
    <ModalFrame onClose={onClose} title='ArchiveConfirm'>
      <div className="bg-white rounded-2xl shadow-2xl border border-slate-200/80 max-w-lg w-full overflow-hidden animate-in zoom-in-95 duration-150">
        {/* Header */}
        <div className="px-6 py-5 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="flex items-center gap-3">
            <div className={`w-10 h-10 rounded-xl flex items-center justify-center border shadow-2xs ${
              isBlocked 
                ? 'bg-rose-50 text-rose-700 border-rose-200' 
                : 'bg-amber-50 text-amber-700 border-amber-200'
            }`}>
              {isBlocked ? <ShieldAlert className="w-5 h-5" /> : <Archive className="w-5 h-5" />}
            </div>
            <div>
              <h3 className="text-sm font-bold text-slate-900">
                {isBlocked ? `Cannot Archive ${entityName}` : `Archive ${entityName}`}
              </h3>
              <p className="text-xs text-slate-500 mt-0.5">
                Soft-delete &amp; preservation policy
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

        {/* Content */}
        <form onSubmit={handleSubmit} className="p-6 space-y-4 text-xs">
          {/* Main Notice */}
          <div className="p-3.5 bg-slate-50 rounded-xl border border-slate-200 text-slate-700 leading-relaxed">
            Archive <strong className="text-slate-950 font-bold">{recordTitle}</strong>? This will hide it from active lists but preserve all records, financial ledgers, and history.
          </div>

          {/* Guard Rail Warning if Blocked */}
          {isBlocked ? (
            <div className="p-4 bg-rose-50/80 border border-rose-200 rounded-xl space-y-2">
              <div className="flex items-start gap-2.5">
                <AlertCircle className="w-4 h-4 text-rose-600 shrink-0 mt-0.5" />
                <div>
                  <h4 className="font-bold text-rose-900 text-xs">Active Dependency Guard Rail</h4>
                  <p className="text-rose-700 text-[11px] mt-1 leading-relaxed">
                    {guardRailResult.reason}
                  </p>
                </div>
              </div>
              <div className="pt-2 border-t border-rose-200/60 text-[11px] text-rose-800">
                To maintain database integrity, you must resolve or reassign these linked records before archiving.
              </div>
            </div>
          ) : (
            <div>
              <label className="block font-semibold text-slate-700 mb-1.5">
                Reason for Archiving <span className="font-normal text-slate-400">(recorded in audit trail)</span>
              </label>
              <input
                type="text"
                value={reason}
                onChange={(e) => setReason(e.target.value)}
                placeholder="e.g. Project completed, resigned, inactive contract..."
                className="w-full px-3.5 py-2.5 bg-slate-50 border border-slate-200 rounded-xl text-xs text-slate-900 focus:bg-white focus:outline-none focus:ring-2 focus:ring-amber-500/20 focus:border-amber-500 transition-all"
              />
            </div>
          )}

          {/* Footer Actions */}
          <div className="flex items-center justify-end gap-2.5 pt-3 border-t border-slate-100">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 bg-slate-100 hover:bg-slate-200/80 text-slate-700 font-semibold rounded-xl transition-colors cursor-pointer"
            >
              {isBlocked ? 'Understood & Close' : 'Cancel'}
            </button>

            {!isBlocked && (
              <button
                type="submit"
                className="inline-flex items-center gap-1.5 px-4.5 py-2 bg-amber-600 hover:bg-amber-700 text-white font-semibold rounded-xl shadow-xs transition-colors cursor-pointer"
              >
                <Archive className="w-4 h-4" />
                <span>Confirm Archive</span>
              </button>
            )}
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};


