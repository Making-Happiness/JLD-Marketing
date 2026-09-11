import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Client } from '../../types';
import { X, User, CheckCircle2 } from 'lucide-react';

interface StakeholderModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (client: Client | Omit<Client, 'idclients'>) => void;
  clientToEdit?: Client | null;
}

export const StakeholderModal: React.FC<StakeholderModalProps> = ({
  isOpen,
  onClose,
  onSave,
  clientToEdit
}) => {
  const isEditing = Boolean(clientToEdit);

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [middleName, setMiddleName] = useState('');
  const [gender, setGender] = useState('Male');
  const [dob, setDob] = useState('');
  const [placeOfBirth, setPlaceOfBirth] = useState('');
  const [spouseName, setSpouseName] = useState('');
  const [contactNo, setContactNo] = useState('');
  const [email, setEmail] = useState('');
  const [errorMsg, setErrorMsg] = useState('');
  const [successMsg, setSuccessMsg] = useState('');

  useEffect(() => {
    if (isOpen) {
      if (clientToEdit) {
        setFirstName(clientToEdit.firstname || '');
        setLastName(clientToEdit.lastname || '');
        setMiddleName(clientToEdit.middlename || '');
        setGender(clientToEdit.gender || 'Male');
        setDob(clientToEdit.dateofbirth || '');
        setPlaceOfBirth(clientToEdit.placeofbirth || '');
        setSpouseName(clientToEdit.spousename || '');
        setContactNo(clientToEdit.contactno || '');
        setEmail(clientToEdit.email || '');
      } else {
        setFirstName('');
        setLastName('');
        setMiddleName('');
        setGender('Male');
        setDob('');
        setPlaceOfBirth('');
        setSpouseName('');
        setContactNo('');
        setEmail('');
      }
      setErrorMsg('');
      setSuccessMsg('');
    }
  }, [isOpen, clientToEdit]);

  if (!isOpen) return null;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!firstName.trim()) {
      setErrorMsg('First Name is required.');
      return;
    }
    if (!lastName.trim()) {
      setErrorMsg('Last Name is required.');
      return;
    }
    if (!contactNo.trim()) {
      setErrorMsg('Contact No. is required.');
      return;
    }

    const payload = {
      ...(clientToEdit ? { idclients: clientToEdit.idclients } : {}),
      firstname: firstName.trim(),
      lastname: lastName.trim(),
      middlename: middleName.trim(),
      gender,
      dateofbirth: dob.trim(),
      placeofbirth: placeOfBirth.trim() || 'South Cotabato',
      spousename: spouseName.trim() || 'N/A',
      contactno: contactNo.trim(),
      fullname: `${lastName.trim()}, ${firstName.trim()}${middleName.trim() ? ' ' + middleName.trim() : ''}`,
      avatarUrl: clientToEdit?.avatarUrl
    };

    setSuccessMsg(isEditing ? 'Stakeholder information updated!' : 'New stakeholder recorded!');
    setTimeout(() => {
      onSave(payload as Client);
      setSuccessMsg('');
      onClose();
    }, 300);
  };

  return (
    <ModalFrame onClose={onClose} title={isEditing ? 'Edit Buyer' : 'Register Buyer'}>
      <div className="stakeholder-modal-shell form-shell">
        {/* Header */}
        <header className="stakeholder-modal-header form-header">
          <div className="stakeholder-modal-icon-wrapper form-heading-icon">
            <User size={22}/>
          </div>
          <div className="stakeholder-modal-title-group">
            <span className="stakeholder-modal-eyebrow form-eyebrow">STAKEHOLDER DIRECTORY</span>
            <h2 className="stakeholder-modal-title">
              {isEditing ? 'Edit Buyer Profile' : 'Register New Buyer'}
            </h2>
            <p className="stakeholder-modal-subtitle">
              {isEditing 
                ? 'Update buyer personal information and contact details.' 
                : 'Register a new buyer / stakeholder in the subdivision records.'}
            </p>
          </div>
          <button
            onClick={onClose}
            aria-label="Close form"
            className="stakeholder-close-btn form-close"
          >
            <X size={18}/>
          </button>
        </header>

        {/* Form */}
        <form onSubmit={handleSubmit} className="stakeholder-form">
          <div className="stakeholder-form-body form-body">
            {errorMsg && (
              <div className="stakeholder-form-error-alert form-error">
                {errorMsg}
              </div>
            )}

            {successMsg && (
              <div className="stakeholder-form-success-alert form-success">
                <CheckCircle2 size={16}/>
                <span>{successMsg}</span>
              </div>
            )}

            <fieldset className="stakeholder-fieldset">
              <legend className="stakeholder-legend">Personal Information</legend>
              <div className="form-grid-3">
                <label className="form-field-label">
                  <span>First Name <span className="req">*</span></span>
                  <input
                    type="text"
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                    placeholder="e.g. Juan"
                    className="stakeholder-form-input"
                    required
                  />
                </label>

                <label className="form-field-label">
                  <span>Last Name <span className="req">*</span></span>
                  <input
                    type="text"
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                    placeholder="e.g. Dela Cruz"
                    className="stakeholder-form-input"
                    required
                  />
                </label>

                <label className="form-field-label">
                  <span>Middle Name</span>
                  <input
                    type="text"
                    value={middleName}
                    onChange={(e) => setMiddleName(e.target.value)}
                    placeholder="e.g. Santos"
                    className="stakeholder-form-input"
                  />
                </label>
              </div>

              <div className="form-grid-3" style={{ marginTop: '14px' }}>
                <label className="form-field-label">
                  <span>Gender</span>
                  <select
                    value={gender}
                    onChange={(e) => setGender(e.target.value)}
                    className="stakeholder-form-select"
                  >
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                  </select>
                </label>

                <label className="form-field-label">
                  <span>Date of Birth (DOB)</span>
                  <input
                    type="date"
                    value={dob}
                    onChange={(e) => setDob(e.target.value)}
                    className="stakeholder-form-input"
                  />
                </label>

                                  <label className="form-field-label">
                    <span>Contact Number <span className="req">*</span></span>
                    <input
                      type="text"
                      value={contactNo}
                      onChange={e => setContactNo(e.target.value)}
                      placeholder="e.g. 0917-123-4567"
                      required
                    />
                  </label>
                  
                  <label className="form-field-label">
                    <span>Email Address</span>
                    <input
                      type="email"
                      value={email}
                      onChange={e => setEmail(e.target.value)}
                      placeholder="e.g. buyer@example.com"
                    />
                  </label>
              </div>
            </fieldset>

            <fieldset className="stakeholder-fieldset">
              <legend className="stakeholder-legend">Civil &amp; Background Information</legend>
              <div className="form-grid">
                <label className="form-field-label">
                  <span>Place of Birth</span>
                  <input
                    type="text"
                    value={placeOfBirth}
                    onChange={(e) => setPlaceOfBirth(e.target.value)}
                    placeholder="e.g. Koronadal City, South Cotabato"
                    className="stakeholder-form-input"
                  />
                </label>

                <label className="form-field-label">
                  <span>Spouse Name</span>
                  <input
                    type="text"
                    value={spouseName}
                    onChange={(e) => setSpouseName(e.target.value)}
                    placeholder="e.g. Maria Santos Dela Cruz (or Single)"
                    className="stakeholder-form-input"
                  />
                </label>
              </div>
            </fieldset>
          </div>

          {/* Action Buttons */}
          <div className="stakeholder-modal-footer form-actions">
            <button
              type="button"
              onClick={onClose}
              className="stakeholder-cancel-btn secondary-button"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="stakeholder-submit-btn primary-button"
            >
              {isEditing ? 'Save changes' : 'Save buyer'}
            </button>
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};




