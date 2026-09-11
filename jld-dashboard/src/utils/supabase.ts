import { createClient } from '@supabase/supabase-js';
import type { 
  Product, Client, Agent, PurchaseDetail, PaymentTransaction, 
  Expense, Employee, LoanRecord, PayrollRecord, PayslipRecord, AuditLogEntry 
} from '../types';

export const SUPABASE_URL = (import.meta.env.VITE_SUPABASE_URL as string) || 'https://mybeqgcbuksgwaojvlvx.supabase.co';
export const SUPABASE_ANON_KEY = (import.meta.env.VITE_SUPABASE_ANON_KEY as string) || 'sb_publishable_KZDYpfcIpFV59pqQD4OwQQ_Q2QIvbSt';

export const supabase = createClient(SUPABASE_URL, SUPABASE_ANON_KEY);

// Helper to handle queries safely, returning empty arrays if table is missing (PGRST205)
async function safeQuery<T>(query: PromiseLike<any>): Promise<T[]> {
  try {
    const { data, error } = await query;
    if (error) {
      if (error.code === 'PGRST205') return []; // Table doesn't exist yet
      console.error('Supabase query error:', error);
      return [];
    }
    return data || [];
  } catch (err) {
    console.error('Supabase unexpected error:', err);
    return [];
  }
}

// ---------------------------------------------------------
// DATA FETCHING (READ)
// ---------------------------------------------------------

export async function fetchAllData() {
  const [
    products, clients, agents, leads, payments, expenses, 
    employees, employee_loans, payrollRecords, payslips, auditLogs
  ] = await Promise.all([
    safeQuery<Product>(supabase.from('products').select('*').order('idproduct', { ascending: true })),
    safeQuery<Client>(supabase.from('clients').select('*').order('idclients', { ascending: true })),
    safeQuery<Agent>(supabase.from('agents').select('*').order('id', { ascending: true })),
    safeQuery<PurchaseDetail>(supabase.from('purchase_details').select('*').order('id', { ascending: true })),
    safeQuery<PaymentTransaction>(supabase.from('payment_transactions').select(`*, items:payment_items(*)`).order('id', { ascending: true })),
    safeQuery<Expense>(supabase.from('expenses').select('*').order('id', { ascending: true })),
    safeQuery<Employee>(supabase.from('employees').select('*').order('idemployee', { ascending: true })),
    safeQuery<LoanRecord>(supabase.from('employee_loans').select('*').order('id', { ascending: true })),
    safeQuery<PayrollRecord>(supabase.from('payroll_records').select('*').order('id', { ascending: true })),
    safeQuery<PayslipRecord>(supabase.from('payslips').select('*').order('id', { ascending: true })),
    safeQuery<AuditLogEntry>(supabase.from('audit_logs').select('*').order('timestamp', { ascending: false }))
  ]);

  const loans = employee_loans.filter(l => l.type === 'DEDUCTION');
  const benefits = employee_loans.filter(l => l.type === 'EARNING');

  return {
    products, clients, agents, leads, payments, expenses,
    employees, loans, benefits, payrollRecords, payslips, auditLogs
  };
}

// ---------------------------------------------------------
// MUTATIONS (WRITE)
// ---------------------------------------------------------

export async function saveProduct(product: Product | Omit<Product, 'idproduct'>) {
  if ('idproduct' in product && product.idproduct > 0) {
    return supabase.from('products').update(product).eq('idproduct', product.idproduct).select().single();
  }
  return supabase.from('products').insert(product).select().single();
}

export async function saveClient(client: Client | Omit<Client, 'idclients'>) {
  if ('idclients' in client && client.idclients > 0) {
    return supabase.from('clients').update(client).eq('idclients', client.idclients).select().single();
  }
  return supabase.from('clients').insert(client).select().single();
}

export async function saveAgent(agent: Agent | Omit<Agent, 'id'>) {
  if ('id' in agent && agent.id > 0) {
    return supabase.from('agents').update(agent).eq('id', agent.id).select().single();
  }
  return supabase.from('agents').insert(agent).select().single();
}

export async function savePurchaseDetail(lead: PurchaseDetail | Omit<PurchaseDetail, 'id'>) {
  if ('id' in lead && lead.id > 0) {
    return supabase.from('purchase_details').update(lead).eq('id', lead.id).select().single();
  }
  return supabase.from('purchase_details').insert(lead).select().single();
}

export async function savePayment(payment: PaymentTransaction | Omit<PaymentTransaction, 'id'>) {
  const { items, ...paymentData } = payment as any;
  let paymentResult;
  if ('id' in paymentData && paymentData.id > 0) {
    paymentResult = await supabase.from('payment_transactions').update(paymentData).eq('id', paymentData.id).select().single();
  } else {
    paymentResult = await supabase.from('payment_transactions').insert(paymentData).select().single();
  }
  if (paymentResult.error) return paymentResult;
  if (paymentResult.data && items && items.length > 0) {
    await supabase.from('payment_items').delete().eq('idpayment', paymentResult.data.id);
    const itemsToInsert = items.map((i: any) => ({ ...i, idpayment: paymentResult.data.id, id: undefined }));
    await supabase.from('payment_items').insert(itemsToInsert);
  }
  return paymentResult;
}

export async function saveExpense(expense: Expense | Omit<Expense, 'id'>) {
  if ('id' in expense && expense.id > 0) {
    return supabase.from('expenses').update(expense).eq('id', expense.id).select().single();
  }
  return supabase.from('expenses').insert(expense).select().single();
}

export async function saveEmployee(emp: Employee | Omit<Employee, 'idemployee'>) {
  if ('idemployee' in emp && emp.idemployee > 0) {
    return supabase.from('employees').update(emp).eq('idemployee', emp.idemployee).select().single();
  }
  return supabase.from('employees').insert(emp).select().single();
}

export async function saveEmployeeLoan(loan: LoanRecord | Omit<LoanRecord, 'id'>) {
  if ('id' in loan && loan.id > 0) {
    return supabase.from('employee_loans').update(loan).eq('id', loan.id).select().single();
  }
  return supabase.from('employee_loans').insert(loan).select().single();
}

export async function savePayroll(payroll: PayrollRecord | Omit<PayrollRecord, 'id'>) {
  if ('id' in payroll && payroll.id > 0) {
    return supabase.from('payroll_records').update(payroll).eq('id', payroll.id).select().single();
  }
  return supabase.from('payroll_records').insert(payroll).select().single();
}

export async function savePayslip(payslip: PayslipRecord | Omit<PayslipRecord, 'id'>) {
  if ('id' in payslip && payslip.id > 0) {
    return supabase.from('payslips').update(payslip).eq('id', payslip.id).select().single();
  }
  return supabase.from('payslips').insert(payslip).select().single();
}

export async function logAudit(entry: Omit<AuditLogEntry, 'id' | 'timestamp'>) {
  return supabase.from('audit_logs').insert(entry);
}
