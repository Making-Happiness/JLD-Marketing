using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;
using RealProperty.Reports_Model;

namespace RealProperty;

public class frmPayrollDetails : Form
{
	private List<Loan> loan_list;

	private Payrolldetails sel_payrolldetails;

	private IContainer components = null;

	private Panel panel1;

	private Label label1;

	private TextBox txtSearch;

	private SplitContainer splitContainer1;

	private DataGridView DGVEmployeeSalary;

	private Panel panel2;

	private FlowLayoutPanel flowLayoutPanel1;

	private FlowLayoutPanel flowLayoutPanel2;

	private FlowLayoutPanel flowLayoutPanel3;

	private Button btnAddDeduction;

	private DataGridView DGVdeduc;

	private FlowLayoutPanel flowLayoutPanel4;

	private Button btnAddEmployee;

	private FlowLayoutPanel flowLayoutPanel5;

	private Panel panel3;

	private NumericUpDown numMerienda;

	private Label label2;

	private Panel panel4;

	private NumericUpDown numEmergencyFund;

	private Label label3;

	private DataGridViewTextBoxColumn idloan_col;

	private DataGridViewTextBoxColumn title_col;

	private DataGridViewTextBoxColumn totalamount_col;

	private DataGridViewTextBoxColumn deduction_col;

	private DataGridViewButtonColumn action;

	private DataGridViewButtonColumn delete_deduction_col;

	private Panel panel5;

	private NumericUpDown numUndertime;

	private Label label4;

	private Button btnPrint;

	private Button btnGenrate;

	private BindingSource payrolldetailsBindingSource;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dailyrateDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn remove_col;

	private DataGridViewTextBoxColumn idemployeeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idpayrollDataGridViewTextBoxColumn;

	private DataGridViewCheckBoxColumn sunDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn satDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn monDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn tueDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn wedDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn thuDataGridViewCheckBoxColumn;

	private DataGridViewCheckBoxColumn friDataGridViewCheckBoxColumn;

	private DataGridViewTextBoxColumn caDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn meriendaDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn eggDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn othersDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn undertimeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn riceDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn emergencyfundDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn recordstatusDataGridViewTextBoxColumn;

	private Label label5;

	private Panel panel9;

	private NumericUpDown numMon;

	private Label label9;

	private Panel panel6;

	private NumericUpDown numTue;

	private Label label6;

	private Panel panel7;

	private NumericUpDown numWed;

	private Label label7;

	private Panel panel10;

	private NumericUpDown numThu;

	private Label label10;

	private Panel panel11;

	private NumericUpDown numFri;

	private Label label11;

	private Panel panel12;

	private NumericUpDown numSat;

	private Label label12;

	private Button btnSaveDays;

	private Button btnSaveFixDeduc;

	public Payroll payroll { get; set; }

	public Payrolldetails prev_payrolldetails { get; set; }

	public frmPayrollDetails()
	{
		InitializeComponent();
		DGVEmployeeSalary.AutoGenerateColumns = false;
		DGVdeduc.AutoGenerateColumns = false;
		loan_list = new List<Loan>();
		Refresh_DGVDeduc();
		sel_payrolldetails = new Payrolldetails();
	}

	private void Refresh_DGVDeduc()
	{
		DGVdeduc.DataSource = loan_list;
	}

	private void chkMon_CheckedChanged(object sender, EventArgs e)
	{
		CheckBox checkBox = sender as CheckBox;
		if (checkBox.Checked)
		{
			checkBox.ForeColor = SystemColors.ControlText;
			checkBox.Parent.BackColor = SystemColors.Control;
		}
		else
		{
			checkBox.ForeColor = SystemColors.GrayText;
			checkBox.Parent.BackColor = Color.Red;
		}
	}

	private void btnAddEmployee_Click(object sender, EventArgs e)
	{
		using frmSearchEmployee frmSearchEmployee2 = new frmSearchEmployee();
		frmSearchEmployee2.ShowDialog(this);
		Employee sel_row = frmSearchEmployee2.sel_row;
		if (sel_row == null)
		{
			return;
		}
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		int mode_of_salary = 1;
		int num = employeeCtrl.setEmployeModeSalary(sel_row, mode_of_salary);
		int num2 = employeeCtrl.generatePayrollDetailsSingle(payroll.id, sel_row.idemployee);
		if (num2 > 0)
		{
			refresh_DGVEmployeeSalary();
		}
	}

	private void refresh_DGVEmployeeSalary()
	{
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		DataTable table = employeeCtrl.view_payrolldetailswithemployee(payroll.id);
		DGVEmployeeSalary.DataSource = AController.DataTableToList<Payrolldetails>(table);
	}

	private void frmPayrollDetails_Load(object sender, EventArgs e)
	{
		refresh_DGVEmployeeSalary();
	}

	private void DGVEmployeeSalary_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		int id = Convert.ToInt32(DGVEmployeeSalary.CurrentRow.Cells[idemployeeDataGridViewTextBoxColumn.Name].Value);
		if (columnIndex != remove_col.Index)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("Are you sure you want to exclude this employee in the payroll?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		Employee employeeObject = employeeCtrl.getEmployeeObject(id);
		int mode_of_salary = 0;
		int num = employeeCtrl.setEmployeModeSalary(employeeObject, mode_of_salary);
		if (num > 0)
		{
			refresh_DGVEmployeeSalary();
		}
	}

