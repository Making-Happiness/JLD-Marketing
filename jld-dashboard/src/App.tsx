
import { useSession } from './utils/session';
import { calculateStatement, validateReceipt } from './utils/statement';
import { useState } from 'react';
import * as API from './utils/supabase';
import { EntryDialog, EntryKind, EntryValues } from './components/EntryDialog';
import { preparePayroll, payslipFromPayroll } from './utils/workflows';
import { AccountantWorkspace } from './components/AccountantWorkspace';
import { PurchaseDetailsTable } from './components/tables/PurchaseDetailsTable';
import { Sidebar, NavigationTab, NAV_SECTIONS } from './components/layout/Sidebar';
import { TopHeader } from './components/layout/TopHeader';
import { InventoryTable } from './components/tables/InventoryTable';
import { StakeholdersTable } from './components/tables/StakeholdersTable';
import { PaymentsTable } from './components/tables/PaymentsTable';
import { AgentsTable } from './components/tables/AgentsTable';
import { ExpensesTable } from './components/tables/ExpensesTable';
import { EmployeesTable } from './components/tables/EmployeesTable';
import { LoansTable } from './components/tables/LoansTable';
import { PayrollTable } from './components/tables/PayrollTable';
import { PrintableAccountsPage } from './components/PrintableAccountsPage';

import { ApplicationModal } from './components/modals/ApplicationModal';
import { PaymentModal } from './components/modals/PaymentModal';
import { CalculatorModal } from './components/modals/CalculatorModal';
import { SOAModal } from './components/modals/SOAModal';
import { ProductModal } from './components/modals/ProductModal';
import { StakeholderModal } from './components/modals/StakeholderModal';
import { PurchaseDetailsModal } from './components/modals/PurchaseDetailsModal';
import { ArchiveConfirmModal } from './components/modals/ArchiveConfirmModal';
import { PermanentDeleteModal } from './components/modals/PermanentDeleteModal';
import { ActivityHistoryModal } from './components/modals/ActivityHistoryModal';

import { 
  INITIAL_PURCHASE_DETAILS, 
  INITIAL_PRODUCTS, 
  INITIAL_AGENTS, 
  INITIAL_CLIENTS, 
  INITIAL_PAYMENTS,
  INITIAL_EXPENSES,
  INITIAL_EMPLOYEES,
  INITIAL_LOANS,
  INITIAL_BENEFITS,
  INITIAL_PAYSLIPS,
  INITIAL_PAYROLL,
  INITIAL_AUDIT_LOGS
} from './data/initialData';
import { 
  PurchaseDetail, 
  PaymentTransaction, 
  Client, 
  Product, 
  Expense, 
  Employee, 
  LoanRecord, 
  PayslipRecord, 
  PayrollRecord,
  Agent,
  EntityTypeName,
  AuditLogEntry,
  GuardRailResult
} from './types';
import { 
  checkProductArchiveGuard,
  checkClientArchiveGuard,
  checkAgentArchiveGuard,
  checkEmployeeArchiveGuard,
  checkPurchaseArchiveGuard,
  checkPermanentDeleteIntegrity,
  FullDatabaseState
} from './utils/guardRails';
import { formatCurrency } from './utils/calculations';
import { CheckCircle2 } from 'lucide-react';

