import { 
  Product, 
  Client, 
  PurchaseDetail, 
  PaymentTransaction, 
  Agent, 
  Employee, 
  LoanRecord, 
  PayrollRecord, 
  PayslipRecord, 
  Expense, 
  GuardRailResult, 
  EntityTypeName 
} from '../types';
import { formatCurrency } from './calculations';

export function checkProductArchiveGuard(
  product: Product,
  purchases: PurchaseDetail[],
  payments: PaymentTransaction[]
): GuardRailResult {
  const activePurchases = purchases.filter(
    (p) => p.idproducts === product.idproduct && p.status === 'active'
  );

  if (activePurchases.length > 0) {
    return {
      allowed: false,
      blockingCount: activePurchases.length,
      reason: `Cannot archive ${product.code}: Project has ${activePurchases.length} active client lot purchase contract(s). Active buyer contracts must be transferred or resolved before archiving this subdivision inventory.`
    };
  }

  return { allowed: true };
}

export function checkClientArchiveGuard(
  client: Client,
  purchases: PurchaseDetail[],
  payments: PaymentTransaction[]
): GuardRailResult {
  const activePurchases = purchases.filter(
    (p) => p.idclients === client.idclients && p.status === 'active'
  );

  if (activePurchases.length > 0) {
    return {
      allowed: false,
      blockingCount: activePurchases.length,
      reason: `Cannot archive ${client.fullname || `${client.lastname}, ${client.firstname}`}: Client has ${activePurchases.length} active lot purchase contract(s). All active subdivision contracts must be settled or archived first.`
    };
  }

  return { allowed: true };
}

export function checkAgentArchiveGuard(
  agent: Agent,
  purchases: PurchaseDetail[]
): GuardRailResult {
  const activePurchases = purchases.filter(
    (p) => p.idagent === agent.id && p.status === 'active'
  );

  if (agent.balance > 0) {
    return {
      allowed: false,
      blockingCount: 1,
      reason: `Cannot archive ${agent.fullname}: Agent has an unclaimed commission balance of ${formatCurrency(agent.balance)}. Please release pending claims before archiving.`
    };
  }

  if (activePurchases.length > 0) {
    return {
      allowed: false,
      blockingCount: activePurchases.length,
      reason: `Cannot archive ${agent.fullname}: Agent has ${activePurchases.length} active sales accounts. Reassign agent accounts before archiving.`
    };
  }

  return { allowed: true };
}

export function checkEmployeeArchiveGuard(
  employee: Employee,
  payrollRecords: PayrollRecord[],
  loans: LoanRecord[],
  payslips: PayslipRecord[]
): GuardRailResult {
  const activeLoans = loans.filter(
    (l) => l.idemployee === employee.idemployee && l.status === 'active'
  );
  if (activeLoans.length > 0) {
    return {
      allowed: false,
      blockingCount: activeLoans.length,
      reason: `Cannot archive ${employee.fullname}: Employee has ${activeLoans.length} active cash advance or loan balance(s). Settle outstanding loans before archiving.`
    };
  }

  const pendingPayroll = payrollRecords.filter(
    (p) => p.idemployee === employee.idemployee && p.status === 'active' && p.approvalStatus === 'Pending'
  );
  if (pendingPayroll.length > 0) {
    return {
      allowed: false,
      blockingCount: pendingPayroll.length,
      reason: `Cannot archive ${employee.fullname}: Employee has pending payroll computations for the current cutoff.`
    };
  }

  return { allowed: true };
}

export function checkPurchaseArchiveGuard(
  purchase: PurchaseDetail,
  payments: PaymentTransaction[]
): GuardRailResult {
  // Purchases can be archived, but if they have outstanding penalties or active collections, we alert the user
  return { allowed: true };
}

export interface FullDatabaseState {
  products: Product[];
  clients: Client[];
  leads: PurchaseDetail[];
  payments: PaymentTransaction[];
  agents: Agent[];
  employees: Employee[];
  loans: LoanRecord[];
  benefits: LoanRecord[];
  payslips: PayslipRecord[];
  payrollRecords: PayrollRecord[];
  expenses: Expense[];
}

/**
 * Super-Admin permanent deletion safety check:
 * Completely blocks permanent deletion if ANY reference exists anywhere in the database,
 * whether active or archived, preserving financial integrity.
 */
