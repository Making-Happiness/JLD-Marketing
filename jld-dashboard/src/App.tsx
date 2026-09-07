import { useState } from 'react';
import { Sidebar, NavigationTab } from './components/layout/Sidebar';
import { TopHeader } from './components/layout/TopHeader';
import { InventoryTable } from './components/tables/InventoryTable';
import { StakeholdersTable } from './components/tables/StakeholdersTable';
import { PaymentsTable } from './components/tables/PaymentsTable';
import { AgentsTable } from './components/tables/AgentsTable';
import { ExpensesTable } from './components/tables/ExpensesTable';
import { EmployeesTable } from './components/tables/EmployeesTable';
import { LoansTable } from './components/tables/LoansTable';
import { PayrollTable } from './components/tables/PayrollTable';

import { ApplicationModal } from './components/modals/ApplicationModal';
import { PaymentModal } from './components/modals/PaymentModal';
import { CalculatorModal } from './components/modals/CalculatorModal';
import { SOAModal } from './components/modals/SOAModal';
import { ProductModal } from './components/modals/ProductModal';
import { StakeholderModal } from './components/modals/StakeholderModal';
import { PurchaseDetailsModal } from './components/modals/PurchaseDetailsModal';

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
  INITIAL_PAYROLL
} from './data/initialData';
import { PurchaseDetail, PaymentTransaction, Client, Product, Expense, Employee, LoanRecord, PayslipRecord, PayrollRecord } from './types';
import { formatCurrency } from './utils/calculations';
import { CheckCircle2 } from 'lucide-react';

