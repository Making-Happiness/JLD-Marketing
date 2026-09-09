import { ModalFrame } from './ModalFrame';
import React, { useState, useEffect } from 'react';
import { PurchaseDetail, PaymentTransaction, PaymentDetailItem, PaymentForType, PaymentMethodType } from '../../types';
import { generatePaymentRef, formatCurrency, formatDate } from '../../utils/calculations';
import { X, Plus, Trash2, CheckCircle2, Receipt, Printer } from 'lucide-react';

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
  const [inchargeByName, setInchargeByName] = useState<string>('Ralph Edwards (Cashier)');

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
    <ModalFrame onClose={onClose} title='Payment form'>
      <div className="payment-modal-card bg-white rounded-2xl max-w-2xl w-full border border-slate-200 shadow-xl overflow-hidden my-6 animate-in fade-in zoom-in-95 duration-150">
        {/* Modal Header */}
        <div className="payment-modal-header px-6 py-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="payment-modal-header-brand flex items-center gap-2">
            <div className="payment-modal-icon-badge w-8 h-8 rounded-lg bg-emerald-100 text-emerald-800 flex items-center justify-center">
              <Receipt className="w-4 h-4" />
            </div>
            <div className="payment-modal-title-group">
              <h2 className="payment-modal-title text-base font-bold text-slate-900">
                {showReceipt ? 'Official Payment Receipt' : 'Record payment'}
              </h2>
              <p className="payment-modal-subtitle text-xs text-slate-500">
                Record collections against a buyer contract
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="payment-modal-close-button p-1.5 rounded-lg text-slate-400 hover:text-slate-600 hover:bg-slate-100 transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {!showReceipt ? (
          /* Payment Processing Form */
          <div className="payment-modal-body p-6 space-y-4 text-xs">{error && <p role="alert" className="payment-modal-error form-error">{error}</p>}
            {/* Payment Ref Bar */}
            <div className="payment-ref-banner bg-emerald-50/60 border border-emerald-200/80 rounded-xl px-4 py-2.5 flex items-center justify-between">
              <div className="payment-ref-code-group">
                <span className="payment-ref-label text-[10px] uppercase font-semibold text-emerald-800 block">Payment Reference Code</span>
                <span className="payment-ref-value text-sm font-mono font-bold text-emerald-950">{paymentRef}</span>
              </div>
              <div className="payment-incharge-group text-right">
                <span className="payment-incharge-label text-[10px] uppercase font-semibold text-slate-500 block">In-Charge / Cashier</span>
                <span className="payment-incharge-value text-xs font-semibold text-slate-800">{inchargeByName}</span>
              </div>
            </div>

            {/* Target Account / Property Selection */}
            <div className="payment-account-select-group">
              <label className="payment-field-label block text-xs font-semibold text-slate-700 mb-1">
                Select Client & Subdivision Lot *
              </label>
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
                className="payment-select-input w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                {applications.map(a => (
                  <option key={a.id} value={a.id}>
                    {a.clientName} — {a.location} (Blk {a.blockno}, Lot {a.lotno}) — Amort: {formatCurrency(a.amortization)}/mo
                  </option>
                ))}
              </select>
            </div>

            {/* Payment Details: OR, Date, Method */}
            <div className="payment-details-grid grid grid-cols-1 sm:grid-cols-3 gap-3">
              <div className="payment-or-group">
                <label className="payment-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                  Official Receipt (OR No.) *
                </label>
                <input
                  type="text"
                  value={orNumber}
                  onChange={(e) => setOrNumber(e.target.value)}
                  className="payment-text-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-900 font-medium focus:border-emerald-600 outline-none"
                />
              </div>

              <div className="payment-date-group">
                <label className="payment-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                  Date of Payment
                </label>
                <input
                  type="date"
                  value={dateOfPayment}
                  onChange={(e) => setDateOfPayment(e.target.value)}
                  className="payment-date-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
                />
              </div>

              <div className="payment-method-group">
                <label className="payment-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                  Payment Method
                </label>
                <select
                  value={paymentType}
                  onChange={(e) => setPaymentType(e.target.value as PaymentMethodType)}
                  className="payment-method-select w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
                >
                  <option value="CASH">CASH</option>
                  <option value="BANK TRANSFER">BANK TRANSFER</option>
                  <option value="GCASH">GCASH</option>
                  <option value="MAYA">MAYA</option>
                  <option value="CHEQUE">CHEQUE</option>
                </select>
              </div>
            </div>

            {paymentType !== 'CASH' && (
              <div className="payment-refno-group">
                <label className="payment-field-label block text-[11px] font-semibold text-slate-600 mb-1">
                  Bank / Transaction Reference Number *
                </label>
                <input
                  type="text"
                  value={referenceNo}
                  onChange={(e) => setReferenceNo(e.target.value)}
                  placeholder="e.g. BDO-TRX-10294 or GCash Ref 8820"
                  className="payment-refno-input w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
                />
              </div>
            )}

            {/* Add Payment Item Line Bar */}
            <div className="payment-add-item-card p-3 bg-slate-50 rounded-xl border border-slate-200/80 space-y-2">
              <span className="payment-add-item-title text-[11px] font-bold text-slate-700 block">Add Payment Item</span>
              <div className="payment-add-item-grid grid grid-cols-1 sm:grid-cols-4 gap-2">
                <div className="payment-for-group">
                  <select
                    value={paymentFor}
                    onChange={(e) => setPaymentFor(e.target.value as PaymentForType)}
                    className="payment-for-select w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs text-slate-800 outline-none"
                  >
                    <option value="RESERVED">RESERVED</option>
                    <option value="DOWN PAYMENT">DOWN PAYMENT</option>
                    <option value="INSTALLMENT">INSTALLMENT</option>
                    <option value="FULL PAYMENT">FULL PAYMENT</option>
                  </select>
                </div>
                <div className="payment-amount-group">
                  <input
                    type="number"
                    step="100"
                    placeholder="Amount"
                    value={itemAmount}
                    onChange={(e) => setItemAmount(Number(e.target.value))}
                    className="payment-amount-input w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs font-semibold text-slate-900 outline-none"
                  />
                </div>
                <div className="payment-desc-group">
                  <input
                    type="text"
                    placeholder="Description notes"
                    value={itemDesc}
                    onChange={(e) => setItemDesc(e.target.value)}
                    className="payment-desc-input w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs text-slate-800 outline-none"
                  />
                </div>
                <div className="payment-add-button-group">
                  <button
                    type="button"
                    onClick={handleAddItem}
                    className="payment-add-item-button w-full py-1.5 bg-emerald-700 hover:bg-emerald-800 text-white font-semibold rounded-lg flex items-center justify-center gap-1 shadow-2xs transition-colors cursor-pointer"
                  >
                    <Plus className="w-3.5 h-3.5" />
                    <span>Add Item</span>
                  </button>
                </div>
              </div>
            </div>

            {/* Payment Items Cart / DGV */}
            <div className="payment-cart-table-container border border-slate-200 rounded-xl overflow-hidden">
              <table className="payment-cart-table w-full text-left text-xs">
                <thead className="payment-cart-header bg-slate-100/80 text-[11px] text-slate-500 font-semibold border-b border-slate-200">
                  <tr>
                    <th className="payment-cart-th py-2 px-3">Payment For</th>
                    <th className="payment-cart-th py-2 px-3">Description</th>
                    <th className="payment-cart-th py-2 px-3 text-right">Amount</th>
                    <th className="payment-cart-th py-2 px-3 text-center w-12">Action</th>
                  </tr>
                </thead>
                <tbody className="payment-cart-body divide-y divide-slate-100">
                  {items.length === 0 ? (
                    <tr className="payment-cart-empty-row">
                      <td colSpan={4} className="payment-cart-empty-cell py-4 text-center text-slate-400">
                        No items added yet. Click "Add Item" above.
                      </td>
                    </tr>
                  ) : (
                    items.map((item) => (
                      <tr key={item.id} className="payment-cart-row hover:bg-slate-50">
                        <td className="payment-cart-cell py-2.5 px-3 font-semibold text-slate-800">
                          {item.paymentfor}
                        </td>
                        <td className="payment-cart-cell py-2.5 px-3 text-slate-600">
                          {item.description}
                        </td>
                        <td className="payment-cart-cell py-2.5 px-3 text-right font-bold text-slate-900">
                          {formatCurrency(item.amount)}
                        </td>
                        <td className="payment-cart-cell py-2.5 px-3 text-center">
                          <button
                            onClick={() => handleRemoveItem(item.id)}
                            className="payment-cart-remove-button p-1 text-slate-400 hover:text-rose-600 rounded transition-colors"
                          >
                            <Trash2 className="w-3.5 h-3.5" />
                          </button>
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>

              {/* Total Row */}
              <div className="payment-total-banner p-3 bg-slate-50/80 border-t border-slate-200 flex items-center justify-between">
                <span className="payment-total-label font-semibold text-slate-700">Total Payment Due:</span>
                <span className="payment-total-value text-base font-bold text-emerald-800">
                  {formatCurrency(totalAmount)}
                </span>
              </div>
            </div>

            {/* Buttons */}
            <div className="payment-actions-bar pt-2 flex items-center justify-end gap-2.5">
              <button
                type="button"
                onClick={onClose}
                className="payment-cancel-button px-4 py-2 bg-slate-100 hover:bg-slate-200/80 text-slate-700 font-semibold rounded-xl transition-colors cursor-pointer"
              >
                Cancel
              </button>
              <button
                type="button"
                onClick={handleSubmitPayment}
                disabled={items.length === 0}
                className="payment-submit-button px-5 py-2 bg-[#00593B] hover:bg-[#004a31] disabled:opacity-50 text-white font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
              >
                Accept Payment & Print Receipt
              </button>
            </div>
          </div>
        ) : (
          /* Printable Receipt View */
          <div className="payment-receipt-view p-6 space-y-5">
            <div className="payment-receipt-sheet p-6 border-2 border-dashed border-slate-300 rounded-2xl bg-white text-xs space-y-4">
              <div className="payment-receipt-header text-center pb-3 border-b border-slate-200">
                <h3 className="payment-receipt-company-title text-base font-bold text-slate-900 tracking-tight">JLD SUBDIVISION & REAL PROPERTY</h3>
                <p className="payment-receipt-doc-title text-[11px] text-slate-500">Official Payment Acknowledgement Receipt</p>
                <div className="payment-receipt-or-badge inline-block mt-2 bg-emerald-50 text-emerald-800 border border-emerald-200 px-2.5 py-0.5 rounded-full text-[11px] font-bold">
                  OR: {savedPayment?.orderreceipt}
                </div>
              </div>

              <div className="payment-receipt-metadata-grid grid grid-cols-2 gap-2 text-slate-600 text-xs">
                <div className="payment-receipt-meta-item"><span className="payment-receipt-meta-label font-semibold text-slate-800">Paid By:</span> {savedPayment?.paidbyName}</div>
                <div className="payment-receipt-meta-item"><span className="payment-receipt-meta-label font-semibold text-slate-800">Date:</span> {formatDate(savedPayment?.dateofpayment)}</div>
                <div className="payment-receipt-meta-item"><span className="payment-receipt-meta-label font-semibold text-slate-800">Payment Ref:</span> {savedPayment?.paymentref}</div>
                <div className="payment-receipt-meta-item"><span className="payment-receipt-meta-label font-semibold text-slate-800">Payment Mode:</span> {savedPayment?.paymenttype} {savedPayment?.referenceno ? `(${savedPayment.referenceno})` : ''}</div>
                <div className="payment-receipt-meta-item"><span className="payment-receipt-meta-label font-semibold text-slate-800">Cashier:</span> {savedPayment?.inchargebyName}</div>
              </div>

              <div className="payment-receipt-items-list border-t border-b border-slate-200 py-3 space-y-2">
                {savedPayment?.items.map(item => (
                  <div key={item.id} className="payment-receipt-item-row flex justify-between">
                    <div className="payment-receipt-item-info">
                      <span className="payment-receipt-item-for font-semibold text-slate-800">{item.paymentfor}</span>
                      <span className="payment-receipt-item-desc text-slate-500 block text-[11px]">{item.description}</span>
                    </div>
                    <span className="payment-receipt-item-amount font-bold text-slate-900">{formatCurrency(item.amount)}</span>
                  </div>
                ))}
              </div>

              <div className="payment-receipt-total-row flex justify-between items-center pt-1 text-sm font-bold text-slate-900">
                <span className="payment-receipt-total-label">TOTAL PAID:</span>
                <span className="payment-receipt-total-value text-emerald-800 text-base">{formatCurrency(savedPayment?.totalamount)}</span>
              </div>
            </div>

            <div className="payment-receipt-actions-bar flex items-center justify-end gap-2.5">
              <button
                onClick={() => window.print()}
                className="payment-receipt-print-button px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl flex items-center gap-1.5 transition-colors cursor-pointer"
              >
                <Printer className="w-3.5 h-3.5" />
                <span>Print OR</span>
              </button>
              <button
                onClick={() => {
                  setShowReceipt(false);
                  setItems([]);
                  onClose();
                }}
                className="payment-receipt-done-button px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
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




