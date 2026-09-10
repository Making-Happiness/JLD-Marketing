import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Trash2, ShieldAlert, X, AlertTriangle, ShieldCheck } from 'lucide-react';
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
    <ModalFrame onClose={onClose} title={isBlocked ? 'Permanent Delete Blocked' : `Permanently Delete ${entityName}`}>
      <div className="delete-modal-shell form-shell form-shell-compact">
        {/* Header */}
        <header className="delete-modal-header form-header">
          <div className={`delete-modal-icon-badge form-heading-icon ${isBlocked ? 'warning' : 'danger'}`}>
            {isBlocked ? <ShieldAlert size={22} /> : <Trash2 size={22} />}
          </div>
          <div className="delete-modal-title-group">
            <span className="delete-modal-eyebrow form-eyebrow">
              {isBlocked ? 'INTEGRITY SAFEGUARD' : 'SECURITY SAFEGUARD'}
            </span>
            <h2 className="delete-modal-title">
              {isBlocked ? 'Permanent Delete Blocked' : `Permanently Delete ${entityName}`}
            </h2>
            <p className="delete-modal-subtitle">
              Physical database removal safeguard · Super-Admin
            </p>
          </div>
          <button
            onClick={onClose}
            className="delete-modal-close-button form-close"
            aria-label="Close dialog"
          >
            <X size={18} />
          </button>
        </header>

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="delete-modal-form">
          <div className="delete-modal-body form-body">
            {/* Target details */}
            <div className="form-note">
              Record: <strong style={{ color: '#173b28' }}>{recordTitle}</strong>
            </div>

            {isBlocked ? (
              <div className="form-error" style={{ display: 'block' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '8px', fontWeight: 700 }}>
                  <AlertTriangle size={16} />
                  <span>Financial &amp; Ledger Integrity Protection</span>
                </div>
                <p style={{ marginTop: '6px', fontSize: '12px', lineHeight: 1.5, color: '#991b1b' }}>
                  {integrityResult.reason}
                </p>
                <div style={{ marginTop: '8px', paddingTop: '8px', borderTop: '1px solid #fecaca', fontSize: '11.5px', color: '#7f1d1d', display: 'flex', alignItems: 'center', gap: '6px' }}>
                  <ShieldCheck size={14} style={{ color: '#15803d', flexShrink: 0 }} />
                  <span>ERP accounting standards mandate keeping this historical record safely archived.</span>
                </div>
              </div>
            ) : (
              <>
                <div className="form-error" style={{ display: 'block' }}>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '8px', fontWeight: 700 }}>
                    <AlertTriangle size={16} />
                    <span>Permanent Deletion Warning</span>
                  </div>
                  <p style={{ marginTop: '6px', fontSize: '12px', lineHeight: 1.5, color: '#991b1b' }}>
                    This will physically remove the record from the database. This action is irreversible. All associated audit logs will record this permanent deletion.
                  </p>
                </div>

                <label className="form-field-label">
                  <span>
                    Type <code style={{ fontWeight: 700, color: '#dc2626', background: '#fee2e2', padding: '2px 6px', borderRadius: '4px', border: '1px solid #fecaca', fontFamily: 'monospace' }}>DELETE-CONFIRM</code> to permanently delete: <span className="req">*</span>
                  </span>
                  <input
                    type="text"
                    value={typedConfirmation}
                    onChange={(e) => setTypedConfirmation(e.target.value)}
                    placeholder="DELETE-CONFIRM"
                    className="delete-challenge-input"
                    autoFocus
                    style={{ fontFamily: 'monospace', letterSpacing: '0.05em' }}
                  />
                </label>
              </>
            )}
          </div>

          {/* Actions */}
          <div className="delete-actions-bar form-actions">
            <button
              type="button"
              onClick={onClose}
              className="delete-cancel-button secondary-button"
            >
              {isBlocked ? 'Understood & Close' : 'Cancel'}
            </button>

            {!isBlocked && (
              <button
                type="submit"
                disabled={!isMatch}
                className="delete-submit-button danger-button"
              >
                <Trash2 size={15} />
                <span>Permanently Delete</span>
              </button>
            )}
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};


