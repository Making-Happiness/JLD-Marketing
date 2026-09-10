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