export function App({ initialState }: { initialState: any }) {
  const user=useSession();
  // Current active workspace navigation tab
  const [currentTab, setCurrentTab] = useState<NavigationTab>('overview');

  const [entryKind,setEntryKind]=useState<EntryKind|null>(null);
  const [claimAgent,setClaimAgent]=useState<Agent|null>(null);
  // Data state
  const [leads, setLeads] = useState<PurchaseDetail[]>(initialState?.leads ?? INITIAL_PURCHASE_DETAILS);
  const [products, setProducts] = useState<Product[]>(initialState?.products ?? INITIAL_PRODUCTS);
  const [agents, setAgents] = useState<Agent[]>(initialState?.agents ?? INITIAL_AGENTS);
  const [clients, setClients] = useState<Client[]>(initialState?.clients ?? INITIAL_CLIENTS);
  const [payments, setPayments] = useState<PaymentTransaction[]>(initialState?.payments ?? INITIAL_PAYMENTS);
  const [expenses, setExpenses] = useState<Expense[]>(initialState?.expenses ?? INITIAL_EXPENSES);
  const [employees, setEmployees] = useState<Employee[]>(initialState?.employees ?? INITIAL_EMPLOYEES);
  const [loans, setLoans] = useState<LoanRecord[]>(initialState?.loans ?? INITIAL_LOANS);
  const [benefits, setBenefits] = useState<LoanRecord[]>(initialState?.benefits ?? INITIAL_BENEFITS);
  const [payslips, setPayslips] = useState<PayslipRecord[]>(initialState?.payslips ?? INITIAL_PAYSLIPS);
  const [payrollRecords, setPayrollRecords] = useState<PayrollRecord[]>(initialState?.payrollRecords ?? INITIAL_PAYROLL);

  // Audit Logs State
  const [auditLogs, setAuditLogs] = useState<AuditLogEntry[]>(initialState?.auditLogs ?? INITIAL_AUDIT_LOGS);

  

  // Enterprise Modals State
  const [archiveTarget, setArchiveTarget] = useState<{
    entityType: EntityTypeName;
    id: string | number;
    recordTitle: string;
    guardRailResult: GuardRailResult;
  } | null>(null);

  const [deleteTarget, setDeleteTarget] = useState<{
    entityType: EntityTypeName;
    id: string | number;
    recordTitle: string;
    integrityResult: GuardRailResult;
  } | null>(null);

  const [historyTarget, setHistoryTarget] = useState<{
    isOpen: boolean;
    selectedRecord: {
      entityType: EntityTypeName;
      recordId: string | number;
      recordLabel: string;
    } | null;
  }>({
    isOpen: false,
    selectedRecord: null
  });

  // Business Modals state
  const [isAppModalOpen, setIsAppModalOpen] = useState<boolean>(false);
  const [editingLead, setEditingLead] = useState<PurchaseDetail | null>(null);

  const [isProductModalOpen, setIsProductModalOpen] = useState<boolean>(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);

  const [isStakeholderModalOpen, setIsStakeholderModalOpen] = useState<boolean>(false);
  const [editingClient, setEditingClient] = useState<Client | null>(null);
  const [applyClientId, setApplyClientId] = useState<number | null>(null);

  // Stakeholder row click -> Purchase Details Modal
  const [modalStakeholder, setModalStakeholder] = useState<Client | null>(null);
  const [isPurchaseModalOpen, setIsPurchaseModalOpen] = useState<boolean>(false);
  
  const [isPaymentModalOpen, setIsPaymentModalOpen] = useState<boolean>(false);
  const [paymentLead, setPaymentLead] = useState<PurchaseDetail | null>(null);

  const [isCalcModalOpen, setIsCalcModalOpen] = useState<boolean>(false);
  const [calcInitialPrice] = useState<number>(108000);

  const [isSOAModalOpen, setIsSOAModalOpen] = useState<boolean>(false);
  const [soaLead, setSoaLead] = useState<PurchaseDetail | null>(null);

  const [toastMessage, setToastMessage] = useState<string>('');

  const showToast = (msg: string) => {
    setToastMessage(msg);
    setTimeout(() => setToastMessage(''), 3000);
  };

  // Construct full database snapshot for relational integrity analysis
  const dbState: FullDatabaseState = {
    products,
    clients,
    leads,
    payments,
    agents,
    employees,
    loans,
    benefits,
    payslips,
    payrollRecords,
    expenses
  };

  // Central audit logger
  const logActivity = (
    entityType: EntityTypeName,
    recordId: string | number,
    recordCode: string,
    action: 'CREATE' | 'EDIT' | 'ARCHIVE' | 'RESTORE' | 'PERMANENT_DELETE',
    details: string
  ) => {
    const newEntry: AuditLogEntry = {
      id: `LOG-${Date.now()}-${Math.floor(Math.random() * 1000)}`,
      timestamp: new Date().toISOString(),
      action,
      entityType,
      recordId,
      recordLabel: recordCode,
      performedBy: user?.email || 'Accounting',
      details,
      reason: details
    };
    setAuditLogs(prev => [newEntry, ...prev]);
  };

  // Open Archive Confirm Modal with automated dependency guard checks
  const handleArchive = (entityType: EntityTypeName, id: string | number, title: string) => {
    let guardResult: GuardRailResult = { allowed: true };
    const idNum = Number(id);

    if (entityType === 'Product') {
      const prod = products.find(p => p.idproduct === idNum);
      if (prod) guardResult = checkProductArchiveGuard(prod, leads, payments);
    } else if (entityType === 'Stakeholder') {
      const cl = clients.find(c => c.idclients === idNum);
      if (cl) guardResult = checkClientArchiveGuard(cl, leads, payments);
    } else if (entityType === 'Agent') {
      const ag = agents.find(a => a.id === idNum);
      if (ag) guardResult = checkAgentArchiveGuard(ag, leads);
    } else if (entityType === 'Employee') {
      const emp = employees.find(e => e.idemployee === idNum);
      if (emp) guardResult = checkEmployeeArchiveGuard(emp, payrollRecords, loans, payslips);
    } else if (entityType === 'Purchase') {
      const p = leads.find(l => l.id === idNum);
      if (p) guardResult = checkPurchaseArchiveGuard(p, payments);
    }

    setArchiveTarget({
      entityType,
      id,
      recordTitle: title,
      guardRailResult: guardResult
    });
  };

  // Perform soft-delete archive
  const confirmArchive = (reason: string) => {
    if (!archiveTarget) return;
    const { entityType, id, recordTitle } = archiveTarget;
    const now = new Date().toISOString();
    const idNum = Number(id);

    switch (entityType) {
      case 'Product':
        setProducts(prev => prev.map(p => p.idproduct === idNum ? { ...p, status: 'archived', deleted_at: now } : p));
        break;
      case 'Stakeholder':
        setClients(prev => prev.map(c => c.idclients === idNum ? { ...c, status: 'archived', deleted_at: now } : c));
        break;
      case 'Purchase':
        setLeads(prev => prev.map(l => l.id === idNum ? { ...l, status: 'archived', deleted_at: now } : l));
        break;
      case 'Payment':
        setPayments(prev => prev.map(p => p.id === idNum ? { ...p, status: 'archived', deleted_at: now } : p));
        break;
      case 'Agent':
        setAgents(prev => prev.map(a => a.id === idNum ? { ...a, status: 'archived', deleted_at: now } : a));
        break;
      case 'Employee':
        setEmployees(prev => prev.map(e => e.idemployee === idNum ? { ...e, status: 'archived', deleted_at: now } : e));
        break;
      case 'Loan':
        setLoans(prev => prev.map(l => l.id === idNum ? { ...l, status: 'archived', deleted_at: now } : l));
        break;
      case 'Benefit':
        setBenefits(prev => prev.map(b => b.id === idNum ? { ...b, status: 'archived', deleted_at: now } : b));
        break;
      case 'Payroll':
        setPayrollRecords(prev => prev.map(p => p.id === idNum ? { ...p, status: 'archived', deleted_at: now } : p));
        break;
      case 'Payslip':
        setPayslips(prev => prev.map(s => s.id === idNum ? { ...s, status: 'archived', deleted_at: now } : s));
        break;
      case 'Expense':
        setExpenses(prev => prev.map(e => e.id === idNum ? { ...e, status: 'archived', deleted_at: now } : e));
        break;
    }

    logActivity(entityType, id, recordTitle, 'ARCHIVE', `Soft-deleted & archived: ${reason}`);
    showToast(`${entityType} "${recordTitle}" safely archived. Preserved in database history.`);
    setArchiveTarget(null);
  };

  // Perform 1-click restore from Archived view
  const handleRestore = (entityType: EntityTypeName, id: string | number, recordTitle: string) => {
    const idNum = Number(id);
    switch (entityType) {
      case 'Product':
        setProducts(prev => prev.map(p => p.idproduct === idNum ? { ...p, status: 'active', deleted_at: null } : p));
        break;
      case 'Stakeholder':
        setClients(prev => prev.map(c => c.idclients === idNum ? { ...c, status: 'active', deleted_at: null } : c));
        break;
      case 'Purchase':
        setLeads(prev => prev.map(l => l.id === idNum ? { ...l, status: 'active', deleted_at: null } : l));
        break;
      case 'Payment':
        setPayments(prev => prev.map(p => p.id === idNum ? { ...p, status: 'active', deleted_at: null } : p));
        break;
      case 'Agent':
        setAgents(prev => prev.map(a => a.id === idNum ? { ...a, status: 'active', deleted_at: null } : a));
        break;
      case 'Employee':
        setEmployees(prev => prev.map(e => e.idemployee === idNum ? { ...e, status: 'active', deleted_at: null } : e));
        break;
      case 'Loan':
        setLoans(prev => prev.map(l => l.id === idNum ? { ...l, status: 'active', deleted_at: null } : l));
        break;
      case 'Benefit':
        setBenefits(prev => prev.map(b => b.id === idNum ? { ...b, status: 'active', deleted_at: null } : b));
        break;
      case 'Payroll':
        setPayrollRecords(prev => prev.map(p => p.id === idNum ? { ...p, status: 'active', deleted_at: null } : p));
        break;
      case 'Payslip':
        setPayslips(prev => prev.map(s => s.id === idNum ? { ...s, status: 'active', deleted_at: null } : s));
        break;
      case 'Expense':
        setExpenses(prev => prev.map(e => e.id === idNum ? { ...e, status: 'active', deleted_at: null } : e));
        break;
    }

    logActivity(entityType, id, recordTitle, 'RESTORE', `Restored ${entityType} to active operations view.`);
    showToast(`${entityType} "${recordTitle}" successfully restored.`);
  };

  // Open Permanent Delete Modal (Super-Admin only) with relational integrity check
  const handleRequestPermanentDelete = (entityType: EntityTypeName, id: string | number, recordTitle: string) => {
    const integrityResult = checkPermanentDeleteIntegrity(entityType, id, dbState);
    setDeleteTarget({
      entityType,
      id,
      recordTitle,
      integrityResult
    });
  };

  // Perform permanent purge if integrity test passes
  const confirmPermanentDelete = () => {
    if (!deleteTarget) return;
    const { entityType, id, recordTitle, integrityResult } = deleteTarget;
    if (!integrityResult.allowed) return;

    const idNum = Number(id);
    switch (entityType) {
      case 'Product':
        setProducts(prev => prev.filter(p => p.idproduct !== idNum));
        break;
      case 'Stakeholder':
        setClients(prev => prev.filter(c => c.idclients !== idNum));
        break;
      case 'Purchase':
        setLeads(prev => prev.filter(l => l.id !== idNum));
        break;
      case 'Payment':
        setPayments(prev => prev.filter(p => p.id !== idNum));
        break;
      case 'Agent':
        setAgents(prev => prev.filter(a => a.id !== idNum));
        break;
      case 'Employee':
        setEmployees(prev => prev.filter(e => e.idemployee !== idNum));
        break;
      case 'Loan':
        setLoans(prev => prev.filter(l => l.id !== idNum));
        break;
      case 'Benefit':
        setBenefits(prev => prev.filter(b => b.id !== idNum));
        break;
      case 'Payroll':
        setPayrollRecords(prev => prev.filter(p => p.id !== idNum));
        break;
      case 'Payslip':
        setPayslips(prev => prev.filter(s => s.id !== idNum));
        break;
      case 'Expense':
        setExpenses(prev => prev.filter(e => e.id !== idNum));
        break;
    }

    logActivity(entityType, id, recordTitle, 'PERMANENT_DELETE', `Super-Admin permanently purged unreferenced record: ${recordTitle}`);
    showToast(`Record "${recordTitle}" permanently removed from database.`);
    setDeleteTarget(null);
  };

  // View Activity History
  const handleOpenHistory = (entityType: EntityTypeName, recordId: string | number, recordLabel: string) => {
    setHistoryTarget({
      isOpen: true,
      selectedRecord: {
        entityType,
        recordId,
        recordLabel
      }
    });
  };

  const handleOpenSystemHistory = () => {
    setHistoryTarget({
      isOpen: true,
      selectedRecord: null
    });
  };

  const handleSaveProduct = (productData: Product | Omit<Product, 'idproduct'>) => {
    if ('idproduct' in productData && productData.idproduct) {
      setProducts(prev => prev.map(p => p.idproduct === productData.idproduct ? (productData as Product) : p));
      void API.saveProduct(productData as Product);
      logActivity('Product', productData.idproduct, productData.code, 'EDIT', `Updated product specifications for ${productData.code}`);
      showToast(`Product ${productData.code} successfully updated!`);
    } else {
      const nextId = products.length > 0 ? Math.max(...products.map(p => p.idproduct)) + 1 : 1;
      const newProduct: Product = {
        ...(productData as Omit<Product, 'idproduct'>),
        idproduct: nextId,
        status: 'active',
        deleted_at: null
      };
      setProducts(prev => [newProduct, ...prev]);
      void API.saveProduct(newProduct);
      logActivity('Product', nextId, newProduct.code, 'CREATE', `Created new subdivision phase: ${newProduct.location} (${newProduct.code})`);
      showToast(`Product ${newProduct.code} successfully added!`);
    }
  };

  const handleSaveClient = (clientData: Client | Omit<Client, 'idclients'>) => {
    if ('idclients' in clientData && clientData.idclients) {
      setClients(prev => prev.map(c => c.idclients === clientData.idclients ? (clientData as Client) : c));
      void API.saveClient(clientData as Client);
      logActivity('Stakeholder', clientData.idclients, `${clientData.firstname} ${clientData.lastname}`, 'EDIT', `Updated stakeholder profile.`);
      showToast(`Stakeholder ${clientData.firstname} ${clientData.lastname} updated!`);
    } else {
      const nextId = clients.length > 0 ? Math.max(...clients.map(c => c.idclients)) + 1 : 1;
      const newClient: Client = {
        ...(clientData as Omit<Client, 'idclients'>),
        idclients: nextId,
        status: 'active',
        deleted_at: null
      };
      setClients(prev => [newClient, ...prev]);
      void API.saveClient(newClient);
      logActivity('Stakeholder', nextId, `${newClient.firstname} ${newClient.lastname}`, 'CREATE', `Registered new stakeholder ${newClient.firstname} ${newClient.lastname}`);
      showToast(`Stakeholder ${newClient.firstname} ${newClient.lastname} registered!`);
    }
  };

  const handleApplyForClient = (client: Client) => {
    setApplyClientId(client.idclients);
    setEditingLead(null);
    setIsAppModalOpen(true);
  };

  const handleEditPurchase = (purchase: PurchaseDetail) => {
    setEditingLead(purchase);
    setApplyClientId(purchase.idclients);
    setIsAppModalOpen(true);
  };

  const handleSaveApplication = (newApp: PurchaseDetail):string|void => {
    const product=products.find(p=>p.idproduct===newApp.idproducts&&p.status==='active');
    if(!product||!clients.some(c=>c.idclients===newApp.idclients&&c.status==='active')||!agents.some(a=>a.id===newApp.idagent&&a.status==='active'))return 'Select an active property, buyer and agent.';
    if(leads.some(l=>l.id!==newApp.id&&l.status==='active'&&l.idproducts===newApp.idproducts&&l.blockno===newApp.blockno&&l.lotno===newApp.lotno))return 'This lot already has an active contract. Select an available lot.';
    if(!Number.isInteger(newApp.blockno)||!Number.isInteger(newApp.lotno)||newApp.blockno<1||newApp.lotno<1||newApp.blockno>product.totalblockno||newApp.lotno>product.totallotno)return 'Block or lot number is outside this property inventory.';
    if(newApp.lotprice<=0||newApp.area<=0||newApp.downpayment<0||newApp.downpayment>newApp.lotprice||newApp.agentpercentage<0||newApp.agentpercentage>100)return 'Review the price, area, downpayment and commission rate.';
    if(editingLead&&payments.some(p=>p.items.some(i=>i.idpurchasedetails===newApp.id))&&(editingLead.idclients!==newApp.idclients||editingLead.idproducts!==newApp.idproducts||editingLead.idagent!==newApp.idagent||editingLead.agentpercentage!==newApp.agentpercentage||editingLead.blockno!==newApp.blockno||editingLead.lotno!==newApp.lotno))return 'A contract with receipts cannot change buyer, property, lot or commission assignment.';
    if (editingLead) {
      const statement=calculateStatement(newApp,payments);
      if(statement.balance<0||statement.downPaymentOutstanding<0)return 'The revised price or down payment conflicts with existing receipts.';
      setLeads(leads.map(l => l.id === newApp.id ? newApp : l));
      void API.savePurchaseDetail(newApp);
      logActivity('Purchase', newApp.id, `${newApp.clientName} (Blk ${newApp.blockno} Lot ${newApp.lotno})`, 'EDIT', `Updated purchase terms for ${newApp.clientName}`);
      showToast(`Purchase details for ${newApp.clientName} successfully updated!`);
    } else {
      const appWithStatus: PurchaseDetail = {
        ...newApp,
        status: 'active',
        deleted_at: null
      };
      setLeads([appWithStatus, ...leads]);
      void API.savePurchaseDetail(appWithStatus);
      logActivity('Purchase', newApp.id, `${newApp.clientName} (Blk ${newApp.blockno} Lot ${newApp.lotno})`, 'CREATE', `Executed lot purchase application for ${newApp.clientName}`);
      showToast(`New lot purchase for ${newApp.clientName} created!`);
    }
  };

  const handleSavePayment = (newPay: PaymentTransaction): string | void => {
    if(payments.some(p=>p.orderreceipt.trim().toLowerCase()===newPay.orderreceipt.trim().toLowerCase()))return 'This receipt number already exists, including archived receipts.';
    if(!newPay.items.length||newPay.items.some(i=>!Number.isFinite(i.amount)||i.amount<=0)||Math.abs(newPay.items.reduce((n,i)=>n+i.amount,0)-newPay.totalamount)>0.01)return 'Payment items do not match the receipt total.';
    if(newPay.items.some(i=>!leads.some(l=>l.id===i.idpurchasedetails&&l.status==='active'&&l.idclients===newPay.paidby)))return 'Payment must reference active contracts for the same buyer.';
    for (const id of new Set(newPay.items.map(i=>i.idpurchasedetails))) { const issue=validateReceipt(leads.find(l=>l.id===id)!,payments,newPay); if(issue)return issue; }
    const earned=new Map<number,number>();
    newPay.items.forEach(i=>{const contract=leads.find(l=>l.id===i.idpurchasedetails)!;earned.set(contract.idagent,(earned.get(contract.idagent)||0)+Math.round(i.amount*contract.agentpercentage)/100);});
    setAgents(prev=>{
      const next = prev.map(a=>{const amount=earned.get(a.id)||0;return {...a,totalEarned:Math.round((a.totalEarned+amount)*100)/100,balance:Math.round((a.balance+amount)*100)/100};});
      next.forEach(a => void API.saveAgent(a));
      return next;
    });
    const payWithStatus: PaymentTransaction = {
      ...newPay,
      status: 'active',
      deleted_at: null
    };
    setPayments([payWithStatus, ...payments]);
    void API.savePayment(payWithStatus);
    logActivity('Payment', newPay.id, newPay.paymentref, 'CREATE', `Recorded payment OR #${newPay.orderreceipt} of ${formatCurrency(newPay.totalamount)} for ${newPay.paidbyName}`);
    showToast(`Payment of ${formatCurrency(newPay.totalamount)} successfully recorded!`);
  };

  const handleOpenSOA = (purchase: PurchaseDetail) => {
    setSoaLead(purchase);
    setIsSOAModalOpen(true);
  };

  const saveEntry = (v:EntryValues):string|void => {
    const id=Date.now(), amount=Number(v.amount), employee=employees.find(e=>e.idemployee===Number(v.employee));
    if(['loan','benefit','expense','commission'].includes(entryKind!)&&(!Number.isFinite(amount)||amount<=0))return 'Enter an amount greater than zero.';
    if(['loan','benefit','expense'].includes(entryKind!)&&(!employee||employee.status!=='active'))return 'Select an active employee.';
    if(entryKind==='agent'){
      const fullname=`${v.firstname.trim()} ${v.lastname.trim()}`;
      setAgents(prev=>[...prev,{id,fullname,contactno:v.contact,role:v.role,commissionRate:Number(v.rate),totalSales:0,totalEarned:0,totalClaimed:0,balance:0,recordstatus:'active',status:'active'}]);
      logActivity('Agent',id,fullname,'CREATE','Registered agent profile.');
    } else if(entryKind==='employee'){
      const fullname=`${v.firstname.trim()} ${v.lastname.trim()}`;
      setEmployees(prev=>[...prev,{idemployee:id,firstname:v.firstname,lastname:v.lastname,middlename:'',gender:'',dateofbirth:'',salary:Number(v.rate),designation:v.role,civilstatus:'',contactno:v.contact,recordstatus:'active',status:'active',fullname}]);
      logActivity('Employee',id,fullname,'CREATE','Registered employee with daily rate.');
    } else if(entryKind==='loan'||entryKind==='benefit'){
      if(entryKind==='loan'&&(!Number.isFinite(Number(v.amortization))||Number(v.amortization)<=0||Number(v.amortization)>amount))return 'Deduction must be greater than zero and cannot exceed the loan.';
      const record:LoanRecord={id,idemployee:employee!.idemployee,employeeName:employee!.fullname,dateapplied:v.date,description:v.description,category:v.category as LoanRecord['category'],type:entryKind==='loan'?'DEDUCTION':'EARNING',amount,amortization:entryKind==='loan'?Number(v.amortization):0,remarks:'',recordstatus:'active',status:'active'};
      (entryKind==='loan'?setLoans:setBenefits)(prev=>[record,...prev]);logActivity(entryKind==='loan'?'Loan':'Benefit',id,record.description,'CREATE','Recorded employee adjustment.');
    } else if(entryKind==='expense'){
      setExpenses(prev=>[{id,receiveby:employee!.idemployee,receivebyName:employee!.fullname,releaseby:0,releasebyName:'Accounting workspace',description:v.description,purpose:v.purpose,amount,daterelease:v.date,remarks:'',status:'active'},...prev]);logActivity('Expense',id,v.description,'CREATE',`Recorded ${formatCurrency(amount)} expense voucher.`);
    } else if(entryKind==='commission'){
      const agent=agents.find(a=>a.id===claimAgent?.id);if(!agent||agent.status!=='active'||amount>agent.balance)return 'The release exceeds the current available balance.';
      setAgents(prev=>prev.map(a=>a.id===agent.id?{...a,totalClaimed:Math.round((a.totalClaimed+amount)*100)/100,balance:Math.round((a.balance-amount)*100)/100}:a));
      setExpenses(prev=>[{id,receiveby:0,receivebyName:agent.fullname,releaseby:0,releasebyName:'Accounting workspace',description:`Commission release · ${agent.fullname}`,purpose:'Agent commission',amount,daterelease:v.date,remarks:`Agent ID ${agent.id}`,status:'active'},...prev]);
      logActivity('Agent',agent.id,agent.fullname,'EDIT',`Released commission ${formatCurrency(amount)} via EXP-${id}.`);logActivity('Expense',id,`Commission ${agent.fullname}`,'CREATE','Recorded commission cash disbursement.');
    } else if(entryKind==='payroll'){
      try {const records=preparePayroll(dbState,v.start,v.end,Number(v.days));setPayrollRecords(prev=>[...records,...prev]);records.forEach(p=>logActivity('Payroll',p.id,p.employeeName,'CREATE',`Prepared draft for ${p.period}.`));} catch(error){return (error as Error).message;}
    }
    showToast('Record saved and connected to the workspace.');
  };
  const generatePayslips=()=>{
    const eligible=payrollRecords.filter(p=>p.status==='active'&&!payslips.some(s=>s.idemployee===p.idemployee&&s.month===p.period));
    if(!eligible.length){showToast('No new payroll records need payslips.');return;}
    setPayslips(prev=>[...eligible.map(payslipFromPayroll),...prev]);eligible.forEach(p=>logActivity('Payslip',p.id,p.employeeName,'CREATE',`Generated draft payslip for ${p.period}.`));showToast(`${eligible.length} draft payslips generated. No payment released.`);
  };  return (
    <div className="app-shell-root flex min-h-screen bg-[#F8FAFC]">
      {/* Toast Notification */}
      {toastMessage && (
        <div role="status" aria-live="polite" className="app-toast-alert fixed bottom-6 right-6 z-50 bg-slate-900 text-white px-4 py-2.5 rounded-xl shadow-lg border border-slate-700 flex items-center gap-2 text-xs font-semibold animate-in fade-in slide-in-from-bottom-3 duration-150">
          <CheckCircle2 className="app-toast-icon w-4 h-4 text-emerald-400" />
          <span className="app-toast-message">{toastMessage}</span>
        </div>
      )}

      {/* Left Sidebar */}
      <Sidebar
        currentTab={currentTab}
        onTabChange={(tab) => {
          setCurrentTab(tab);
        }}
      />

      {/* Main Content Area */}
      <div className="app-main-viewport flex-1 flex flex-col min-w-0">
        {/* Top Header */}
        <TopHeader
          currentTab={currentTab}
          onOpenNotifications={handleOpenSystemHistory}
        />

        {/* Dynamic Workspace Ledger Views */}
        <div className="app-content-viewport flex-1 pb-10 erp-content">
          {(currentTab === 'overview' || currentTab === 'reports') && <AccountantWorkspace key={currentTab} db={dbState} report={currentTab === 'reports'} onNavigate={setCurrentTab} onPayment={() => {setPaymentLead(null);setIsPaymentModalOpen(true);}} onContract={() => {setEditingLead(null);setApplyClientId(null);setIsAppModalOpen(true);}} onSOA={handleOpenSOA}/>}
          {currentTab === 'printable-accounts' && <PrintableAccountsPage contracts={leads} payments={payments} />}
          {currentTab !== 'overview' && currentTab !== 'reports' && currentTab !== 'printable-accounts' && <div className="workspace-view-context"><h1 className="workspace-view-title">{NAV_SECTIONS.flatMap(s=>s.items).find(i=>i.id===currentTab)?.label}</h1><p className="workspace-view-description">{['employees','loans-benefits','payroll'].includes(currentTab) ? 'Employee records → adjustments → payroll → payslips → expense vouchers' : 'Properties → buyers → contracts → collections → commissions → cash flow'}</p><details className="related-work"><summary className="related-work-summary">Related tables</summary><div className="context-links">{( ['employees','loans-benefits','payroll'].includes(currentTab) ? NAV_SECTIONS.filter(s=>s.id==='people'||s.id==='finance') : NAV_SECTIONS.filter(s=>s.id==='sales'||s.id==='agents'||s.id==='finance')).flatMap(s=>s.items).map(i=><button key={i.id} className="related-nav-link" aria-current={currentTab===i.id?'page':undefined} onClick={()=>setCurrentTab(i.id as NavigationTab)}>{i.label}</button>)}</div></details></div>}
          {currentTab === 'contracts' && <PurchaseDetailsTable payments={payments} purchases={leads} onNewPurchase={() => {setEditingLead(null);setApplyClientId(null);setIsAppModalOpen(true);}} onEditPurchase={handleEditPurchase} onViewDetails={handleOpenSOA} onViewHistory={handleOpenSOA} onArchivePurchase={p=>handleArchive('Purchase',p.id,p.clientName)} onRestorePurchase={p=>handleRestore('Purchase',p.id,p.clientName)} onPermanentDeletePurchase={p=>handleRequestPermanentDelete('Purchase',p.id,p.clientName)}/>}
          {/* Properties & Lots */}
          {currentTab === 'products' && (
            <InventoryTable
              products={products.map(p => {
                const soldLots = leads.filter(l => l.idproducts === p.idproduct && l.status === 'active').length;
                const availableLots = Math.max(0, p.totallotno - soldLots);
                let computedPhase: 'Open' | 'Nearly Sold' | 'Completed' = 'Open';
                if (p.status === 'archived') {
                  computedPhase = 'Completed';
                } else if (soldLots === 0) {
                  computedPhase = 'Open';
                } else if (availableLots === 0) {
                  computedPhase = 'Completed';
                } else if (p.totallotno > 0 && availableLots <= Math.ceil(p.totallotno * 0.15)) {
                  computedPhase = 'Nearly Sold';
                } else {
                  computedPhase = 'Open';
                }
                return { ...p, availableLots, projectPhase: computedPhase };
              })}
              onAddProduct={() => {
                setEditingProduct(null);
                setIsProductModalOpen(true);
              }}
              onEditProduct={(prod) => {
                setEditingProduct(prod);
                setIsProductModalOpen(true);
              }}
              onArchiveProduct={(prod) => handleArchive('Product', prod.idproduct, `${prod.code} - ${prod.location}`)}
              onRestoreProduct={(prod) => handleRestore('Product', prod.idproduct, `${prod.code} - ${prod.location}`)}
              onPermanentDeleteProduct={(prod) => handleRequestPermanentDelete('Product', prod.idproduct, `${prod.code} - ${prod.location}`)}
              onViewHistory={(prod) => handleOpenHistory('Product', prod.idproduct, `${prod.code} (${prod.location})`)}
            />
          )}

          {currentTab === 'stakeholder' && (
            <StakeholdersTable
              clients={clients}
              onNewRecord={() => {
                setEditingClient(null);
                setIsStakeholderModalOpen(true);
              }}
              onEditClient={(client) => {
                setEditingClient(client);
                setIsStakeholderModalOpen(true);
              }}
              onArchiveClient={(client) => handleArchive('Stakeholder', client.idclients, client.fullname || `${client.lastname}, ${client.firstname}`)}
              onRestoreClient={(client) => handleRestore('Stakeholder', client.idclients, client.fullname || `${client.lastname}, ${client.firstname}`)}
              onPermanentDeleteClient={(client) => handleRequestPermanentDelete('Stakeholder', client.idclients, client.fullname || `${client.lastname}, ${client.firstname}`)}
              onViewHistory={(client) => handleOpenHistory('Stakeholder', client.idclients, client.fullname || `${client.lastname}, ${client.firstname}`)}
              onApply={handleApplyForClient}
              onSelectClient={(client) => {
                setModalStakeholder(client);
                setIsPurchaseModalOpen(true);
              }}
            />
          )}

          {currentTab === 'payment' && (
            <PaymentsTable
              payments={payments}
              onNewPayment={() => {
                setPaymentLead(null);
                setIsPaymentModalOpen(true);
              }}
              onArchivePayment={(payment) => handleArchive('Payment', payment.id, `${payment.paymentref} (${payment.orderreceipt})`)}
              onRestorePayment={(payment) => handleRestore('Payment', payment.id, `${payment.paymentref} (${payment.orderreceipt})`)}
              onPermanentDeletePayment={(payment) => handleRequestPermanentDelete('Payment', payment.id, `${payment.paymentref} (${payment.orderreceipt})`)}
              onViewHistory={(payment) => handleOpenHistory('Payment', payment.id, `${payment.paymentref} (${payment.orderreceipt})`)}
            />
          )}

          {/* Sales Partners: Agents & Commissions */}
          {currentTab === 'agents-commissions' && (
            <AgentsTable
              agents={agents.map(a=>({...a,totalSales:leads.filter(l=>l.idagent===a.id&&l.status==='active').reduce((sum,l)=>sum+l.lotprice,0)}))}
              onReleaseClaim={(agent) => {setClaimAgent(agent);setEntryKind('commission');}}
              onAddAgent={() => setEntryKind('agent')}
              onArchiveAgent={(agent) => handleArchive('Agent', agent.id, agent.fullname)}
              onRestoreAgent={(agent) => handleRestore('Agent', agent.id, agent.fullname)}
              onPermanentDeleteAgent={(agent) => handleRequestPermanentDelete('Agent', agent.id, agent.fullname)}
              onViewHistory={(agent) => handleOpenHistory('Agent', agent.id, agent.fullname)}
            />
          )}

          {/* People & Payroll */}
          {currentTab === 'employees' && (
            <EmployeesTable
              employees={employees}
              onAddEmployee={() => setEntryKind('employee')}
              onArchiveEmployee={(emp) => handleArchive('Employee', emp.idemployee, emp.fullname)}
              onRestoreEmployee={(emp) => handleRestore('Employee', emp.idemployee, emp.fullname)}
              onPermanentDeleteEmployee={(emp) => handleRequestPermanentDelete('Employee', emp.idemployee, emp.fullname)}
              onViewHistory={(emp) => handleOpenHistory('Employee', emp.idemployee, emp.fullname)}
            />
          )}

          {currentTab === 'loans-benefits' && (
            <LoansTable
              loans={loans}
              benefits={benefits}
              onAddLoan={() => setEntryKind('loan')}
              onAddBenefit={() => setEntryKind('benefit')}
              onArchiveLoan={(record) => handleArchive(record.type === 'DEDUCTION' ? 'Loan' : 'Benefit', record.id, `${record.employeeName} - ${record.description}`)}
              onRestoreLoan={(record) => handleRestore(record.type === 'DEDUCTION' ? 'Loan' : 'Benefit', record.id, `${record.employeeName} - ${record.description}`)}
              onPermanentDeleteLoan={(record) => handleRequestPermanentDelete(record.type === 'DEDUCTION' ? 'Loan' : 'Benefit', record.id, `${record.employeeName} - ${record.description}`)}
              onViewHistory={(record) => handleOpenHistory(record.type === 'DEDUCTION' ? 'Loan' : 'Benefit', record.id, `${record.employeeName} - ${record.description}`)}
            />
          )}

          {currentTab === 'payroll' && (
            <PayrollTable
              payrollRecords={payrollRecords}
              payslips={payslips}
              onProcessPayroll={() => setEntryKind('payroll')}
              onGeneratePayslip={generatePayslips}
              onArchivePayroll={(p) => handleArchive('Payroll', p.id, `Payroll Cutoff ${p.period}`)}
              onRestorePayroll={(p) => handleRestore('Payroll', p.id, `Payroll Cutoff ${p.period}`)}
              onPermanentDeletePayroll={(p) => handleRequestPermanentDelete('Payroll', p.id, `Payroll Cutoff ${p.period}`)}
              onArchivePayslip={(s) => handleArchive('Payslip', s.id, `Payslip for ${s.employeeName}`)}
              onRestorePayslip={(s) => handleRestore('Payslip', s.id, `Payslip for ${s.employeeName}`)}
              onPermanentDeletePayslip={(s) => handleRequestPermanentDelete('Payslip', s.id, `Payslip for ${s.employeeName}`)}
              onViewHistory={(rec, type) => handleOpenHistory(type, rec.id, type === 'Payroll' ? (rec as PayrollRecord).period : (rec as PayslipRecord).employeeName)}
            />
          )}

          {/* Finance & Ledgers */}
          {currentTab === 'expenses' && (
            <ExpensesTable
              expenses={expenses}
              onAddExpense={() => setEntryKind('expense')}
              onArchiveExpense={(exp) => handleArchive('Expense', exp.id, `EXP-${exp.id}: ${exp.description}`)}
              onRestoreExpense={(exp) => handleRestore('Expense', exp.id, `EXP-${exp.id}: ${exp.description}`)}
              onPermanentDeleteExpense={(exp) => handleRequestPermanentDelete('Expense', exp.id, `EXP-${exp.id}: ${exp.description}`)}
              onViewHistory={(exp) => handleOpenHistory('Expense', exp.id, `EXP-${exp.id}: ${exp.description}`)}
            />
          )}


        </div>
      </div>

      {entryKind && <EntryDialog kind={entryKind} employees={employees} claimName={claimAgent?.fullname} claimBalance={claimAgent?.balance} onClose={()=>{setEntryKind(null);setClaimAgent(null);}} onSave={saveEntry}/>} 
      {/* Application / New Lot Purchase Form Modal */}
      <ApplicationModal
        isOpen={isAppModalOpen}
        onClose={() => {
          setIsAppModalOpen(false);
          setEditingLead(null);
          setApplyClientId(null);
        }}
        onSave={handleSaveApplication}
        clients={clients.filter(c=>c.status==='active')}
        products={products.filter(p=>p.status==='active')}
        agents={agents.filter(a=>a.status==='active')}
        initialData={editingLead}
        defaultClientId={applyClientId}
      />

      {/* Record Payment Modal */}
      <PaymentModal
        isOpen={isPaymentModalOpen}
        onClose={() => {
          setIsPaymentModalOpen(false);
          setPaymentLead(null);
        }}
        onSavePayment={handleSavePayment}
        applications={leads.filter(l=>l.status==='active')}
        selectedApplication={paymentLead}
      />

      {/* Loan / Amortization Calculator Modal */}
      <CalculatorModal
        isOpen={isCalcModalOpen}
        onClose={() => setIsCalcModalOpen(false)}
        initialPrice={calcInitialPrice}
        onApply={(price) => {
          showToast(`Applied ${formatCurrency(price)} terms to calculation!`);
        }}
      />

      {/* Statement of Account (SOA) / Payment History Modal */}
      <SOAModal
        isOpen={isSOAModalOpen}
        onClose={() => {
          setIsSOAModalOpen(false);
          setSoaLead(null);
        }}
        application={soaLead}
        payments={payments}
      />

      {/* Product Modal (Add / Edit) */}
      <ProductModal
        isOpen={isProductModalOpen}
        onClose={() => {
          setIsProductModalOpen(false);
          setEditingProduct(null);
        }}
        onSave={handleSaveProduct}
        productToEdit={editingProduct}
      />

      {/* Stakeholder Modal (Add / Edit) */}
      <StakeholderModal
        isOpen={isStakeholderModalOpen}
        onClose={() => {
          setIsStakeholderModalOpen(false);
          setEditingClient(null);
        }}
        onSave={handleSaveClient}
        clientToEdit={editingClient}
      />

      {/* Stakeholder Purchase Details Popup Modal (Fill-Up Form Layout) */}
      <PurchaseDetailsModal payments={payments}
        isOpen={isPurchaseModalOpen}
        onClose={() => {
          setIsPurchaseModalOpen(false);
          setModalStakeholder(null);
        }}
        client={modalStakeholder}
        purchases={modalStakeholder ? leads.filter(l => l.idclients === modalStakeholder.idclients) : []}
        onEditPurchase={(purchase) => {
          setIsPurchaseModalOpen(false);
          handleEditPurchase(purchase);
        }}
        onViewHistory={(purchase) => {
          setIsPurchaseModalOpen(false);
          handleOpenSOA(purchase);
        }}
        onAddNewPurchase={(client) => {
          setIsPurchaseModalOpen(false);
          handleApplyForClient(client);
        }}
        onArchivePurchase={(purchase) => {
          setIsPurchaseModalOpen(false);
          handleArchive('Purchase', purchase.id, `${purchase.clientName} - ${purchase.productCode} Blk ${purchase.blockno} Lot ${purchase.lotno}`);
        }}
      />

      {/* Soft-Delete Archive Confirmation Modal */}
      {archiveTarget && (
        <ArchiveConfirmModal
          isOpen={!!archiveTarget}
          onClose={() => setArchiveTarget(null)}
          onConfirm={confirmArchive}
          entityName={archiveTarget.entityType}
          recordTitle={archiveTarget.recordTitle}
          guardRailResult={archiveTarget.guardRailResult}
        />
      )}

      {/* Super-Admin Permanent Deletion Gated Modal */}
      {deleteTarget && (
        <PermanentDeleteModal
          isOpen={!!deleteTarget}
          onClose={() => setDeleteTarget(null)}
          onConfirm={confirmPermanentDelete}
          entityName={deleteTarget.entityType}
          recordTitle={deleteTarget.recordTitle}
          integrityResult={deleteTarget.integrityResult}
        />
      )}

      {/* System-wide & Record-level Audit Trail Modal */}
      <ActivityHistoryModal key={`${historyTarget.isOpen}-${historyTarget.selectedRecord?.entityType}-${historyTarget.selectedRecord?.recordId}`}
        isOpen={historyTarget.isOpen}
        onClose={() => setHistoryTarget(prev => ({ ...prev, isOpen: false }))}
        auditLogs={auditLogs}
        selectedRecord={historyTarget.selectedRecord}
      />
    </div>
  );
}

export default App;







