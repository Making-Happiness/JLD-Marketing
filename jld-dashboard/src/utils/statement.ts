import type { PurchaseDetail, PaymentTransaction } from '../types';

const cents = (value: number) => {
  if (!Number.isFinite(value)) throw new Error('Account amounts must be valid numbers.');
  return Math.round(value * 100);
};

/** Printable Accounts: price less contractual DP less installments.
 * DP receipts confirm the opening credit; reservations remain unapplied.
 * Full settlements reduce principal. Archiving is not a cash reversal.
 */
export function calculateStatement(contract: PurchaseDetail, payments: PaymentTransaction[]) {
  const opening = cents(contract.lotprice) - cents(contract.downpayment);
  let balance = opening, paid = 0, downPaymentReceived = 0, reserved = 0;
  const rows = payments.flatMap(payment => payment.items
    .filter(item => item.idpurchasedetails === contract.id)
    .map(item => ({ id: `${payment.id}-${item.id}`, paymentId: payment.id, itemId: item.id,
      date: payment.dateofpayment, receipt: payment.orderreceipt, type: item.paymentfor, amount: item.amount })))
    .sort((a, b) => a.date.localeCompare(b.date) || a.paymentId - b.paymentId || a.itemId - b.itemId)
    .map(row => {
      const amount = cents(row.amount);
      paid += amount;
      if (row.type === 'INSTALLMENT' || row.type === 'FULL PAYMENT') balance -= amount;
      if (row.type === 'DOWN PAYMENT') downPaymentReceived += amount;
      if (row.type === 'RESERVED') reserved += amount;
      return { ...row, balance: balance / 100 };
    });
  return { rows, openingBalance: opening / 100, balance: balance / 100,
    totalPayments: paid / 100, downPaymentReceived: downPaymentReceived / 100,
    downPaymentOutstanding: (cents(contract.downpayment) - downPaymentReceived) / 100,
    unappliedReservations: reserved / 100 };
}

export function validateReceipt(contract: PurchaseDetail, existing: PaymentTransaction[], receipt: PaymentTransaction) {
  const statement = calculateStatement(contract, [...existing, receipt]);
  if (statement.balance < 0) return 'Payment exceeds the remaining lot balance. Review the statement first.';
  if (statement.downPaymentOutstanding < 0) return 'Down-payment receipts exceed the contract down payment. Allocate the excess as an installment.';
}
