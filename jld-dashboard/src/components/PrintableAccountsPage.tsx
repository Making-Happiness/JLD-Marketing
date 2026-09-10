import { useEffect, useMemo, useState } from 'react';
import Papa from 'papaparse';
import { jsPDF } from 'jspdf';
import { Download, FileText, Search, UserRound } from 'lucide-react';

type Account = {
  account_id: string;
  sheet_name: string;
  account_category: string;
  reservation_date?: string;
  location_or_origin?: string;
  primary_client_name: string;
  secondary_client_or_note?: string;
  sqm?: string;
  sqm_raw?: string;
  block?: string;
  lot?: string;
  list_price?: string;
  monthly_amortization?: string;
  payment_terms?: string;
  due_date?: string;
};

type Payment = {
  account_id: string;
  payment_date?: string;
  amount?: string;
  payment_type?: string;
  or_ar_number?: string;
  raw_remark?: string;
};

type Balance = {
  account_id: string;
  calculated_balance?: string;
  stated_balance?: string;
  installment_total?: string;
  status?: string;
};

const CSV_BASE = `${import.meta.env.BASE_URL}data/`;

function readCsv<T>(filename: string): Promise<T[]> {
  return new Promise((resolve, reject) => {
    Papa.parse<T>(CSV_BASE + filename, {
      download: true,
      header: true,
      skipEmptyLines: true,
      complete: (result) => {
        if (result.errors.length) {
          reject(new Error(`Unable to read ${filename}: ${result.errors[0].message}`));
          return;
        }
        resolve(result.data);
      },
      error: (error) => reject(error),
    });
  });
}

function compact(value?: string | number | null): string {
  return String(value ?? '').trim();
}

function display(value?: string | number | null): string {
  const result = compact(value);
  return result && result.toLowerCase() !== 'nan' ? result : '—';
}

function asCurrency(value?: string | number | null): string {
  const parsed = Number(String(value ?? '').replace(/,/g, ''));
  return Number.isFinite(parsed) ? parsed.toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '—';
}

function readableDate(value?: string): string {
  const text = compact(value);
  if (!text) return '—';
  const date = new Date(`${text}T00:00:00`);
  return Number.isNaN(date.getTime()) ? text : date.toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' });
}

/** Supports First Last, Last, First, punctuation, and partial token searches. */
function matchesName(name: string, query: string): boolean {
  const tokens = query.toLocaleLowerCase().match(/[\p{L}\p{N}]+/gu) ?? [];
  if (!tokens.length) return true;
  const target = (name.toLocaleLowerCase().match(/[\p{L}\p{N}]+/gu) ?? []).join(' ');
  return tokens.every((token) => target.includes(token));
}

function accountLocation(account: Account): string {
  return compact(account.location_or_origin) || account.sheet_name;
}

function pdfLine(document: jsPDF, label: string, value: string, x: number, y: number): number {
  document.setFont('helvetica', 'bold');
  document.text(`${label}:`, x, y);
  document.setFont('helvetica', 'normal');
  const lines = document.splitTextToSize(value, 265);
  document.text(lines, x + 100, y);
  return y + Math.max(18, lines.length * 14);
}

