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

	private Button btnProducts;

	private Panel panelLeft;

	private Panel panelTop;

	private Panel panelContainer;

	private Button btnExpenses;

	private Button btnClients;

	private Button btnPayments;

	private Button btnApply;

	private Button btnCommision;

	private Label label1;

	private PictureBox logo;

	private Button btnReports;

	private Button btnAgentDicer;

	private Button btnEmployee;

	private Button btnPayroll;

	private Button btnLoan;

	private Button btnOtherBenefits;

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

	private void btnProducts_Click(object sender, EventArgs e)
	{
		frmProducts myForm = new frmProducts();
		showForm(myForm);
	}

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

	private void btnClients_Click(object sender, EventArgs e)
	{
		refresh_view_agentsTable();
		frmClients myForm = new frmClients();
		showForm(myForm);
	}

	private void btnPayments_Click(object sender, EventArgs e)
	{
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

	private void frmMain_Load(object sender, EventArgs e)
	{
		refresh_view_clientsTable();
	}

	private void refresh_view_purchasedetails_full()
	{
		using PyamentdetailsCtrl pyamentdetailsCtrl = new PyamentdetailsCtrl();
		if (view_purchasedetails_full == null)
		{
			view_purchasedetails_full = pyamentdetailsCtrl.get_proc_purchasedetails_fullTable();
		}
	}

	private void refresh_view_agentsTable()
	{
		using ClientsCtrl clientsCtrl = new ClientsCtrl();
		if (view_agentsTable == null)
		{
			view_agentsTable = clientsCtrl.get_view_agentsTable();
		}
	}

	private void refresh_view_clientsTable(bool force = false)
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

	private void refresh_view_EmployeesTable(bool force = false)
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

	private void refresh_view_expensesTable(bool force = false)
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

	private void refresh_view_paymentsTable(bool force = false)
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

	private void btnApply_Click(object sender, EventArgs e)
	{
		Client sel_row;
		using (frmSearchClient frmSearchClient2 = new frmSearchClient())
		{
			refresh_view_clientsTable();
			frmSearchClient2.ShowDialog(this);
			sel_row = frmSearchClient2.sel_row;
		}
		if (sel_row == null)
		{
			return;
		}
		using frmApplicationForm frmApplicationForm2 = new frmApplicationForm(1, sel_row);
		refresh_view_clientsTable();
		frmApplicationForm2.clientTable = view_clientsTable;
		frmApplicationForm2.productTable = null;
		frmApplicationForm2.BackColor = BackColor;
		frmApplicationForm2.StartPosition = FormStartPosition.CenterParent;
		frmApplicationForm2.ShowDialog(this);
	}

	private void btnExpenses_Click(object sender, EventArgs e)
	{
		refresh_view_EmployeesTable(force: true);
		frmExpenses myForm = new frmExpenses();
		showForm(myForm);
	}

	private void btnReports_Click(object sender, EventArgs e)
	{
		AReports reports = new rpt_StatementOfCashflow();
		frmReportView frmReportView2 = new frmReportView();
		frmReportView2.reports = reports;
		frmReportView2.Show();
	}

	private void btnAgentDicer_Click(object sender, EventArgs e)
	{
		refresh_view_agentsTable();
		frmAgentDicer myForm = new frmAgentDicer();
		showForm(myForm);
	}

	private void btnEmployee_Click(object sender, EventArgs e)
	{
		frmEmployees myForm = new frmEmployees();
		showForm(myForm);
	}

	private void btnCommision_Click(object sender, EventArgs e)
	{
		frmCommision myForm = new frmCommision();
		showForm(myForm);
	}

	private void btnPayroll_Click(object sender, EventArgs e)
	{
		frmPaySlip myForm = new frmPaySlip();
		showForm(myForm);
	}

	private void btnLoan_Click(object sender, EventArgs e)
	{
		frmLoan frmLoan2 = new frmLoan();
		frmLoan2.Transaction = "DEDUCTIONS";
		showForm(frmLoan2);
	}

	private void btnOtherBenefits_Click(object sender, EventArgs e)
	{
		frmLoan frmLoan2 = new frmLoan();
		frmLoan2.Transaction = "EARNINGS";
		showForm(frmLoan2);
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
		this.btnProducts = new System.Windows.Forms.Button();
		this.panelLeft = new System.Windows.Forms.Panel();
		this.btnOtherBenefits = new System.Windows.Forms.Button();
		this.btnLoan = new System.Windows.Forms.Button();
		this.btnPayroll = new System.Windows.Forms.Button();
		this.btnEmployee = new System.Windows.Forms.Button();
		this.btnAgentDicer = new System.Windows.Forms.Button();
		this.logo = new System.Windows.Forms.PictureBox();
		this.btnReports = new System.Windows.Forms.Button();
		this.btnCommision = new System.Windows.Forms.Button();
		this.btnExpenses = new System.Windows.Forms.Button();
		this.btnClients = new System.Windows.Forms.Button();
		this.btnApply = new System.Windows.Forms.Button();
		this.btnPayments = new System.Windows.Forms.Button();
		this.panelTop = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panelContainer = new System.Windows.Forms.Panel();
		this.panelLeft.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.logo).BeginInit();
		this.panelTop.SuspendLayout();
		base.SuspendLayout();
		this.btnProducts.Location = new System.Drawing.Point(3, 105);
		this.btnProducts.Name = "btnProducts";
		this.btnProducts.Size = new System.Drawing.Size(148, 44);
		this.btnProducts.TabIndex = 0;
		this.btnProducts.Text = "Products";
		this.btnProducts.UseVisualStyleBackColor = true;
		this.btnProducts.Click += new System.EventHandler(btnProducts_Click);
		this.panelLeft.BackColor = System.Drawing.SystemColors.Highlight;
		this.panelLeft.Controls.Add(this.btnOtherBenefits);
		this.panelLeft.Controls.Add(this.btnLoan);
		this.panelLeft.Controls.Add(this.btnPayroll);
		this.panelLeft.Controls.Add(this.btnEmployee);
		this.panelLeft.Controls.Add(this.btnAgentDicer);
		this.panelLeft.Controls.Add(this.logo);
		this.panelLeft.Controls.Add(this.btnReports);
		this.panelLeft.Controls.Add(this.btnCommision);
		this.panelLeft.Controls.Add(this.btnExpenses);
		this.panelLeft.Controls.Add(this.btnClients);
		this.panelLeft.Controls.Add(this.btnApply);
		this.panelLeft.Controls.Add(this.btnPayments);
		this.panelLeft.Controls.Add(this.btnProducts);
		this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.panelLeft.Location = new System.Drawing.Point(0, 0);
		this.panelLeft.Name = "panelLeft";
		this.panelLeft.Size = new System.Drawing.Size(154, 767);
		this.panelLeft.TabIndex = 1;
		this.btnOtherBenefits.Location = new System.Drawing.Point(7, 607);
		this.btnOtherBenefits.Name = "btnOtherBenefits";
		this.btnOtherBenefits.Size = new System.Drawing.Size(145, 45);
		this.btnOtherBenefits.TabIndex = 2;
		this.btnOtherBenefits.Text = "Other Earnings/Benefits";
		this.btnOtherBenefits.UseVisualStyleBackColor = true;
		this.btnOtherBenefits.Click += new System.EventHandler(btnOtherBenefits_Click);
		this.btnLoan.Location = new System.Drawing.Point(6, 556);
		this.btnLoan.Name = "btnLoan";
		this.btnLoan.Size = new System.Drawing.Size(145, 45);
		this.btnLoan.TabIndex = 2;
		this.btnLoan.Text = "Loans/Cash Adavance";
		this.btnLoan.UseVisualStyleBackColor = true;
		this.btnLoan.Click += new System.EventHandler(btnLoan_Click);
		this.btnPayroll.Location = new System.Drawing.Point(4, 658);
		this.btnPayroll.Name = "btnPayroll";
		this.btnPayroll.Size = new System.Drawing.Size(145, 45);
		this.btnPayroll.TabIndex = 1;
		this.btnPayroll.Text = "Payslip";
		this.btnPayroll.UseVisualStyleBackColor = true;
		this.btnPayroll.Click += new System.EventHandler(btnPayroll_Click);
		this.btnEmployee.Location = new System.Drawing.Point(6, 505);
		this.btnEmployee.Name = "btnEmployee";
		this.btnEmployee.Size = new System.Drawing.Size(145, 45);
		this.btnEmployee.TabIndex = 1;
		this.btnEmployee.Text = "Employees";
		this.btnEmployee.UseVisualStyleBackColor = true;
		this.btnEmployee.Click += new System.EventHandler(btnEmployee_Click);
		this.btnAgentDicer.Location = new System.Drawing.Point(6, 403);
		this.btnAgentDicer.Name = "btnAgentDicer";
		this.btnAgentDicer.Size = new System.Drawing.Size(145, 45);
		this.btnAgentDicer.TabIndex = 1;
		this.btnAgentDicer.Text = "Agent/Dicer";
		this.btnAgentDicer.UseVisualStyleBackColor = true;
		this.btnAgentDicer.Click += new System.EventHandler(btnAgentDicer_Click);
		this.logo.Image = (System.Drawing.Image)resources.GetObject("logo.Image");
		this.logo.Location = new System.Drawing.Point(7, 6);
		this.logo.Name = "logo";
		this.logo.Size = new System.Drawing.Size(136, 93);
		this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.logo.TabIndex = 0;
		this.logo.TabStop = false;
		this.btnReports.Location = new System.Drawing.Point(6, 454);
		this.btnReports.Name = "btnReports";
		this.btnReports.Size = new System.Drawing.Size(145, 45);
		this.btnReports.TabIndex = 0;
		this.btnReports.Text = "Reports";
		this.btnReports.UseVisualStyleBackColor = true;
		this.btnReports.Click += new System.EventHandler(btnReports_Click);
		this.btnCommision.Location = new System.Drawing.Point(7, 355);
		this.btnCommision.Name = "btnCommision";
		this.btnCommision.Size = new System.Drawing.Size(145, 42);
		this.btnCommision.TabIndex = 0;
		this.btnCommision.Text = "Commision";
		this.btnCommision.UseVisualStyleBackColor = true;
		this.btnCommision.Click += new System.EventHandler(btnCommision_Click);
		this.btnExpenses.Location = new System.Drawing.Point(6, 304);
		this.btnExpenses.Name = "btnExpenses";
		this.btnExpenses.Size = new System.Drawing.Size(145, 45);
		this.btnExpenses.TabIndex = 0;
		this.btnExpenses.Text = "Expenses";
		this.btnExpenses.UseVisualStyleBackColor = true;
		this.btnExpenses.Click += new System.EventHandler(btnExpenses_Click);
		this.btnClients.Location = new System.Drawing.Point(4, 155);
		this.btnClients.Name = "btnClients";
		this.btnClients.Size = new System.Drawing.Size(145, 44);
		this.btnClients.TabIndex = 0;
		this.btnClients.Text = "Stakeholder";
		this.btnClients.UseVisualStyleBackColor = true;
		this.btnClients.Click += new System.EventHandler(btnClients_Click);
		this.btnApply.Location = new System.Drawing.Point(3, 205);
		this.btnApply.Name = "btnApply";
		this.btnApply.Size = new System.Drawing.Size(148, 41);
		this.btnApply.TabIndex = 0;
		this.btnApply.Text = "Application";
		this.btnApply.UseVisualStyleBackColor = true;
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		this.btnPayments.Location = new System.Drawing.Point(3, 252);
		this.btnPayments.Name = "btnPayments";
		this.btnPayments.Size = new System.Drawing.Size(148, 46);
		this.btnPayments.TabIndex = 0;
		this.btnPayments.Text = "Payments";
		this.btnPayments.UseVisualStyleBackColor = true;
		this.btnPayments.Click += new System.EventHandler(btnPayments_Click);
		this.panelTop.BackColor = System.Drawing.SystemColors.Highlight;
		this.panelTop.Controls.Add(this.label1);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(154, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(1042, 59);
		this.panelTop.TabIndex = 2;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Chartreuse;
		this.label1.Location = new System.Drawing.Point(6, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(375, 48);
		this.label1.TabIndex = 0;
		this.label1.Text = "JLD Private Market";
		this.panelContainer.AutoScroll = true;
		this.panelContainer.BackColor = System.Drawing.SystemColors.InactiveCaption;
		this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.panelContainer.Location = new System.Drawing.Point(154, 59);
		this.panelContainer.Name = "panelContainer";
		this.panelContainer.Size = new System.Drawing.Size(1042, 708);
		this.panelContainer.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(1196, 767);
		base.Controls.Add(this.panelContainer);
		base.Controls.Add(this.panelTop);
		base.Controls.Add(this.panelLeft);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmMain";
		this.Text = "JLD Private Market";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmMain_Load);
		this.panelLeft.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.logo).EndInit();
		this.panelTop.ResumeLayout(false);
		this.panelTop.PerformLayout();
		base.ResumeLayout(false);
	}
}
