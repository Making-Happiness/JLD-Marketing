import React, { useState } from 'react';
import { Client, PurchaseDetail } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { 
  X, 
  Building2, 
  MapPin, 
  Calendar, 
  Phone, 
  User, 
  CreditCard, 
  Receipt, 
  Edit3, 
  Plus, 
  FileText,
  BadgeCheck,
  Clock,
  AlertCircle
} from 'lucide-react';

interface PurchaseDetailsModalProps {
  isOpen: boolean;
  onClose: () => void;
  client: Client | null;
  purchases: PurchaseDetail[];
  onEditPurchase: (purchase: PurchaseDetail) => void;
  onViewHistory: (purchase: PurchaseDetail) => void;
  onAddNewPurchase: (client: Client) => void;
}

export const PurchaseDetailsModal: React.FC<PurchaseDetailsModalProps> = ({
  isOpen,
  onClose,
  client,
  purchases,
  onEditPurchase,
  onViewHistory,
  onAddNewPurchase
}) => {
  const [selectedPurchaseIndex, setSelectedPurchaseIndex] = useState<number>(0);

  if (!isOpen || !client) return null;

  const activePurchase = purchases[selectedPurchaseIndex] || purchases[0] || null;
  const hasPurchases = purchases.length > 0;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/50 backdrop-blur-xs p-4 animate-in fade-in duration-200">
      <div className="bg-white rounded-2xl shadow-2xl border border-slate-100 w-full max-w-3xl overflow-hidden flex flex-col max-h-[92vh]">
        {/* Header */}
        <div className="px-6 py-5 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="flex items-center gap-3.5">
            <div className="w-11 h-11 rounded-xl bg-emerald-50 border border-emerald-200/80 flex items-center justify-center text-emerald-800 shadow-2xs">
              <FileText className="w-5 h-5" />
            </div>
            <div>
              <div className="flex items-center gap-2">
                <h2 className="text-base font-bold text-slate-900">Purchase Details Form</h2>
                <span className="px-2.5 py-0.5 rounded-full text-[11px] font-semibold bg-emerald-100/70 text-emerald-900 border border-emerald-200">
                  ID #{client.idclients}
                </span>
              </div>
              <p className="text-xs text-slate-500 mt-0.5">
                Subdivision lot acquisition records and financing profile for <span className="font-semibold text-slate-700">{client.firstname} {client.lastname}</span>
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

        {/* Content Body */}
        <div className="p-6 overflow-y-auto space-y-5">
          {/* Stakeholder Identity Banner */}
          <div className="p-4 bg-slate-50/70 rounded-xl border border-slate-200/70 grid grid-cols-1 sm:grid-cols-3 gap-3 text-xs">
            <div>
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider">Stakeholder Name</span>
              <span className="font-bold text-slate-900 text-sm">{client.fullname || `${client.lastname}, ${client.firstname}`}</span>
            </div>
            <div>
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider">Contact Number</span>
              <span className="font-medium text-slate-700">{client.contactno || 'N/A'}</span>
            </div>
            <div>
              <span className="text-[10px] uppercase font-bold text-slate-400 block tracking-wider">Spouse / Civil Status</span>
              <span className="font-medium text-slate-700">{client.spousename || 'Single'}</span>
            </div>
          </div>

          {/* Multiple Lots Tabs if Client owns more than 1 lot */}
          {hasPurchases && purchases.length > 1 && (
            <div className="flex items-center gap-2 overflow-x-auto pb-1">
              <span className="text-xs font-semibold text-slate-500 shrink-0 mr-1">Select Lot:</span>
              {purchases.map((p, idx) => (
                <button
                  key={p.id}
                  onClick={() => setSelectedPurchaseIndex(idx)}
                  className={`px-3 py-1.5 rounded-xl text-xs font-semibold transition-all cursor-pointer shrink-0 ${
                    selectedPurchaseIndex === idx
                      ? 'bg-[#00593B] text-white shadow-xs'
                      : 'bg-white hover:bg-slate-100 text-slate-700 border border-slate-200'
                  }`}
                >
                  Blk {p.blockno} Lot {p.lotno} ({p.productCode})
                </button>
              ))}
            </div>
          )}

          {/* Purchase Details Fill-Up Form Format */}
          {hasPurchases && activePurchase ? (
            <div className="space-y-4">
              {/* Section 1: Property Location & Allocation */}
              <div className="bg-white p-4 rounded-xl border border-slate-200/90 shadow-2xs space-y-3">
                <div className="flex items-center justify-between border-b border-slate-100 pb-2">
                  <div className="flex items-center gap-2">
                    <Building2 className="w-4 h-4 text-emerald-700" />
                    <h4 className="text-xs font-bold text-slate-900 uppercase tracking-wide">
                      1. Subdivision Location &amp; Lot Specifications
                    </h4>
                  </div>
                  <span className="px-2 py-0.5 bg-emerald-50 text-emerald-800 text-[10px] font-bold rounded-md border border-emerald-200">
                    {activePurchase.productCode}
                  </span>
                </div>

                <div className="grid grid-cols-2 sm:grid-cols-4 gap-3 text-xs">
                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Project / Phase</label>
                    <div className="flex items-center gap-1 font-semibold text-slate-800">
                      <MapPin className="w-3.5 h-3.5 text-emerald-600 shrink-0" />
                      <span>{activePurchase.location}</span>
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Block Number</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg font-mono font-bold text-slate-900 border border-slate-200/60">
                      Block {activePurchase.blockno}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Lot Number</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg font-mono font-bold text-slate-900 border border-slate-200/60">
                      Lot {activePurchase.lotno}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Total Lot Area</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg font-semibold text-slate-900 border border-slate-200/60">
                      {activePurchase.area.toLocaleString()} sq.m.
                    </div>
                  </div>
                </div>
              </div>

              {/* Section 2: Pricing, Financing & Payment Schedule */}
              <div className="bg-white p-4 rounded-xl border border-slate-200/90 shadow-2xs space-y-3">
                <div className="flex items-center justify-between border-b border-slate-100 pb-2">
                  <div className="flex items-center gap-2">
                    <CreditCard className="w-4 h-4 text-emerald-700" />
                    <h4 className="text-xs font-bold text-slate-900 uppercase tracking-wide">
                      2. Pricing, Financing &amp; Amortization Terms
                    </h4>
                  </div>
                  <span className="text-xs font-bold text-emerald-950">
                    {activePurchase.terms === 0 ? 'SPOT CASH' : `${activePurchase.terms} YEARS TERM`}
                  </span>
                </div>

                <div className="grid grid-cols-2 sm:grid-cols-4 gap-3 text-xs">
                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Total Lot Price</label>
                    <div className="px-3 py-2 bg-slate-50 rounded-lg font-bold text-slate-900 border border-slate-200/60 text-sm">
                      {formatCurrency(activePurchase.lotprice)}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Downpayment Paid</label>
                    <div className="px-3 py-2 bg-emerald-50/70 rounded-lg font-bold text-emerald-900 border border-emerald-200/70 text-sm">
                      {formatCurrency(activePurchase.downpayment || 0)}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Monthly Amortization</label>
                    <div className="px-3 py-2 bg-emerald-900 text-white rounded-lg font-bold border border-emerald-950 text-sm">
                      {activePurchase.terms === 0 ? '₱0.00 (Cash)' : formatCurrency(activePurchase.amortization)}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Financing Terms</label>
                    <div className="px-3 py-2 bg-slate-50 rounded-lg font-semibold text-slate-800 border border-slate-200/60">
                      {activePurchase.terms === 0 ? '0 (Cash Sale)' : `${activePurchase.terms} Year${activePurchase.terms > 1 ? 's' : ''} (15% p.a.)`}
                    </div>
                  </div>
                </div>

                <div className="grid grid-cols-3 gap-3 text-xs pt-1">
                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Processing / Docs Fee</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg text-slate-700 font-medium border border-slate-200/60">
                      {formatCurrency(activePurchase.otherfees || 0)}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Accumulated Penalty</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg text-slate-700 font-medium border border-slate-200/60">
                      {formatCurrency(activePurchase.penalty || 0)}
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Next Payment Due Date</label>
                    <div className="px-3 py-1.5 bg-slate-50 rounded-lg text-slate-800 font-mono font-semibold border border-slate-200/60 flex items-center gap-1.5">
                      <Clock className="w-3.5 h-3.5 text-slate-400" />
                      <span>{activePurchase.duedate || 'N/A'}</span>
                    </div>
                  </div>
                </div>
              </div>

              {/* Section 3: Sales Representation & Notes */}
              <div className="bg-white p-4 rounded-xl border border-slate-200/90 shadow-2xs space-y-3">
                <div className="flex items-center justify-between border-b border-slate-100 pb-2">
                  <div className="flex items-center gap-2">
                    <User className="w-4 h-4 text-emerald-700" />
                    <h4 className="text-xs font-bold text-slate-900 uppercase tracking-wide">
                      3. Sales Representation &amp; Documentation Remarks
                    </h4>
                  </div>
                </div>

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 text-xs">
                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Assigned Agent / Dicer</label>
                    <div className="px-3 py-2 bg-slate-50 rounded-lg font-semibold text-slate-800 border border-slate-200/60 flex items-center justify-between">
                      <span>{activePurchase.agentName}</span>
                      <span className="text-[10px] font-bold text-emerald-800 bg-emerald-50 px-2 py-0.5 rounded border border-emerald-200">
                        {activePurchase.agentpercentage}% Comm ({formatCurrency((activePurchase.lotprice * activePurchase.agentpercentage) / 100)})
                      </span>
                    </div>
                  </div>

                  <div>
                    <label className="text-[10px] font-semibold text-slate-400 block mb-1">Official Remarks / Application Status</label>
                    <div className="px-3 py-2 bg-slate-50 rounded-lg text-slate-700 font-medium border border-slate-200/60">
                      {activePurchase.remarks || 'Standard lot purchase terms verified.'}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          ) : (
            /* Empty State: No purchases for this client */
            <div className="p-8 text-center bg-slate-50 rounded-2xl border border-dashed border-slate-200 space-y-3">
              <div className="w-12 h-12 rounded-full bg-slate-200/70 flex items-center justify-center mx-auto text-slate-500">
                <AlertCircle className="w-6 h-6" />
              </div>
              <div>
                <h4 className="text-sm font-bold text-slate-800">No Purchase Details Recorded</h4>
                <p className="text-xs text-slate-500 mt-1 max-w-sm mx-auto">
                  There are currently no subdivision lot applications or active purchases registered for <span className="font-semibold text-slate-700">{client.firstname} {client.lastname}</span>.
                </p>
              </div>
              <button
                onClick={() => {
                  onClose();
                  onAddNewPurchase(client);
                }}
                className="mt-2 inline-flex items-center gap-1.5 px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-colors cursor-pointer"
              >
                <Plus className="w-4 h-4 stroke-[2.5]" />
                <span>Fill Up Lot Purchase Form</span>
              </button>
            </div>
          )}
        </div>

        {/* Footer Actions */}
        <div className="px-6 py-4 border-t border-slate-100 flex flex-wrap items-center justify-between gap-3 bg-slate-50/50">
          <div className="flex items-center gap-2">
            {hasPurchases && activePurchase && (
              <>
                {/* Edit Information */}
                <button
                  onClick={() => {
                    onClose();
                    onEditPurchase(activePurchase);
                  }}
                  className="inline-flex items-center gap-1.5 px-3.5 py-2 bg-emerald-50 hover:bg-emerald-100 text-emerald-900 border border-emerald-200 text-xs font-semibold rounded-xl shadow-2xs transition-colors cursor-pointer"
                >
                  <Edit3 className="w-3.5 h-3.5" />
                  <span>Edit Information</span>
                </button>

                {/* Details / Payment History & SOA */}
                <button
                  onClick={() => {
                    onClose();
                    onViewHistory(activePurchase);
                  }}
                  className="inline-flex items-center gap-1.5 px-3.5 py-2 bg-white hover:bg-slate-100 text-slate-700 border border-slate-200 text-xs font-semibold rounded-xl shadow-2xs transition-colors cursor-pointer"
                >
                  <Receipt className="w-3.5 h-3.5 text-slate-500" />
                  <span>Payment History (SOA)</span>
                </button>
              </>
            )}

            <button
              onClick={() => {
                onClose();
                onAddNewPurchase(client);
              }}
              className="inline-flex items-center gap-1.5 px-3.5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-colors cursor-pointer"
            >
              <Plus className="w-3.5 h-3.5 stroke-[2.5]" />
              <span>+ Add Another Lot</span>
            </button>
          </div>

          <button
            onClick={onClose}
            className="px-4 py-2 text-xs font-semibold text-slate-600 hover:text-slate-800 hover:bg-slate-100 rounded-xl transition-colors cursor-pointer"
          >
            Close
          </button>
        </div>
      </div>
    </div>
  );
};

