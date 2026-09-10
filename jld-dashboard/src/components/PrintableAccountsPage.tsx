import { useMemo, useState } from 'react';
import { jsPDF } from 'jspdf';
import { Download, FileText, Printer, Search, UserRound, FolderX } from 'lucide-react';
import type { PurchaseDetail, PaymentTransaction } from '../types';
import './modals/SOAModal.css';

export interface AccountRecord {
  account_id: string;
  sheet_name: string;
  account_category: string;
  reservation_date?: string | number;
  location_or_origin?: string;
  primary_client_name: string;
  secondary_client_or_note?: string;
  sqm?: string | number;
  sqm_raw?: string | number;
  block?: string | number;
  lot?: string | number;
  list_price?: string | number;
  down_payment?: string | number;
  stated_balance?: string | number;
  monthly_amortization?: string | number;
  payment_terms?: string | number;
  due_date?: string | number;
  [key: string]: any;
}

export interface PaymentRecord {
  payment_id?: string | number;
  account_id: string;
  payment_date?: string;
  amount?: string | number;
  payment_type?: string;
  or_ar_number?: string | number;
  raw_remark?: string;
  [key: string]: any;
}

export interface BalanceRecord {
  account_id: string;
  primary_client_name?: string;
  list_price?: string | number;
  down_payment?: string | number;
  calculated_balance?: string | number;
  stated_balance?: string | number;
  installment_total?: string | number;
  balance_discrepancy?: string | number;
  status?: string;
  [key: string]: any;
}

function compact(value?: string | number | null): string {
  return String(value ?? '').trim();
}

function display(value?: string | number | null): string {
  const result = compact(value);
  return result && result.toLowerCase() !== 'nan' ? result : '—';
}

