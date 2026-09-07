import React, { useState, useEffect } from 'react';
import { PurchaseDetail, PaymentTransaction, PaymentDetailItem, PaymentForType, PaymentMethodType } from '../../types';
import { generatePaymentRef, formatCurrency, formatDate } from '../../utils/calculations';
import { X, Plus, Trash2, CheckCircle2, Receipt, Printer } from 'lucide-react';

interface PaymentModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSavePayment: (payment: PaymentTransaction) => void;
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
  const [orNumber, setOrNumber] = useState<string>('OR-2026-8825');
  const [paymentType, setPaymentType] = useState<PaymentMethodType>('CASH');
  const [referenceNo, setReferenceNo] = useState<string>('');
  const [paymentRef] = useState<string>(() => generatePaymentRef());
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

  const currentApp = applications.find(a => a.id === Number(appId)) || applications[0];

  const handleAddItem = () => {
    if (itemAmount <= 0) return;

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
    if (items.length === 0) {
      alert('Please add at least one payment item.');
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
      items: items
    };

    onSavePayment(transaction);
    setSavedPayment(transaction);
    setShowReceipt(true);
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/40 backdrop-blur-xs p-4 overflow-y-auto">
      <div className="bg-white rounded-2xl max-w-2xl w-full border border-slate-200 shadow-xl overflow-hidden my-6 animate-in fade-in zoom-in-95 duration-150">
        {/* Modal Header */}
        <div className="px-6 py-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/70">
          <div className="flex items-center gap-2">
            <div className="w-8 h-8 rounded-lg bg-emerald-100 text-emerald-800 flex items-center justify-center">
              <Receipt className="w-4 h-4" />
            </div>
            <div>
              <h2 className="text-base font-bold text-slate-900">
                {showReceipt ? 'Official Payment Receipt' : 'Cashier Payment Collection'}
              </h2>
              <p className="text-xs text-slate-500">
                JLD RealProperty <code className="text-emerald-700 bg-emerald-50 px-1.5 py-0.5 rounded">frmpayments.cs</code>
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-1.5 rounded-lg text-slate-400 hover:text-slate-600 hover:bg-slate-100 transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {!showReceipt ? (
          /* Payment Processing Form */
          <div className="p-6 space-y-4 text-xs">
            {/* Payment Ref Bar */}
            <div className="bg-emerald-50/60 border border-emerald-200/80 rounded-xl px-4 py-2.5 flex items-center justify-between">
              <div>
                <span className="text-[10px] uppercase font-semibold text-emerald-800 block">Payment Reference Code</span>
                <span className="text-sm font-mono font-bold text-emerald-950">{paymentRef}</span>
              </div>
              <div className="text-right">
                <span className="text-[10px] uppercase font-semibold text-slate-500 block">In-Charge / Cashier</span>
                <span className="text-xs font-semibold text-slate-800">{inchargeByName}</span>
              </div>
            </div>

            {/* Target Account / Property Selection */}
            <div>
              <label className="block text-xs font-semibold text-slate-700 mb-1">
                Select Client & Subdivision Lot *
              </label>
              <select
                value={appId}
                onChange={(e) => {
                  const id = Number(e.target.value);
                  setAppId(id);
                  const selected = applications.find(a => a.id === id);
                  if (selected) {
                    setItemAmount(selected.amortization || 5000);
                    setItemDesc(`Amortization for ${selected.location} Blk ${selected.blockno} Lot ${selected.lotno}`);
                  }
                }}
                className="w-full bg-white border border-slate-200 rounded-xl px-3 py-2 text-xs text-slate-800 focus:border-emerald-600 outline-none"
              >
                {applications.map(a => (
                  <option key={a.id} value={a.id}>
                    {a.clientName} — {a.location} (Blk {a.blockno}, Lot {a.lotno}) — Amort: {formatCurrency(a.amortization)}/mo
                  </option>
                ))}
              </select>
            </div>

            {/* Payment Details: OR, Date, Method */}
            <div className="grid grid-cols-1 sm:grid-cols-3 gap-3">
              <div>
                <label className="block text-[11px] font-semibold text-slate-600 mb-1">
                  Official Receipt (OR No.) *
                </label>
                <input
                  type="text"
                  value={orNumber}
                  onChange={(e) => setOrNumber(e.target.value)}
                  className="w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-900 font-medium focus:border-emerald-600 outline-none"
                />
              </div>

              <div>
                <label className="block text-[11px] font-semibold text-slate-600 mb-1">
                  Date of Payment
                </label>
                <input
                  type="date"
                  value={dateOfPayment}
                  onChange={(e) => setDateOfPayment(e.target.value)}
                  className="w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
                />
              </div>

              <div>
                <label className="block text-[11px] font-semibold text-slate-600 mb-1">
                  Payment Method
                </label>
                <select
                  value={paymentType}
                  onChange={(e) => setPaymentType(e.target.value as PaymentMethodType)}
                  className="w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
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
              <div>
                <label className="block text-[11px] font-semibold text-slate-600 mb-1">
                  Bank / Transaction Reference Number *
                </label>
                <input
                  type="text"
                  value={referenceNo}
                  onChange={(e) => setReferenceNo(e.target.value)}
                  placeholder="e.g. BDO-TRX-10294 or GCash Ref 8820"
                  className="w-full bg-white border border-slate-200 rounded-lg px-2.5 py-1.5 text-xs text-slate-800 focus:border-emerald-600 outline-none"
                />
              </div>
            )}

            {/* Add Payment Item Line Bar */}
            <div className="p-3 bg-slate-50 rounded-xl border border-slate-200/80 space-y-2">
              <span className="text-[11px] font-bold text-slate-700 block">Add Payment Item</span>
              <div className="grid grid-cols-1 sm:grid-cols-4 gap-2">
                <div>
                  <select
                    value={paymentFor}
                    onChange={(e) => setPaymentFor(e.target.value as PaymentForType)}
                    className="w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs text-slate-800 outline-none"
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
                    placeholder="Amount"
                    value={itemAmount}
                    onChange={(e) => setItemAmount(Number(e.target.value))}
                    className="w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs font-semibold text-slate-900 outline-none"
                  />
                </div>
                <div>
                  <input
                    type="text"
                    placeholder="Description notes"
                    value={itemDesc}
                    onChange={(e) => setItemDesc(e.target.value)}
                    className="w-full bg-white border border-slate-200 rounded-lg px-2 py-1.5 text-xs text-slate-800 outline-none"
                  />
                </div>
                <div>
                  <button
                    type="button"
                    onClick={handleAddItem}
                    className="w-full py-1.5 bg-emerald-700 hover:bg-emerald-800 text-white font-semibold rounded-lg flex items-center justify-center gap-1 shadow-2xs transition-colors cursor-pointer"
                  >
                    <Plus className="w-3.5 h-3.5" />
                    <span>Add Item</span>
                  </button>
                </div>
              </div>
            </div>

            {/* Payment Items Cart / DGV */}
            <div className="border border-slate-200 rounded-xl overflow-hidden">
              <table className="w-full text-left text-xs">
                <thead className="bg-slate-100/80 text-[11px] text-slate-500 font-semibold border-b border-slate-200">
                  <tr>
                    <th className="py-2 px-3">Payment For</th>
                    <th className="py-2 px-3">Description</th>
                    <th className="py-2 px-3 text-right">Amount</th>
                    <th className="py-2 px-3 text-center w-12">Action</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                  {items.length === 0 ? (
                    <tr>
                      <td colSpan={4} className="py-4 text-center text-slate-400">
                        No items added yet. Click "Add Item" above.
                      </td>
                    </tr>
                  ) : (
                    items.map((item) => (
                      <tr key={item.id} className="hover:bg-slate-50">
                        <td className="py-2.5 px-3 font-semibold text-slate-800">
                          {item.paymentfor}
                        </td>
                        <td className="py-2.5 px-3 text-slate-600">
                          {item.description}
                        </td>
                        <td className="py-2.5 px-3 text-right font-bold text-slate-900">
                          {formatCurrency(item.amount)}
                        </td>
                        <td className="py-2.5 px-3 text-center">
                          <button
                            onClick={() => handleRemoveItem(item.id)}
                            className="p-1 text-slate-400 hover:text-rose-600 rounded transition-colors"
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
              <div className="p-3 bg-slate-50/80 border-t border-slate-200 flex items-center justify-between">
                <span className="font-semibold text-slate-700">Total Payment Due:</span>
                <span className="text-base font-bold text-emerald-800">
                  {formatCurrency(totalAmount)}
                </span>
              </div>
            </div>

            {/* Buttons */}
            <div className="pt-2 flex items-center justify-end gap-2.5">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 bg-slate-100 hover:bg-slate-200/80 text-slate-700 font-semibold rounded-xl transition-colors cursor-pointer"
              >
                Cancel
              </button>
              <button
                type="button"
                onClick={handleSubmitPayment}
                disabled={items.length === 0}
                className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] disabled:opacity-50 text-white font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
              >
                Accept Payment & Print Receipt
              </button>
            </div>
          </div>
        ) : (
          /* Printable Receipt View */
          <div className="p-6 space-y-5">
            <div className="p-6 border-2 border-dashed border-slate-300 rounded-2xl bg-white text-xs space-y-4">
              <div className="text-center pb-3 border-b border-slate-200">
                <h3 className="text-base font-bold text-slate-900 tracking-tight">JLD SUBDIVISION & REAL PROPERTY</h3>
                <p className="text-[11px] text-slate-500">Official Payment Acknowledgement Receipt</p>
                <div className="inline-block mt-2 bg-emerald-50 text-emerald-800 border border-emerald-200 px-2.5 py-0.5 rounded-full text-[11px] font-bold">
                  OR: {savedPayment?.orderreceipt}
                </div>
              </div>

              <div className="grid grid-cols-2 gap-2 text-slate-600 text-xs">
                <div><span className="font-semibold text-slate-800">Paid By:</span> {savedPayment?.paidbyName}</div>
                <div><span className="font-semibold text-slate-800">Date:</span> {formatDate(savedPayment?.dateofpayment)}</div>
                <div><span className="font-semibold text-slate-800">Payment Ref:</span> {savedPayment?.paymentref}</div>
                <div><span className="font-semibold text-slate-800">Payment Mode:</span> {savedPayment?.paymenttype} {savedPayment?.referenceno ? `(${savedPayment.referenceno})` : ''}</div>
                <div><span className="font-semibold text-slate-800">Cashier:</span> {savedPayment?.inchargebyName}</div>
              </div>

              <div className="border-t border-b border-slate-200 py-3 space-y-2">
                {savedPayment?.items.map(item => (
                  <div key={item.id} className="flex justify-between">
                    <div>
                      <span className="font-semibold text-slate-800">{item.paymentfor}</span>
                      <span className="text-slate-500 block text-[11px]">{item.description}</span>
                    </div>
                    <span className="font-bold text-slate-900">{formatCurrency(item.amount)}</span>
                  </div>
                ))}
              </div>

              <div className="flex justify-between items-center pt-1 text-sm font-bold text-slate-900">
                <span>TOTAL PAID:</span>
                <span className="text-emerald-800 text-base">{formatCurrency(savedPayment?.totalamount)}</span>
              </div>
            </div>

            <div className="flex items-center justify-end gap-2.5">
              <button
                onClick={() => window.print()}
                className="px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl flex items-center gap-1.5 transition-colors cursor-pointer"
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
                className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl shadow-sm transition-all cursor-pointer"
              >
                Done
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};
