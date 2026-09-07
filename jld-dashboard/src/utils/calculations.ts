/**
 * JLD Subdivision Amortization Calculation Formula (from RealProperty C# BAL/BEL)
 * 
 * Formula:
 * Interest = (LotPrice - Downpayment) * 0.15 * Terms (in years)
 * Total Financed = (LotPrice - Downpayment) + Interest
 * Monthly Amortization = Total Financed / (12 * Terms)
 */
export function calculateAmortization(lotPrice: number, downpayment: number, terms: number): number {
  if (terms <= 0 || lotPrice <= 0) return 0;
  const principal = Math.max(0, lotPrice - downpayment);
  if (principal === 0) return 0;
  
  // 15% interest rate per year over total loan term as implemented in JLD C#
  const interest = principal * 0.15 * terms;
  const totalPayable = principal + interest;
  const monthlyFee = totalPayable / (12 * terms);
  
  return Math.round(monthlyFee);
}

/**
 * Generates an alphanumeric Payment Reference code matching C# Payment.cs generateRefNo()
 */
export function generatePaymentRef(): string {
  const letters = ['0', '1', 'N', '3', 'V', 'E', 'R', 'S', 'A', 'L'];
  const now = new Date();
  const yearMonth = now.getFullYear().toString().slice(2) + String(now.getMonth() + 1).padStart(2, '0');
  
  let randomSuffix = '';
  for (let i = 0; i < 6; i++) {
    const idx = Math.floor(Math.random() * letters.length);
    randomSuffix += letters[idx];
  }
  
  return `${yearMonth}-${randomSuffix}`;
}

/**
 * Format numbers as Philippine Peso currency
 */
export function formatCurrency(amount: number | undefined | null): string {
  if (amount === undefined || amount === null || isNaN(amount)) return '₱0.00';
  return '₱' + Number(amount).toLocaleString('en-PH', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
}

/**
 * Format date string nicely (e.g., Sep 15, 2026)
 */
export function formatDate(dateStr: string | undefined): string {
  if (!dateStr) return 'N/A';
  try {
    const d = new Date(dateStr);
    return d.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  } catch {
    return dateStr;
  }
}

/**
 * Badge styling for statuses matching Optivox design
 */
export function getStatusStyle(status: string): { bg: string; text: string; border: string } {
  switch (status) {
    case 'New':
      return { bg: 'bg-blue-50', text: 'text-blue-600', border: 'border-blue-200' };
    case 'Demo Scheduled':
    case 'Reserved':
      return { bg: 'bg-purple-50', text: 'text-purple-600', border: 'border-purple-200' };
    case 'Negotiation':
    case 'Active':
      return { bg: 'bg-amber-50', text: 'text-amber-700', border: 'border-amber-200' };
    case 'Proposal Sent':
      return { bg: 'bg-cyan-50', text: 'text-cyan-700', border: 'border-cyan-200' };
    case 'Contacted':
      return { bg: 'bg-orange-50', text: 'text-orange-600', border: 'border-orange-200' };
    case 'Qualified':
    case 'Fully Paid':
      return { bg: 'bg-emerald-50', text: 'text-emerald-700', border: 'border-emerald-200' };
    case 'Overdue':
      return { bg: 'bg-rose-50', text: 'text-rose-600', border: 'border-rose-200' };
    default:
      return { bg: 'bg-slate-50', text: 'text-slate-600', border: 'border-slate-200' };
  }
}

/**
 * Score indicator styling (e.g. ↑ 92)
 */
export function getScoreStyle(score: number): { text: string; icon: string } {
  if (score >= 80) return { text: 'text-slate-900 font-semibold', icon: '↑' };
  if (score >= 65) return { text: 'text-slate-700 font-medium', icon: '↑' };
  return { text: 'text-slate-500 font-normal', icon: '↓' };
}

/**
 * Intent indicator styling (● High, ● Medium, ● Low)
 */
export function getIntentStyle(intent: string): { dot: string; text: string } {
  switch (intent) {
    case 'High':
      return { dot: 'text-emerald-500', text: 'text-slate-800' };
    case 'Medium':
      return { dot: 'text-amber-500', text: 'text-slate-700' };
    case 'Low':
      return { dot: 'text-rose-400', text: 'text-slate-500' };
    default:
      return { dot: 'text-slate-400', text: 'text-slate-600' };
  }
}

