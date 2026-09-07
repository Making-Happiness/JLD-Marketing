using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;
using RealProperty.Reports_Model;

namespace RealProperty;

public class frmMain : Form
{
	private IContainer components = null;

	private Panel panelLeft;
	private Panel panelTop;
	private Panel panelContainer;
	private PictureBox logo;
	private Label label1;
	private Label lblCurrentSection;
	private Label lblBrandName;
	private Panel panelBrand;
	private FlowLayoutPanel flowNav;

	// Module 1: Sales & Properties
	private Button btnModSales;
	private Panel panelSubSales;
	private Button btnNavProducts;
	private Button btnNavClients;
	private Button btnNavPayments;

	// Module 2: Agents & Commissions
	private Button btnModAgents;
	private Panel panelSubAgents;
	private Button btnNavAgentDicer;
	private Button btnNavCommission;

	// Module 3: HR & Payroll
	private Button btnModHR;
	private Panel panelSubHR;
	private Button btnNavEmployees;
	private Button btnNavLoans;
	private Button btnNavPayroll;

	// Module 4: Finance & Reports
	private Button btnModFinance;
	private Panel panelSubFinance;
	private Button btnNavExpenses;
	private Button btnNavReports;

	// Tracking current active button
	private Button currentActiveBtn = null;

	public DataTable view_purchasedetails_full { get; set; }
	public DataTable view_agentsTable { get; set; }
	public DataTable view_paymentsTable { get; set; }
	public DataTable view_clientsTable { get; set; }
	public DataTable view_EmployeesTable { get; set; }
	public List<Expense_view> view_expensesTable { get; set; }

	public frmMain()
	{
		InitializeComponent();
	}

	private void frmMain_Load(object sender, EventArgs e)
	{
		refresh_view_clientsTable();
		// Open default view: Products
		btnNavProducts_Click(btnNavProducts, EventArgs.Empty);
	}

