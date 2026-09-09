import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { PurchaseDetail, Client, Product, Agent } from '../../types';
import { calculateAmortization, formatCurrency } from '../../utils/calculations';
import { X, CheckCircle2, Calculator } from 'lucide-react';

interface ApplicationModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (application: PurchaseDetail) => string | void;
  clients: Client[];
  products: Product[];
  agents: Agent[];
  initialData?: PurchaseDetail | null;
  defaultClientId?: number | null;
}

export const ApplicationModal: React.FC<ApplicationModalProps> = ({
  isOpen,
  onClose,
  onSave,
  clients,
  products,
  agents,
  initialData,
  defaultClientId
}) => {
  const [clientId, setClientId] = useState<number>(defaultClientId || clients[0]?.idclients || 1);
  const [productId, setProductId] = useState<number>(products[0]?.idproduct || 1);
  const [agentId, setAgentId] = useState<number>(agents[0]?.id || 1);
  
  const [blockNo, setBlockNo] = useState<number>(1);
  const [lotNo, setLotNo] = useState<number>(1);
  const [area, setArea] = useState<number>(150);
  const [lotPrice, setLotPrice] = useState<number>(108000);
  const [terms, setTerms] = useState<number>(2);
  const [downpayment, setDownpayment] = useState<number>(20000);
  const [monthlyAmortization, setMonthlyAmortization] = useState<number>(0);
  const [agentPercentage, setAgentPercentage] = useState<number>(7);
  const [otherFees, setOtherFees] = useState<number>(1500);
  const [penalty, setPenalty] = useState<number>(0);
  const [dueDate, setDueDate] = useState<string>('2028-09-07');
  const [remarks, setRemarks] = useState<string>('Standard application pending verification');
  const [status, setStatus] = useState<any>('New');
  const [error,setError]=useState('');
  const [successMsg, setSuccessMsg] = useState<string>('');

  // When initialData changes (editing mode)
  useEffect(() => {
    if (initialData) {
      setClientId(initialData.idclients);
      setProductId(initialData.idproducts);
      setAgentId(initialData.idagent);
      setBlockNo(initialData.blockno);
      setLotNo(initialData.lotno);
      setArea(initialData.area);
      setLotPrice(initialData.lotprice);
      setTerms(initialData.terms);
      setDownpayment(initialData.downpayment || 0);
      setMonthlyAmortization(initialData.amortization);
      setAgentPercentage(initialData.agentpercentage);
      setOtherFees(initialData.otherfees || 0);
      setPenalty(initialData.penalty || 0);
      setDueDate(initialData.duedate || '');
      setRemarks(initialData.remarks || '');
      setStatus(initialData.leadStatus || 'Active');
    } else {
      if (defaultClientId) {
        setClientId(defaultClientId);
      }
      // Default new application
      const defaultProduct = products[0];
      if (defaultProduct) {
        setLotPrice(defaultProduct.cashprice);
      }
    }
  }, [initialData, products, defaultClientId]);

  // Recalculate amortization whenever lotPrice, downpayment, or terms change
  useEffect(() => {
    const amort = calculateAmortization(lotPrice, downpayment, terms);
    setMonthlyAmortization(amort);
  }, [lotPrice, downpayment, terms]);

  // Update default price when selecting another product
  const handleProductChange = (prodId: number) => {
    setProductId(prodId);
    const selected = products.find(p => p.idproduct === prodId);
    if (selected && !initialData) {
      setLotPrice(selected.cashprice);
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const selectedClient = clients.find(c => c.idclients === Number(clientId)) || clients[0];
    const selectedProduct = products.find(p => p.idproduct === Number(productId)) || products[0];
    const selectedAgent = agents.find(a => a.id === Number(agentId)) || agents[0];

    const application: PurchaseDetail = {
      id: initialData?.id || Date.now(),
      blockno: Number(blockNo),
      lotno: Number(lotNo),
      area: Number(area),
      lotprice: Number(lotPrice),
      amortization: monthlyAmortization,
      terms: Number(terms),
      downpayment: Number(downpayment),
      agentpercentage: Number(agentPercentage),
      idclients: Number(clientId),
      idproducts: Number(productId),
      idagent: Number(agentId),
      remarks,
      recordstatus: 'active',
      otherfees: Number(otherFees),
      penalty: Number(penalty),
      duedate: dueDate,
      
      clientName: selectedClient.fullname || `${selectedClient.lastname}, ${selectedClient.firstname}`,
      clientRole: initialData?.clientRole || `${selectedProduct.location} • Blk ${blockNo} Lot ${lotNo}`,
      clientAvatar: selectedClient.avatarUrl,
      productCode: selectedProduct.code,
      location: selectedProduct.location,
      agentName: selectedAgent.fullname,
      agentAvatar: selectedAgent.avatarUrl,
      status: status,
      score: initialData?.score || 88,
      intent: initialData?.intent || 'High',
      source: initialData?.source || 'Direct Inquiry',
      nextAction: initialData?.nextAction || 'Schedule Contract Signing',
      aiRecommendation: initialData?.aiRecommendation || 'High conversion probability - verified financials',
      dateApplied: initialData?.dateApplied || new Date().toISOString().split('T')[0]
    };

    const result=onSave(application);if(result){setError(result);return;} setError('');onClose();
  };

  if (!isOpen) return null;

  const currentProduct = products.find(p => p.idproduct === Number(productId)) || products[0];

  return (
    <ModalFrame onClose={onClose} title='Application form'>
      <div className="application-modal-card bg-white rounded-2xl max-w-3xl w-full border border-slate-200 shadow-xl overflow-hidden my-6 animate-in fade-in zoom-in-95 duration-150">
        {error&&<p role="alert" className="application-form-error-alert form-error px-6 pt-4">{error}</p>}{/* Header */}
        <div className="application-modal-header px-6 py-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="application-header-titles">
            <h2 className="application-modal-title text-base font-bold text-slate-900">
              {initialData ? 'Edit Purchase Application' : 'New Subdivision Lot Application'}
            </h2>
            <p className="application-modal-subtitle text-xs text-slate-500 mt-0.5">
              Linked workspace records
            </p>
          </div>
          <button
            onClick={onClose}
            className="application-close-btn p-1.5 rounded-lg text-slate-400 hover:text-slate-600 hover:bg-slate-100 transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Form Body */}
        <form onSubmit={handleSubmit} className="application-form-body p-6 space-y-5">
          {successMsg && (
            <div className="application-form-success-alert p-3 bg-emerald-50 border border-emerald-200 rounded-xl text-xs font-semibold text-emerald-800 flex items-center gap-2">
              <CheckCircle2 className="w-4 h-4 text-emerald-600" />
              <span>{successMsg}</span>
            </div>
          )}

          {/* Section 1: Client and Subdivision Selection */}
          <div className="application-section-buyer-project grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Stakeholder / Buyer Full Name *
              </label>
              <select
                value={clientId}
                onChange={(e) => setClientId(Number(e.target.value))}
                className="application-field-select w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 outline-none shadow-2xs"
              >
                {clients.map(c => (
                  <option key={c.idclients} value={c.idclients}>
                    {c.fullname || `${c.lastname}, ${c.firstname}`} ({c.contactno})
                  </option>
                ))}
              </select>
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Subdivision Project / Location *
              </label>
              <select
                value={productId}
                onChange={(e) => handleProductChange(Number(e.target.value))}
                className="application-field-select w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 outline-none shadow-2xs"
              >
                {products.map(p => (
                  <option key={p.idproduct} value={p.idproduct}>
                    {p.code} - {p.location}
                  </option>
                ))}
              </select>
            </div>
          </div>

          {/* Section 2: Block, Lot, Area & Pricing */}
          <div className="application-pricing-panel bg-slate-50/60 p-4 rounded-xl border border-slate-100 grid grid-cols-2 sm:grid-cols-4 gap-3.5">
            <div className="application-form-field">
              <label className="application-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                Block No.
              </label>
              <select
                value={blockNo}
                onChange={(e) => setBlockNo(Number(e.target.value))}
                className="application-field-select w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                {Array.from({ length: currentProduct?.totalblockno || 10 }, (_, i) => i + 1).map(b => (
                  <option key={b} value={b}>Block {b}</option>
                ))}
              </select>
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                Lot No.
              </label>
              <input
                type="number"
                min="1"
                max={currentProduct?.totallotno || 150}
                value={lotNo}
                onChange={(e) => setLotNo(Number(e.target.value))}
                className="application-field-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                Area (sq.m)
              </label>
              <input
                type="number"
                min="50"
                step="1"
                value={area}
                onChange={(e) => setArea(Number(e.target.value))}
                className="application-field-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                Total Lot Price (₱) *
              </label>
              <input
                type="number"
                step="1000"
                value={lotPrice}
                onChange={(e) => setLotPrice(Number(e.target.value))}
                className="application-field-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-900 font-semibold focus:border-emerald-600 outline-none"
              />
            </div>
          </div>

          {/* Section 3: Financing, Terms & Amortization Formula */}
          <div className="application-terms-section grid grid-cols-1 sm:grid-cols-3 gap-4">
            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Terms (Years)
              </label>
              <select
                value={terms}
                onChange={(e) => setTerms(Number(e.target.value))}
                className="application-field-select w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                <option value={1}>1 Year (12 months)</option>
                <option value={2}>2 Years (24 months)</option>
                <option value={3}>3 Years (36 months)</option>
                <option value={4}>4 Years (48 months)</option>
                <option value={5}>5 Years (60 months)</option>
                <option value={6}>6 Years (72 months)</option>
              </select>
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Estimated Downpayment (₱)
              </label>
              <input
                type="number"
                step="5000"
                value={downpayment}
                onChange={(e) => setDownpayment(Number(e.target.value))}
                className="application-field-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>

            <div className="application-amortization-card bg-emerald-50/70 border border-emerald-200/80 rounded-xl p-3 flex flex-col justify-center">
              <span className="application-amortization-label text-[10px] uppercase font-semibold text-emerald-800 tracking-wider flex items-center gap-1">
                <Calculator className="w-3 h-3 text-emerald-700" />
                Monthly Amortization (15% APR)
              </span>
              <span className="application-amortization-value text-lg font-bold text-emerald-950 mt-1">
                {formatCurrency(monthlyAmortization)} / mo
              </span>
            </div>
          </div>

          {/* Section 4: Agent, Commission, Fees, Due Date */}
          <div className="application-agent-section grid grid-cols-1 sm:grid-cols-3 gap-4">
            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Assigned Agent / Dicer
              </label>
              <select
                value={agentId}
                onChange={(e) => {
                  const id = Number(e.target.value);
                  setAgentId(id);
                  const a = agents.find(ag => ag.id === id);
                  if (a) setAgentPercentage(a.commissionRate);
                }}
                className="application-field-select w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                {agents.map(a => (
                  <option key={a.id} value={a.id}>{a.fullname} ({a.role})</option>
                ))}
              </select>
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Agent Commission Rate (%)
              </label>
              <input
                type="number"
                step="0.5"
                value={agentPercentage}
                onChange={(e) => setAgentPercentage(Number(e.target.value))}
                className="application-field-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>

            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                First Due Date
              </label>
              <input
                type="date"
                value={dueDate}
                onChange={(e) => setDueDate(e.target.value)}
                className="application-field-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>
          </div>

          {/* Section 5: Application Status & Remarks */}
          <div className="application-status-section grid grid-cols-1 sm:grid-cols-3 gap-4">
            <div className="application-form-field">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Pipeline Status
              </label>
              <select
                value={status}
                onChange={(e) => setStatus(e.target.value as any)}
                className="application-field-select w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                <option value="New">New</option>
                <option value="Demo Scheduled">Demo Scheduled / Reserved</option>
                <option value="Negotiation">Negotiation</option>
                <option value="Proposal Sent">Proposal Sent</option>
                <option value="Contacted">Contacted</option>
                <option value="Qualified">Qualified</option>
                <option value="Active">Active Installment</option>
                <option value="Fully Paid">Fully Paid</option>
              </select>
            </div>

            <div className="application-form-field sm:col-span-2">
              <label className="application-field-label block text-xs font-semibold text-slate-700 mb-1.5">
                Remarks / Notes
              </label>
              <input
                type="text"
                value={remarks}
                onChange={(e) => setRemarks(e.target.value)}
                placeholder="e.g. Requested survey plan, 2 valid IDs submitted"
                className="application-field-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              />
            </div>
          </div>

          {/* Action Footer */}
          <div className="application-modal-footer pt-4 border-t border-slate-100 flex items-center justify-end gap-2.5">
            <button
              type="button"
              onClick={onClose}
              className="application-cancel-btn px-4 py-2 bg-slate-100 hover:bg-slate-200/80 text-slate-700 text-xs font-semibold rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="application-submit-btn px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
            >
              {initialData ? 'Update Application' : 'Save Application'}
            </button>
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};





