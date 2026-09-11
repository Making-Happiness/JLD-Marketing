-- =============================================================================
-- JLD SUBDIVISION CLOUD DATABASE SCHEMA FOR SUPABASE
-- Run this in your Supabase SQL Editor:
-- https://supabase.com/dashboard/project/mybeqgcbuksgwaojvlvx/sql
-- =============================================================================

-- Drop the old workspace JSON blob table if it exists
DROP TABLE IF EXISTS public.workspace;

-- 1. PRODUCTS
CREATE TABLE IF NOT EXISTS public.products (
  idproduct SERIAL PRIMARY KEY,
  code TEXT UNIQUE NOT NULL,
  location TEXT NOT NULL,
  totalblockno INTEGER NOT NULL DEFAULT 0,
  totallotno INTEGER NOT NULL DEFAULT 0,
  totalarea NUMERIC(12,2) NOT NULL DEFAULT 0,
  cashprice NUMERIC(12,2) NOT NULL DEFAULT 0,
  "availableLots" INTEGER NOT NULL DEFAULT 0,
  "projectPhase" TEXT NOT NULL DEFAULT 'Open',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 2. CLIENTS (Stakeholders)
CREATE TABLE IF NOT EXISTS public.clients (
  idclients SERIAL PRIMARY KEY,
  firstname TEXT NOT NULL,
  lastname TEXT NOT NULL,
  middlename TEXT,
  fullname TEXT NOT NULL,
  gender TEXT,
  dateofbirth DATE,
  placeofbirth TEXT,
  spousename TEXT,
  contactno TEXT,
  email TEXT,
  "avatarUrl" TEXT,
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 3. AGENTS
CREATE TABLE IF NOT EXISTS public.agents (
  id SERIAL PRIMARY KEY,
  fullname TEXT NOT NULL,
  contactno TEXT,
  role TEXT DEFAULT 'Real Estate Agent',
  "avatarUrl" TEXT,
  "totalSales" NUMERIC(14,2) DEFAULT 0,
  "commissionRate" NUMERIC(5,2) DEFAULT 5.0,
  "totalEarned" NUMERIC(14,2) DEFAULT 0,
  "totalClaimed" NUMERIC(14,2) DEFAULT 0,
  balance NUMERIC(14,2) DEFAULT 0,
  recordstatus TEXT DEFAULT 'Active',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 4. PURCHASE DETAILS (Leads/Contracts)
CREATE TABLE IF NOT EXISTS public.purchase_details (
  id SERIAL PRIMARY KEY,
  blockno INTEGER NOT NULL,
  lotno INTEGER NOT NULL,
  area NUMERIC(12,2) NOT NULL,
  lotprice NUMERIC(14,2) NOT NULL,
  amortization NUMERIC(14,2) NOT NULL,
  terms INTEGER NOT NULL,
  downpayment NUMERIC(14,2) NOT NULL,
  agentpercentage NUMERIC(5,2) NOT NULL,
  idclients INTEGER NOT NULL REFERENCES public.clients(idclients),
  idproducts INTEGER NOT NULL REFERENCES public.products(idproduct),
  idagent INTEGER NOT NULL REFERENCES public.agents(id),
  remarks TEXT,
  recordstatus TEXT NOT NULL DEFAULT 'Active',
  otherfees NUMERIC(14,2) DEFAULT 0,
  penalty NUMERIC(14,2) DEFAULT 0,
  duedate DATE,
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE,
  
  -- CRM Fields
  "clientName" TEXT,
  "productCode" TEXT,
  location TEXT,
  "agentName" TEXT,
  "leadStatus" TEXT,
  score INTEGER DEFAULT 0,
  intent TEXT DEFAULT 'Low',
  source TEXT,
  "nextAction" TEXT,
  "aiRecommendation" TEXT,
  "dateApplied" TIMESTAMP WITH TIME ZONE DEFAULT timezone('utc'::text, now())
);

-- 5. PAYMENT TRANSACTIONS
CREATE TABLE IF NOT EXISTS public.payment_transactions (
  id SERIAL PRIMARY KEY,
  dateofpayment TIMESTAMP WITH TIME ZONE NOT NULL,
  orderreceipt TEXT,
  referenceno TEXT,
  paymentref TEXT UNIQUE,
  paymenttype TEXT NOT NULL,
  paidby INTEGER NOT NULL REFERENCES public.clients(idclients),
  "paidbyName" TEXT NOT NULL,
  inchargeby INTEGER NOT NULL,
  "inchargebyName" TEXT NOT NULL,
  recordedby INTEGER,
  totalamount NUMERIC(14,2) NOT NULL,
  recordstatus TEXT NOT NULL DEFAULT 'Active',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 6. PAYMENT ITEMS
CREATE TABLE IF NOT EXISTS public.payment_items (
  id SERIAL PRIMARY KEY,
  idpurchasedetails INTEGER NOT NULL REFERENCES public.purchase_details(id),
  idpayment INTEGER NOT NULL REFERENCES public.payment_transactions(id) ON DELETE CASCADE,
  paymentfor TEXT NOT NULL,
  amount NUMERIC(14,2) NOT NULL,
  description TEXT
);

-- 7. EXPENSES
CREATE TABLE IF NOT EXISTS public.expenses (
  id SERIAL PRIMARY KEY,
  receiveby INTEGER NOT NULL,
  "receivebyName" TEXT NOT NULL,
  releaseby INTEGER NOT NULL,
  "releasebyName" TEXT NOT NULL,
  description TEXT NOT NULL,
  purpose TEXT NOT NULL,
  amount NUMERIC(14,2) NOT NULL,
  daterelease TIMESTAMP WITH TIME ZONE NOT NULL,
  remarks TEXT,
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 8. EMPLOYEES
CREATE TABLE IF NOT EXISTS public.employees (
  idemployee SERIAL PRIMARY KEY,
  firstname TEXT NOT NULL,
  lastname TEXT NOT NULL,
  middlename TEXT,
  fullname TEXT NOT NULL,
  gender TEXT,
  dateofbirth DATE NOT NULL,
  salary NUMERIC(14,2) NOT NULL,
  designation TEXT NOT NULL,
  civilstatus TEXT NOT NULL,
  contactno TEXT NOT NULL,
  recordstatus TEXT NOT NULL DEFAULT 'Active',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 9. LOANS AND BENEFITS
CREATE TABLE IF NOT EXISTS public.employee_loans (
  id SERIAL PRIMARY KEY,
  idemployee INTEGER NOT NULL REFERENCES public.employees(idemployee),
  "employeeName" TEXT NOT NULL,
  dateapplied TIMESTAMP WITH TIME ZONE NOT NULL,
  description TEXT NOT NULL,
  category TEXT NOT NULL,
  type TEXT NOT NULL, -- DEDUCTION or EARNING
  amount NUMERIC(14,2) NOT NULL,
  amortization NUMERIC(14,2) NOT NULL,
  duedate DATE,
  remarks TEXT,
  recordstatus TEXT NOT NULL DEFAULT 'Active',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 10. PAYROLL RECORDS
CREATE TABLE IF NOT EXISTS public.payroll_records (
  id SERIAL PRIMARY KEY,
  idemployee INTEGER NOT NULL REFERENCES public.employees(idemployee),
  "employeeName" TEXT NOT NULL,
  designation TEXT NOT NULL,
  "dailyRate" NUMERIC(14,2) NOT NULL,
  "daysWorked" NUMERIC(5,2) NOT NULL,
  "grossPay" NUMERIC(14,2) NOT NULL,
  "cashAdvance" NUMERIC(14,2) DEFAULT 0,
  merienda NUMERIC(14,2) DEFAULT 0,
  egg NUMERIC(14,2) DEFAULT 0,
  rice NUMERIC(14,2) DEFAULT 0,
  "emergencyFund" NUMERIC(14,2) DEFAULT 0,
  undertime NUMERIC(14,2) DEFAULT 0,
  "otherDeductions" NUMERIC(14,2) DEFAULT 0,
  "totalDeductions" NUMERIC(14,2) DEFAULT 0,
  "netPay" NUMERIC(14,2) NOT NULL,
  period TEXT NOT NULL,
  "approvalStatus" TEXT NOT NULL DEFAULT 'Pending',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 11. PAYSLIPS
CREATE TABLE IF NOT EXISTS public.payslips (
  id SERIAL PRIMARY KEY,
  idemployee INTEGER NOT NULL REFERENCES public.employees(idemployee),
  "employeeName" TEXT NOT NULL,
  designation TEXT NOT NULL,
  month TEXT NOT NULL,
  "basicSalary" NUMERIC(14,2) NOT NULL,
  "overtimeEarnings" NUMERIC(14,2) DEFAULT 0,
  benefits NUMERIC(14,2) DEFAULT 0,
  "cashAdvanceDeduction" NUMERIC(14,2) DEFAULT 0,
  "otherDeductions" NUMERIC(14,2) DEFAULT 0,
  "netPay" NUMERIC(14,2) NOT NULL,
  "dateGenerated" TIMESTAMP WITH TIME ZONE NOT NULL,
  "payoutStatus" TEXT NOT NULL DEFAULT 'Draft',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- 12. AUDIT LOGS
CREATE TABLE IF NOT EXISTS public.audit_logs (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  action TEXT NOT NULL,
  "entityType" TEXT NOT NULL,
  "recordId" TEXT NOT NULL,
  "recordLabel" TEXT NOT NULL,
  timestamp TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT timezone('utc'::text, now()),
  "performedBy" TEXT NOT NULL
);

-- Enable RLS on all tables
ALTER TABLE public.products ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.clients ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.agents ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.purchase_details ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.payment_transactions ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.payment_items ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.expenses ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.employees ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.employee_loans ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.payroll_records ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.payslips ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.audit_logs ENABLE ROW LEVEL SECURITY;

-- Create Policies allowing all access (replace with Auth based policies when needed)
CREATE POLICY "Public Access Products" ON public.products FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Clients" ON public.clients FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Agents" ON public.agents FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Purchase Details" ON public.purchase_details FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Payments" ON public.payment_transactions FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Payment Items" ON public.payment_items FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Expenses" ON public.expenses FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Employees" ON public.employees FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Employee Loans" ON public.employee_loans FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Payroll" ON public.payroll_records FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Payslips" ON public.payslips FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Access Audit Logs" ON public.audit_logs FOR ALL USING (true) WITH CHECK (true);
