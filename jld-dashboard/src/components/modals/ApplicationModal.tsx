import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { PurchaseDetail, Client, Product, Agent } from '../../types';
import { X, PencilLine, FileSpreadsheet } from 'lucide-react';

interface ApplicationModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (application: PurchaseDetail) => string | void;
  clients: Client[];
  products: Product[];
  agents: Agent[];
  initialData?: PurchaseDetail | null;
  defaultClientId?: number | null;
  onRegisterBuyer: () => void;
}

export const ApplicationModal: React.FC<ApplicationModalProps> = ({
  isOpen,
  onClose,
  onSave,
  clients,
  products,
  agents,
  initialData,
  defaultClientId,
  onRegisterBuyer
}) => {
  const [clientId, setClientId] = useState<number | ''>(defaultClientId || clients[0]?.idclients || '');
  const [productId, setProductId] = useState<number>(products[0]?.idproduct || 1);
  const [agentId, setAgentId] = useState<number>(agents[0]?.id || 1);
  
  const [blockNo, setBlockNo] = useState<number>(1);
  // Keep the field blank until the user enters a lot number. A numeric-only
  // state would coerce an empty input to 0 and make it difficult to clear.
  const [lotNo, setLotNo] = useState<number | ''>('');
  const [area, setArea] = useState<number>(150);
  const [lotPrice, setLotPrice] = useState<number>(108000);
  const [terms, setTerms] = useState<number>(2);
  const [downpayment, setDownpayment] = useState<number>(20000);
  const [monthlyAmortization, setMonthlyAmortization] = useState<number | ''>('');
  const [agentPercentage, setAgentPercentage] = useState<number>(7);
  const [otherFees, setOtherFees] = useState<number>(1500);
  const [penalty, setPenalty] = useState<number>(0);
  const [dueDate, setDueDate] = useState<string>('2028-09-07');
  const [remarks, setRemarks] = useState<string>('Standard application pending verification');
  const [status, setStatus] = useState<any>('New');
  const [error,setError]=useState('');

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
      setMonthlyAmortization(initialData.amortization ?? '');
      setAgentPercentage(initialData.agentpercentage);
      setOtherFees(initialData.otherfees || 0);
      setPenalty(initialData.penalty || 0);
      setDueDate(initialData.duedate || '');
      setRemarks(initialData.remarks || '');
      setStatus(initialData.leadStatus || 'Active');
    } else {
      setMonthlyAmortization('');
      setClientId(defaultClientId || clients[0]?.idclients || '');
      // Default new application
      const defaultProduct = products[0];
      if (defaultProduct) {
        setLotPrice(defaultProduct.cashprice);
      }
    }
  }, [initialData, products, defaultClientId]);

  // A missing-buyer error may have been shown before the user registered a
  // buyer. Remove only that stale message once all current selections are valid.
  useEffect(() => {
    const hasActiveBuyer = clientId !== '' && clients.some(client => String(client.idclients) === String(clientId));
    const hasActiveProperty = products.some(product => String(product.idproduct) === String(productId));
    const hasActiveAgent = agents.some(agent => String(agent.id) === String(agentId));

    if (hasActiveBuyer && hasActiveProperty && hasActiveAgent) {
      setError(current => current === 'Select an active property, buyer and agent.' ? '' : current);
    }
  }, [agentId, agents, clientId, clients, productId, products]);

  // Update default price when selecting another product
  const handleProductChange = (prodId: number) => {
    setProductId(prodId);
    setError('');
    const selected = products.find(p => p.idproduct === prodId);
    if (selected && !initialData) {
      setLotPrice(selected.cashprice);
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const optionalAmortization = monthlyAmortization === '' ? null : Number(monthlyAmortization);

    if (clientId === '') {
      setError('Select an active buyer or register a new buyer before saving the application.');
      return;
    }
    if (lotNo === '' || !Number.isInteger(lotNo) || lotNo < 1) {
      setError('Enter a whole-number Lot No. greater than 0.');
      return;
    }
    if (optionalAmortization !== null && (!Number.isFinite(optionalAmortization) || optionalAmortization < 0)) {
      setError('Monthly amortization must be zero or more when it is entered.');
      return;
    }

    const selectedClient = clients.find(c => c.idclients === Number(clientId)) || clients[0];
    const selectedProduct = products.find(p => p.idproduct === Number(productId)) || products[0];
    const selectedAgent = agents.find(a => a.id === Number(agentId)) || agents[0];

    const application: PurchaseDetail = {
      id: initialData?.id || Date.now(),
      blockno: Number(blockNo),
      lotno: Number(lotNo),
      area: Number(area),
      lotprice: Number(lotPrice),
      amortization: optionalAmortization,
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
    <ModalFrame onClose={onClose} title="Purchase Application">
      <div className="application-modal-shell form-shell form-shell-wide">
        <header className="application-modal-header form-header">
          <div className="application-modal-icon-wrapper form-heading-icon">
            <FileSpreadsheet size={22}/>
          </div>
          <div className="application-modal-title-group">
            <span className="application-modal-eyebrow form-eyebrow">PROPERTY CONTRACTS</span>
            <h2 className="application-modal-title">
              {initialData ? 'Edit Purchase Application' : 'New Subdivision Lot Application'}
            </h2>
            <p className="application-modal-subtitle">
              Establish lot assignment, financing terms, and sales commission schedule.
            </p>
          </div>
          <button onClick={onClose} className="application-close-btn form-close" aria-label="Close form">
            <X size={18}/>
          </button>
        </header>

        <form onSubmit={handleSubmit} className="application-form">
          <div className="application-form-body form-body">
            {error && <p role="alert" className="application-form-error-alert form-error">{error}</p>}

            <fieldset className="application-fieldset">
              <legend className="application-legend">Buyer &amp; Project Selection</legend>
              <div className="form-grid">
                <label className="form-field-label">
                  <span>Stakeholder / Buyer Full Name <span className="req">*</span></span>
                  <select
                    value={clientId}
                    onChange={(e) => {
                      setClientId(Number(e.target.value));
                      setError('');
                    }}
                    className="application-field-select"
                    required
                  >
                    <option value="" disabled>
                      {clients.length ? 'Select a buyer' : 'No active buyers available'}
                    </option>
                    {clients.map(c => (
                      <option key={c.idclients} value={c.idclients}>
                        {c.fullname || `${c.lastname}, ${c.firstname}`} ({c.contactno})
                      </option>
                    ))}
                  </select>
                  <button
                    type="button"
                    onClick={onRegisterBuyer}
                    className="text-xs font-semibold text-[#1a5e3f] text-left mt-1 hover:underline"
                  >
                    + Register new buyer
                  </button>
                </label>

                <label className="form-field-label">
                  <span>Subdivision Project / Location <span className="req">*</span></span>
                  <select
                    value={productId}
                    onChange={(e) => handleProductChange(Number(e.target.value))}
                    className="application-field-select"
                  >
                    {products.map(p => (
                      <option key={p.idproduct} value={p.idproduct}>
                        {p.code} - {p.location}
                      </option>
                    ))}
                  </select>
                </label>
              </div>
            </fieldset>

            <fieldset className="application-fieldset">
              <legend className="application-legend">Land Inventory &amp; Pricing</legend>
              <div className="form-grid-4">
                <label className="form-field-label">
                  <span>Block No. <span className="req">*</span></span>
                  <select
                    value={blockNo}
                    onChange={(e) => setBlockNo(Number(e.target.value))}
                    className="application-field-select"
                  >
                    {Array.from({ length: currentProduct?.totalblockno || 10 }, (_, i) => i + 1).map(b => (
                      <option key={b} value={b}>Block {b}</option>
                    ))}
                  </select>
                </label>

                <label className="form-field-label">
                  <span>Lot No. <span className="req">*</span></span>
                  <input
                    type="number"
                    min="1"
                    step="1"
                    inputMode="numeric"
                    required
                    value={lotNo}
                    onChange={(e) => setLotNo(e.target.value === '' ? '' : Number(e.target.value))}
                    className="application-field-input"
                  />
                </label>

                <label className="form-field-label">
                  <span>Area (sq.m) <span className="req">*</span></span>
                  <input
                    type="number"
                    min="50"
                    step="1"
                    value={area}
                    onChange={(e) => setArea(Number(e.target.value))}
                    className="application-field-input"
                  />
                </label>

                <label className="form-field-label">
                  <span>Total Lot Price (₱) <span className="req">*</span></span>
                  <input
                    type="number"
                    step="1000"
                    value={lotPrice}
                    onChange={(e) => setLotPrice(Number(e.target.value))}
                    className="application-field-input"
                    style={{ fontWeight: 600 }}
                  />
                </label>
              </div>
            </fieldset>

            <fieldset className="application-fieldset">
              <legend className="application-legend">Financing &amp; Amortization Schedule</legend>
              <div className="form-grid-3" style={{ alignItems: 'flex-start' }}>
                <label className="form-field-label">
                  <span>Payment Term</span>
                  <select
                    value={terms}
                    onChange={(e) => setTerms(Number(e.target.value))}
                    className="application-field-select"
                  >
                    <option value={1}>1 Year (12 months)</option>
                    <option value={2}>2 Years (24 months)</option>
                    <option value={3}>3 Years (36 months)</option>
                    <option value={4}>4 Years (48 months)</option>
                    <option value={5}>5 Years (60 months)</option>
                    <option value={6}>6 Years (72 months)</option>
                  </select>
                </label>

                <label className="form-field-label">
                  <span>Estimated Downpayment (₱)</span>
                  <input
                    type="number"
                    step="5000"
                    value={downpayment}
                    onChange={(e) => setDownpayment(Number(e.target.value))}
                    className="application-field-input"
                  />
                </label>

                <label className="application-amortization-card p-3 rounded-lg border border-[#cbe1d5] bg-[#f1f7f3] flex flex-col justify-center">
                  <span className="text-[10px] uppercase font-bold text-[#1e583c] tracking-wider flex items-center gap-1.5">
                    <PencilLine size={13} className="text-[#1a5e3f]"/>
                    Monthly Amortization (₱) — Optional
                  </span>
                  <input
                    type="number"
                    min="0"
                    step="0.01"
                    inputMode="decimal"
                    value={monthlyAmortization}
                    onChange={(e) => setMonthlyAmortization(e.target.value === '' ? '' : Number(e.target.value))}
                    className="application-field-input mt-1"
                    aria-describedby="monthly-amortization-help"
                    placeholder="Leave blank if not set"
                  />
                  <span id="monthly-amortization-help" className="text-xs font-normal text-[#4d725f] mt-1">
                    Set an amount only when it has been approved. Blank values remain blank in contracts and statements.
                  </span>
                </label>
              </div>
            </fieldset>

            <fieldset className="application-fieldset">
              <legend className="application-legend">Sales Agent &amp; Terms</legend>
              <div className="form-grid-3">
                <label className="form-field-label">
                  <span>Assigned Agent / Dicer</span>
                  <select
                    value={agentId}
                    onChange={(e) => {
                      const id = Number(e.target.value);
                      setAgentId(id);
                      setError('');
                      const a = agents.find(ag => ag.id === id);
                      if (a) setAgentPercentage(a.commissionRate);
                    }}
                    className="application-field-select"
                  >
                    {agents.map(a => (
                      <option key={a.id} value={a.id}>{a.fullname} ({a.role})</option>
                    ))}
                  </select>
                </label>

                <label className="form-field-label">
                  <span>Agent Commission Rate (%)</span>
                  <input
                    type="number"
                    step="0.5"
                    value={agentPercentage}
                    onChange={(e) => setAgentPercentage(Number(e.target.value))}
                    className="application-field-input"
                  />
                </label>

                <label className="form-field-label">
                  <span>First Due Date</span>
                  <input
                    type="date"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                    className="application-field-input"
                  />
                </label>
              </div>

              <div className="form-grid" style={{ marginTop: '14px' }}>
                <label className="form-field-label">
                  <span>Pipeline Status</span>
                  <select
                    value={status}
                    onChange={(e) => setStatus(e.target.value as any)}
                    className="application-field-select"
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
                </label>

                <label className="form-field-label">
                  <span>Remarks / Contract Notes</span>
                  <input
                    type="text"
                    value={remarks}
                    onChange={(e) => setRemarks(e.target.value)}
                    placeholder="e.g. Survey plan attached, 2 valid IDs submitted"
                    className="application-field-input"
                  />
                </label>
              </div>
            </fieldset>
          </div>

          <div className="application-modal-actions form-actions">
            <button
              type="button"
              onClick={onClose}
              className="application-cancel-btn secondary-button"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="application-submit-btn primary-button"
            >
              {initialData ? 'Update Application' : 'Save Application'}
            </button>
          </div>
        </form>
      </div>
    </ModalFrame>
  );
};





