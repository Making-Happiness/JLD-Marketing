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

export const INITIAL_PRODUCTS: Product[] = [
  {
    idproduct: 1,
    code: 'JLD-STN',
    location: 'Sto. Niño (Tupaz Subdivision)',
    totalblockno: 12,
    totallotno: 120,
    totalarea: 15000,
    cashprice: 108000,
    availableLots: 120,
    projectPhase: 'Open',
    status: 'active',
    deleted_at: null
  },
  {
    idproduct: 2,
    code: 'JLD-NOR1',
    location: 'Norala (Frondozo-Famulag Phase 1)',
    totalblockno: 10,
    totallotno: 170,
    totalarea: 28000,
    cashprice: 94200,
    availableLots: 170,
    projectPhase: 'Open',
    status: 'active',
    deleted_at: null
  },
  {
    idproduct: 3,
    code: 'JLD-NOR2',
    location: 'Norala (Suganob-Siauso Phase 2)',
    totalblockno: 8,
    totallotno: 95,
    totalarea: 18500,
    cashprice: 150000,
    availableLots: 95,
    projectPhase: 'Open',
    status: 'active',
    deleted_at: null
  },
  {
    idproduct: 4,
    code: 'JLD-TUP3',
    location: 'Tupi Commercial & Residential Phase 3',
    totalblockno: 15,
    totallotno: 150,
    totalarea: 32000,
    cashprice: 220000,
    availableLots: 150,
    projectPhase: 'Open',
    status: 'active',
    deleted_at: null
  },
  {
    idproduct: 5,
    code: 'JLD-SUR1',
    location: 'Surallah (Commercial Phase 1)',
    totalblockno: 6,
    totallotno: 60,
    totalarea: 12000,
    cashprice: 180000,
    availableLots: 0,
    projectPhase: 'Completed',
    status: 'archived',
    deleted_at: '2026-08-10T14:30:00Z'
  }
];

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
    products: INITIAL_PRODUCTS,
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
