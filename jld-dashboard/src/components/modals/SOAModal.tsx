import { ModalFrame } from './ModalFrame';
import type { PurchaseDetail, PaymentTransaction } from '../../types';
import { formatCurrency, formatDate } from '../../utils/calculations';
import { calculateStatement } from '../../utils/statement';
import { X, Printer } from 'lucide-react';
import './SOAModal.css';

export function SOAModal({ isOpen, onClose, application, payments }: {
  isOpen: boolean; onClose: () => void; application: PurchaseDetail | null; payments: PaymentTransaction[];
}) {
  if (!isOpen || !application) return null;
  const account = calculateStatement(application, payments);
  const fields = [
    ['Vendee', application.clientName],
    ['Area location', `${application.location} · Block ${application.blockno}, Lot ${application.lotno}`],
    ['Area size', `${application.area.toLocaleString()} sq. m.`],
    ['Lot price', formatCurrency(application.lotprice)],
    ['Monthly', formatCurrency(application.amortization)],
    ['Terms', `${application.terms} ${application.terms === 1 ? 'year' : 'years'}`],
    ['Due date', formatDate(application.duedate)],
  ];
  return <ModalFrame onClose={onClose} title="Statement of account">
    <div className="form-shell form-shell-wide soa-shell">
      <header className="form-header soa-controls">
        <h2>Statement of account</h2>
        <div className="soa-toolbar">
          <button className="primary-button" onClick={() => window.print()}><Printer size={16}/> Print statement</button>
          <button className="form-close" onClick={onClose} aria-label="Close statement"><X size={20}/></button>
        </div>
      </header>
      <article className="jld-statement" aria-label="Printable statement of account">
        <header className="statement-letterhead">
          <strong className="statement-monogram">JLD</strong>
          <div><strong>JLD Private Markets</strong><p>Prk. Pagkakaisa, Brgy. New Carmen, Tac. City</p></div>
        </header>
        <table className="statement-particulars"><tbody>{fields.map(([label, value]) =>
          <tr key={label}><th scope="row">{label}</th><td>{value}</td></tr>
        )}</tbody></table>
        <table className="statement-ledger">
          <thead><tr><th scope="col">Date of payment</th><th scope="col">OR/AR #</th><th scope="col">Payment release</th><th scope="col">Balance</th></tr></thead>
          <tbody>
            <tr className="statement-opening"><td colSpan={2}>Opening balance after down payment of {formatCurrency(application.downpayment)}</td><td>—</td><td>{formatCurrency(account.openingBalance)}</td></tr>
            {account.rows.map(row => <tr key={row.id}>
              <td>{formatDate(row.date)}</td><td>{row.receipt || '—'}
                {row.type === 'DOWN PAYMENT' && <small>Down payment · opening credit</small>}
                {row.type === 'RESERVED' && <small>Reservation · unapplied</small>}
              </td><td>{formatCurrency(row.amount)}</td><td>{formatCurrency(row.balance)}</td>
            </tr>)}
            {!account.rows.length && <tr><td colSpan={4} className="statement-empty">No payments recorded.</td></tr>}
          </tbody>
        </table>
        <footer className="statement-totals"><span>Date issued: <strong>{formatDate(new Date().toISOString())}</strong></span><div><span>Total payment <strong>{formatCurrency(account.totalPayments)}</strong></span><span>Balance <strong>{formatCurrency(account.balance)}</strong></span></div></footer>
        <div className="statement-notes">
          <p>Balance = lot price − contract down payment − installments and full settlements. Down-payment receipts are not deducted twice.</p>
          {account.downPaymentOutstanding > 0 && <p>Down payment awaiting receipts: <strong>{formatCurrency(account.downPaymentOutstanding)}</strong>. The opening credit is a contract amount, not a cash receipt.</p>}
          {account.unappliedReservations > 0 && <p>Unapplied reservation payments: {formatCurrency(account.unappliedReservations)}.</p>}
          {(application.otherfees > 0 || application.penalty > 0) && <p>Separate charges, excluded from this lot balance: fees {formatCurrency(application.otherfees)}; penalties {formatCurrency(application.penalty)}.</p>}
        </div>
        <div className="statement-signature">Person In-Charge</div>
      </article>
    </div>
  </ModalFrame>;
}
