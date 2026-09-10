import {
  Client,
  Product,
  PurchaseDetail,
  Agent,
  PaymentTransaction,
  Expense,
  Employee,
  LoanRecord,
  PayslipRecord,
  PayrollRecord,
  AuditLogEntry
} from '../types/index.ts';
import type { FullDatabaseState } from '../utils/guardRails.ts';

export const INITIAL_PRODUCTS: Product[] = [];
export const INITIAL_CLIENTS: Client[] = [];
export const INITIAL_AGENTS: Agent[] = [];
export const INITIAL_PURCHASE_DETAILS: PurchaseDetail[] = [];
export const INITIAL_PAYMENTS: PaymentTransaction[] = [];
export const INITIAL_EMPLOYEES: Employee[] = [];
export const INITIAL_LOANS: LoanRecord[] = [];
export const INITIAL_BENEFITS: LoanRecord[] = [];
export const INITIAL_PAYROLL: PayrollRecord[] = [];
export const INITIAL_PAYSLIPS: PayslipRecord[] = [];
export const INITIAL_EXPENSES: Expense[] = [];
export const INITIAL_AUDIT_LOGS: AuditLogEntry[] = [];

export function getDefaultFullState(): FullDatabaseState & { auditLogs: AuditLogEntry[] } {
  return {
    products: [],
    clients: [],
    agents: [],
    leads: [],
    payments: [],
    employees: [],
    loans: [],
    benefits: [],
    payrollRecords: [],
    payslips: [],
    expenses: [],
    auditLogs: []
  };
}