function downloadStatement(account: Account, payments: Payment[], balance?: Balance): void {
  const document = new jsPDF({ unit: 'pt', format: 'a4' });
  const pageWidth = document.internal.pageSize.getWidth();
  const pageHeight = document.internal.pageSize.getHeight();
  let y = 54;

  document.setFillColor(20, 95, 73);
  document.rect(0, 0, pageWidth, 86, 'F');
  document.setTextColor(255, 255, 255);
  document.setFont('helvetica', 'bold');
  document.setFontSize(20);
  document.text('STATEMENT OF ACCOUNT', pageWidth / 2, 38, { align: 'center' });
  document.setFont('helvetica', 'normal');
  document.setFontSize(9);
  document.text(`Date Issued: ${new Date().toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' })}`, pageWidth / 2, 59, { align: 'center' });
  document.setTextColor(31, 41, 55);
  document.setFontSize(10);
  y = 112;

  const fields: [string, string][] = [
    ['Vendee Name', display(account.primary_client_name)],
    ['Area Location', display(accountLocation(account))],
    ['Area Size', compact(account.sqm) ? `${asCurrency(account.sqm)} sq.m.` : display(account.sqm_raw)],
    ['Lot Price', asCurrency(account.list_price)],
    ['Monthly Payment', asCurrency(account.monthly_amortization)],
    ['Terms', display(account.payment_terms)],
    ['Due Date', readableDate(account.due_date)],
    ['Balance', asCurrency(balance?.calculated_balance ?? balance?.stated_balance)],
  ];
  fields.forEach(([label, value]) => { y = pdfLine(document, label, value, 48, y); });

  y += 12;
  document.setFont('helvetica', 'bold');
  document.setFontSize(12);
  document.text('Payment History', 48, y);
  y += 20;
  document.setFontSize(8);
  document.setFillColor(237, 246, 241);
  document.rect(48, y - 13, pageWidth - 96, 20, 'F');
  document.setFont('helvetica', 'bold');
  document.text('Date paid', 54, y);
  document.text('OR/AR #', 148, y);
  document.text('Payment release', 224, y);
  document.text('Type / note', 340, y);
  y += 18;
  document.setFont('helvetica', 'normal');

  const rows = payments.length ? payments : [{} as Payment];
  rows.forEach((payment, index) => {
    if (y > pageHeight - 50) {
      document.addPage();
      y = 52;
    }
    const note = payment.raw_remark || payment.payment_type || (index === 0 ? 'No ledger payments recorded' : '');
    document.text(readableDate(payment.payment_date), 54, y);
    document.text(display(payment.or_ar_number), 148, y);
    document.text(asCurrency(payment.amount), 224, y);
    document.text(document.splitTextToSize(display(note), 190), 340, y);
    y += Math.max(17, document.splitTextToSize(display(note), 190).length * 11 + 4);
  });
  document.setFontSize(9);
  document.setFont('helvetica', 'bold');
  document.text(`Total installment payments: ${asCurrency(balance?.installment_total)}`, pageWidth - 48, y + 15, { align: 'right' });
  document.text(`Balance: ${asCurrency(balance?.calculated_balance ?? balance?.stated_balance)}`, pageWidth - 48, y + 31, { align: 'right' });
  document.save(`statement-${account.account_id}.pdf`);
}

