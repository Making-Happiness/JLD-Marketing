import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { PurchaseDetail, PaymentTransaction, PaymentDetailItem, PaymentForType, PaymentMethodType } from '../../types';
import { generatePaymentRef, formatCurrency, formatDate } from '../../utils/calculations';
import { X, Plus, Trash2, Receipt, Printer } from 'lucide-react';

interface PaymentModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSavePayment: (payment: PaymentTransaction) => string | void;
  applications: PurchaseDetail[];
  selectedApplication?: PurchaseDetail | null;
}

export const PaymentModal: React.FC<PaymentModalProps> = ({
  isOpen,
  onClose,
  onSavePayment,
  applications,
  selectedApplication
}) => {
  const [appId, setAppId] = useState<number>(selectedApplication?.id || applications[0]?.id || 1);
  const [dateOfPayment, setDateOfPayment] = useState<string>(new Date().toISOString().split('T')[0]);
  const [orNumber, setOrNumber] = useState<string>('');
  const [paymentType, setPaymentType] = useState<PaymentMethodType>('CASH');
  const [referenceNo, setReferenceNo] = useState<string>('');
  const [paymentRef, setPaymentRef] = useState<string>(() => generatePaymentRef());
  const inchargeByName = 'Ralph Edwards (Cashier)';

  // New item line
  const [paymentFor, setPaymentFor] = useState<PaymentForType>('INSTALLMENT');
  const [itemAmount, setItemAmount] = useState<number>(6582);
  const [itemDesc, setItemDesc] = useState<string>('Month 2 Amortization Payment');

  // Items cart list
  const [items, setItems] = useState<PaymentDetailItem[]>([]);
  const [showReceipt, setShowReceipt] = useState<boolean>(false);
  const [savedPayment, setSavedPayment] = useState<PaymentTransaction | null>(null);

  useEffect(() => {
    if (selectedApplication) {
      setAppId(selectedApplication.id);
      setItemAmount(selectedApplication.amortization || 5000);
      setItemDesc(`Amortization for ${selectedApplication.location} Blk ${selectedApplication.blockno} Lot ${selectedApplication.lotno}`);
    }
  }, [selectedApplication]);

  const [error,setError]=useState('');
  useEffect(()=>{if(isOpen){setItems([]);setShowReceipt(false);setSavedPayment(null);setOrNumber('');setPaymentRef(generatePaymentRef());setError('');setReferenceNo('');setAppId(selectedApplication?.id||applications[0]?.id||0);}},[isOpen]);
  const currentApp = applications.find(a => a.id === Number(appId)) || applications[0];

  const handleAddItem = () => {
    if (!currentApp || !Number.isFinite(itemAmount) || itemAmount <= 0) {setError('Select a contract and enter a valid amount.');return;}

    const newItem: PaymentDetailItem = {
      id: Date.now(),
      idpurchasedetails: Number(appId),
      idpayment: 0,
      paymentfor: paymentFor,
      amount: Number(itemAmount),
      description: itemDesc || `${paymentFor} Payment`
    };

    setItems([...items, newItem]);
    // Reset amount
    setItemDesc('');
  };

  const handleRemoveItem = (id: number) => {
    setItems(items.filter(i => i.id !== id));
  };

  const totalAmount = items.reduce((acc, curr) => acc + curr.amount, 0);

  const handleSubmitPayment = () => {
    if (!currentApp || !orNumber.trim() || !dateOfPayment) {setError('Select a contract, enter its receipt number and payment date.');return;}
    if(paymentType!=='CASH'&&!referenceNo.trim()){setError('Enter the bank, cheque or wallet reference.');return;}
    if(items.some(i=>applications.find(a=>a.id===i.idpurchasedetails)?.idclients!==currentApp.idclients)){setError('All items on a receipt must belong to the selected buyer.');return;}
    if (items.length === 0) {
      setError('Please add at least one payment item.');
      return;
    }

    const transaction: PaymentTransaction = {
      id: Date.now(),
      dateofpayment: dateOfPayment,
      orderreceipt: orNumber,
      referenceno: referenceNo,
      paymentref: paymentRef,
      paymenttype: paymentType,
      paidby: currentApp?.idclients || 1,
      paidbyName: currentApp?.clientName || 'Client',
      inchargeby: 1,
      inchargebyName: inchargeByName,
      recordedby: 1,
      totalamount: totalAmount,
      recordstatus: 'active',
      status: 'active',
      deleted_at: null,
      items: items
    };

    transaction.items=transaction.items.map(i=>({...i,idpayment:transaction.id}));
    const saveError=onSavePayment(transaction);if(saveError){setError(saveError);return;}
    setSavedPayment(transaction);
    setShowReceipt(true);
  };

  if (!isOpen) return null;

  return (
    <ModalFrame onClose={onClose} title={showReceipt ? 'Official Payment Receipt' : 'Record Payment'}>
      <div className="payment-modal-shell form-shell form-shell-wide">
        {/* Header */}
        <header className="payment-modal-header form-header">
          <div className="payment-modal-icon-wrapper form-heading-icon">
            <Receipt size={22}/>
          </div>
          <div className="payment-modal-title-group">
            <span className="payment-modal-eyebrow form-eyebrow">COLLECTIONS &amp; REVENUE</span>
            <h2 className="payment-modal-title">
              {showReceipt ? 'Official Payment Receipt' : 'Record Payment Voucher'}
            </h2>
            <p className="payment-modal-subtitle">
              {showReceipt
                ? 'Official payment acknowledgement receipt for client records.'
                : 'Process installment collections and generate official payment receipt.'}
            </p>
          </div>
          <button
            onClick={onClose}
            className="payment-modal-close-button form-close"
            aria-label="Close modal"
          >
            <X size={18}/>
          </button>
        </header>

        {!showReceipt ? (
          /* Payment Processing Form */
          <div className="payment-modal-body form-body">
            {error && <p role="alert" className="payment-modal-error form-error">{error}</p>}

            {/* Payment Ref Bar */}
            <div className="payment-ref-banner form-note" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '18px' }}>
              <div>
                <span style={{ fontSize: '11px', textTransform: 'uppercase', letterSpacing: '0.6px', fontWeight: 700, color: '#1a5e3f', display: 'block' }}>
                  Payment Reference Code
                </span>
                <span style={{ fontSize: '15px', fontFamily: 'monospace', fontWeight: 700, color: '#123927' }}>
                  {paymentRef}
                </span>
              </div>
              <div style={{ textAlign: 'right' }}>
                <span style={{ fontSize: '11px', textTransform: 'uppercase', letterSpacing: '0.6px', fontWeight: 600, color: '#688272', display: 'block' }}>
                  Cashier In-Charge
                </span>
                <span style={{ fontSize: '13px', fontWeight: 600, color: '#1f3c2d' }}>
                  {inchargeByName}
                </span>
              </div>
            </div>

            {/* Target Account / Property Selection */}
            <fieldset className="payment-fieldset">
              <legend className="payment-legend">Contract &amp; Account Selection</legend>
              <label className="form-field-label full">
                <span>Select Client &amp; Subdivision Lot <span className="req">*</span></span>
                <select
                  value={appId}
                  onChange={(e) => {
                    const id = Number(e.target.value);
                    setAppId(id); setItems([]); setError('');
                    const selected = applications.find(a => a.id === id);
                    if (selected) {
                      setItemAmount(selected.amortization || 5000);
                      setItemDesc(`Amortization for ${selected.location} Blk ${selected.blockno} Lot ${selected.lotno}`);
                    }
                  }}
                  className="payment-select-input"
                >
                  {applications.map(a => (
                    <option key={a.id} value={a.id}>
                      {a.clientName} — {a.location} (Blk {a.blockno}, Lot {a.lotno}) — Amort: {formatCurrency(a.amortization)}/mo
                    </option>
                  ))}
                </select>
              </label>
            </fieldset>

            {/* Payment Details: OR, Date, Method */}
            <fieldset className="payment-fieldset">
              <legend className="payment-legend">Voucher &amp; Payment Details</legend>
              <div className="form-grid-3">
                <label className="form-field-label">
                  <span>Official Receipt (OR No.) <span className="req">*</span></span>
                  <input
                    type="text"
                    value={orNumber}
                    onChange={(e) => setOrNumber(e.target.value)}
                    placeholder="e.g. OR-9921"
                    className="payment-text-input"
                    style={{ fontWeight: 600 }}
                  />
                </label>

                <label className="form-field-label">
                  <span>Date of Payment <span className="req">*</span></span>
                  <input
                    type="date"
                    value={dateOfPayment}
                    onChange={(e) => setDateOfPayment(e.target.value)}
                    className="payment-date-input"
                  />
                </label>

                <label className="form-field-label">
                  <span>Payment Method <span className="req">*</span></span>
                  <select
                    value={paymentType}
                    onChange={(e) => setPaymentType(e.target.value as PaymentMethodType)}
                    className="payment-method-select"
                  >
                    <option value="CASH">CASH</option>
                    <option value="BANK TRANSFER">BANK TRANSFER</option>
                    <option value="GCASH">GCASH</option>
                    <option value="MAYA">MAYA</option>
                    <option value="CHEQUE">CHEQUE</option>
                  </select>
                </label>
              </div>

              {paymentType !== 'CASH' && (
                <div style={{ marginTop: '12px' }}>
                  <label className="form-field-label">
                    <span>Bank / Transaction Reference Number <span className="req">*</span></span>
                    <input
                      type="text"
                      value={referenceNo}
                      onChange={(e) => setReferenceNo(e.target.value)}
                      placeholder="e.g. BDO-TRX-10294 or GCash Ref 8820"
                      className="payment-refno-input"
                    />
                  </label>
                </div>
              )}
            </fieldset>

            {/* Add Payment Item Line Bar */}
            <fieldset className="payment-fieldset">
              <legend className="payment-legend">Payment Line Items</legend>
              <div className="p-3.5 bg-[#f8faf8] rounded-lg border border-[#d8e4dc] mb-3">
                <span style={{ fontSize: '11px', fontWeight: 700, color: '#274e38', textTransform: 'uppercase', letterSpacing: '0.5px', display: 'block', marginBottom: '8px' }}>
                  Add Line Item to Receipt
                </span>
                <div className="grid grid-cols-1 sm:grid-cols-4 gap-2.5">
                  <div>
                    <select
                      value={paymentFor}
                      onChange={(e) => setPaymentFor(e.target.value as PaymentForType)}
                      className="payment-for-select"
                    >
                      <option value="RESERVED">RESERVED</option>
                      <option value="DOWN PAYMENT">DOWN PAYMENT</option>
                      <option value="INSTALLMENT">INSTALLMENT</option>
                      <option value="FULL PAYMENT">FULL PAYMENT</option>
                    </select>
                  </div>
                  <div>
                    <input
                      type="number"
                      step="100"
                      placeholder="Amount (PHP)"
                      value={itemAmount}
                      onChange={(e) => setItemAmount(Number(e.target.value))}
                      className="payment-amount-input"
                      style={{ fontWeight: 600 }}
                    />
                  </div>
                  <div>
                    <input
                      type="text"
                      placeholder="Description notes"
                      value={itemDesc}
                      onChange={(e) => setItemDesc(e.target.value)}
                      className="payment-desc-input"
                    />
                  </div>
                  <div>
                    <button
                      type="button"
                      onClick={handleAddItem}
                      className="primary-button w-full justify-center"
                    >
                      <Plus size={15}/>
                      <span>Add Item</span>
                    </button>
                  </div>
                </div>
              </div>

              {/* Payment Items Cart / DGV */}
              <div className="border border-[#d5e0d8] rounded-lg overflow-hidden">
                <table className="w-full text-left" style={{ borderCollapse: 'collapse', fontSize: '13px' }}>
                  <thead className="bg-[#f6f9f7] text-[#637d6e] border-b border-[#e2eae5]">
                    <tr>
                      <th className="py-2.5 px-3.5 font-semibold text-xs">Payment For</th>
                      <th className="py-2.5 px-3.5 font-semibold text-xs">Description</th>
                      <th className="py-2.5 px-3.5 font-semibold text-xs text-right">Amount</th>
                      <th className="py-2.5 px-3.5 font-semibold text-xs text-center w-12">Action</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-[#edf1ee]">
                    {items.length === 0 ? (
                      <tr>
                        <td colSpan={4} className="py-4 text-center text-[#869b8f] text-xs">
                          No items added yet. Specify the details above and click "Add Item".
                        </td>
                      </tr>
                    ) : (
                      items.map((item) => (
                        <tr key={item.id} className="hover:bg-[#f8fbf9]">
                          <td className="py-2.5 px-3.5 font-semibold text-[#1e3b2b]">
                            {item.paymentfor}
                          </td>
                          <td className="py-2.5 px-3.5 text-[#516b5c]">
                            {item.description}
                          </td>
                          <td className="py-2.5 px-3.5 text-right font-bold text-[#14472f]">
                            {formatCurrency(item.amount)}
                          </td>
                          <td className="py-2.5 px-3.5 text-center">
                            <button
                              onClick={() => handleRemoveItem(item.id)}
                              className="p-1 text-[#8fa296] hover:text-rose-600 rounded transition-colors"
                              title="Remove item"
                            >
                              <Trash2 size={14}/>
                            </button>
                          </td>
                        </tr>
                      ))
                    )}
                  </tbody>
                </table>

                {/* Total Row */}
                <div className="p-3 bg-[#f3f7f4] border-t border-[#d8e4dc] flex items-center justify-between">
                  <span className="font-semibold text-[#294c39] text-xs">Total Payment Due:</span>
                  <span className="text-base font-bold text-[#145f49]">
                    {formatCurrency(totalAmount)}
                  </span>
                </div>
              </div>
            </fieldset>

            {/* Buttons */}
            <div className="payment-actions-bar form-actions">
              <button
                type="button"
                onClick={onClose}
                className="payment-cancel-button secondary-button"
              >
                Cancel
              </button>
              <button
                type="button"
                onClick={handleSubmitPayment}
                disabled={items.length === 0}
                className="payment-submit-button primary-button"
              >
                Accept Payment &amp; Print Receipt
              </button>
            </div>
          </div>
        ) : (
          /* Printable Receipt View */
          <div className="payment-receipt-view form-body" style={{ paddingBottom: '0' }}>
            <div className="payment-receipt-sheet p-6 border-2 border-dashed border-[#b8ccbf] rounded-xl bg-white text-xs space-y-4">
              <div className="payment-receipt-header text-center pb-3 border-b border-[#e2eae5]">
                <h3 className="text-base font-bold text-[#143d29] tracking-tight">JLD SUBDIVISION &amp; REAL PROPERTY</h3>
                <p className="text-[#647c6e] text-[11px]">Official Payment Acknowledgement Receipt</p>
                <div className="inline-block mt-2 bg-[#e8f3ed] text-[#145f49] border border-[#c3dfce] px-3 py-0.5 rounded-full text-[11px] font-bold">
                  OR: {savedPayment?.orderreceipt}
                </div>
              </div>

              <div className="grid grid-cols-2 gap-2.5 text-[#4e6859] text-xs">
                <div><span className="font-semibold text-[#234231]">Paid By:</span> {savedPayment?.paidbyName}</div>
                <div><span className="font-semibold text-[#234231]">Date:</span> {formatDate(savedPayment?.dateofpayment)}</div>
                <div><span className="font-semibold text-[#234231]">Payment Ref:</span> {savedPayment?.paymentref}</div>
                <div><span className="font-semibold text-[#234231]">Payment Mode:</span> {savedPayment?.paymenttype} {savedPayment?.referenceno ? `(${savedPayment.referenceno})` : ''}</div>
                <div><span className="font-semibold text-[#234231]">Cashier:</span> {savedPayment?.inchargebyName}</div>
              </div>

              <div className="border-t border-b border-[#e2eae5] py-3 space-y-2">
                {savedPayment?.items.map(item => (
                  <div key={item.id} className="flex justify-between">
                    <div>
                      <span className="font-semibold text-[#1e3b2b]">{item.paymentfor}</span>
                      <span className="text-[#758b7e] block text-[11px]">{item.description}</span>
                    </div>
                    <span className="font-bold text-[#123927]">{formatCurrency(item.amount)}</span>
                  </div>
                ))}
              </div>

              <div className="flex justify-between items-center pt-1 text-sm font-bold text-[#143d29]">
                <span>TOTAL PAID:</span>
                <span className="text-[#145f49] text-base">{formatCurrency(savedPayment?.totalamount)}</span>
              </div>
            </div>

            <div className="payment-receipt-actions-bar form-actions">
              <button
                onClick={() => window.print()}
                className="secondary-button"
              >
                <Printer size={15}/>
                <span>Print OR</span>
              </button>
              <button
                onClick={() => {
                  setShowReceipt(false);
                  setItems([]);
                  onClose();
                }}
                className="primary-button"
              >
                Done
              </button>
            </div>
          </div>
        )}
      </div>
    </ModalFrame>
  );
};




