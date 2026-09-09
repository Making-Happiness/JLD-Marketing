import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Trash2, AlertTriangle, ShieldCheck, ShieldAlert, X } from 'lucide-react';
import { GuardRailResult } from '../../types';

interface PermanentDeleteModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  entityName: string;
  recordTitle: string;
  integrityResult: GuardRailResult;
}

export const PermanentDeleteModal: React.FC<PermanentDeleteModalProps> = ({
  isOpen,
  onClose,
  onConfirm,
  entityName,
  recordTitle,
  integrityResult
}) => {
  const [typedConfirmation, setTypedConfirmation] = useState('');

  useEffect(() => {
    if (isOpen) {
      setTypedConfirmation('');
    }
  }, [isOpen]);

  if (!isOpen) return null;

  const isBlocked = !integrityResult.allowed;
  const isMatch = typedConfirmation === 'DELETE-CONFIRM';

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (isBlocked || !isMatch) return;
    onConfirm();
  };

  return (
    <ModalFrame onClose={onClose} title='PermanentDelete'>
      <div className="bg-white rounded-2xl shadow-2xl border border-slate-200 max-w-lg w-full overflow-hidden animate-in zoom-in-95 duration-150">
        {/* Header */}
        <div className="px-6 py-5 border-b border-slate-100 flex items-center justify-between bg-slate-50/80">
          <div className="flex items-center gap-3">
            <div className={`w-10 h-10 rounded-xl flex items-center justify-center border shadow-2xs ${
              isBlocked 
                ? 'bg-amber-50 text-amber-800 border-amber-200' 
                : 'bg-rose-50 text-rose-700 border-rose-200'
            }`}>
              {isBlocked ? <ShieldAlert className="w-5 h-5" /> : <Trash2 className="w-5 h-5" />}
            </div>
            <div>
              <div className="flex items-center gap-2">
                <h3 className="text-sm font-bold text-slate-900">
                  {isBlocked ? 'Permanent Delete Blocked' : `Permanently Delete ${entityName}`}
                </h3>
                <span className="px-2 py-0.5 rounded-full text-[10px] font-bold bg-rose-100 text-rose-800 border border-rose-200">
                  Super-Admin
                </span>
              </div>
              <p className="text-xs text-slate-500 mt-0.5">
                Physical database removal safeguard
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

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="p-6 space-y-4 text-xs">
          {/* Target details */}
          <div className="p-3.5 bg-slate-50 rounded-xl border border-slate-200 text-slate-700 leading-relaxed">
            Record: <strong className="text-slate-950 font-bold">{recordTitle}</strong>
          </div>

          {isBlocked ? (
            <div className="p-4 bg-amber-50/90 border border-amber-200 rounded-xl space-y-2">
              <div className="flex items-start gap-2.5">
                <AlertTriangle className="w-4 h-4 text-amber-700 shrink-0 mt-0.5" />
                <div>
                  <h4 className="font-bold text-amber-950 text-xs">Financial &amp; Ledger Integrity Protection</h4>
                  <p className="text-amber-800 text-[11px] mt-1 leading-relaxed">
                    {integrityResult.reason}
                  </p>
                </div>
              </div>
              <div className="pt-2 border-t border-amber-200/70 text-[11px] text-amber-900 flex items-center gap-1.5 font-medium">
                <ShieldCheck className="w-3.5 h-3.5 text-emerald-700 shrink-0" />
                <span>ERP accounting standards mandate keeping this historical record safely archived.</span>
              </div>
            </div>
          ) : (
            <div className="space-y-3">
              <div className="p-3 bg-rose-50 border border-rose-200 rounded-xl text-rose-800 text-[11px] leading-relaxed flex items-start gap-2">
                <AlertTriangle className="w-4 h-4 text-rose-600 shrink-0 mt-0.5" />
                <span>
                  <strong>Warning:</strong> This will physically remove the record from the database. This action is irreversible. All associated logs will record this permanent deletion.
                </span>
              </div>

              <div>
                <label className="block font-semibold text-slate-700 mb-1.5">
                  Type <span className="font-mono font-bold text-rose-600 bg-rose-50 px-1.5 py-0.5 rounded border border-rose-200">DELETE-CONFIRM</span> to permanently delete:
                </label>
                <input
                  type="text"
                  value={typedConfirmation}
                  onChange={(e) => setTypedConfirmation(e.target.value)}
                  placeholder="DELETE-CONFIRM"
                  className="w-full px-3.5 py-2.5 bg-slate-50 border border-slate-200 rounded-xl font-mono text-xs text-slate-900 focus:bg-white focus:outline-none focus:ring-2 focus:ring-rose-500/20 focus:border-rose-500 transition-all"
                  autoFocus
                />
              </div>
            </div>
          )}

          {/* Actions */}
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
                disabled={!isMatch}
                className="inline-flex items-center gap-1.5 px-4.5 py-2 bg-rose-600 hover:bg-rose-700 disabled:bg-slate-200 disabled:text-slate-400 disabled:cursor-not-allowed text-white font-semibold rounded-xl shadow-xs transition-colors cursor-pointer"
              >
                <Trash2 className="w-4 h-4" />
                <span>Permanently Delete</span>
              </button>
            )}
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};