	private void btnAddDeduction_Click(object sender, EventArgs e)
	{
		dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployeeDataGridViewTextBoxColumn.Name].Value;
		object value2 = DGVEmployeeSalary.CurrentRow.Cells[fullnameDataGridViewTextBoxColumn.Name].Value;
		Loan loan = new Loan();
		loan.idemployee = value;
		using frmLoanInfo frmLoanInfo2 = new frmLoanInfo(loan, "ADD");
		frmLoanInfo2.Category = "DEDUCTIONS";
		frmLoanInfo2.Employeename = value2;
		frmLoanInfo2.ShowDialog(this);
		if (frmLoanInfo2.CommitChanges)
		{
			Refresh_DGVDeduc(loan.idemployee);
		}
	}

	private void Refresh_DGVDeduc(int id)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable loansTable = payrollCtrl.getLoansTable(id);
		string filterExpression = string.Format("category not like '%{0}%' AND category not like '%{1}%'", "BENEFIT", "EARNING");
		DataRow[] source = loansTable.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataSource = loansTable.Clone();
			source = null;
			DGVdeduc.DataSource = dataSource;
		}
		else
		{
			DGVdeduc.DataSource = source.CopyToDataTable();
		}
	}

	private void DGVEmployeeSalary_SelectionChanged(object sender, EventArgs e)
	{
		if (DGVEmployeeSalary.CurrentRow != null && DGVEmployeeSalary.CurrentRow.Index >= 0)
		{
			sel_payrolldetails = DGVEmployeeSalary.CurrentRow.DataBoundItem as Payrolldetails;
			dynamic idemployee = sel_payrolldetails.idemployee;
			Refresh_DGVDeduc(idemployee);
			Bind_payrollDetails();
		}
	}

	private void Bind_payrollDetails()
	{
		removeAllBindindings();
		numMon.DataBindings.Add("Value", sel_payrolldetails, "mon", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numTue.DataBindings.Add("Value", sel_payrolldetails, "tue", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numWed.DataBindings.Add("Value", sel_payrolldetails, "wed", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numThu.DataBindings.Add("Value", sel_payrolldetails, "thu", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numFri.DataBindings.Add("Value", sel_payrolldetails, "fri", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numSat.DataBindings.Add("Value", sel_payrolldetails, "sat", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numMerienda.DataBindings.Add("Value", sel_payrolldetails, "merienda", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numUndertime.DataBindings.Add("Value", sel_payrolldetails, "undertime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numEmergencyFund.DataBindings.Add("Value", sel_payrolldetails, "emergencyfund", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		prev_payrolldetails = (Payrolldetails)sel_payrolldetails.Clone();
	}

	private void removeAllBindindings()
	{
		if (numMon.DataBindings != null)
		{
			numMon.DataBindings.Clear();
		}
		if (numTue.DataBindings != null)
		{
			numTue.DataBindings.Clear();
		}
		if (numWed.DataBindings != null)
		{
			numWed.DataBindings.Clear();
		}
		if (numThu.DataBindings != null)
		{
			numThu.DataBindings.Clear();
		}
		if (numFri.DataBindings != null)
		{
			numFri.DataBindings.Clear();
		}
		if (numSat.DataBindings != null)
		{
			numSat.DataBindings.Clear();
		}
		if (numMerienda.DataBindings != null)
		{
			numMerienda.DataBindings.Clear();
		}
		if (numUndertime.DataBindings != null)
		{
			numUndertime.DataBindings.Clear();
		}
		if (numEmergencyFund.DataBindings != null)
		{
			numEmergencyFund.DataBindings.Clear();
		}
	}

	private void chkDays_MouseClick(object sender, MouseEventArgs e)
	{
		updatePayrollDetails();
	}

	private void updatePayrollDetails()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		int num = payrollCtrl.editPayrollDetails(sel_payrolldetails);
		if (num > 0)
		{
			MessageBox.Show("Changes Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			prev_payrolldetails = (Payrolldetails)sel_payrolldetails.Clone();
		}
		else
		{
			MessageBox.Show("Changes Not Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			sel_payrolldetails = (Payrolldetails)prev_payrolldetails.Clone();
			Bind_payrollDetails();
		}
	}

	private void num_Validated(object sender, EventArgs e)
	{
	}

	private void num_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			num_Validated(null, null);
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		AReports reports = new rpt_payroll(payroll);
		frmReportView frmReportView2 = new frmReportView();
		frmReportView2.reports = reports;
		frmReportView2.Show();
	}

	private void btnSaveDays_Click(object sender, EventArgs e)
	{
		updatePayrollDetails();
	}

	private void btnSaveFixDeduc_Click(object sender, EventArgs e)
	{
		updatePayrollDetails();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnPrint = new System.Windows.Forms.Button();
		this.btnGenrate = new System.Windows.Forms.Button();
		this.btnAddEmployee = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.DGVEmployeeSalary = new System.Windows.Forms.DataGridView();
		this.remove_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
		this.label5 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.numMerienda = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.panel4 = new System.Windows.Forms.Panel();
		this.numEmergencyFund = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.panel5 = new System.Windows.Forms.Panel();
		this.numUndertime = new System.Windows.Forms.NumericUpDown();
		this.label4 = new System.Windows.Forms.Label();
		this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
		this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnAddDeduction = new System.Windows.Forms.Button();
		this.DGVdeduc = new System.Windows.Forms.DataGridView();
		this.idloan_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.title_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalamount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.deduction_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.action = new System.Windows.Forms.DataGridViewButtonColumn();
		this.delete_deduction_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.numMon = new System.Windows.Forms.NumericUpDown();
		this.label9 = new System.Windows.Forms.Label();
		this.panel6 = new System.Windows.Forms.Panel();
		this.numTue = new System.Windows.Forms.NumericUpDown();
		this.label6 = new System.Windows.Forms.Label();
		this.panel7 = new System.Windows.Forms.Panel();
		this.numWed = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.panel10 = new System.Windows.Forms.Panel();
		this.numThu = new System.Windows.Forms.NumericUpDown();
		this.label10 = new System.Windows.Forms.Label();
		this.panel11 = new System.Windows.Forms.Panel();
		this.numFri = new System.Windows.Forms.NumericUpDown();
		this.label11 = new System.Windows.Forms.Label();
		this.panel12 = new System.Windows.Forms.Panel();
		this.numSat = new System.Windows.Forms.NumericUpDown();
		this.label12 = new System.Windows.Forms.Label();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dailyrateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idemployeeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idpayrollDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sunDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.satDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.monDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.tueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.wedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.thuDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.friDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.caDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.meriendaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.eggDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.othersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.undertimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.riceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.emergencyfundDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payrolldetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.btnSaveDays = new System.Windows.Forms.Button();
		this.btnSaveFixDeduc = new System.Windows.Forms.Button();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).BeginInit();
		this.flowLayoutPanel2.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.flowLayoutPanel5.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numMerienda).BeginInit();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numEmergencyFund).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numUndertime).BeginInit();
		this.flowLayoutPanel3.SuspendLayout();
		this.flowLayoutPanel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVdeduc).BeginInit();
		this.panel9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numMon).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numTue).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numWed).BeginInit();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numThu).BeginInit();
		this.panel11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numFri).BeginInit();
		this.panel12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numSat).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.payrolldetailsBindingSource).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.btnPrint);
		this.panel1.Controls.Add(this.btnGenrate);
		this.panel1.Controls.Add(this.btnAddEmployee);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.txtSearch);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(1, 1);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1166, 67);
		this.panel1.TabIndex = 22;
		this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnPrint.Location = new System.Drawing.Point(1046, 9);
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(117, 52);
		this.btnPrint.TabIndex = 15;
		this.btnPrint.Text = "Print / Preview";
		this.btnPrint.UseVisualStyleBackColor = true;
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.btnGenrate.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnGenrate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnGenrate.Location = new System.Drawing.Point(418, 9);
		this.btnGenrate.Name = "btnGenrate";
		this.btnGenrate.Size = new System.Drawing.Size(117, 52);
		this.btnGenrate.TabIndex = 15;
		this.btnGenrate.Text = "Re-Generate Update";
		this.btnGenrate.UseVisualStyleBackColor = false;
		this.btnGenrate.Click += new System.EventHandler(btnAddEmployee_Click);
		this.btnAddEmployee.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnAddEmployee.Location = new System.Drawing.Point(288, 27);
		this.btnAddEmployee.Name = "btnAddEmployee";
		this.btnAddEmployee.Size = new System.Drawing.Size(117, 34);
		this.btnAddEmployee.TabIndex = 15;
		this.btnAddEmployee.Text = "add employee";
		this.btnAddEmployee.UseVisualStyleBackColor = false;
		this.btnAddEmployee.Click += new System.EventHandler(btnAddEmployee_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(55, 18);
		this.label1.TabIndex = 14;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(3, 33);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(259, 28);
		this.txtSearch.TabIndex = 13;
		this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.IsSplitterFixed = true;
		this.splitContainer1.Location = new System.Drawing.Point(1, 68);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.DGVEmployeeSalary);
		this.splitContainer1.Panel1MinSize = 400;
		this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
		this.splitContainer1.Panel2.Controls.Add(this.panel2);
		this.splitContainer1.Size = new System.Drawing.Size(1166, 593);
		this.splitContainer1.SplitterDistance = 406;
		this.splitContainer1.TabIndex = 23;
		this.DGVEmployeeSalary.AllowUserToAddRows = false;
		this.DGVEmployeeSalary.AutoGenerateColumns = false;
		this.DGVEmployeeSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVEmployeeSalary.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.fullnameDataGridViewTextBoxColumn, this.dailyrateDataGridViewTextBoxColumn, this.remove_col, this.idemployeeDataGridViewTextBoxColumn, this.idpayrollDataGridViewTextBoxColumn, this.sunDataGridViewCheckBoxColumn, this.satDataGridViewCheckBoxColumn, this.monDataGridViewCheckBoxColumn, this.tueDataGridViewCheckBoxColumn, this.wedDataGridViewCheckBoxColumn, this.thuDataGridViewCheckBoxColumn, this.friDataGridViewCheckBoxColumn, this.caDataGridViewTextBoxColumn, this.meriendaDataGridViewTextBoxColumn, this.eggDataGridViewTextBoxColumn, this.othersDataGridViewTextBoxColumn, this.undertimeDataGridViewTextBoxColumn, this.riceDataGridViewTextBoxColumn, this.emergencyfundDataGridViewTextBoxColumn, this.recordstatusDataGridViewTextBoxColumn);
		this.DGVEmployeeSalary.DataSource = this.payrolldetailsBindingSource;
		this.DGVEmployeeSalary.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVEmployeeSalary.Location = new System.Drawing.Point(0, 0);
		this.DGVEmployeeSalary.MultiSelect = false;
		this.DGVEmployeeSalary.Name = "DGVEmployeeSalary";
		this.DGVEmployeeSalary.ReadOnly = true;
		this.DGVEmployeeSalary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVEmployeeSalary.RowHeadersWidth = 20;
		this.DGVEmployeeSalary.RowTemplate.Height = 24;
		this.DGVEmployeeSalary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVEmployeeSalary.Size = new System.Drawing.Size(404, 591);
		this.DGVEmployeeSalary.TabIndex = 16;
		this.DGVEmployeeSalary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVEmployeeSalary_CellClick);
		this.DGVEmployeeSalary.SelectionChanged += new System.EventHandler(DGVEmployeeSalary_SelectionChanged);
		this.remove_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(3);
		this.remove_col.DefaultCellStyle = dataGridViewCellStyle;
		this.remove_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.remove_col.HeaderText = "action";
		this.remove_col.Name = "remove_col";
		this.remove_col.ReadOnly = true;
		this.remove_col.Text = "remove";
		this.remove_col.ToolTipText = "Apply loan / CA";
		this.remove_col.UseColumnTextForButtonValue = true;
		this.remove_col.Width = 5;
		this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel1);
		this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel5);
		this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
		this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Size = new System.Drawing.Size(754, 537);
		this.flowLayoutPanel2.TabIndex = 19;
		this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
		this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel1.Controls.Add(this.panel9);
		this.flowLayoutPanel1.Controls.Add(this.panel6);
		this.flowLayoutPanel1.Controls.Add(this.panel7);
		this.flowLayoutPanel1.Controls.Add(this.panel10);
		this.flowLayoutPanel1.Controls.Add(this.panel11);
		this.flowLayoutPanel1.Controls.Add(this.panel12);
		this.flowLayoutPanel1.Controls.Add(this.btnSaveDays);
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(662, 66);
		this.flowLayoutPanel1.TabIndex = 18;
		this.flowLayoutPanel5.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.flowLayoutPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel5.Controls.Add(this.label5);
		this.flowLayoutPanel5.Controls.Add(this.panel3);
		this.flowLayoutPanel5.Controls.Add(this.panel4);
		this.flowLayoutPanel5.Controls.Add(this.panel5);
		this.flowLayoutPanel5.Controls.Add(this.btnSaveFixDeduc);
		this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 75);
		this.flowLayoutPanel5.Name = "flowLayoutPanel5";
		this.flowLayoutPanel5.Size = new System.Drawing.Size(662, 95);
		this.flowLayoutPanel5.TabIndex = 20;
		this.label5.Dock = System.Windows.Forms.DockStyle.Top;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.Blue;
		this.label5.Location = new System.Drawing.Point(3, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(604, 20);
		this.label5.TabIndex = 4;
		this.label5.Text = "Note: Press enter to update / save";
		this.panel3.AutoSize = true;
		this.panel3.Controls.Add(this.numMerienda);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Location = new System.Drawing.Point(3, 23);
		this.panel3.Name = "panel3";
		this.panel3.Padding = new System.Windows.Forms.Padding(5);
		this.panel3.Size = new System.Drawing.Size(141, 58);
		this.panel3.TabIndex = 0;
		this.numMerienda.DecimalPlaces = 2;
		this.numMerienda.Location = new System.Drawing.Point(10, 26);
		this.numMerienda.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.numMerienda.Name = "numMerienda";
		this.numMerienda.Size = new System.Drawing.Size(123, 24);
		this.numMerienda.TabIndex = 1;
		this.numMerienda.KeyUp += new System.Windows.Forms.KeyEventHandler(num_KeyUp);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(7, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 18);
		this.label2.TabIndex = 0;
		this.label2.Text = "Merienda";
		this.panel4.AutoSize = true;
		this.panel4.Controls.Add(this.numEmergencyFund);
		this.panel4.Controls.Add(this.label3);
		this.panel4.Location = new System.Drawing.Point(150, 23);
		this.panel4.Name = "panel4";
		this.panel4.Padding = new System.Windows.Forms.Padding(5);
		this.panel4.Size = new System.Drawing.Size(141, 58);
		this.panel4.TabIndex = 1;
		this.numEmergencyFund.DecimalPlaces = 2;
		this.numEmergencyFund.Location = new System.Drawing.Point(10, 26);
		this.numEmergencyFund.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.numEmergencyFund.Name = "numEmergencyFund";
		this.numEmergencyFund.Size = new System.Drawing.Size(123, 24);
		this.numEmergencyFund.TabIndex = 1;
		this.numEmergencyFund.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.numEmergencyFund.KeyUp += new System.Windows.Forms.KeyEventHandler(num_KeyUp);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(7, 5);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(120, 18);
		this.label3.TabIndex = 0;
		this.label3.Text = "Emergency Fund";
		this.panel5.AutoSize = true;
		this.panel5.Controls.Add(this.numUndertime);
		this.panel5.Controls.Add(this.label4);
		this.panel5.Location = new System.Drawing.Point(297, 23);
		this.panel5.Name = "panel5";
		this.panel5.Padding = new System.Windows.Forms.Padding(5);
		this.panel5.Size = new System.Drawing.Size(141, 58);
		this.panel5.TabIndex = 2;
		this.numUndertime.DecimalPlaces = 2;
		this.numUndertime.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numUndertime.Location = new System.Drawing.Point(10, 26);
		this.numUndertime.Maximum = new decimal(new int[4] { 99999999, 0, 0, 131072 });
		this.numUndertime.Name = "numUndertime";
		this.numUndertime.Size = new System.Drawing.Size(123, 24);
		this.numUndertime.TabIndex = 1;
		this.numUndertime.ThousandsSeparator = true;
		this.numUndertime.KeyUp += new System.Windows.Forms.KeyEventHandler(num_KeyUp);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 5);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(76, 18);
		this.label4.TabIndex = 0;
		this.label4.Text = "Undertime";
		this.flowLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
		this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
		this.flowLayoutPanel3.Controls.Add(this.DGVdeduc);
		this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 176);
		this.flowLayoutPanel3.Name = "flowLayoutPanel3";
		this.flowLayoutPanel3.Size = new System.Drawing.Size(662, 213);
		this.flowLayoutPanel3.TabIndex = 19;
		this.flowLayoutPanel4.Controls.Add(this.btnAddDeduction);
		this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
		this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
		this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.flowLayoutPanel4.Name = "flowLayoutPanel4";
		this.flowLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.flowLayoutPanel4.Size = new System.Drawing.Size(654, 45);
		this.flowLayoutPanel4.TabIndex = 8;
		this.btnAddDeduction.BackColor = System.Drawing.Color.DarkRed;
		this.btnAddDeduction.ForeColor = System.Drawing.SystemColors.ButtonFace;
		this.btnAddDeduction.Location = new System.Drawing.Point(513, 6);
		this.btnAddDeduction.Name = "btnAddDeduction";
		this.btnAddDeduction.Size = new System.Drawing.Size(138, 36);
		this.btnAddDeduction.TabIndex = 6;
		this.btnAddDeduction.Text = "Add Deduction";
		this.btnAddDeduction.UseVisualStyleBackColor = false;
		this.btnAddDeduction.Click += new System.EventHandler(btnAddDeduction_Click);
		this.DGVdeduc.AllowUserToAddRows = false;
		this.DGVdeduc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVdeduc.Columns.AddRange(this.idloan_col, this.title_col, this.totalamount_col, this.deduction_col, this.action, this.delete_deduction_col);
		this.DGVdeduc.Location = new System.Drawing.Point(3, 51);
		this.DGVdeduc.Name = "DGVdeduc";
		this.DGVdeduc.ReadOnly = true;
		this.DGVdeduc.RowHeadersWidth = 21;
		this.DGVdeduc.RowTemplate.Height = 24;
		this.DGVdeduc.Size = new System.Drawing.Size(654, 150);
		this.DGVdeduc.TabIndex = 7;
		this.idloan_col.DataPropertyName = "id";
		this.idloan_col.HeaderText = "id";
		this.idloan_col.Name = "idloan_col";
		this.idloan_col.ReadOnly = true;
		this.idloan_col.Visible = false;
		this.title_col.DataPropertyName = "description";
		this.title_col.HeaderText = "title";
		this.title_col.Name = "title_col";
		this.title_col.ReadOnly = true;
		this.title_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.totalamount_col.DataPropertyName = "amount";
		this.totalamount_col.HeaderText = "totalamount";
		this.totalamount_col.Name = "totalamount_col";
		this.totalamount_col.ReadOnly = true;
		this.deduction_col.DataPropertyName = "amortization";
		this.deduction_col.HeaderText = "deduction";
		this.deduction_col.Name = "deduction_col";
		this.deduction_col.ReadOnly = true;
		this.action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
		this.action.DefaultCellStyle = dataGridViewCellStyle2;
		this.action.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.action.HeaderText = "edit";
		this.action.Name = "action";
		this.action.ReadOnly = true;
		this.action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.action.Text = "edit";
		this.action.UseColumnTextForButtonValue = true;
		this.action.Width = 5;
		this.delete_deduction_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red;
		this.delete_deduction_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.delete_deduction_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_deduction_col.HeaderText = "remove";
		this.delete_deduction_col.Name = "delete_deduction_col";
		this.delete_deduction_col.ReadOnly = true;
		this.delete_deduction_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.delete_deduction_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.delete_deduction_col.Text = "remove";
		this.delete_deduction_col.UseColumnTextForButtonValue = true;
		this.delete_deduction_col.Width = 5;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 537);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(754, 54);
		this.panel2.TabIndex = 17;
		this.panel9.AutoSize = true;
		this.panel9.Controls.Add(this.numMon);
		this.panel9.Controls.Add(this.label9);
		this.panel9.Location = new System.Drawing.Point(3, 3);
		this.panel9.Name = "panel9";
		this.panel9.Padding = new System.Windows.Forms.Padding(5);
		this.panel9.Size = new System.Drawing.Size(84, 58);
		this.panel9.TabIndex = 9;
		this.numMon.DecimalPlaces = 2;
		this.numMon.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numMon.Location = new System.Drawing.Point(10, 26);
		this.numMon.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numMon.Name = "numMon";
		this.numMon.Size = new System.Drawing.Size(66, 24);
		this.numMon.TabIndex = 1;
		this.numMon.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(7, 5);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(61, 18);
		this.label9.TabIndex = 0;
		this.label9.Text = "Monday";
		this.panel6.AutoSize = true;
		this.panel6.Controls.Add(this.numTue);
		this.panel6.Controls.Add(this.label6);
		this.panel6.Location = new System.Drawing.Point(93, 3);
		this.panel6.Name = "panel6";
		this.panel6.Padding = new System.Windows.Forms.Padding(5);
		this.panel6.Size = new System.Drawing.Size(84, 58);
		this.panel6.TabIndex = 10;
		this.numTue.DecimalPlaces = 2;
		this.numTue.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numTue.Location = new System.Drawing.Point(10, 26);
		this.numTue.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numTue.Name = "numTue";
		this.numTue.Size = new System.Drawing.Size(66, 24);
		this.numTue.TabIndex = 1;
		this.numTue.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(7, 5);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(64, 18);
		this.label6.TabIndex = 0;
		this.label6.Text = "Tuesday";
		this.panel7.AutoSize = true;
		this.panel7.Controls.Add(this.numWed);
		this.panel7.Controls.Add(this.label7);
		this.panel7.Location = new System.Drawing.Point(183, 3);
		this.panel7.Name = "panel7";
		this.panel7.Padding = new System.Windows.Forms.Padding(5);
		this.panel7.Size = new System.Drawing.Size(101, 58);
		this.panel7.TabIndex = 11;
		this.numWed.DecimalPlaces = 2;
		this.numWed.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numWed.Location = new System.Drawing.Point(10, 26);
		this.numWed.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numWed.Name = "numWed";
		this.numWed.Size = new System.Drawing.Size(66, 24);
		this.numWed.TabIndex = 1;
		this.numWed.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(7, 5);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(86, 18);
		this.label7.TabIndex = 0;
		this.label7.Text = "Wednesday";
		this.panel10.AutoSize = true;
		this.panel10.Controls.Add(this.numThu);
		this.panel10.Controls.Add(this.label10);
		this.panel10.Location = new System.Drawing.Point(290, 3);
		this.panel10.Name = "panel10";
		this.panel10.Padding = new System.Windows.Forms.Padding(5);
		this.panel10.Size = new System.Drawing.Size(84, 58);
		this.panel10.TabIndex = 13;
		this.numThu.DecimalPlaces = 2;
		this.numThu.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numThu.Location = new System.Drawing.Point(10, 26);
		this.numThu.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numThu.Name = "numThu";
		this.numThu.Size = new System.Drawing.Size(66, 24);
		this.numThu.TabIndex = 1;
		this.numThu.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(7, 5);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(69, 18);
		this.label10.TabIndex = 0;
		this.label10.Text = "Thursday";
		this.panel11.AutoSize = true;
		this.panel11.Controls.Add(this.numFri);
		this.panel11.Controls.Add(this.label11);
		this.panel11.Location = new System.Drawing.Point(380, 3);
		this.panel11.Name = "panel11";
		this.panel11.Padding = new System.Windows.Forms.Padding(5);
		this.panel11.Size = new System.Drawing.Size(84, 58);
		this.panel11.TabIndex = 14;
		this.numFri.DecimalPlaces = 2;
		this.numFri.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numFri.Location = new System.Drawing.Point(10, 26);
		this.numFri.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numFri.Name = "numFri";
		this.numFri.Size = new System.Drawing.Size(66, 24);
		this.numFri.TabIndex = 1;
		this.numFri.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(7, 5);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(48, 18);
		this.label11.TabIndex = 0;
		this.label11.Text = "Friday";
		this.panel12.AutoSize = true;
		this.panel12.Controls.Add(this.numSat);
		this.panel12.Controls.Add(this.label12);
		this.panel12.Location = new System.Drawing.Point(470, 3);
		this.panel12.Name = "panel12";
		this.panel12.Padding = new System.Windows.Forms.Padding(5);
		this.panel12.Size = new System.Drawing.Size(84, 58);
		this.panel12.TabIndex = 15;
		this.numSat.DecimalPlaces = 2;
		this.numSat.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.numSat.Location = new System.Drawing.Point(10, 26);
		this.numSat.Maximum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.numSat.Name = "numSat";
		this.numSat.Size = new System.Drawing.Size(66, 24);
		this.numSat.TabIndex = 1;
		this.numSat.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(7, 5);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(66, 18);
		this.label12.TabIndex = 0;
		this.label12.Text = "Saturday";
		this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
		this.idDataGridViewTextBoxColumn.HeaderText = "id";
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.fullnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.fullnameDataGridViewTextBoxColumn.DataPropertyName = "fullname";
		this.fullnameDataGridViewTextBoxColumn.HeaderText = "fullname";
		this.fullnameDataGridViewTextBoxColumn.Name = "fullnameDataGridViewTextBoxColumn";
		this.fullnameDataGridViewTextBoxColumn.ReadOnly = true;
		this.dailyrateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.dailyrateDataGridViewTextBoxColumn.DataPropertyName = "daily_rate";
		this.dailyrateDataGridViewTextBoxColumn.HeaderText = "daily_rate";
		this.dailyrateDataGridViewTextBoxColumn.Name = "dailyrateDataGridViewTextBoxColumn";
		this.dailyrateDataGridViewTextBoxColumn.ReadOnly = true;
		this.dailyrateDataGridViewTextBoxColumn.Width = 5;
		this.idemployeeDataGridViewTextBoxColumn.DataPropertyName = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.HeaderText = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.Name = "idemployeeDataGridViewTextBoxColumn";
		this.idemployeeDataGridViewTextBoxColumn.ReadOnly = true;
		this.idemployeeDataGridViewTextBoxColumn.Visible = false;
		this.idpayrollDataGridViewTextBoxColumn.DataPropertyName = "idpayroll";
		this.idpayrollDataGridViewTextBoxColumn.HeaderText = "idpayroll";
		this.idpayrollDataGridViewTextBoxColumn.Name = "idpayrollDataGridViewTextBoxColumn";
		this.idpayrollDataGridViewTextBoxColumn.ReadOnly = true;
		this.idpayrollDataGridViewTextBoxColumn.Visible = false;
		this.sunDataGridViewCheckBoxColumn.DataPropertyName = "sun";
		this.sunDataGridViewCheckBoxColumn.HeaderText = "sun";
		this.sunDataGridViewCheckBoxColumn.Name = "sunDataGridViewCheckBoxColumn";
		this.sunDataGridViewCheckBoxColumn.ReadOnly = true;
		this.sunDataGridViewCheckBoxColumn.Visible = false;
		this.satDataGridViewCheckBoxColumn.DataPropertyName = "sat";
		this.satDataGridViewCheckBoxColumn.HeaderText = "sat";
		this.satDataGridViewCheckBoxColumn.Name = "satDataGridViewCheckBoxColumn";
		this.satDataGridViewCheckBoxColumn.ReadOnly = true;
		this.satDataGridViewCheckBoxColumn.Visible = false;
		this.monDataGridViewCheckBoxColumn.DataPropertyName = "mon";
		this.monDataGridViewCheckBoxColumn.HeaderText = "mon";
		this.monDataGridViewCheckBoxColumn.Name = "monDataGridViewCheckBoxColumn";
		this.monDataGridViewCheckBoxColumn.ReadOnly = true;
		this.monDataGridViewCheckBoxColumn.Visible = false;
		this.tueDataGridViewCheckBoxColumn.DataPropertyName = "tue";
		this.tueDataGridViewCheckBoxColumn.HeaderText = "tue";
		this.tueDataGridViewCheckBoxColumn.Name = "tueDataGridViewCheckBoxColumn";
		this.tueDataGridViewCheckBoxColumn.ReadOnly = true;
		this.tueDataGridViewCheckBoxColumn.Visible = false;
		this.wedDataGridViewCheckBoxColumn.DataPropertyName = "wed";
		this.wedDataGridViewCheckBoxColumn.HeaderText = "wed";
		this.wedDataGridViewCheckBoxColumn.Name = "wedDataGridViewCheckBoxColumn";
		this.wedDataGridViewCheckBoxColumn.ReadOnly = true;
		this.wedDataGridViewCheckBoxColumn.Visible = false;
		this.thuDataGridViewCheckBoxColumn.DataPropertyName = "thu";
		this.thuDataGridViewCheckBoxColumn.HeaderText = "thu";
		this.thuDataGridViewCheckBoxColumn.Name = "thuDataGridViewCheckBoxColumn";
		this.thuDataGridViewCheckBoxColumn.ReadOnly = true;
		this.thuDataGridViewCheckBoxColumn.Visible = false;
		this.friDataGridViewCheckBoxColumn.DataPropertyName = "fri";
		this.friDataGridViewCheckBoxColumn.HeaderText = "fri";
		this.friDataGridViewCheckBoxColumn.Name = "friDataGridViewCheckBoxColumn";
		this.friDataGridViewCheckBoxColumn.ReadOnly = true;
		this.friDataGridViewCheckBoxColumn.Visible = false;
		this.caDataGridViewTextBoxColumn.DataPropertyName = "ca";
		this.caDataGridViewTextBoxColumn.HeaderText = "ca";
		this.caDataGridViewTextBoxColumn.Name = "caDataGridViewTextBoxColumn";
		this.caDataGridViewTextBoxColumn.ReadOnly = true;
		this.caDataGridViewTextBoxColumn.Visible = false;
		this.meriendaDataGridViewTextBoxColumn.DataPropertyName = "merienda";
		this.meriendaDataGridViewTextBoxColumn.HeaderText = "merienda";
		this.meriendaDataGridViewTextBoxColumn.Name = "meriendaDataGridViewTextBoxColumn";
		this.meriendaDataGridViewTextBoxColumn.ReadOnly = true;
		this.meriendaDataGridViewTextBoxColumn.Visible = false;
		this.eggDataGridViewTextBoxColumn.DataPropertyName = "egg";
		this.eggDataGridViewTextBoxColumn.HeaderText = "egg";
		this.eggDataGridViewTextBoxColumn.Name = "eggDataGridViewTextBoxColumn";
		this.eggDataGridViewTextBoxColumn.ReadOnly = true;
		this.eggDataGridViewTextBoxColumn.Visible = false;
		this.othersDataGridViewTextBoxColumn.DataPropertyName = "others";
		this.othersDataGridViewTextBoxColumn.HeaderText = "others";
		this.othersDataGridViewTextBoxColumn.Name = "othersDataGridViewTextBoxColumn";
		this.othersDataGridViewTextBoxColumn.ReadOnly = true;
		this.othersDataGridViewTextBoxColumn.Visible = false;
		this.undertimeDataGridViewTextBoxColumn.DataPropertyName = "undertime";
		this.undertimeDataGridViewTextBoxColumn.HeaderText = "undertime";
		this.undertimeDataGridViewTextBoxColumn.Name = "undertimeDataGridViewTextBoxColumn";
		this.undertimeDataGridViewTextBoxColumn.ReadOnly = true;
		this.undertimeDataGridViewTextBoxColumn.Visible = false;
		this.riceDataGridViewTextBoxColumn.DataPropertyName = "rice";
		this.riceDataGridViewTextBoxColumn.HeaderText = "rice";
		this.riceDataGridViewTextBoxColumn.Name = "riceDataGridViewTextBoxColumn";
		this.riceDataGridViewTextBoxColumn.ReadOnly = true;
		this.riceDataGridViewTextBoxColumn.Visible = false;
		this.emergencyfundDataGridViewTextBoxColumn.DataPropertyName = "emergencyfund";
		this.emergencyfundDataGridViewTextBoxColumn.HeaderText = "emergencyfund";
		this.emergencyfundDataGridViewTextBoxColumn.Name = "emergencyfundDataGridViewTextBoxColumn";
		this.emergencyfundDataGridViewTextBoxColumn.ReadOnly = true;
		this.emergencyfundDataGridViewTextBoxColumn.Visible = false;
		this.recordstatusDataGridViewTextBoxColumn.DataPropertyName = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.HeaderText = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.Name = "recordstatusDataGridViewTextBoxColumn";
		this.recordstatusDataGridViewTextBoxColumn.ReadOnly = true;
		this.recordstatusDataGridViewTextBoxColumn.Visible = false;
		this.payrolldetailsBindingSource.DataSource = typeof(RealProperty.BEL.Payrolldetails);
		this.btnSaveDays.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnSaveDays.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSaveDays.Location = new System.Drawing.Point(560, 3);
		this.btnSaveDays.Name = "btnSaveDays";
		this.btnSaveDays.Size = new System.Drawing.Size(97, 58);
		this.btnSaveDays.TabIndex = 15;
		this.btnSaveDays.Text = "SAVE CHANGES";
		this.btnSaveDays.UseVisualStyleBackColor = false;
		this.btnSaveDays.Click += new System.EventHandler(btnSaveDays_Click);
		this.btnSaveFixDeduc.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnSaveFixDeduc.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSaveFixDeduc.Location = new System.Drawing.Point(444, 23);
		this.btnSaveFixDeduc.Name = "btnSaveFixDeduc";
		this.btnSaveFixDeduc.Size = new System.Drawing.Size(97, 58);
		this.btnSaveFixDeduc.TabIndex = 16;
		this.btnSaveFixDeduc.Text = "SAVE CHANGES";
		this.btnSaveFixDeduc.UseVisualStyleBackColor = false;
		this.btnSaveFixDeduc.Click += new System.EventHandler(btnSaveFixDeduc_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1168, 662);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPayrollDetails";
		base.Padding = new System.Windows.Forms.Padding(1);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmPayrollDetails";
		base.Load += new System.EventHandler(frmPayrollDetails_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).EndInit();
		this.flowLayoutPanel2.ResumeLayout(false);
		this.flowLayoutPanel2.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.flowLayoutPanel5.ResumeLayout(false);
		this.flowLayoutPanel5.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numMerienda).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numEmergencyFund).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numUndertime).EndInit();
		this.flowLayoutPanel3.ResumeLayout(false);
		this.flowLayoutPanel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVdeduc).EndInit();
		this.panel9.ResumeLayout(false);
		this.panel9.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numMon).EndInit();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numTue).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numWed).EndInit();
		this.panel10.ResumeLayout(false);
		this.panel10.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numThu).EndInit();
		this.panel11.ResumeLayout(false);
		this.panel11.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numFri).EndInit();
		this.panel12.ResumeLayout(false);
		this.panel12.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numSat).EndInit();
		((System.ComponentModel.ISupportInitialize)this.payrolldetailsBindingSource).EndInit();
		base.ResumeLayout(false);
	}
}