export function checkPermanentDeleteIntegrity(
  entityType: EntityTypeName,
  recordId: string | number,
  db: FullDatabaseState
): GuardRailResult {
  const idNum = Number(recordId);

  switch (entityType) {
    case 'Product': {
      const linkedPurchases = db.leads.filter((l) => l.idproducts === idNum);
      if (linkedPurchases.length > 0) {
        return {
          allowed: false,
          blockingCount: linkedPurchases.length,
          reason: `Permanent deletion blocked: ${linkedPurchases.length} lot purchase contract(s) exist for this subdivision. Physical deletion violates ERP database foreign key integrity. Record must remain safely archived.`
        };
      }
      break;
    }
    case 'Stakeholder': {
      const linkedPurchases = db.leads.filter((l) => l.idclients === idNum);
      const linkedPayments = db.payments.filter((p) => p.paidby === idNum);
      const totalRefs = linkedPurchases.length + linkedPayments.length;
      if (totalRefs > 0) {
        return {
          allowed: false,
          blockingCount: totalRefs,
          reason: `Permanent deletion blocked: Client has ${linkedPurchases.length} contract(s) and ${linkedPayments.length} payment transaction(s) in historical records. Accounting regulations prohibit removing ledger participants.`
        };
      }
      break;
    }
    case 'Purchase': {
      const linkedPayments = db.payments.filter((p) =>
        p.items.some((i) => i.idpurchasedetails === idNum)
      );
      if (linkedPayments.length > 0) {
        return {
          allowed: false,
          blockingCount: linkedPayments.length,
          reason: `Permanent deletion blocked: This purchase contract has ${linkedPayments.length} official payment receipt(s) in the financial ledger. Hard deletion is strictly forbidden.`
        };
      }
      break;
    }
    case 'Payment': {
      // Payments are official OR ledger entries
      return {
        allowed: false,
        blockingCount: 1,
        reason: 'Permanent deletion blocked: Payment receipts and Official Receipts (OR) form part of the subdivision financial ledger and cannot be physically destroyed. They must remain archived with an audit trail.'
      };
    }
    case 'Agent': {
      const linkedPurchases = db.leads.filter((l) => l.idagent === idNum);
      if (linkedPurchases.length > 0) {
        return {
          allowed: false,
          blockingCount: linkedPurchases.length,
          reason: `Permanent deletion blocked: Agent has ${linkedPurchases.length} historical sales contracts in the ledger. Real estate records require preserving agent audit trails.`
        };
      }
      break;
    }
    case 'Employee': {
      const linkedPayroll = db.payrollRecords.filter((p) => p.idemployee === idNum);
      const linkedPayslips = db.payslips.filter((s) => s.idemployee === idNum);
      const linkedLoans = [...db.loans, ...db.benefits].filter((l) => l.idemployee === idNum);
      const linkedExpenses = db.expenses.filter(
        (e) => e.receiveby === idNum || e.releaseby === idNum
      );
      const total = linkedPayroll.length + linkedPayslips.length + linkedLoans.length + linkedExpenses.length;
      if (total > 0) {
        return {
          allowed: false,
          blockingCount: total,
          reason: `Permanent deletion blocked: Employee has ${total} linked payroll, payslip, loan, or expense voucher records. ERP payroll integrity prevents physical deletion.`
        };
      }
      break;
    }
    case 'Payroll':
    case 'Payslip': {
      return {
        allowed: false,
        blockingCount: 1,
        reason: 'Permanent deletion blocked: Payroll cutoff runs and payslips are statutory tax and labor compliance records. Hard delete is disabled.'
      };
    }
    case 'Expense': {
      return {
        allowed: false,
        blockingCount: 1,
        reason: 'Permanent deletion blocked: Expense disbursement vouchers are certified financial outflow records. They must remain safely archived.'
      };
    }
    case 'Loan':
    case 'Benefit': {
      const loan = db.loans.find((l) => l.id === idNum) || db.benefits.find((b) => b.id === idNum);
      if (loan && loan.amount > 0) {
        // If unreferenced, allow with confirmation
        break;
      }
      break;
    }
    default:
      break;
  }

  return { allowed: true };
}