	private void HighlightActiveButton(Button btn, string sectionTitle)
	{
		if (currentActiveBtn != null)
		{
			currentActiveBtn.BackColor = Color.FromArgb(26, 44, 38);
			currentActiveBtn.ForeColor = Color.FromArgb(220, 235, 225);
			currentActiveBtn.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular);
		}
		currentActiveBtn = btn;
		if (btn != null)
		{
			btn.BackColor = Color.FromArgb(0, 115, 77);
			btn.ForeColor = Color.White;
			btn.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold);
		}
		if (lblCurrentSection != null)
		{
			lblCurrentSection.Text = "Module View: " + sectionTitle;
		}
	}

	private void ToggleAccordion(Panel subPanel, Button btnHeader, string moduleTitle)
	{
		subPanel.Visible = !subPanel.Visible;
		btnHeader.Text = (subPanel.Visible ? "▼  " : "►  ") + moduleTitle;
	}

	private void btnModSales_Click(object sender, EventArgs e)
	{
		ToggleAccordion(panelSubSales, btnModSales, "Sales & Properties");
	}

	private void btnModAgents_Click(object sender, EventArgs e)
	{
		ToggleAccordion(panelSubAgents, btnModAgents, "Agents & Commissions");
	}

	private void btnModHR_Click(object sender, EventArgs e)
	{
		ToggleAccordion(panelSubHR, btnModHR, "HR & Payroll");
	}

	private void btnModFinance_Click(object sender, EventArgs e)
	{
		ToggleAccordion(panelSubFinance, btnModFinance, "Finance & Reports");
	}

	public void btnNavProducts_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavProducts, "Products (Inventory & Lots)");
		frmProducts myForm = new frmProducts();
		showForm(myForm);
	}

	public void btnNavClients_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavClients, "Stakeholders & Contracts");
		refresh_view_agentsTable();
		frmClients myForm = new frmClients();
		showForm(myForm);
	}

	public void btnNavPayments_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavPayments, "Client Payments");
		Client sel_row;
		using (frmSearchClient frmSearchClient2 = new frmSearchClient())
		{
			refresh_view_clientsTable();
			frmSearchClient2.ShowDialog(this);
			sel_row = frmSearchClient2.sel_row;
		}
		if (sel_row != null)
		{
			refresh_view_purchasedetails_full();
			refresh_view_paymentsTable();
			frmpayments frmpayments2 = new frmpayments();
			frmpayments2.selected_client = sel_row;
			showForm(frmpayments2);
		}
	}

	public void btnNavAgentDicer_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavAgentDicer, "Agent Directory");
		refresh_view_agentsTable();
		frmAgentDicer myForm = new frmAgentDicer();
		showForm(myForm);
	}

	public void btnNavCommission_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavCommission, "Unified Commission & Claims Center");
		frmCommision myForm = new frmCommision();
		showForm(myForm);
	}

	public void btnNavEmployees_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavEmployees, "Employee Directory");
		frmEmployees myForm = new frmEmployees();
		showForm(myForm);
	}

	public void btnNavLoans_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavLoans, "Employee Adjustments (Loans & Benefits)");
		frmLoan frmLoan2 = new frmLoan();
		frmLoan2.Transaction = "DEDUCTIONS";
		showForm(frmLoan2);
	}

	public void btnNavPayroll_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavPayroll, "Payroll & Payslips Management");
		frmPayroll myForm = new frmPayroll();
		showForm(myForm);
	}

	public void btnNavExpenses_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavExpenses, "Company Expenses");
		refresh_view_EmployeesTable(force: true);
		frmExpenses myForm = new frmExpenses();
		showForm(myForm);
	}

	public void btnNavReports_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavReports, "Financial Reports & Cash Flow");
		AReports reports = new rpt_StatementOfCashflow();
		frmReportView frmReportView2 = new frmReportView();
		frmReportView2.reports = reports;
		frmReportView2.Show();
	}

	// Legacy aliases for backward compatibility
	public void btnProducts_Click(object sender, EventArgs e) => btnNavProducts_Click(sender, e);
	public void btnClients_Click(object sender, EventArgs e) => btnNavClients_Click(sender, e);
	public void btnPayments_Click(object sender, EventArgs e) => btnNavPayments_Click(sender, e);
	public void btnAgentDicer_Click(object sender, EventArgs e) => btnNavAgentDicer_Click(sender, e);
	public void btnCommision_Click(object sender, EventArgs e) => btnNavCommission_Click(sender, e);
	public void btnEmployee_Click(object sender, EventArgs e) => btnNavEmployees_Click(sender, e);
	public void btnLoan_Click(object sender, EventArgs e) => btnNavLoans_Click(sender, e);
	public void btnOtherBenefits_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavLoans, "Employee Adjustments (Loans & Benefits)");
		frmLoan frmLoan2 = new frmLoan();
		frmLoan2.Transaction = "EARNINGS";
		showForm(frmLoan2);
	}
	public void tbnPayroll_Click(object sender, EventArgs e) => btnNavPayroll_Click(sender, e);
	public void btnPayslip_Click(object sender, EventArgs e)
	{
		HighlightActiveButton(btnNavPayroll, "Payroll & Payslips");
		frmPaySlip myForm = new frmPaySlip();
		showForm(myForm);
	}
	public void btnExpenses_Click(object sender, EventArgs e) => btnNavExpenses_Click(sender, e);
	public void btnReports_Click(object sender, EventArgs e) => btnNavReports_Click(sender, e);

	private void disposeforms()
	{
		panelContainer.Controls.Clear();
	}

	public void showForm(Form myForm)
	{
		disposeforms();
		myForm.TopLevel = false;
		myForm.AutoScroll = true;
		myForm.FormBorderStyle = FormBorderStyle.None;
		myForm.BackColor = BackColor;
		myForm.Top = 10;
		myForm.Left = 10;
		panelContainer.Controls.Add(myForm);
		myForm.Show();
	}

	public void refresh_view_purchasedetails_full()
	{
		using PyamentdetailsCtrl pyamentdetailsCtrl = new PyamentdetailsCtrl();
		if (view_purchasedetails_full == null)
		{
			view_purchasedetails_full = pyamentdetailsCtrl.get_proc_purchasedetails_fullTable();
		}
	}

	public void refresh_view_agentsTable()
	{
		using ClientsCtrl clientsCtrl = new ClientsCtrl();
		if (view_agentsTable == null)
		{
			view_agentsTable = clientsCtrl.get_view_agentsTable();
		}
	}

	public void refresh_view_clientsTable(bool force = false)
	{
		using ClientsCtrl clientsCtrl = new ClientsCtrl();
		if (!force)
		{
			view_clientsTable = clientsCtrl.getcustomTable("clientswithfullname");
		}
		else if (view_clientsTable == null)
		{
			view_clientsTable = clientsCtrl.getcustomTable("clientswithfullname");
		}
	}

	public void refresh_view_EmployeesTable(bool force = false)
	{
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		if (!force)
		{
			view_EmployeesTable = employeeCtrl.getcustomTable("view_employeeswithfullname");
		}
		else if (view_EmployeesTable == null)
		{
			view_EmployeesTable = employeeCtrl.getcustomTable("view_employeeswithfullname");
		}
	}

	public void refresh_view_expensesTable(bool force = false)
	{
		using ExpensesCtrl expensesCtrl = new ExpensesCtrl();
		if (!force)
		{
			view_expensesTable = expensesCtrl.getexpenses_view();
		}
		else if (view_expensesTable == null)
		{
			view_expensesTable = expensesCtrl.getexpenses_view();
		}
	}

	public void refresh_view_paymentsTable(bool force = false)
	{
		using PaymentsCtrl paymentsCtrl = new PaymentsCtrl();
		if (!force)
		{
			view_paymentsTable = paymentsCtrl.get_view_paymentsTable();
		}
		else if (view_paymentsTable == null)
		{
			view_paymentsTable = paymentsCtrl.get_view_paymentsTable();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RealProperty.frmMain));
		this.panelLeft = new System.Windows.Forms.Panel();
		this.flowNav = new System.Windows.Forms.FlowLayoutPanel();
		this.panelBrand = new System.Windows.Forms.Panel();
		this.logo = new System.Windows.Forms.PictureBox();
		this.lblBrandName = new System.Windows.Forms.Label();

		// Module 1
		this.btnModSales = new System.Windows.Forms.Button();
		this.panelSubSales = new System.Windows.Forms.Panel();
		this.btnNavProducts = new System.Windows.Forms.Button();
		this.btnNavClients = new System.Windows.Forms.Button();
		this.btnNavPayments = new System.Windows.Forms.Button();

		// Module 2
		this.btnModAgents = new System.Windows.Forms.Button();
		this.panelSubAgents = new System.Windows.Forms.Panel();
		this.btnNavAgentDicer = new System.Windows.Forms.Button();
		this.btnNavCommission = new System.Windows.Forms.Button();

		// Module 3
		this.btnModHR = new System.Windows.Forms.Button();
		this.panelSubHR = new System.Windows.Forms.Panel();
		this.btnNavEmployees = new System.Windows.Forms.Button();
		this.btnNavLoans = new System.Windows.Forms.Button();
		this.btnNavPayroll = new System.Windows.Forms.Button();

		// Module 4
		this.btnModFinance = new System.Windows.Forms.Button();
		this.panelSubFinance = new System.Windows.Forms.Panel();
		this.btnNavExpenses = new System.Windows.Forms.Button();
		this.btnNavReports = new System.Windows.Forms.Button();

		this.panelTop = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.lblCurrentSection = new System.Windows.Forms.Label();
		this.panelContainer = new System.Windows.Forms.Panel();

		this.panelLeft.SuspendLayout();
		this.panelBrand.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.logo).BeginInit();
		this.flowNav.SuspendLayout();
		this.panelSubSales.SuspendLayout();
		this.panelSubAgents.SuspendLayout();
		this.panelSubHR.SuspendLayout();
		this.panelSubFinance.SuspendLayout();
		this.panelTop.SuspendLayout();
		base.SuspendLayout();

		// panelLeft
		this.panelLeft.BackColor = System.Drawing.Color.FromArgb(18, 30, 25);
		this.panelLeft.Controls.Add(this.flowNav);
		this.panelLeft.Controls.Add(this.panelBrand);
		this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.panelLeft.Location = new System.Drawing.Point(0, 0);
		this.panelLeft.Name = "panelLeft";
		this.panelLeft.Size = new System.Drawing.Size(240, 800);
		this.panelLeft.TabIndex = 1;

		// panelBrand
		this.panelBrand.BackColor = System.Drawing.Color.FromArgb(14, 24, 20);
		this.panelBrand.Controls.Add(this.logo);
		this.panelBrand.Controls.Add(this.lblBrandName);
		this.panelBrand.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelBrand.Location = new System.Drawing.Point(0, 0);
		this.panelBrand.Name = "panelBrand";
		this.panelBrand.Size = new System.Drawing.Size(240, 112);
		this.panelBrand.TabIndex = 0;

		// logo
		this.logo.Image = (System.Drawing.Image)resources.GetObject("logo.Image");
		this.logo.Location = new System.Drawing.Point(16, 10);
		this.logo.Name = "logo";
		this.logo.Size = new System.Drawing.Size(206, 68);
		this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.logo.TabIndex = 0;
		this.logo.TabStop = false;

		// lblBrandName
		this.lblBrandName.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblBrandName.ForeColor = System.Drawing.Color.FromArgb(74, 222, 128);
		this.lblBrandName.Location = new System.Drawing.Point(12, 82);
		this.lblBrandName.Name = "lblBrandName";
		this.lblBrandName.Size = new System.Drawing.Size(214, 22);
		this.lblBrandName.TabIndex = 1;
		this.lblBrandName.Text = "REAL PROPERTY SYSTEM";
		this.lblBrandName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

		// flowNav
		this.flowNav.AutoScroll = true;
		this.flowNav.BackColor = System.Drawing.Color.FromArgb(18, 30, 25);
		this.flowNav.Controls.Add(this.btnModSales);
		this.flowNav.Controls.Add(this.panelSubSales);
		this.flowNav.Controls.Add(this.btnModAgents);
		this.flowNav.Controls.Add(this.panelSubAgents);
		this.flowNav.Controls.Add(this.btnModHR);
		this.flowNav.Controls.Add(this.panelSubHR);
		this.flowNav.Controls.Add(this.btnModFinance);
		this.flowNav.Controls.Add(this.panelSubFinance);
		this.flowNav.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowNav.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.flowNav.Location = new System.Drawing.Point(0, 112);
		this.flowNav.Name = "flowNav";
		this.flowNav.Padding = new System.Windows.Forms.Padding(8, 8, 8, 16);
		this.flowNav.Size = new System.Drawing.Size(240, 688);
		this.flowNav.TabIndex = 1;
		this.flowNav.WrapContents = false;

		// ================= Module 1: Sales & Properties =================
		this.btnModSales.BackColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.btnModSales.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnModSales.FlatAppearance.BorderSize = 0;
		this.btnModSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModSales.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModSales.ForeColor = System.Drawing.Color.White;
		this.btnModSales.Location = new System.Drawing.Point(8, 8);
		this.btnModSales.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.btnModSales.Name = "btnModSales";
		this.btnModSales.Size = new System.Drawing.Size(216, 42);
		this.btnModSales.TabIndex = 10;
		this.btnModSales.Text = "▼  Sales & Properties";
		this.btnModSales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModSales.UseVisualStyleBackColor = false;
		this.btnModSales.Click += new System.EventHandler(this.btnModSales_Click);

		this.panelSubSales.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.panelSubSales.Controls.Add(this.btnNavProducts);
		this.panelSubSales.Controls.Add(this.btnNavClients);
		this.panelSubSales.Controls.Add(this.btnNavPayments);
		this.panelSubSales.Location = new System.Drawing.Point(8, 50);
		this.panelSubSales.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
		this.panelSubSales.Name = "panelSubSales";
		this.panelSubSales.Size = new System.Drawing.Size(216, 114);
		this.panelSubSales.TabIndex = 11;

		// btnNavProducts
		this.btnNavProducts.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavProducts.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavProducts.FlatAppearance.BorderSize = 0;
		this.btnNavProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavProducts.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavProducts.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavProducts.Location = new System.Drawing.Point(0, 0);
		this.btnNavProducts.Name = "btnNavProducts";
		this.btnNavProducts.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavProducts.Size = new System.Drawing.Size(216, 38);
		this.btnNavProducts.TabIndex = 0;
		this.btnNavProducts.Text = "•  Products & Inventory";
		this.btnNavProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavProducts.UseVisualStyleBackColor = false;
		this.btnNavProducts.Click += new System.EventHandler(this.btnNavProducts_Click);

		// btnNavClients
		this.btnNavClients.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavClients.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavClients.FlatAppearance.BorderSize = 0;
		this.btnNavClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavClients.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavClients.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavClients.Location = new System.Drawing.Point(0, 38);
		this.btnNavClients.Name = "btnNavClients";
		this.btnNavClients.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavClients.Size = new System.Drawing.Size(216, 38);
		this.btnNavClients.TabIndex = 1;
		this.btnNavClients.Text = "•  Stakeholders & Lots";
		this.btnNavClients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavClients.UseVisualStyleBackColor = false;
		this.btnNavClients.Click += new System.EventHandler(this.btnNavClients_Click);

		// btnNavPayments
		this.btnNavPayments.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavPayments.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavPayments.FlatAppearance.BorderSize = 0;
		this.btnNavPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavPayments.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavPayments.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavPayments.Location = new System.Drawing.Point(0, 76);
		this.btnNavPayments.Name = "btnNavPayments";
		this.btnNavPayments.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavPayments.Size = new System.Drawing.Size(216, 38);
		this.btnNavPayments.TabIndex = 2;
		this.btnNavPayments.Text = "•  Client Payments";
		this.btnNavPayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavPayments.UseVisualStyleBackColor = false;
		this.btnNavPayments.Click += new System.EventHandler(this.btnNavPayments_Click);

		// ================= Module 2: Agents & Commissions =================
		this.btnModAgents.BackColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.btnModAgents.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnModAgents.FlatAppearance.BorderSize = 0;
		this.btnModAgents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModAgents.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModAgents.ForeColor = System.Drawing.Color.White;
		this.btnModAgents.Location = new System.Drawing.Point(8, 170);
		this.btnModAgents.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.btnModAgents.Name = "btnModAgents";
		this.btnModAgents.Size = new System.Drawing.Size(216, 42);
		this.btnModAgents.TabIndex = 20;
		this.btnModAgents.Text = "▼  Agents & Commissions";
		this.btnModAgents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModAgents.UseVisualStyleBackColor = false;
		this.btnModAgents.Click += new System.EventHandler(this.btnModAgents_Click);

		this.panelSubAgents.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.panelSubAgents.Controls.Add(this.btnNavAgentDicer);
		this.panelSubAgents.Controls.Add(this.btnNavCommission);
		this.panelSubAgents.Location = new System.Drawing.Point(8, 212);
		this.panelSubAgents.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
		this.panelSubAgents.Name = "panelSubAgents";
		this.panelSubAgents.Size = new System.Drawing.Size(216, 76);
		this.panelSubAgents.TabIndex = 21;

		// btnNavAgentDicer
		this.btnNavAgentDicer.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavAgentDicer.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavAgentDicer.FlatAppearance.BorderSize = 0;
		this.btnNavAgentDicer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavAgentDicer.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavAgentDicer.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavAgentDicer.Location = new System.Drawing.Point(0, 0);
		this.btnNavAgentDicer.Name = "btnNavAgentDicer";
		this.btnNavAgentDicer.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavAgentDicer.Size = new System.Drawing.Size(216, 38);
		this.btnNavAgentDicer.TabIndex = 0;
		this.btnNavAgentDicer.Text = "•  Agent Directory";
		this.btnNavAgentDicer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavAgentDicer.UseVisualStyleBackColor = false;
		this.btnNavAgentDicer.Click += new System.EventHandler(this.btnNavAgentDicer_Click);

		// btnNavCommission
		this.btnNavCommission.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavCommission.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavCommission.FlatAppearance.BorderSize = 0;
		this.btnNavCommission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavCommission.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavCommission.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavCommission.Location = new System.Drawing.Point(0, 38);
		this.btnNavCommission.Name = "btnNavCommission";
		this.btnNavCommission.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavCommission.Size = new System.Drawing.Size(216, 38);
		this.btnNavCommission.TabIndex = 1;
		this.btnNavCommission.Text = "•  Commissions & Claims";
		this.btnNavCommission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavCommission.UseVisualStyleBackColor = false;
		this.btnNavCommission.Click += new System.EventHandler(this.btnNavCommission_Click);

		// ================= Module 3: HR & Payroll =================
		this.btnModHR.BackColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.btnModHR.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnModHR.FlatAppearance.BorderSize = 0;
		this.btnModHR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModHR.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModHR.ForeColor = System.Drawing.Color.White;
		this.btnModHR.Location = new System.Drawing.Point(8, 294);
		this.btnModHR.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.btnModHR.Name = "btnModHR";
		this.btnModHR.Size = new System.Drawing.Size(216, 42);
		this.btnModHR.TabIndex = 30;
		this.btnModHR.Text = "▼  HR & Payroll";
		this.btnModHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModHR.UseVisualStyleBackColor = false;
		this.btnModHR.Click += new System.EventHandler(this.btnModHR_Click);

		this.panelSubHR.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.panelSubHR.Controls.Add(this.btnNavEmployees);
		this.panelSubHR.Controls.Add(this.btnNavLoans);
		this.panelSubHR.Controls.Add(this.btnNavPayroll);
		this.panelSubHR.Location = new System.Drawing.Point(8, 336);
		this.panelSubHR.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
		this.panelSubHR.Name = "panelSubHR";
		this.panelSubHR.Size = new System.Drawing.Size(216, 114);
		this.panelSubHR.TabIndex = 31;

		// btnNavEmployees
		this.btnNavEmployees.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavEmployees.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavEmployees.FlatAppearance.BorderSize = 0;
		this.btnNavEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavEmployees.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavEmployees.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavEmployees.Location = new System.Drawing.Point(0, 0);
		this.btnNavEmployees.Name = "btnNavEmployees";
		this.btnNavEmployees.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavEmployees.Size = new System.Drawing.Size(216, 38);
		this.btnNavEmployees.TabIndex = 0;
		this.btnNavEmployees.Text = "•  Employee Directory";
		this.btnNavEmployees.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavEmployees.UseVisualStyleBackColor = false;
		this.btnNavEmployees.Click += new System.EventHandler(this.btnNavEmployees_Click);

		// btnNavLoans
		this.btnNavLoans.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavLoans.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavLoans.FlatAppearance.BorderSize = 0;
		this.btnNavLoans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavLoans.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavLoans.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavLoans.Location = new System.Drawing.Point(0, 38);
		this.btnNavLoans.Name = "btnNavLoans";
		this.btnNavLoans.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavLoans.Size = new System.Drawing.Size(216, 38);
		this.btnNavLoans.TabIndex = 1;
		this.btnNavLoans.Text = "•  Loans & Benefits";
		this.btnNavLoans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavLoans.UseVisualStyleBackColor = false;
		this.btnNavLoans.Click += new System.EventHandler(this.btnNavLoans_Click);

		// btnNavPayroll
		this.btnNavPayroll.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavPayroll.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavPayroll.FlatAppearance.BorderSize = 0;
		this.btnNavPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavPayroll.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavPayroll.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavPayroll.Location = new System.Drawing.Point(0, 76);
		this.btnNavPayroll.Name = "btnNavPayroll";
		this.btnNavPayroll.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavPayroll.Size = new System.Drawing.Size(216, 38);
		this.btnNavPayroll.TabIndex = 2;
		this.btnNavPayroll.Text = "•  Payroll & Payslips";
		this.btnNavPayroll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavPayroll.UseVisualStyleBackColor = false;
		this.btnNavPayroll.Click += new System.EventHandler(this.btnNavPayroll_Click);

		// ================= Module 4: Finance & Reports =================
		this.btnModFinance.BackColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.btnModFinance.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnModFinance.FlatAppearance.BorderSize = 0;
		this.btnModFinance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModFinance.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModFinance.ForeColor = System.Drawing.Color.White;
		this.btnModFinance.Location = new System.Drawing.Point(8, 456);
		this.btnModFinance.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.btnModFinance.Name = "btnModFinance";
		this.btnModFinance.Size = new System.Drawing.Size(216, 42);
		this.btnModFinance.TabIndex = 40;
		this.btnModFinance.Text = "▼  Finance & Reports";
		this.btnModFinance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModFinance.UseVisualStyleBackColor = false;
		this.btnModFinance.Click += new System.EventHandler(this.btnModFinance_Click);

		this.panelSubFinance.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.panelSubFinance.Controls.Add(this.btnNavExpenses);
		this.panelSubFinance.Controls.Add(this.btnNavReports);
		this.panelSubFinance.Location = new System.Drawing.Point(8, 498);
		this.panelSubFinance.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
		this.panelSubFinance.Name = "panelSubFinance";
		this.panelSubFinance.Size = new System.Drawing.Size(216, 76);
		this.panelSubFinance.TabIndex = 41;

		// btnNavExpenses
		this.btnNavExpenses.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavExpenses.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavExpenses.FlatAppearance.BorderSize = 0;
		this.btnNavExpenses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavExpenses.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavExpenses.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavExpenses.Location = new System.Drawing.Point(0, 0);
		this.btnNavExpenses.Name = "btnNavExpenses";
		this.btnNavExpenses.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavExpenses.Size = new System.Drawing.Size(216, 38);
		this.btnNavExpenses.TabIndex = 0;
		this.btnNavExpenses.Text = "•  Company Expenses";
		this.btnNavExpenses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavExpenses.UseVisualStyleBackColor = false;
		this.btnNavExpenses.Click += new System.EventHandler(this.btnNavExpenses_Click);

		// btnNavReports
		this.btnNavReports.BackColor = System.Drawing.Color.FromArgb(24, 40, 34);
		this.btnNavReports.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNavReports.FlatAppearance.BorderSize = 0;
		this.btnNavReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNavReports.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNavReports.ForeColor = System.Drawing.Color.FromArgb(220, 235, 225);
		this.btnNavReports.Location = new System.Drawing.Point(0, 38);
		this.btnNavReports.Name = "btnNavReports";
		this.btnNavReports.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
		this.btnNavReports.Size = new System.Drawing.Size(216, 38);
		this.btnNavReports.TabIndex = 1;
		this.btnNavReports.Text = "•  Financial Reports & SOA";
		this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnNavReports.UseVisualStyleBackColor = false;
		this.btnNavReports.Click += new System.EventHandler(this.btnNavReports_Click);

		// ================= Top Header Panel =================
		this.panelTop.BackColor = System.Drawing.Color.FromArgb(0, 72, 48);
		this.panelTop.Controls.Add(this.label1);
		this.panelTop.Controls.Add(this.lblCurrentSection);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(240, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(956, 60);
		this.panelTop.TabIndex = 2;

		// label1
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(14, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(430, 35);
		this.label1.TabIndex = 0;
		this.label1.Text = "JLD Real Property System";

		// lblCurrentSection
		this.lblCurrentSection.AutoSize = true;
		this.lblCurrentSection.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCurrentSection.ForeColor = System.Drawing.Color.FromArgb(187, 247, 208);
		this.lblCurrentSection.Location = new System.Drawing.Point(16, 37);
		this.lblCurrentSection.Name = "lblCurrentSection";
		this.lblCurrentSection.Size = new System.Drawing.Size(250, 20);
		this.lblCurrentSection.TabIndex = 1;
		this.lblCurrentSection.Text = "Module View: Products";

		// ================= Center Container Panel =================
		this.panelContainer.AutoScroll = true;
		this.panelContainer.BackColor = System.Drawing.SystemColors.InactiveCaption;
		this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panelContainer.Location = new System.Drawing.Point(240, 60);
		this.panelContainer.Name = "panelContainer";
		this.panelContainer.Size = new System.Drawing.Size(956, 740);
		this.panelContainer.TabIndex = 4;

		// ================= Form Level =================
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(1196, 800);
		base.Controls.Add(this.panelContainer);
		base.Controls.Add(this.panelTop);
		base.Controls.Add(this.panelLeft);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmMain";
		this.Text = "JLD Real Property System";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(this.frmMain_Load);

		this.panelLeft.ResumeLayout(false);
		this.panelBrand.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.logo).EndInit();
		this.flowNav.ResumeLayout(false);
		this.panelSubSales.ResumeLayout(false);
		this.panelSubAgents.ResumeLayout(false);
		this.panelSubHR.ResumeLayout(false);
		this.panelSubFinance.ResumeLayout(false);
		this.panelTop.ResumeLayout(false);
		this.panelTop.PerformLayout();
		base.ResumeLayout(false);
	}
}
