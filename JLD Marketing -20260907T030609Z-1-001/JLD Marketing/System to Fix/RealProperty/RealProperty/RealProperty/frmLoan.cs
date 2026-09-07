using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmLoan : Form
{
	private DataTable dt_employee;

	private PayrollCtrl controller;

	private IContainer components = null;

	private Panel panel1;

	private SplitContainer splitContainer1;

	private DataGridView DGVEmployeeSalary;

	private DataGridView DGVLoans;

	private Panel panel2;

	private TextBox txtSearch;

	private Label label1;

	private Button btnToggleDeductions;

	private Button btnToggleEarnings;

	private Label lblCategoryMode;

	private DataGridViewTextBoxColumn idemployee_col;

	private DataGridViewTextBoxColumn fullname_col;

	private DataGridViewTextBoxColumn salary_col;

	private DataGridViewButtonColumn apply_col;

	private DataGridViewTextBoxColumn idpayslipdetails_col;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dateapplied_col;

	private DataGridViewTextBoxColumn description_col;

	private DataGridViewTextBoxColumn payslipcategory_col;

	private DataGridViewTextBoxColumn amountpayslipdetails_col;

	private DataGridViewTextBoxColumn amortization_col;

	private DataGridViewTextBoxColumn recordstatus_col;

	private DataGridViewButtonColumn editloan_col;

	public string Transaction { get; set; }

	public frmLoan()
	{
		InitializeComponent();
		controller = new PayrollCtrl();
		DGVEmployeeSalary.AutoGenerateColumns = false;
		DGVLoans.AutoGenerateColumns = false;
	}

	private void frmLoan_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Transaction))
		{
			Transaction = "DEDUCTIONS";
		}
		SetCategoryMode(Transaction);
		Refresh_DGVEmployee();
	}

	public void SetCategoryMode(string mode)
	{
		Transaction = mode;
		if (Transaction == "DEDUCTIONS")
		{
			btnToggleDeductions.BackColor = Color.FromArgb(0, 89, 59);
			btnToggleDeductions.ForeColor = Color.White;
			btnToggleEarnings.BackColor = Color.FromArgb(235, 238, 235);
			btnToggleEarnings.ForeColor = Color.FromArgb(40, 40, 40);
			lblCategoryMode.Text = "Mode: Loans & Cash Advances (Deductions)";
			this.Text = "Employee Adjustments — Loans & Cash Advances";
		}
		else
		{
			btnToggleEarnings.BackColor = Color.FromArgb(0, 89, 59);
			btnToggleEarnings.ForeColor = Color.White;
			btnToggleDeductions.BackColor = Color.FromArgb(235, 238, 235);
			btnToggleDeductions.ForeColor = Color.FromArgb(40, 40, 40);
			lblCategoryMode.Text = "Mode: Other Earnings & Benefits (Earnings)";
			this.Text = "Employee Adjustments — Other Earnings & Benefits";
		}

		if (DGVEmployeeSalary.CurrentRow != null && DGVEmployeeSalary.CurrentRow.Index >= 0)
		{
			dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployee_col.Name].Value;
			Refresh_DGVLoans(value);
		}
	}

	private void btnToggleDeductions_Click(object sender, EventArgs e)
	{
		SetCategoryMode("DEDUCTIONS");
	}

	private void btnToggleEarnings_Click(object sender, EventArgs e)
	{
		SetCategoryMode("EARNINGS");
	}

	private void Refresh_DGVEmployee(string filter = null)
	{
		if (filter == null)
		{
			dt_employee = controller.getEmployeeSalaryTable();
			DGVEmployeeSalary.DataSource = dt_employee;
			return;
		}
		if (dt_employee == null)
		{
			dt_employee = controller.getEmployeeSalaryTable();
		}
		if (filter == "")
		{
			DGVEmployeeSalary.DataSource = dt_employee;
			return;
		}
		string filterExpression = $"fullname like '%{filter}%'";
		DataRow[] source = dt_employee.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataSource = dt_employee.Clone();
			DGVEmployeeSalary.DataSource = dataSource;
		}
		else
		{
			DGVEmployeeSalary.DataSource = source.CopyToDataTable();
		}
	}

	private void txtSearch_TextChanged(object sender, EventArgs e)
	{
		Refresh_DGVEmployee(txtSearch.Text);
	}

	private void DGVEmployeeSalary_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0) return;
		dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployee_col.Name].Value;
		object value2 = DGVEmployeeSalary.CurrentRow.Cells[fullname_col.Name].Value;
		if (columnIndex != DGVEmployeeSalary.Columns[apply_col.Name].Index)
		{
			return;
		}
		Loan loan = new Loan();
		loan.idemployee = value;
		using frmLoanInfo frmLoanInfo2 = new frmLoanInfo(loan, "ADD");
		frmLoanInfo2.Category = Transaction;
		frmLoanInfo2.Employeename = value2;
		frmLoanInfo2.ShowDialog(this);
		if (frmLoanInfo2.CommitChanges)
		{
			Refresh_DGVLoans(loan.idemployee);
		}
	}

	private void DGVEmployeeSalary_SelectionChanged(object sender, EventArgs e)
	{
		if (DGVEmployeeSalary.CurrentRow != null && DGVEmployeeSalary.CurrentRow.Index >= 0)
		{
			dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployee_col.Name].Value;
			Refresh_DGVLoans(value);
		}
	}

	private void Refresh_DGVLoans(int idemployee)
	{
		DataTable dataTable = controller.get_viewLoansTable(idemployee);
		string filterExpression = string.Format("category like '%{0}%' OR category like '%{1}%'", "BENEFIT", "EARNING");
		if (Transaction == "DEDUCTIONS")
		{
			filterExpression = string.Format("category not like '%{0}%' AND category not like '%{1}%'", "BENEFIT", "EARNING");
		}
		DataRow[] source = dataTable.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataSource = dataTable.Clone();
			DGVLoans.DataSource = dataSource;
		}
		else
		{
			DGVLoans.DataSource = source.CopyToDataTable();
		}
	}

	private void DGVLoans_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0) return;
		dynamic value = DGVLoans.CurrentRow.Cells[idpayslipdetails_col.Name].Value;
		object value2 = DGVEmployeeSalary.CurrentRow.Cells[fullname_col.Name].Value;
		if ((value == null) || columnIndex != DGVLoans.Columns[editloan_col.Name].Index)
		{
			return;
		}
		Loan loan;
		using (PayrollCtrl payrollCtrl = new PayrollCtrl())
		{
			loan = payrollCtrl.getLoansObject(value);
		}
		using frmLoanInfo frmLoanInfo2 = new frmLoanInfo(loan);
		frmLoanInfo2.Employeename = value2;
		frmLoanInfo2.Category = Transaction;
		frmLoanInfo2.ShowDialog(this);
		if (frmLoanInfo2.CommitChanges)
		{
			Refresh_DGVLoans(loan.idemployee);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel1 = new System.Windows.Forms.Panel();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.DGVEmployeeSalary = new System.Windows.Forms.DataGridView();
		this.DGVLoans = new System.Windows.Forms.DataGridView();
		this.panel2 = new System.Windows.Forms.Panel();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.lblCategoryMode = new System.Windows.Forms.Label();
		this.btnToggleDeductions = new System.Windows.Forms.Button();
		this.btnToggleEarnings = new System.Windows.Forms.Button();
		this.idemployee_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.salary_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.apply_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.idpayslipdetails_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateapplied_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.description_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payslipcategory_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountpayslipdetails_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amortization_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.editloan_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGVLoans).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.txtSearch);
		this.panel1.Controls.Add(this.lblCategoryMode);
		this.panel1.Controls.Add(this.btnToggleDeductions);
		this.panel1.Controls.Add(this.btnToggleEarnings);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1125, 67);
		this.panel1.TabIndex = 19;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 70);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.DGVEmployeeSalary);
		this.splitContainer1.Panel2.Controls.Add(this.panel2);
		this.splitContainer1.Panel2.Controls.Add(this.DGVLoans);
		this.splitContainer1.Size = new System.Drawing.Size(1125, 577);
		this.splitContainer1.SplitterDistance = 423;
		this.splitContainer1.TabIndex = 20;
		this.DGVEmployeeSalary.AllowUserToAddRows = false;
		this.DGVEmployeeSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVEmployeeSalary.Columns.AddRange(this.idemployee_col, this.fullname_col, this.salary_col, this.apply_col);
		this.DGVEmployeeSalary.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVEmployeeSalary.Location = new System.Drawing.Point(0, 0);
		this.DGVEmployeeSalary.MultiSelect = false;
		this.DGVEmployeeSalary.Name = "DGVEmployeeSalary";
		this.DGVEmployeeSalary.ReadOnly = true;
		this.DGVEmployeeSalary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVEmployeeSalary.RowHeadersWidth = 20;
		this.DGVEmployeeSalary.RowTemplate.Height = 24;
		this.DGVEmployeeSalary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVEmployeeSalary.Size = new System.Drawing.Size(423, 577);
		this.DGVEmployeeSalary.TabIndex = 16;
		this.DGVEmployeeSalary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVEmployeeSalary_CellClick);
		this.DGVEmployeeSalary.SelectionChanged += new System.EventHandler(DGVEmployeeSalary_SelectionChanged);
		this.DGVLoans.AllowUserToAddRows = false;
		this.DGVLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVLoans.Columns.AddRange(this.idpayslipdetails_col, this.dataGridViewTextBoxColumn1, this.dateapplied_col, this.description_col, this.payslipcategory_col, this.amountpayslipdetails_col, this.amortization_col, this.recordstatus_col, this.editloan_col);
		this.DGVLoans.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVLoans.Location = new System.Drawing.Point(0, 0);
		this.DGVLoans.MultiSelect = false;
		this.DGVLoans.Name = "DGVLoans";
		this.DGVLoans.ReadOnly = true;
		this.DGVLoans.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVLoans.RowHeadersWidth = 20;
		this.DGVLoans.RowTemplate.Height = 24;
		this.DGVLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVLoans.Size = new System.Drawing.Size(698, 577);
		this.DGVLoans.TabIndex = 16;
		this.DGVLoans.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVLoans_CellClick);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 523);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(698, 54);
		this.panel2.TabIndex = 17;
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(3, 33);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(423, 28);
		this.txtSearch.TabIndex = 13;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(55, 18);
		this.label1.TabIndex = 14;
		this.label1.Text = "Search";
		this.lblCategoryMode.AutoSize = true;
		this.lblCategoryMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCategoryMode.ForeColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.lblCategoryMode.Location = new System.Drawing.Point(440, 9);
		this.lblCategoryMode.Name = "lblCategoryMode";
		this.lblCategoryMode.Size = new System.Drawing.Size(320, 18);
		this.lblCategoryMode.Text = "Mode: Loans & Cash Advances (Deductions)";
		this.btnToggleDeductions.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnToggleDeductions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnToggleDeductions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnToggleDeductions.Location = new System.Drawing.Point(440, 29);
		this.btnToggleDeductions.Name = "btnToggleDeductions";
		this.btnToggleDeductions.Size = new System.Drawing.Size(225, 32);
		this.btnToggleDeductions.TabIndex = 15;
		this.btnToggleDeductions.Text = "Loans / Cash Advance";
		this.btnToggleDeductions.UseVisualStyleBackColor = true;
		this.btnToggleDeductions.Click += new System.EventHandler(this.btnToggleDeductions_Click);
		this.btnToggleEarnings.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnToggleEarnings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnToggleEarnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnToggleEarnings.Location = new System.Drawing.Point(672, 29);
		this.btnToggleEarnings.Name = "btnToggleEarnings";
		this.btnToggleEarnings.Size = new System.Drawing.Size(225, 32);
		this.btnToggleEarnings.TabIndex = 16;
		this.btnToggleEarnings.Text = "Other Earnings / Benefits";
		this.btnToggleEarnings.UseVisualStyleBackColor = true;
		this.btnToggleEarnings.Click += new System.EventHandler(this.btnToggleEarnings_Click);
		this.idemployee_col.DataPropertyName = "idemployee";
		this.idemployee_col.HeaderText = "idemployee";
		this.idemployee_col.Name = "idemployee_col";
		this.idemployee_col.ReadOnly = true;
		this.idemployee_col.Visible = false;
		this.fullname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.fullname_col.DataPropertyName = "fullname";
		this.fullname_col.HeaderText = "Name";
		this.fullname_col.Name = "fullname_col";
		this.fullname_col.ReadOnly = true;
		this.salary_col.DataPropertyName = "salary";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle.Format = "N2";
		dataGridViewCellStyle.NullValue = null;
		this.salary_col.DefaultCellStyle = dataGridViewCellStyle;
		this.salary_col.HeaderText = "basic salary";
		this.salary_col.Name = "salary_col";
		this.salary_col.ReadOnly = true;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Green;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
		this.apply_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.apply_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.apply_col.HeaderText = "action";
		this.apply_col.Name = "apply_col";
		this.apply_col.ReadOnly = true;
		this.apply_col.Text = "apply";
		this.apply_col.ToolTipText = "Apply Loan / Benefit";
		this.apply_col.UseColumnTextForButtonValue = true;
		this.idpayslipdetails_col.DataPropertyName = "id";
		this.idpayslipdetails_col.HeaderText = "id";
		this.idpayslipdetails_col.Name = "idpayslipdetails_col";
		this.idpayslipdetails_col.ReadOnly = true;
		this.idpayslipdetails_col.Visible = false;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "idemployee";
		this.dataGridViewTextBoxColumn1.HeaderText = "idemployee";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Visible = false;
		this.dateapplied_col.DataPropertyName = "dateapplied";
		this.dateapplied_col.HeaderText = "Date Applied";
		this.dateapplied_col.Name = "dateapplied_col";
		this.dateapplied_col.ReadOnly = true;
		this.description_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.description_col.DataPropertyName = "description";
		this.description_col.HeaderText = "Description";
		this.description_col.Name = "description_col";
		this.description_col.ReadOnly = true;
		this.payslipcategory_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.payslipcategory_col.DataPropertyName = "category";
		this.payslipcategory_col.HeaderText = "category";
		this.payslipcategory_col.Name = "payslipcategory_col";
		this.payslipcategory_col.ReadOnly = true;
		this.payslipcategory_col.Width = 94;
		this.amountpayslipdetails_col.DataPropertyName = "amount";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.amountpayslipdetails_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.amountpayslipdetails_col.HeaderText = "amount";
		this.amountpayslipdetails_col.Name = "amountpayslipdetails_col";
		this.amountpayslipdetails_col.ReadOnly = true;
		this.amortization_col.DataPropertyName = "amortization";
		this.amortization_col.HeaderText = "Monthly Pay";
		this.amortization_col.Name = "amortization_col";
		this.amortization_col.ReadOnly = true;
		this.recordstatus_col.DataPropertyName = "recordstatus";
		this.recordstatus_col.HeaderText = "status";
		this.recordstatus_col.Name = "recordstatus_col";
		this.recordstatus_col.ReadOnly = true;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Lime;
		this.editloan_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.editloan_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.editloan_col.HeaderText = "action";
		this.editloan_col.Name = "editloan_col";
		this.editloan_col.ReadOnly = true;
		this.editloan_col.Text = "edit";
		this.editloan_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1131, 650);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmLoan";
		base.Padding = new System.Windows.Forms.Padding(3);
		this.Text = "Employee Adjustments — Loans & Benefits";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmLoan_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGVLoans).EndInit();
		base.ResumeLayout(false);
	}
}
