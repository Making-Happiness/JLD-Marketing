export type EntityStatus = 'active' | 'archived';

export interface Client {
  idclients: number;
  firstname: string;
  lastname: string;
  middlename: string;
  gender: string;
  dateofbirth?: string;
  placeofbirth?: string;
  spousename?: string;
  contactno: string;
  fullname: string;
  email?: string;
  avatarUrl?: string;
  status: EntityStatus;
  deleted_at?: string | null;
}

export interface Product {
  idproduct: number;
  code: string;
  location: string;
  totalblockno: number;
  totallotno: number;
  totalarea: number;
  cashprice: number;
  availableLots?: number;
  projectPhase?: 'Open' | 'Nearly Sold' | 'Completed';
  status: EntityStatus;
  deleted_at?: string | null;
}

export type ApplicationStatus = 'New' | 'Demo Scheduled' | 'Negotiation' | 'Proposal Sent' | 'Contacted' | 'Qualified' | 'Active' | 'Overdue' | 'Fully Paid';
export type LeadIntent = 'High' | 'Medium' | 'Low';

export interface PurchaseDetail {
  id: number;
  blockno: number;
  lotno: number;
  area: number;
  lotprice: number;
  amortization: number;
  terms: number; // in years
  downpayment: number;
  agentpercentage: number;
  idclients: number;
  idproducts: number;
  idagent: number;
  remarks: string;
  recordstatus: string;
  otherfees: number;
  penalty: number;
  duedate: string;
  
  // Soft delete fields
  status: EntityStatus;
  deleted_at?: string | null;

  // Extended fields for SaaS CRM view
  clientName: string;
  clientRole?: string;
  clientAvatar?: string;
  productCode: string;
  location: string;
  agentName: string;
  agentAvatar?: string;
  leadStatus?: ApplicationStatus;
  score: number; // 0-100 CRM score
  intent: LeadIntent;
  source: string; // e.g. "Direct Client", "Google Ads", "Referral", "Site Visit", "Facebook"
  nextAction: string;
  aiRecommendation: string;
  dateApplied: string;
}

export type PaymentForType = 'RESERVED' | 'DOWN PAYMENT' | 'INSTALLMENT' | 'FULL PAYMENT';
export type PaymentMethodType = 'CASH' | 'CHEQUE' | 'BANK TRANSFER' | 'GCASH' | 'MAYA';

export interface PaymentDetailItem {
  id: number;
  idpurchasedetails: number;
  idpayment: number;
  paymentfor: PaymentForType;
  amount: number;
  description: string;
}

export interface PaymentTransaction {
  id: number;
  dateofpayment: string;
  orderreceipt: string; // OR Number
  referenceno: string;
  paymentref: string; // Auto-generated payment reference code
  paymenttype: PaymentMethodType;
  paidby: number; // Client ID
  paidbyName: string;
  inchargeby: number; // Area In-charge / Employee ID
  inchargebyName: string;
  recordedby: number;
  totalamount: number;
  recordstatus: string;
  status: EntityStatus;
  deleted_at?: string | null;
  items: PaymentDetailItem[];
}

export interface Agent {
  id: number;
  fullname: string;
  contactno: string;
  recordstatus: string;
  status: EntityStatus;
  deleted_at?: string | null;
  role: 'Senior Broker' | 'Sales Director' | 'Area Dicer' | 'Area Dicer Manager' | 'Property Specialist' | string;
  avatarUrl?: string;
  totalSales: number;
  commissionRate: number; // e.g. 7, 10
  totalEarned: number;
  totalClaimed: number;
  balance: number;
}

export interface Expense {
  id: number;
  receiveby: number;
  receivebyName: string;
  releaseby: number;
  releasebyName: string;
  description: string;
  purpose: string;
  amount: number;
  daterelease: string;
  remarks: string;
  status: EntityStatus;
  deleted_at?: string | null;
}

export interface SOAStatement {
  client: Client;
  purchase: PurchaseDetail;
  payments: PaymentDetailItem[];
  totalPriceWithFees: number;
  totalPaid: number;
  remainingBalance: number;
  nextDueDate: string;
  accountStatus: 'Up to date' | 'Overdue' | 'Cleared';
  status: EntityStatus;
  deleted_at?: string | null;
}

export interface Employee {
  idemployee: number;
  firstname: string;
  lastname: string;
  middlename: string;
  gender: string;
  dateofbirth: string;
  salary: number;
  designation: string;
  civilstatus: string;
  contactno: string;
  recordstatus: string;
  status: EntityStatus;
  deleted_at?: string | null;
  fullname: string;
}

export interface LoanRecord {
  id: number;
  idemployee: number;
  employeeName: string;
  dateapplied: string;
  description: string;
  category: 'CASH ADVANCE' | 'SALARY LOAN' | 'EMERGENCY LOAN' | 'ALLOWANCE' | 'BONUS' | 'COMMISSION INCENTIVE';
  type: 'DEDUCTION' | 'EARNING';
  amount: number;
  amortization: number;
  duedate?: string;
  remarks: string;
  recordstatus: string;
  status: EntityStatus;
  deleted_at?: string | null;
}

export interface PayslipRecord {
  id: number;
  idemployee: number;
  employeeName: string;
  designation: string;
  month: string;
  basicSalary: number;
  overtimeEarnings: number;
  benefits: number;
  cashAdvanceDeduction: number;
  otherDeductions: number;
  netPay: number;
  dateGenerated: string;
  payoutStatus: 'Draft' | 'Released' | 'Paid';
  status: EntityStatus;
  deleted_at?: string | null;
}

export interface PayrollRecord {
  id: number;
  idemployee: number;
  employeeName: string;
  designation: string;
  dailyRate: number;
  daysWorked: number;
  grossPay: number;
  cashAdvance: number;
  merienda: number;
  egg: number;
  rice: number;
  emergencyFund: number;
  undertime: number;
  otherDeductions: number;
  totalDeductions: number;
  netPay: number;
  period: string;
  approvalStatus: 'Pending' | 'Approved';
  status: EntityStatus;
  deleted_at?: string | null;
}

export type AuditAction = 'CREATE' | 'EDIT' | 'ARCHIVE' | 'RESTORE' | 'PERMANENT_DELETE';

export type EntityTypeName = 
  | 'Product' 
  | 'Stakeholder' 
  | 'Purchase' 
  | 'Payment' 
  | 'Agent' 
  | 'Employee' 
  | 'Loan' 
  | 'Benefit' 
  | 'Payroll' 
  | 'Payslip' 
  | 'Expense' 
  | 'SOA';

export interface AuditLogEntry {
  id: string;
  action: AuditAction;
  entityType: EntityTypeName;
  recordId: string | number;
  recordLabel: string;
  timestamp: string;
  performedBy: string;
  reason?: string;
  details?: string;
}

export interface GuardRailResult {
  allowed: boolean;
  reason?: string;
  blockingCount?: number;
}