export function PrintableAccountsPage() {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [payments, setPayments] = useState<Payment[]>([]);
  const [balances, setBalances] = useState<Balance[]>([]);
  const [query, setQuery] = useState('');
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    Promise.all([
      readCsv<Account>('dim_accounts.csv'),
      readCsv<Payment>('fact_payments.csv'),
      readCsv<Balance>('account_balance_verification.csv'),
    ])
      .then(([loadedAccounts, loadedPayments, loadedBalances]) => {
        setAccounts(loadedAccounts.filter((account) => compact(account.primary_client_name)));
        setPayments(loadedPayments);
        setBalances(loadedBalances);
        setSelectedId(loadedAccounts.find((account) => compact(account.primary_client_name))?.account_id ?? null);
      })
      .catch((loadError: Error) => setError(loadError.message))
      .finally(() => setLoading(false));
  }, []);

  const matchingAccounts = useMemo(
    () => accounts.filter((account) => matchesName(account.primary_client_name, query)),
    [accounts, query],
  );
  const suggestions = useMemo(() => matchingAccounts.slice(0, 8), [matchingAccounts]);
  const visibleAccounts = matchingAccounts.slice(0, 50);
  const selected = accounts.find((account) => account.account_id === selectedId) ?? matchingAccounts[0] ?? null;
  const selectedPayments = useMemo(
    () => selected ? payments.filter((payment) => payment.account_id === selected.account_id) : [],
    [payments, selected],
  );
  const selectedBalance = selected ? balances.find((balance) => balance.account_id === selected.account_id) : undefined;

  const choose = (account: Account) => {
    setSelectedId(account.account_id);
    setQuery(account.primary_client_name);
  };

  return (
    <main className="printables-page accounting-page">
      <section className="page-heading">
        <div>
          <div className="eyebrow">RECOVERED WORKBOOK DATA</div>
          <h1>Printable account statements</h1>
          <p>Search a vendee, inspect the final document, then download the statement as a PDF.</p>
        </div>
        {selected && (
          <button className="primary-button" onClick={() => downloadStatement(selected, selectedPayments, selectedBalance)}>
            <Download size={16} /> Convert to PDF
          </button>
        )}
      </section>

      {error && <div className="printables-error" role="alert">{error}</div>}
      {loading && <div className="printables-loading">Loading normalized CSV data…</div>}

      {!loading && !error && <>
        <section className="printables-browser surface" aria-label="Search recovered accounts">
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
          {query && <div className="printables-suggestions" role="listbox" aria-label="Matching vendee names">
            {suggestions.length ? suggestions.map((account) => (
              <button key={account.account_id} role="option" aria-selected={selectedId === account.account_id} onClick={() => choose(account)}>
                <UserRound size={15} /><span><strong>{account.primary_client_name}</strong><small>{accountLocation(account)} · Block {display(account.block)} · Lot {display(account.lot)}</small></span>
              </button>
            )) : <p>No vendee matches that name.</p>}
          </div>}
          <div className="printables-table-scroll">
            <table className="record-table">
              <thead><tr><th>Vendee</th><th>Location</th><th>Block / lot</th><th>Terms</th><th className="numeric">Balance</th><th /></tr></thead>
              <tbody>
                {visibleAccounts.map((account) => {
                  const balance = balances.find((item) => item.account_id === account.account_id);
                  return <tr key={account.account_id} className={selectedId === account.account_id ? 'printables-selected-row' : ''}>
                    <td><strong>{account.primary_client_name}</strong><small>{account.account_category}</small></td>
                    <td>{accountLocation(account)}</td>
                    <td>{display(account.block)} / {display(account.lot)}</td>
                    <td>{display(account.payment_terms)}</td>
                    <td className="numeric">{asCurrency(balance?.calculated_balance ?? balance?.stated_balance)}</td>
                    <td className="row-actions"><button className="table-action" onClick={() => choose(account)}>Preview</button></td>
                  </tr>;
                })}
                {!visibleAccounts.length && <tr><td colSpan={6} className="record-empty">No account records match this search.</td></tr>}
              </tbody>
            </table>
          </div>
          <footer className="records-footer"><span>Showing {visibleAccounts.length} of {matchingAccounts.length} matching account{matchingAccounts.length === 1 ? '' : 's'}.</span><span>CSV source: normalized exports</span></footer>
        </section>

        {selected && <section className="statement-preview" aria-label="Printable preview">
          <header className="statement-preview-header">
            <FileText size={22} /><div><span>PRINTABLE PREVIEW</span><h2>Statement of Account</h2></div><p>Date issued: {new Date().toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' })}</p>
          </header>
          <div className="statement-preview-body">
            <dl className="statement-details">
              <div><dt>Vendee Name</dt><dd>{display(selected.primary_client_name)}</dd></div>
              <div><dt>Area Location</dt><dd>{display(accountLocation(selected))}</dd></div>
              <div><dt>Area Size</dt><dd>{compact(selected.sqm) ? `${asCurrency(selected.sqm)} sq.m.` : display(selected.sqm_raw)}</dd></div>
              <div><dt>Lot Price</dt><dd>{asCurrency(selected.list_price)}</dd></div>
              <div><dt>Monthly Payment</dt><dd>{asCurrency(selected.monthly_amortization)}</dd></div>
              <div><dt>Terms</dt><dd>{display(selected.payment_terms)}</dd></div>
              <div><dt>Due Date</dt><dd>{readableDate(selected.due_date)}</dd></div>
              <div><dt>Balance</dt><dd>{asCurrency(selectedBalance?.calculated_balance ?? selectedBalance?.stated_balance)}</dd></div>
            </dl>
            <div className="statement-payments">
              <h3>Payment history</h3>
              <div className="printables-table-scroll"><table>
                <thead><tr><th>Date of Payment</th><th>OR/AR #</th><th>Payment Release</th><th>Type / Note</th></tr></thead>
                <tbody>{selectedPayments.length ? selectedPayments.map((payment, index) => <tr key={`${payment.account_id}-${index}`}><td>{readableDate(payment.payment_date)}</td><td>{display(payment.or_ar_number)}</td><td className="numeric">{asCurrency(payment.amount)}</td><td>{display(payment.raw_remark || payment.payment_type)}</td></tr>) : <tr><td colSpan={4}>No ledger payments recorded.</td></tr>}</tbody>
              </table></div>
            </div>
            <div className="statement-totals"><span>Payment Release Total <strong>{asCurrency(selectedBalance?.installment_total)}</strong></span><span>Calculated Balance <strong>{asCurrency(selectedBalance?.calculated_balance)}</strong></span><span>Stated Balance <strong>{asCurrency(selectedBalance?.stated_balance)}</strong></span></div>
          </div>
        </section>}
      </>}
    </main>
  );
}