function formatCurrencyDisplay(value?: string | number | null): string {
  const parsed = Number(String(value ?? '').replace(/,/g, ''));
  return Number.isFinite(parsed)
    ? `₱${parsed.toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
    : '—';
}

function formatCurrencyPdf(value?: string | number | null): string {
  const parsed = Number(String(value ?? '').replace(/,/g, ''));
  return Number.isFinite(parsed)
    ? `Php ${parsed.toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
    : '—';
}

function asCurrencyRaw(value?: string | number | null): string {
  const parsed = Number(String(value ?? '').replace(/,/g, ''));
  return Number.isFinite(parsed)
    ? parsed.toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
    : '—';
}

function readableDate(value?: string | number | null): string {
  const text = compact(value);
  if (!text) return '—';
  const date = new Date(`${text.slice(0, 10)}T00:00:00`);
  return Number.isNaN(date.getTime())
    ? text
    : date.toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' });
}

function matchesName(name: string, query: string): boolean {
  const tokens = query.toLocaleLowerCase().match(/[\p{L}\p{N}]+/gu) ?? [];
  if (!tokens.length) return true;
  const target = (name.toLocaleLowerCase().match(/[\p{L}\p{N}]+/gu) ?? []).join(' ');
  return tokens.every((token) => target.includes(token));
}

function accountLocation(account: AccountRecord): string {
  return compact(account.location_or_origin) || account.sheet_name || 'Subdivision Lot';
}

function downloadStatement(
  account: AccountRecord,
  payments: PaymentRecord[],
  balance?: BalanceRecord
): void {
  const doc = new jsPDF({ unit: 'pt', format: 'a4' });
  const pageWidth = doc.internal.pageSize.getWidth();
  const pageHeight = doc.internal.pageSize.getHeight();
  let y = 40;

  // Header monogram and company
  doc.setFillColor(52, 89, 55);
  doc.rect(40, y, 44, 44, 'F');
  doc.setTextColor(255, 255, 255);
  doc.setFont('helvetica', 'bold');
  doc.setFontSize(18);
  doc.text('JLD', 62, y + 28, { align: 'center' });

  doc.setTextColor(52, 89, 55);
  doc.setFontSize(16);
  doc.setFont('helvetica', 'bold');
  doc.text('JLD Private Markets', 96, y + 20);
  doc.setFontSize(9);
  doc.setFont('helvetica', 'normal');
  doc.setTextColor(100, 114, 96);
  doc.text('Prk. Pagkakaisa, Brgy. New Carmen, Tac. City', 96, y + 34);

  // Statement title
  doc.setFontSize(13);
  doc.setFont('helvetica', 'bold');
  doc.setTextColor(52, 89, 55);
  doc.text('STATEMENT OF ACCOUNT', pageWidth - 40, y + 20, { align: 'right' });
  doc.setFontSize(8.5);
  doc.setFont('helvetica', 'normal');
  doc.setTextColor(100, 114, 96);
  doc.text(
    `Date Issued: ${new Date().toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' })}`,
    pageWidth - 40,
    y + 34,
    { align: 'right' }
  );

  y += 52;
  doc.setDrawColor(128, 151, 122);
  doc.setLineWidth(1.5);
  doc.line(40, y, pageWidth - 40, y);
  y += 18;

  // Particulars Table
  const particulars: [string, string][] = [
    ['Vendee', display(account.primary_client_name)],
    ['Area Location', `${accountLocation(account)}${account.block ? ` · Block ${display(account.block)}, Lot ${display(account.lot)}` : ''}`],
    ['Area Size', compact(account.sqm) ? `${asCurrencyRaw(account.sqm)} sq.m.` : display(account.sqm_raw)],
    ['Lot Price', formatCurrencyPdf(account.list_price || balance?.list_price)],
    ['Monthly', formatCurrencyPdf(account.monthly_amortization)],
    ['Terms', display(account.payment_terms)],
    ['Due Date', readableDate(account.due_date)],
  ];

  doc.setFontSize(9);
  particulars.forEach(([label, val]) => {
    doc.setFillColor(243, 246, 238);
    doc.rect(40, y - 10, 110, 16, 'F');
    doc.setDrawColor(151, 169, 142);
    doc.setLineWidth(0.5);
    doc.rect(40, y - 10, 110, 16, 'S');
    doc.rect(150, y - 10, pageWidth - 190, 16, 'S');

    doc.setFont('helvetica', 'bold');
    doc.setTextColor(52, 89, 55);
    doc.text(label.toUpperCase(), 46, y + 1);

    doc.setFont('helvetica', 'normal');
    doc.setTextColor(36, 54, 47);
    doc.text(String(val), 156, y + 1);
    y += 16;
  });

  y += 14;

  // Ledger Table
  doc.setFont('helvetica', 'bold');
  doc.setFontSize(10);
  doc.setTextColor(52, 89, 55);
  doc.text('PAYMENT LEDGER', 40, y);
  y += 8;

  const colWidths = [110, 145, 120, 140];
  const colX = [40, 150, 295, 415];
  const tableWidth = pageWidth - 80;

  doc.setFillColor(243, 246, 238);
  doc.rect(40, y, tableWidth, 18, 'F');
  doc.setDrawColor(151, 169, 142);
  doc.rect(40, y, tableWidth, 18, 'S');

  doc.setFontSize(8);
  doc.setFont('helvetica', 'bold');
  doc.setTextColor(52, 89, 55);
  doc.text('DATE OF PAYMENT', colX[0] + 6, y + 12);
  doc.text('OR / AR #', colX[1] + 6, y + 12);
  doc.text('PAYMENT RELEASE', colX[2] + colWidths[2] - 6, y + 12, { align: 'right' });
  doc.text('BALANCE', colX[3] + colWidths[3] - 6, y + 12, { align: 'right' });
  y += 18;

  const listPriceNum = Number(account.list_price || balance?.list_price) || 0;
  const downPaymentNum = Number(account.down_payment || balance?.down_payment) || 0;
  const openingBalNum = Math.max(0, listPriceNum - downPaymentNum);
  let runBal = openingBalNum;
  let totalPaid = 0;

  doc.setFillColor(247, 249, 244);
  doc.rect(40, y, tableWidth, 16, 'F');
  doc.rect(40, y, tableWidth, 16, 'S');
  doc.setFont('helvetica', 'italic');
  doc.setFontSize(8);
  doc.setTextColor(96, 115, 91);
  doc.text(`Opening balance after down payment of ${formatCurrencyPdf(downPaymentNum)}`, colX[0] + 6, y + 11);
  doc.text('—', colX[2] + colWidths[2] - 6, y + 11, { align: 'right' });
  doc.setFont('helvetica', 'normal');
  doc.text(formatCurrencyPdf(openingBalNum), colX[3] + colWidths[3] - 6, y + 11, { align: 'right' });
  y += 16;

  const sorted = [...payments].sort((a, b) => (a.payment_date || '').localeCompare(b.payment_date || ''));
  doc.setFont('helvetica', 'normal');
  doc.setFontSize(8);

  if (!sorted.length) {
    doc.rect(40, y, tableWidth, 18, 'S');
    doc.text('No ledger payments recorded.', pageWidth / 2, y + 12, { align: 'center' });
    y += 18;
  } else {
    sorted.forEach((p) => {
      if (y > pageHeight - 90) {
        doc.addPage();
        y = 40;
      }
      const amt = Number(p.amount) || 0;
      totalPaid += amt;
      const pType = (p.payment_type || '').toUpperCase();
      const remark = (p.raw_remark || '').toUpperCase();
      if (!pType.includes('RESERV') && !remark.includes('RESERV') && !pType.includes('DOWN') && !remark.includes('DP')) {
        runBal = Math.max(0, runBal - amt);
      }

      doc.rect(40, y, tableWidth, 16, 'S');
      doc.setTextColor(36, 54, 47);
      doc.text(readableDate(p.payment_date), colX[0] + 6, y + 11);
      const receiptText = `${p.or_ar_number || '—'}${p.payment_type ? ` (${p.payment_type})` : ''}`;
      doc.text(receiptText.slice(0, 32), colX[1] + 6, y + 11);
      doc.text(formatCurrencyPdf(amt), colX[2] + colWidths[2] - 6, y + 11, { align: 'right' });
      doc.text(formatCurrencyPdf(runBal), colX[3] + colWidths[3] - 6, y + 11, { align: 'right' });
      y += 16;
    });
  }

  y += 10;
  const finalBalNum =
    balance?.calculated_balance !== undefined && balance?.calculated_balance !== ''
      ? Number(balance.calculated_balance)
      : runBal;

  doc.setFillColor(241, 237, 179);
  doc.rect(pageWidth - 280, y, 240, 36, 'F');
  doc.setDrawColor(151, 169, 142);
  doc.rect(pageWidth - 280, y, 240, 36, 'S');

  doc.setFontSize(8.5);
  doc.setFont('helvetica', 'normal');
  doc.setTextColor(52, 89, 55);
  doc.text('Total Payments:', pageWidth - 270, y + 15);
  doc.setFont('helvetica', 'bold');
  doc.text(formatCurrencyPdf(totalPaid), pageWidth - 50, y + 15, { align: 'right' });

  doc.setFont('helvetica', 'normal');
  doc.text('Ending Balance:', pageWidth - 270, y + 29);
  doc.setFont('helvetica', 'bold');
  doc.text(formatCurrencyPdf(finalBalNum), pageWidth - 50, y + 29, { align: 'right' });

  y += 48;
  doc.setFont('helvetica', 'normal');
  doc.setFontSize(7.5);
  doc.setTextColor(100, 114, 96);
  doc.text(
    'Note: Balance = lot price - contract down payment - installments and full settlements. Down-payment receipts are not deducted twice.',
    40,
    y
  );
  y += 36;

  doc.line(pageWidth - 200, y, pageWidth - 40, y);
  doc.setFont('helvetica', 'bold');
  doc.setFontSize(8.5);
  doc.setTextColor(52, 89, 55);
  doc.text('Person In-Charge', pageWidth - 120, y + 12, { align: 'center' });

  doc.save(`statement-${account.account_id}.pdf`);
}

export function PrintableAccountsPage({
  contracts = [],
  payments = []
}: {
  contracts?: PurchaseDetail[];
  payments?: PaymentTransaction[];
}) {
  // Convert live contracts from App state into AccountRecord objects
  const accounts: AccountRecord[] = useMemo(() => {
    return contracts.map((c) => ({
      account_id: `contract-${c.id}`,
      sheet_name: c.location || 'Subdivision',
      account_category: c.status === 'active' ? 'Active' : 'Archived',
      reservation_date: c.dateApplied || c.duedate,
      location_or_origin: c.location,
      primary_client_name: c.clientName,
      secondary_client_or_note: '',
      sqm: String(c.area),
      sqm_raw: String(c.area),
      block: String(c.blockno),
      lot: String(c.lotno),
      list_price: String(c.lotprice),
      down_payment: String(c.downpayment),
      stated_balance: String(Math.max(0, c.lotprice - c.downpayment)),
      monthly_amortization: String(c.amortization),
      payment_terms: `${c.terms} ${c.terms === 1 ? 'yr' : 'yrs'}`,
      due_date: c.duedate,
      _contract: c,
    }));
  }, [contracts]);

  const [query, setQuery] = useState('');
  const [selectedId, setSelectedId] = useState<string | null>(() => accounts[0]?.account_id ?? null);

  const matchingAccounts = useMemo(
    () => accounts.filter((account) => matchesName(account.primary_client_name, query)),
    [accounts, query]
  );

  const suggestions = useMemo(() => matchingAccounts.slice(0, 8), [matchingAccounts]);
  const visibleAccounts = matchingAccounts.slice(0, 50);
  const selected = accounts.find((account) => account.account_id === selectedId) ?? matchingAccounts[0] ?? null;

  // Payments for selected account
  const selectedPayments: PaymentRecord[] = useMemo(() => {
    if (!selected) return [];
    const contractId = selected._contract?.id;
    if (!contractId) return [];

    return payments
      .flatMap((p) =>
        p.items
          .filter((item) => item.idpurchasedetails === contractId)
          .map((item) => ({
            account_id: selected.account_id,
            payment_date: p.dateofpayment,
            amount: String(item.amount),
            payment_type: item.paymentfor,
            or_ar_number: p.orderreceipt,
            raw_remark: item.description,
          }))
      );
  }, [payments, selected]);

  const choose = (account: AccountRecord) => {
    setSelectedId(account.account_id);
    setQuery(account.primary_client_name);
  };

  const listPrice = Number(selected?.list_price || 0);
  const downPayment = Number(selected?.down_payment || 0);
  const openingBalance = Math.max(0, listPrice - downPayment);

  let runningBalance = openingBalance;
  let currentTotalPayments = 0;
  let currentDownPaymentReceived = 0;

  const sortedPayments = useMemo(() => {
    return [...selectedPayments].sort((a, b) => (a.payment_date || '').localeCompare(b.payment_date || ''));
  }, [selectedPayments]);

  const ledgerRows = sortedPayments.map((p, idx) => {
    const amount = Number(p.amount) || 0;
    currentTotalPayments += amount;
    const pType = (p.payment_type || '').toUpperCase();
    const remark = (p.raw_remark || '').toUpperCase();
    const isDown = pType.includes('DOWN') || remark.includes('DP') || remark.includes('DOWN');
    const isReserve = pType.includes('RESERV') || remark.includes('RESERV');

    if (isDown) {
      currentDownPaymentReceived += pType.includes('DOWN') || remark.includes('DP') || remark.includes('DOWN') ? amount : 0;
    } else if (!isReserve) {
      runningBalance = Math.max(0, runningBalance - amount);
    }

    return {
      id: `${p.account_id}-${idx}`,
      date: p.payment_date,
      receipt: p.or_ar_number || '—',
      type: p.payment_type || p.raw_remark || 'Installment',
      amount,
      balance: runningBalance,
    };
  });

  const finalBalance = runningBalance;
  const downPaymentOutstanding = Math.max(0, downPayment - currentDownPaymentReceived);

  // If no contracts/accounts exist in the database (clean empty state)
  if (!accounts.length) {
    return (
      <main className="printables-page accounting-page">
        <section className="page-heading">
          <div>
            <div className="eyebrow">PRINTABLE STATEMENTS</div>
            <h1>Printable accounts</h1>
            <p>Generate, print, and export official Statements of Account for buyer contracts.</p>
          </div>
        </section>

        <section className="surface" style={{ marginTop: '24px', padding: '60px 24px', textAlign: 'center' }}>
          <div style={{ maxWidth: '440px', margin: 'auto', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <div style={{ width: '56px', height: '56px', borderRadius: '50%', background: '#eaf2ec', display: 'grid', placeItems: 'center', marginBottom: '16px', color: '#145f49' }}>
              <FolderX size={28} />
            </div>
            <h3 style={{ fontSize: '18px', fontWeight: 600, color: '#1f3629', margin: '0 0 8px' }}>No accounts registered yet</h3>
            <p style={{ fontSize: '13px', color: '#687e71', lineHeight: 1.6, margin: '0 0 20px' }}>
              All previous demo records have been cleared. To view and print statements of account, add sales contracts in <strong>Property & Sales &rarr; Sales contracts</strong>, or import a database JSON file.
            </p>
          </div>
        </section>
      </main>
    );
  }

  return (
    <main className="printables-page accounting-page">
      <section className="page-heading">
        <div>
          <div className="eyebrow">PRINTABLE STATEMENTS</div>
          <h1>Printable accounts</h1>
          <p>Search a vendee, inspect the official Statement of Account, and print or convert to PDF.</p>
        </div>
        {selected && (
          <div className="page-actions" style={{ display: 'flex', gap: '10px' }}>
            <button
              type="button"
              className="primary-button"
              onClick={() => window.print()}
              title="Print Statement of Account"
            >
              <Printer size={16} /> Print statement
            </button>
            <button
              type="button"
              className="secondary-button"
              onClick={() => downloadStatement(selected, selectedPayments)}
              title="Convert Statement to PDF"
            >
              <Download size={16} /> Convert to PDF
            </button>
          </div>
        )}
      </section>

      <section className="printables-browser surface" aria-label="Search accounts">
        <div className="printables-search-wrap">
          <Search size={18} aria-hidden="true" />
          <input
            value={query}
            onChange={(event) => setQuery(event.target.value)}
            placeholder="Search: Firstname Lastname or Lastname, Firstname"
            aria-label="Search vendee name"
            autoComplete="off"
          />
        </div>
        {query && (
          <div className="printables-suggestions" role="listbox" aria-label="Matching vendee names">
            {suggestions.length ? (
              suggestions.map((account) => (
                <button
                  key={account.account_id}
                  role="option"
                  aria-selected={selectedId === account.account_id}
                  onClick={() => choose(account)}
                >
                  <UserRound size={15} />
                  <span>
                    <strong>{account.primary_client_name}</strong>
                    <small>
                      {accountLocation(account)} · Block {display(account.block)} · Lot {display(account.lot)}
                    </small>
                  </span>
                </button>
              ))
            ) : (
              <p>No vendee matches that name.</p>
            )}
          </div>
        )}
        <div className="printables-table-scroll">
          <table className="record-table">
            <thead>
              <tr>
                <th>Vendee</th>
                <th>Location</th>
                <th>Block / lot</th>
                <th>Terms</th>
                <th className="numeric">Balance</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {visibleAccounts.map((account) => {
                return (
                  <tr
                    key={account.account_id}
                    className={selectedId === account.account_id ? 'printables-selected-row' : ''}
                  >
                    <td>
                      <strong>{account.primary_client_name}</strong>
                      <small>{account.account_category}</small>
                    </td>
                    <td>{accountLocation(account)}</td>
                    <td>
                      {display(account.block)} / {display(account.lot)}
                    </td>
                    <td>{display(account.payment_terms)}</td>
                    <td className="numeric">
                      {formatCurrencyDisplay(account.stated_balance)}
                    </td>
                    <td className="row-actions">
                      <button className="table-action" onClick={() => choose(account)}>
                        Preview
                      </button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
        <footer className="records-footer">
          <span>
            Showing {visibleAccounts.length} of {matchingAccounts.length} account
            {matchingAccounts.length === 1 ? '' : 's'}.
          </span>
          <span>Live Database Contracts</span>
        </footer>
      </section>

      {selected && (
        <section className="statement-preview surface" aria-label="Printable preview" style={{ marginTop: '24px' }}>
          <header className="statement-preview-header soa-controls" style={{ padding: '16px 24px', borderBottom: '1px solid #e1e8e3', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
              <FileText size={20} color="#175c49" />
              <div>
                <span style={{ fontSize: '11px', letterSpacing: '1px', fontWeight: 650, color: '#175c49' }}>
                  PRINTABLE PREVIEW
                </span>
                <h2 style={{ fontSize: '18px', fontWeight: 620, margin: '2px 0 0', color: '#203a2f' }}>
                  Statement of Account
                </h2>
              </div>
            </div>
            <div className="soa-toolbar" style={{ display: 'flex', gap: '10px' }}>
              <button
                type="button"
                className="primary-button"
                onClick={() => window.print()}
                title="Print this Statement of Account"
              >
                <Printer size={16} /> Print statement
              </button>
              <button
                type="button"
                className="secondary-button"
                onClick={() => downloadStatement(selected, selectedPayments)}
                title="Download Statement of Account as PDF"
              >
                <Download size={16} /> Convert to PDF
              </button>
            </div>
          </header>

          <article className="jld-statement" aria-label="Printable statement of account">
            <header className="statement-letterhead">
              <strong className="statement-monogram">JLD</strong>
              <div>
                <strong>JLD Private Markets</strong>
                <p>Prk. Pagkakaisa, Brgy. New Carmen, Tac. City</p>
              </div>
            </header>

            <table className="statement-particulars">
              <tbody>
                <tr>
                  <th scope="row">Vendee</th>
                  <td>{display(selected.primary_client_name)}</td>
                </tr>
                <tr>
                  <th scope="row">Area location</th>
                  <td>
                    {accountLocation(selected)}
                    {selected.block ? ` · Block ${display(selected.block)}, Lot ${display(selected.lot)}` : ''}
                  </td>
                </tr>
                <tr>
                  <th scope="row">Area size</th>
                  <td>
                    {compact(selected.sqm) ? `${asCurrencyRaw(selected.sqm)} sq.m.` : display(selected.sqm_raw)}
                  </td>
                </tr>
                <tr>
                  <th scope="row">Lot price</th>
                  <td>{formatCurrencyDisplay(listPrice)}</td>
                </tr>
                <tr>
                  <th scope="row">Monthly</th>
                  <td>{formatCurrencyDisplay(selected.monthly_amortization)}</td>
                </tr>
                <tr>
                  <th scope="row">Terms</th>
                  <td>{display(selected.payment_terms)}</td>
                </tr>
                <tr>
                  <th scope="row">Due date</th>
                  <td>{readableDate(selected.due_date)}</td>
                </tr>
              </tbody>
            </table>

            <table className="statement-ledger">
              <thead>
                <tr>
                  <th scope="col">Date of payment</th>
                  <th scope="col">OR/AR #</th>
                  <th scope="col">Payment release</th>
                  <th scope="col">Balance</th>
                </tr>
              </thead>
              <tbody>
                <tr className="statement-opening">
                  <td colSpan={2}>
                    Opening balance after down payment of {formatCurrencyDisplay(downPayment)}
                  </td>
                  <td>—</td>
                  <td>{formatCurrencyDisplay(openingBalance)}</td>
                </tr>
                {ledgerRows.map((row) => (
                  <tr key={row.id}>
                    <td>{readableDate(row.date)}</td>
                    <td>
                      {row.receipt}
                      {row.type && <small>{row.type}</small>}
                    </td>
                    <td>{formatCurrencyDisplay(row.amount)}</td>
                    <td>{formatCurrencyDisplay(row.balance)}</td>
                  </tr>
                ))}
                {!ledgerRows.length && (
                  <tr>
                    <td colSpan={4} className="statement-empty">
                      No payments recorded.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>

            <footer className="statement-totals">
              <span>
                Date issued:{' '}
                <strong>
                  {new Date().toLocaleDateString('en-PH', {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                  })}
                </strong>
              </span>
              <div>
                <span>
                  Total payment <strong>{formatCurrencyDisplay(currentTotalPayments)}</strong>
                </span>
                <span>
                  Balance <strong>{formatCurrencyDisplay(finalBalance)}</strong>
                </span>
              </div>
            </footer>

            <div className="statement-notes">
              <p>
                Balance = lot price − contract down payment − installments and full settlements. Down-payment receipts
                are not deducted twice.
              </p>
              {downPaymentOutstanding > 0 && (
                <p>
                  Down payment awaiting receipts: <strong>{formatCurrencyDisplay(downPaymentOutstanding)}</strong>. The
                  opening credit is a contract amount, not a cash receipt.
                </p>
              )}
            </div>

            <div className="statement-signature">Person In-Charge</div>
          </article>
        </section>
      )}
    </main>
  );
}
