import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Archive, X, ShieldAlert, AlertCircle } from 'lucide-react';
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
    <ModalFrame onClose={onClose} title={isBlocked ? `Cannot Archive ${entityName}` : `Archive ${entityName}`}>
      <div className="archive-confirm-modal-shell form-shell form-shell-compact">
        {/* Header */}
        <header className="archive-confirm-modal-header form-header">
          <div className={`archive-confirm-icon-badge form-heading-icon ${isBlocked ? 'danger' : 'warning'}`}>
            {isBlocked ? <ShieldAlert size={22}/> : <Archive size={22}/>}
          </div>
          <div className="archive-confirm-title-group">
            <span className="archive-confirm-eyebrow form-eyebrow">ARCHIVE SAFEGUARD</span>
            <h2 className="archive-confirm-title">
              {isBlocked ? `Cannot Archive ${entityName}` : `Archive ${entityName}`}
            </h2>
            <p className="archive-confirm-subtitle">
              Soft-delete and audit preservation policy
            </p>
          </div>
          <button
            onClick={onClose}
            className="archive-confirm-close-button form-close"
            aria-label="Close dialog"
          >
            <X size={18}/>
          </button>
        </header>

        {/* Content */}
        <form onSubmit={handleSubmit} className="archive-confirm-form">
          <div className="archive-confirm-body form-body">
            {/* Main Notice */}
            <div className="form-note">
              Archive <strong style={{ color: '#173b28' }}>{recordTitle}</strong>? This will hide it from active lists but preserve all financial ledgers, linked audit trails, and history.
            </div>

            {/* Guard Rail Warning if Blocked */}
            {isBlocked ? (
              <div className="form-error" style={{ display: 'block' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '8px', fontWeight: 700 }}>
                  <AlertCircle size={16}/>
                  <span>Active Dependency Guard Rail</span>
                </div>
                <p style={{ marginTop: '6px', fontSize: '12px', lineHeight: 1.5, color: '#991b1b' }}>
                  {guardRailResult.reason}
                </p>
                <div style={{ marginTop: '8px', paddingTop: '8px', borderTop: '1px solid #fecaca', fontSize: '11.5px', color: '#7f1d1d' }}>
                  To maintain database integrity, you must resolve or reassign these linked records before archiving.
                </div>
              </div>
            ) : (
              <label className="form-field-label">
                <span>Reason for Archiving <span className="req">*</span></span>
                <input
                  type="text"
                  value={reason}
                  onChange={(e) => setReason(e.target.value)}
                  placeholder="e.g. Project completed, resigned, inactive contract..."
                  className="archive-reason-input"
                  autoFocus
                />
              </label>
            )}
          </div>

          {/* Footer Actions */}
          <div className="archive-actions-bar form-actions">
            <button
              type="button"
              onClick={onClose}
              className="archive-cancel-button secondary-button"
            >
              {isBlocked ? 'Understood & Close' : 'Cancel'}
            </button>

            {!isBlocked && (
              <button
                type="submit"
                className="archive-confirm-submit-button warning-button"
              >
                <Archive size={15}/>
                <span>Confirm Archive</span>
              </button>
            )}
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};


