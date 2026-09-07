using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmSearchLoans : Form
{
	public int selected_idloan;

	public decimal selected_balance;

	private IContainer components = null;

	private Label label1;

	private TextBox txtSearch;

	private DataGridView DGV;

	private BindingSource loanBindingSource;

	private Label lblNameDisplay;

	private Button btnApplyNew;

	private DataGridViewTextBoxColumn idloan_col;

	private DataGridViewTextBoxColumn idemployeeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dateappliedDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn penaltyDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn duedateDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn amortizationDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn recordstatusDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn balance_col;

	private DataGridViewButtonColumn add_col;

	public dynamic idemployee { get; set; }

	public dynamic employeename { get; set; }

	public string searchType { get; set; }

	public frmSearchLoans()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
	}

	private void frmSearchLoans_Load(object sender, EventArgs e)
	{
		lblNameDisplay.Text = string.Format("Name: {0}", employeename);
		refresh_DGV();
	}

	private void refresh_DGV()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		if (searchType == "PAYROLL")
		{
			DataTable dataSource = payrollCtrl.get_view_PayrollValidLoansTable(idemployee);
			DGV.DataSource = dataSource;
		}
	}

	private void btnApplyNew_Click(object sender, EventArgs e)
	{
		Loan loan = new Loan();
		loan.idemployee = idemployee;
		using frmLoanInfo frmLoanInfo2 = new frmLoanInfo(loan, "ADD");
		frmLoanInfo2.Category = "DEDUCTIONS";
		frmLoanInfo2.Employeename = (object)employeename;
		frmLoanInfo2.ShowDialog(this);
		if (frmLoanInfo2.CommitChanges)
		{
			refresh_DGV();
		}
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e != null)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			int num = Convert.ToInt32(DGV.CurrentRow.Cells[idloan_col.Name].Value);
			decimal num2 = Convert.ToDecimal(DGV.CurrentRow.Cells[balance_col.Name].Value);
			if (columnIndex == add_col.Index)
			{
				selected_idloan = num;
				selected_balance = num2;
				Close();
			}
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.lblNameDisplay = new System.Windows.Forms.Label();
		this.btnApplyNew = new System.Windows.Forms.Button();
		this.loanBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.idloan_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idemployeeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateappliedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.categoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.penaltyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.duedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amortizationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.balance_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.add_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.loanBindingSource).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 46);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(173, 18);
		this.label1.TabIndex = 8;
		this.label1.Text = "Search (loan description)";
		this.txtSearch.Location = new System.Drawing.Point(193, 43);
		this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(403, 24);
		this.txtSearch.TabIndex = 7;
		this.DGV.AllowUserToAddRows = false;
		this.DGV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV.AutoGenerateColumns = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idloan_col, this.idemployeeDataGridViewTextBoxColumn, this.dateappliedDataGridViewTextBoxColumn, this.descriptionDataGridViewTextBoxColumn, this.amountDataGridViewTextBoxColumn, this.categoryDataGridViewTextBoxColumn, this.penaltyDataGridViewTextBoxColumn, this.duedateDataGridViewTextBoxColumn, this.amortizationDataGridViewTextBoxColumn, this.remarksDataGridViewTextBoxColumn, this.recordstatusDataGridViewTextBoxColumn, this.balance_col, this.add_col);
		this.DGV.DataSource = this.loanBindingSource;
		this.DGV.Location = new System.Drawing.Point(15, 75);
		this.DGV.Margin = new System.Windows.Forms.Padding(4);
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersWidth = 22;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.Size = new System.Drawing.Size(844, 556);
		this.DGV.TabIndex = 6;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.lblNameDisplay.AutoSize = true;
		this.lblNameDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblNameDisplay.Location = new System.Drawing.Point(12, 9);
		this.lblNameDisplay.Name = "lblNameDisplay";
		this.lblNameDisplay.Size = new System.Drawing.Size(268, 25);
		this.lblNameDisplay.TabIndex = 9;
		this.lblNameDisplay.Text = "Name: JUAN D. DELA CRUZ";
		this.btnApplyNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnApplyNew.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnApplyNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnApplyNew.ForeColor = System.Drawing.Color.White;
		this.btnApplyNew.Location = new System.Drawing.Point(722, 23);
		this.btnApplyNew.Name = "btnApplyNew";
		this.btnApplyNew.Size = new System.Drawing.Size(137, 45);
		this.btnApplyNew.TabIndex = 10;
		this.btnApplyNew.Text = "+ APPLY NEW";
		this.btnApplyNew.UseVisualStyleBackColor = false;
		this.btnApplyNew.Click += new System.EventHandler(btnApplyNew_Click);
		this.loanBindingSource.DataSource = typeof(RealProperty.BEL.Loan);
		this.idloan_col.DataPropertyName = "id";
		this.idloan_col.HeaderText = "id";
		this.idloan_col.Name = "idloan_col";
		this.idloan_col.ReadOnly = true;
		this.idloan_col.Visible = false;
		this.idemployeeDataGridViewTextBoxColumn.DataPropertyName = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.HeaderText = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.Name = "idemployeeDataGridViewTextBoxColumn";
		this.idemployeeDataGridViewTextBoxColumn.ReadOnly = true;
		this.idemployeeDataGridViewTextBoxColumn.Visible = false;
		this.dateappliedDataGridViewTextBoxColumn.DataPropertyName = "dateapplied";
		this.dateappliedDataGridViewTextBoxColumn.HeaderText = "dateapplied";
		this.dateappliedDataGridViewTextBoxColumn.Name = "dateappliedDataGridViewTextBoxColumn";
		this.dateappliedDataGridViewTextBoxColumn.ReadOnly = true;
		this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
		this.descriptionDataGridViewTextBoxColumn.HeaderText = "description";
		this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
		this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
		this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
		this.amountDataGridViewTextBoxColumn.HeaderText = "amount";
		this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
		this.amountDataGridViewTextBoxColumn.ReadOnly = true;
		this.categoryDataGridViewTextBoxColumn.DataPropertyName = "category";
		this.categoryDataGridViewTextBoxColumn.HeaderText = "category";
		this.categoryDataGridViewTextBoxColumn.Name = "categoryDataGridViewTextBoxColumn";
		this.categoryDataGridViewTextBoxColumn.ReadOnly = true;
		this.penaltyDataGridViewTextBoxColumn.DataPropertyName = "penalty";
		this.penaltyDataGridViewTextBoxColumn.HeaderText = "penalty";
		this.penaltyDataGridViewTextBoxColumn.Name = "penaltyDataGridViewTextBoxColumn";
		this.penaltyDataGridViewTextBoxColumn.ReadOnly = true;
		this.penaltyDataGridViewTextBoxColumn.Visible = false;
		this.duedateDataGridViewTextBoxColumn.DataPropertyName = "duedate";
		this.duedateDataGridViewTextBoxColumn.HeaderText = "duedate";
		this.duedateDataGridViewTextBoxColumn.Name = "duedateDataGridViewTextBoxColumn";
		this.duedateDataGridViewTextBoxColumn.ReadOnly = true;
		this.duedateDataGridViewTextBoxColumn.Visible = false;
		this.amortizationDataGridViewTextBoxColumn.DataPropertyName = "amortization";
		this.amortizationDataGridViewTextBoxColumn.HeaderText = "amortization";
		this.amortizationDataGridViewTextBoxColumn.Name = "amortizationDataGridViewTextBoxColumn";
		this.amortizationDataGridViewTextBoxColumn.ReadOnly = true;
		this.remarksDataGridViewTextBoxColumn.DataPropertyName = "remarks";
		this.remarksDataGridViewTextBoxColumn.HeaderText = "remarks";
		this.remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
		this.remarksDataGridViewTextBoxColumn.ReadOnly = true;
		this.remarksDataGridViewTextBoxColumn.Visible = false;
		this.recordstatusDataGridViewTextBoxColumn.DataPropertyName = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.HeaderText = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.Name = "recordstatusDataGridViewTextBoxColumn";
		this.recordstatusDataGridViewTextBoxColumn.ReadOnly = true;
		this.recordstatusDataGridViewTextBoxColumn.Visible = false;
		this.balance_col.DataPropertyName = "balance";
		this.balance_col.HeaderText = "balance";
		this.balance_col.Name = "balance_col";
		this.balance_col.ReadOnly = true;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(1);
		this.add_col.DefaultCellStyle = dataGridViewCellStyle;
		this.add_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.add_col.HeaderText = "Action";
		this.add_col.Name = "add_col";
		this.add_col.ReadOnly = true;
		this.add_col.Text = "add";
		this.add_col.ToolTipText = "click add to add deduction";
		this.add_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(872, 641);
		base.Controls.Add(this.btnApplyNew);
		base.Controls.Add(this.lblNameDisplay);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmSearchLoans";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Search Loans and Purchase";
		base.Load += new System.EventHandler(frmSearchLoans_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.loanBindingSource).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