export function App() {
  // 4 Business Modules navigation (9 consolidated sub-navigation tabs)
  const [currentTab, setCurrentTab] = useState<NavigationTab>('products');

  // Data state
  const [leads, setLeads] = useState<PurchaseDetail[]>(INITIAL_PURCHASE_DETAILS);
  const [products, setProducts] = useState<Product[]>(INITIAL_PRODUCTS);
  const [agents] = useState(INITIAL_AGENTS);
  const [clients, setClients] = useState<Client[]>(INITIAL_CLIENTS);
  const [payments, setPayments] = useState<PaymentTransaction[]>(INITIAL_PAYMENTS);
  const [expenses] = useState<Expense[]>(INITIAL_EXPENSES);
  const [employees] = useState<Employee[]>(INITIAL_EMPLOYEES);
  const [loans] = useState<LoanRecord[]>(INITIAL_LOANS);
  const [benefits] = useState<LoanRecord[]>(INITIAL_BENEFITS);
  const [payslips] = useState<PayslipRecord[]>(INITIAL_PAYSLIPS);
  const [payrollRecords] = useState<PayrollRecord[]>(INITIAL_PAYROLL);

  // Modals state
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

  const handleSaveProduct = (productData: Product | Omit<Product, 'idproduct'>) => {
    if ('idproduct' in productData && productData.idproduct) {
      setProducts(prev => prev.map(p => p.idproduct === productData.idproduct ? (productData as Product) : p));
      showToast(`Product ${productData.code} successfully updated!`);
    } else {
      const nextId = products.length > 0 ? Math.max(...products.map(p => p.idproduct)) + 1 : 1;
      const newProduct: Product = {
        ...(productData as Omit<Product, 'idproduct'>),
        idproduct: nextId
      };
      setProducts(prev => [newProduct, ...prev]);
      showToast(`Product ${newProduct.code} successfully added!`);
    }
  };

  const handleDeleteProduct = (idproduct: number) => {
    const prod = products.find(p => p.idproduct === idproduct);
    setProducts(prev => prev.filter(p => p.idproduct !== idproduct));
    showToast(`Product ${prod?.code || ''} successfully deleted.`);
  };

  const handleSaveClient = (clientData: Client | Omit<Client, 'idclients'>) => {
    if ('idclients' in clientData && clientData.idclients) {
      setClients(prev => prev.map(c => c.idclients === clientData.idclients ? (clientData as Client) : c));
      showToast(`Stakeholder ${clientData.firstname} ${clientData.lastname} updated!`);
    } else {
      const nextId = clients.length > 0 ? Math.max(...clients.map(c => c.idclients)) + 1 : 1;
      const newClient: Client = {
        ...(clientData as Omit<Client, 'idclients'>),
        idclients: nextId
      };
      setClients(prev => [newClient, ...prev]);
      showToast(`Stakeholder ${newClient.firstname} ${newClient.lastname} registered!`);
    }
  };

  const handleDeleteClient = (idclients: number) => {
    const target = clients.find(c => c.idclients === idclients);
    setClients(prev => prev.filter(c => c.idclients !== idclients));
    showToast(`Stakeholder ${target?.firstname || ''} ${target?.lastname || ''} deleted.`);
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

  const handleDeletePurchase = (id: number) => {
    setLeads(prev => prev.filter(l => l.id !== id));
    showToast('Lot purchase record deleted.');
  };

  const handleSaveApplication = (newApp: PurchaseDetail) => {
    if (editingLead) {
      setLeads(leads.map(l => l.id === newApp.id ? newApp : l));
      showToast(`Purchase details for ${newApp.clientName} successfully updated!`);
    } else {
      setLeads([newApp, ...leads]);
      showToast(`New lot purchase for ${newApp.clientName} created!`);
    }
  };

  const handleSavePayment = (newPay: PaymentTransaction) => {
    setPayments([newPay, ...payments]);
    showToast(`Payment of ${formatCurrency(newPay.totalamount)} successfully recorded!`);
  };

  const handleOpenSOA = (purchase: PurchaseDetail) => {
    setSoaLead(purchase);
    setIsSOAModalOpen(true);
  };

  return (
    <div className="flex min-h-screen bg-[#F8FAFC]">
      {/* Toast Notification */}
      {toastMessage && (
        <div className="fixed bottom-6 right-6 z-50 bg-slate-900 text-white px-4 py-2.5 rounded-xl shadow-lg border border-slate-700 flex items-center gap-2 text-xs font-semibold animate-in fade-in slide-in-from-bottom-3 duration-150">
          <CheckCircle2 className="w-4 h-4 text-emerald-400" />
          <span>{toastMessage}</span>
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
      <div className="flex-1 flex flex-col min-w-0">
        {/* Top Header */}
        <TopHeader
          currentTab={currentTab}
          onOpenNotifications={() => showToast('All subdivision operations are normal and up to date.')}
        />

        {/* Dynamic View strictly mapping the 4 business modules / 9 navigation tabs */}
        <div className="flex-1 pb-10">
          {/* Module 1: Sales & Properties */}
          {currentTab === 'products' && (
            <InventoryTable
              products={products}
              onAddProduct={() => {
                setEditingProduct(null);
                setIsProductModalOpen(true);
              }}
              onEditProduct={(prod) => {
                setEditingProduct(prod);
                setIsProductModalOpen(true);
              }}
              onDeleteProduct={handleDeleteProduct}
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
              onDeleteClient={handleDeleteClient}
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
            />
          )}

          {/* Module 2: Agents & Commissions */}
          {currentTab === 'agents-commissions' && (
            <AgentsTable
              agents={agents}
              onReleaseClaim={(agent) => showToast(`Claim voucher prepared for ${agent.fullname}`)}
              onAddAgent={() => showToast('New Agent registration ready.')}
            />
          )}

          {/* Module 3: HR & Payroll */}
          {currentTab === 'employees' && (
            <EmployeesTable
              employees={employees}
              onAddEmployee={() => showToast('Add Employee form ready.')}
            />
          )}

          {currentTab === 'loans-benefits' && (
            <LoansTable
              loans={loans}
              benefits={benefits}
              onAddLoan={() => showToast('New Cash Advance form ready.')}
              onAddBenefit={() => showToast('Add Benefit / Incentive form ready.')}
            />
          )}

          {currentTab === 'payroll' && (
            <PayrollTable
              payrollRecords={payrollRecords}
              payslips={payslips}
              onProcessPayroll={() => showToast('Payroll calculation initialized!')}
              onGeneratePayslip={() => showToast('Monthly payslips generated!')}
            />
          )}

          {/* Module 4: Finance & Reports */}
          {currentTab === 'expenses' && (
            <ExpensesTable
              expenses={expenses}
              onAddExpense={() => showToast('Expense disbursement voucher ready.')}
            />
          )}

          {currentTab === 'reports' && (
            <div className="px-8 py-4 space-y-4">
              <div className="bg-white p-6 rounded-2xl border border-slate-200/80 shadow-2xs">
                <h3 className="text-base font-bold text-slate-900">Reports &amp; Financial Statements</h3>
                <p className="text-xs text-slate-500 mt-1">
                  Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmReportView.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">rpt_StatementOfCashflow</code>
                </p>
                <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 mt-5 text-xs">
                  <div className="p-4 border border-slate-200 rounded-xl bg-slate-50/50 space-y-2">
                    <span className="font-bold text-slate-900 block text-sm">Statement of Cash Inflows</span>
                    <p className="text-slate-500 text-[11px]">Daily, monthly, and annual subdivision amortization collections breakdown.</p>
                    <button 
                      onClick={() => showToast('Generated Cash Inflows Report for current month.')}
                      className="mt-2 px-3 py-1.5 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-lg cursor-pointer"
                    >
                      View Report
                    </button>
                  </div>
                  <div className="p-4 border border-slate-200 rounded-xl bg-slate-50/50 space-y-2">
                    <span className="font-bold text-slate-900 block text-sm">Statements of Account (SOA)</span>
                    <p className="text-slate-500 text-[11px]">Generate batch SOAs with principal, amortization, penalties, and balance.</p>
                    <button 
                      onClick={() => {
                        if (leads[0]) handleOpenSOA(leads[0]);
                      }}
                      className="mt-2 px-3 py-1.5 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-lg cursor-pointer"
                    >
                      Open Sample SOA
                    </button>
                  </div>
                  <div className="p-4 border border-slate-200 rounded-xl bg-slate-50/50 space-y-2">
                    <span className="font-bold text-slate-900 block text-sm">Agent Commission Claims</span>
                    <p className="text-slate-500 text-[11px]">Summary of earned commission percentages and released payment vouchers.</p>
                    <button 
                      onClick={() => setCurrentTab('agents-commissions')}
                      className="mt-2 px-3 py-1.5 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-lg cursor-pointer"
                    >
                      View Commissions
                    </button>
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>

      {/* Application / New Lot Purchase Form Modal */}
      <ApplicationModal
        isOpen={isAppModalOpen}
        onClose={() => {
          setIsAppModalOpen(false);
          setEditingLead(null);
          setApplyClientId(null);
        }}
        onSave={handleSaveApplication}
        clients={clients}
        products={products}
        agents={agents}
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
        applications={leads}
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
      <PurchaseDetailsModal
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
          handleOpenSOA(purchase);
        }}
        onAddNewPurchase={(client) => {
          setIsPurchaseModalOpen(false);
          handleApplyForClient(client);
        }}
      />
    </div>
  );
}

export default App;
