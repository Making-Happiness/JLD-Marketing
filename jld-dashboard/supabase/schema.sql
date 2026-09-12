-- =============================================================================
-- JLD SUBDIVISION CLOUD DATABASE SCHEMA FOR SUPABASE
-- Run this in your Supabase SQL Editor:
-- https://supabase.com/dashboard/project/mybeqgcbuksgwaojvlvx/sql
-- =============================================================================

-- 1. Main JSON Workspace Table (Primary Cloud State Storage)
CREATE TABLE IF NOT EXISTS public.workspace (
  id TEXT PRIMARY KEY DEFAULT 'default',
  revision INTEGER NOT NULL DEFAULT 1,
  payload JSONB NOT NULL DEFAULT '{}'::jsonb,
  updated_at TIMESTAMP WITH TIME ZONE DEFAULT timezone('utc'::text, now()) NOT NULL
);

-- Enable Row Level Security (RLS)
ALTER TABLE public.workspace ENABLE ROW LEVEL SECURITY;

-- Allow read & write access with Supabase Publishable / Anon Key
DROP POLICY IF EXISTS "Public Read Workspace" ON public.workspace;
CREATE POLICY "Public Read Workspace" ON public.workspace
  FOR SELECT USING (true);

DROP POLICY IF EXISTS "Public Insert Workspace" ON public.workspace;
CREATE POLICY "Public Insert Workspace" ON public.workspace
  FOR INSERT WITH CHECK (true);

DROP POLICY IF EXISTS "Public Update Workspace" ON public.workspace;
CREATE POLICY "Public Update Workspace" ON public.workspace
  FOR UPDATE USING (true) WITH CHECK (true);

-- Seed initial empty clean database state
INSERT INTO public.workspace (id, revision, payload)
VALUES (
  'default',
  1,
  '{
    "products": [],
    "clients": [],
    "agents": [],
    "leads": [],
    "payments": [],
    "employees": [],
    "loans": [],
    "benefits": [],
    "payrollRecords": [],
    "payslips": [],
    "expenses": [],
    "auditLogs": []
  }'::jsonb
)
ON CONFLICT (id) DO NOTHING;

-- =============================================================================
-- 2. Optional Normalized Relational Tables (Enterprise Projection)
-- =============================================================================

CREATE TABLE IF NOT EXISTS public.products (
  idproduct SERIAL PRIMARY KEY,
  code TEXT UNIQUE NOT NULL,
  location TEXT NOT NULL,
  totalblockno INTEGER NOT NULL DEFAULT 0,
  totallotno INTEGER NOT NULL DEFAULT 0,
  totalarea NUMERIC(12,2) NOT NULL DEFAULT 0,
  cashprice NUMERIC(12,2) NOT NULL DEFAULT 0,
  availableLots INTEGER NOT NULL DEFAULT 0,
  projectPhase TEXT NOT NULL DEFAULT 'Open',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

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
  avatarUrl TEXT,
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

CREATE TABLE IF NOT EXISTS public.agents (
  id SERIAL PRIMARY KEY,
  fullname TEXT NOT NULL,
  contactno TEXT,
  role TEXT DEFAULT 'Real Estate Agent',
  avatarUrl TEXT,
  totalSales NUMERIC(14,2) DEFAULT 0,
  commissionRate NUMERIC(5,2) DEFAULT 5.0,
  totalEarned NUMERIC(14,2) DEFAULT 0,
  totalClaimed NUMERIC(14,2) DEFAULT 0,
  balance NUMERIC(14,2) DEFAULT 0,
  recordstatus TEXT DEFAULT 'Active',
  status TEXT NOT NULL DEFAULT 'active',
  deleted_at TIMESTAMP WITH TIME ZONE
);

-- Enable RLS on relational tables
ALTER TABLE public.products ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.clients ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.agents ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Public Read Products" ON public.products FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Read Clients" ON public.clients FOR ALL USING (true) WITH CHECK (true);
CREATE POLICY "Public Read Agents" ON public.agents FOR ALL USING (true) WITH CHECK (true);
