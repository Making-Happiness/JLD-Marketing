import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { Client } from '../../types';
import { X, User, CheckCircle2, Calendar, Phone, MapPin, Heart } from 'lucide-react';

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
      } else {
        setFirstName('');
        setLastName('');
        setMiddleName('');
        setGender('Male');
        setDob('');
        setPlaceOfBirth('');
        setSpouseName('');
        setContactNo('');
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
    <ModalFrame onClose={onClose} title='Stakeholder form'>
      <div className="stakeholder-modal-card bg-white rounded-2xl shadow-2xl border border-slate-100 w-full max-w-2xl overflow-hidden flex flex-col max-h-[90vh]">
        {/* Header */}
        <div className="stakeholder-modal-header px-6 py-5 border-b border-slate-100 flex items-center justify-between bg-slate-50/50">
          <div className="stakeholder-header-details flex items-center gap-3">
            <div className="stakeholder-header-icon-badge w-10 h-10 rounded-xl bg-emerald-50 border border-emerald-200/80 flex items-center justify-center text-emerald-800 shadow-2xs">
              <User className="w-5 h-5" />
            </div>
            <div className="stakeholder-title-group">
              <h2 className="stakeholder-modal-title text-base font-bold text-slate-900">
                {isEditing ? `Edit buyer` : 'Add buyer'}
              </h2>
              <p className="stakeholder-modal-subtitle text-xs text-slate-500">
                {isEditing 
                  ? 'Update buyer personal information and contact details.' 
                  : 'Register a new buyer / stakeholder in the subdivision records.'}
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="stakeholder-close-btn p-1.5 text-slate-400 hover:text-slate-600 rounded-lg hover:bg-white transition-colors cursor-pointer"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Form */}
        <form onSubmit={handleSubmit} className="stakeholder-form-body p-6 overflow-y-auto space-y-4">
          {errorMsg && (
            <div className="stakeholder-form-error-alert p-3 bg-rose-50 border border-rose-200 rounded-xl text-xs text-rose-700 font-medium">
              {errorMsg}
            </div>
          )}

          {successMsg && (
            <div className="stakeholder-form-success-alert p-3 bg-emerald-50 border border-emerald-200 rounded-xl text-xs text-emerald-800 font-semibold flex items-center gap-2">
              <CheckCircle2 className="w-4 h-4 text-emerald-600" />
              <span>{successMsg}</span>
            </div>
          )}

          <div className="stakeholder-form-row grid grid-cols-1 sm:grid-cols-3 gap-4">
            {/* First Name */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5">First Name *</label>
              <input
                type="text"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                placeholder="e.g. Juan"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Last Name */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5">Last Name *</label>
              <input
                type="text"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                placeholder="e.g. Dela Cruz"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Middle Name */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5">Middle Name</label>
              <input
                type="text"
                value={middleName}
                onChange={(e) => setMiddleName(e.target.value)}
                placeholder="e.g. Santos"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>
          </div>

          <div className="stakeholder-form-row grid grid-cols-1 sm:grid-cols-3 gap-4">
            {/* Gender */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5">Gender</label>
              <select
                value={gender}
                onChange={(e) => setGender(e.target.value)}
                className="stakeholder-form-select w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              >
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>

            {/* Date of Birth */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Calendar className="w-3.5 h-3.5 text-slate-400" />
                <span>Date of Birth (DOB)</span>
              </label>
              <input
                type="date"
                value={dob}
                onChange={(e) => setDob(e.target.value)}
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Contact No. */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Phone className="w-3.5 h-3.5 text-slate-400" />
                <span>Contact No. *</span>
              </label>
              <input
                type="text"
                value={contactNo}
                onChange={(e) => setContactNo(e.target.value)}
                placeholder="e.g. 0917-123-4567"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>
          </div>

          <div className="stakeholder-form-row grid grid-cols-1 sm:grid-cols-2 gap-4">
            {/* Place of Birth */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <MapPin className="w-3.5 h-3.5 text-slate-400" />
                <span>Place of Birth</span>
              </label>
              <input
                type="text"
                value={placeOfBirth}
                onChange={(e) => setPlaceOfBirth(e.target.value)}
                placeholder="e.g. Koronadal City, South Cotabato"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>

            {/* Spouse Name */}
            <div className="stakeholder-form-field-group">
              <label className="stakeholder-form-label block text-xs font-semibold text-slate-700 mb-1.5 flex items-center gap-1.5">
                <Heart className="w-3.5 h-3.5 text-slate-400" />
                <span>Spouse Name</span>
              </label>
              <input
                type="text"
                value={spouseName}
                onChange={(e) => setSpouseName(e.target.value)}
                placeholder="e.g. Maria Santos Dela Cruz (or Single)"
                className="stakeholder-form-input w-full px-3.5 py-2.5 bg-slate-50/50 border border-slate-200 rounded-xl text-xs font-medium text-slate-900 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
              />
            </div>
          </div>

          {/* Action Buttons */}
          <div className="stakeholder-modal-footer pt-4 flex items-center justify-end gap-2.5 border-t border-slate-100">
            <button
              type="button"
              onClick={onClose}
              className="stakeholder-cancel-btn px-4 py-2.5 text-xs font-semibold text-slate-600 hover:text-slate-800 hover:bg-slate-100 rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="stakeholder-submit-btn px-5 py-2.5 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
            >
              {isEditing ? 'Save changes' : 'Save buyer'}
            </button>
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};




