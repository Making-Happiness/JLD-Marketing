using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;
using RealProperty.Reports_Model;

namespace RealProperty;

public class frmPayslipDetails : Form
{
	private Payrollsettings sel_row;

	private IContainer components = null;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Button btnClose;

	private Panel panel1;

	private Label label2;

	private DataGridView DGVEmployeeSalary;

	private DataGridView DGVGroupSet;

	private Button btnNew;

	private DataGridView DGVPayslipDetails;

	private SplitContainer splitContainer1;

	private DataGridViewTextBoxColumn idpayslipdetails_col;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn title_col;

	private DataGridViewTextBoxColumn payslipcategory_col;

	private DataGridViewTextBoxColumn amountpayslipdetails_col;

	private DataGridViewButtonColumn editpaylisdetails_col;

	private DataGridViewTextBoxColumn id_col;

	private DataGridViewTextBoxColumn description_col;

	private DataGridViewTextBoxColumn category_col;

	private DataGridViewTextBoxColumn recordstatus_col;

	private DataGridViewTextBoxColumn amount_col;

	private DataGridViewButtonColumn payroll_edit_settings_col;

	private DataGridViewButtonColumn payroll_remove_settings_col;

	private Button btnDeductions;

	private Button btnEarnings;

	private Splitter splitter1;

	private Splitter splitter2;

	private DataGridViewTextBoxColumn idemployee_col;

	private DataGridViewTextBoxColumn fullname_col;

	private DataGridViewTextBoxColumn salary_col;

	private DataGridViewButtonColumn adddetails_col;

	private Button button1;

	private Button btnPreview;

	public Payslip payslip { get; set; }

	public frmPayslipDetails()
	{
		InitializeComponent();
		DGVGroupSet.AutoGenerateColumns = false;
		DGVEmployeeSalary.AutoGenerateColumns = false;
		DGVPayslipDetails.AutoGenerateColumns = false;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Refresh_DGVGroupSet()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable payrollsettingsTable = payrollCtrl.getPayrollsettingsTable();
		DGVGroupSet.DataSource = payrollsettingsTable;
	}

	private void refresh_DGVEmployeeSalary()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable employeeSalaryTable = payrollCtrl.getEmployeeSalaryTable();
		DGVEmployeeSalary.DataSource = employeeSalaryTable;
		if (DGVEmployeeSalary.Rows.Count >= employeeSalaryTable.Rows.Count)
		{
			dynamic value = DGVEmployeeSalary.Rows[0].Cells[idemployee_col.Name].Value;
			refresh_DGVPayslipDetails(value);
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		Payrollsettings payrollsettings = new Payrollsettings();
		using frmPayrollGrpSettings frmPayrollGrpSettings2 = new frmPayrollGrpSettings(payrollsettings);
		frmPayrollGrpSettings2.Tansaction = "ADD";
		frmPayrollGrpSettings2.ShowDialog(this);
		if (frmPayrollGrpSettings2.commitcahnges)
		{
			Refresh_DGVGroupSet();
		}
	}

	private void frmPayroll_Load(object sender, EventArgs e)
	{
		Refresh_DGVGroupSet();
		refresh_DGVEmployeeSalary();
	}

	private void DGVGroupSet_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		PropertyInfo[] properties;
		if (columnIndex == DGVGroupSet.Columns["payroll_edit_settings_col"].Index)
		{
			sel_row = AController.DataRowToClass<Payrollsettings>(((DataRowView)DGVGroupSet.CurrentRow.DataBoundItem).Row);
			using frmPayrollGrpSettings frmPayrollGrpSettings2 = new frmPayrollGrpSettings(sel_row);
			frmPayrollGrpSettings2.ShowDialog(this);
			if (!frmPayrollGrpSettings2.commitcahnges)
			{
				return;
			}
			sel_row = null;
			sel_row = frmPayrollGrpSettings2.payrollsettings;
			properties = sel_row.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				try
				{
					DGVGroupSet.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
				}
				catch (Exception)
				{
				}
			}
			return;
		}
		if (columnIndex != DGVGroupSet.Columns["payroll_remove_settings_col"].Index)
		{
			return;
		}
		sel_row = AController.DataRowToClass<Payrollsettings>(((DataRowView)DGVGroupSet.CurrentRow.DataBoundItem).Row);
		DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Alert information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		PayrollCtrl payrollCtrl = new PayrollCtrl();
		string recordstatus = sel_row.recordstatus;
		sel_row.recordstatus = "deleted";
		int num = payrollCtrl.edit(sel_row);
		if (num <= 0)
		{
			sel_row.recordstatus = recordstatus;
			return;
		}
		properties = sel_row.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			try
			{
				DGVGroupSet.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
			}
			catch (Exception)
			{
			}
		}
	}

	private void tabControl1_Selected(object sender, TabControlEventArgs e)
	{
	}

	private void DGVEmployeeSalary_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployee_col.Name].Value;
		object value2 = DGVEmployeeSalary.CurrentRow.Cells[fullname_col.Name].Value;
		if (columnIndex == DGVEmployeeSalary.Columns["adddetails_col"].Index)
		{
			Payslipdetail payslipdetail = new Payslipdetail();
			payslipdetail.idemployee = value;
			using frmPayslipDetailsInfo frmPayslipDetailsInfo2 = new frmPayslipDetailsInfo(payslipdetail, "ADD");
			frmPayslipDetailsInfo2.Employeename = value2;
			frmPayslipDetailsInfo2.ShowDialog(this);
			if (frmPayslipDetailsInfo2.CommitChanges)
			{
				refresh_DGVPayslipDetails(payslipdetail.idemployee);
			}
			return;
		}
		refresh_DGVPayslipDetails(value);
	}

	private void refresh_DGVPayslipDetails(int idemplyee)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable payslipdetailsTable = payrollCtrl.getPayslipdetailsTable(idemplyee);
		DGVPayslipDetails.DataSource = payslipdetailsTable;
		ApplyColor_DGVPayslipDetails();
	}

	private void ApplyColor_DGVPayslipDetails()
	{
		new Thread((ThreadStart)delegate
		{
			Thread.Sleep(500);
			foreach (DataGridViewRow item in (IEnumerable)DGVPayslipDetails.Rows)
			{
				if (item.Cells[payslipcategory_col.Name].Value.ToString().IndexOf("BENEFIT") >= 0 || item.Cells[payslipcategory_col.Name].Value.ToString().IndexOf("EARNING") >= 0)
				{
					DGVPayslipDetails.Rows[item.Index].DefaultCellStyle.ForeColor = Color.Green;
				}
				else
				{
					DGVPayslipDetails.Rows[item.Index].DefaultCellStyle.ForeColor = Color.Red;
				}
			}
		}).Start();
	}

	private void DGVPayslipDetails_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		int num = Convert.ToInt32(DGVPayslipDetails.CurrentRow.Cells[idpayslipdetails_col.Name].Value);
		object value = DGVEmployeeSalary.CurrentRow.Cells[fullname_col.Name].Value;
		if (columnIndex != DGVPayslipDetails.Columns[editpaylisdetails_col.Name].Index)
		{
			return;
		}
		if (num < 0)
		{
			MessageBox.Show("Cannot Edit this record because its auto generated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		Payslipdetail payslipdetailsObject;
		using (PayrollCtrl payrollCtrl = new PayrollCtrl())
		{
			payslipdetailsObject = payrollCtrl.getPayslipdetailsObject(num);
		}
		using frmPayslipDetailsInfo frmPayslipDetailsInfo2 = new frmPayslipDetailsInfo(payslipdetailsObject);
		frmPayslipDetailsInfo2.Employeename = value;
		frmPayslipDetailsInfo2.ShowDialog(this);
		if (frmPayslipDetailsInfo2.CommitChanges)
		{
			refresh_DGVPayslipDetails(payslipdetailsObject.idemployee);
		}
	}

	private void btnEarnings_Click(object sender, EventArgs e)
	{
		add_payslipDetails("BENEFIT");
	}

	private void add_payslipDetails(string p)
	{
		dynamic value = DGVEmployeeSalary.CurrentRow.Cells[idemployee_col.Name].Value;
		object value2 = DGVEmployeeSalary.CurrentRow.Cells[fullname_col.Name].Value;
		Payslipdetail payslipdetail = new Payslipdetail();
		payslipdetail.idemployee = value;
		payslipdetail.idpayslip = payslip.id;
		using frmPayslipDetailsInfo frmPayslipDetailsInfo2 = new frmPayslipDetailsInfo(payslipdetail, "ADD");
		frmPayslipDetailsInfo2.category_mode = p;
		frmPayslipDetailsInfo2.Employeename = value2;
		frmPayslipDetailsInfo2.ShowDialog(this);
		if (frmPayslipDetailsInfo2.CommitChanges)
		{
			refresh_DGVPayslipDetails(payslipdetail.idemployee);
		}
	}

	private void btnDeductions_Click(object sender, EventArgs e)
	{
		add_payslipDetails("");
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		AReports reports = new rpt_payslip_all(payslip);
		frmReportView frmReportView2 = new frmReportView();
		frmReportView2.reports = reports;
		frmReportView2.Show();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.DGVEmployeeSalary = new System.Windows.Forms.DataGridView();
		this.idemployee_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.salary_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.adddetails_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.DGVPayslipDetails = new System.Windows.Forms.DataGridView();
		this.idpayslipdetails_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.title_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payslipcategory_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountpayslipdetails_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.editpaylisdetails_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.btnDeductions = new System.Windows.Forms.Button();
		this.btnEarnings = new System.Windows.Forms.Button();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.btnNew = new System.Windows.Forms.Button();
		this.DGVGroupSet = new System.Windows.Forms.DataGridView();
		this.id_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.description_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.category_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payroll_edit_settings_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.payroll_remove_settings_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.btnClose = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.btnPreview = new System.Windows.Forms.Button();
		this.tabControl1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGVPayslipDetails).BeginInit();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVGroupSet).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabControl1.Location = new System.Drawing.Point(6, 52);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(1191, 582);
		this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
		this.tabControl1.TabIndex = 0;
		this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(tabControl1_Selected);
		this.tabPage2.Controls.Add(this.splitContainer1);
		this.tabPage2.Location = new System.Drawing.Point(4, 34);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(1183, 544);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Individual Settings";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 3);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.DGVEmployeeSalary);
		this.splitContainer1.Panel2.Controls.Add(this.DGVPayslipDetails);
		this.splitContainer1.Panel2.Controls.Add(this.splitter2);
		this.splitContainer1.Panel2.Controls.Add(this.splitter1);
		this.splitContainer1.Panel2.Controls.Add(this.btnPreview);
		this.splitContainer1.Panel2.Controls.Add(this.btnDeductions);
		this.splitContainer1.Panel2.Controls.Add(this.btnEarnings);
		this.splitContainer1.Size = new System.Drawing.Size(1177, 538);
		this.splitContainer1.SplitterDistance = 444;
		this.splitContainer1.TabIndex = 17;
		this.DGVEmployeeSalary.AllowUserToAddRows = false;
		this.DGVEmployeeSalary.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVEmployeeSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVEmployeeSalary.Columns.AddRange(this.idemployee_col, this.fullname_col, this.salary_col, this.adddetails_col);
		this.DGVEmployeeSalary.Location = new System.Drawing.Point(3, 3);
		this.DGVEmployeeSalary.MultiSelect = false;
		this.DGVEmployeeSalary.Name = "DGVEmployeeSalary";
		this.DGVEmployeeSalary.ReadOnly = true;
		this.DGVEmployeeSalary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVEmployeeSalary.RowHeadersWidth = 20;
		this.DGVEmployeeSalary.RowTemplate.Height = 24;
		this.DGVEmployeeSalary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVEmployeeSalary.Size = new System.Drawing.Size(438, 532);
		this.DGVEmployeeSalary.TabIndex = 16;
		this.DGVEmployeeSalary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVEmployeeSalary_CellClick);
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
		this.adddetails_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.adddetails_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.adddetails_col.HeaderText = "action";
		this.adddetails_col.Name = "adddetails_col";
		this.adddetails_col.ReadOnly = true;
		this.adddetails_col.Text = "add details";
		this.adddetails_col.UseColumnTextForButtonValue = true;
		this.adddetails_col.Visible = false;
		this.DGVPayslipDetails.AllowUserToAddRows = false;
		this.DGVPayslipDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVPayslipDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVPayslipDetails.Columns.AddRange(this.idpayslipdetails_col, this.dataGridViewTextBoxColumn1, this.title_col, this.payslipcategory_col, this.amountpayslipdetails_col, this.editpaylisdetails_col);
		this.DGVPayslipDetails.Location = new System.Drawing.Point(22, 64);
		this.DGVPayslipDetails.MultiSelect = false;
		this.DGVPayslipDetails.Name = "DGVPayslipDetails";
		this.DGVPayslipDetails.ReadOnly = true;
		this.DGVPayslipDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVPayslipDetails.RowHeadersWidth = 20;
		this.DGVPayslipDetails.RowTemplate.Height = 24;
		this.DGVPayslipDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGVPayslipDetails.Size = new System.Drawing.Size(692, 471);
		this.DGVPayslipDetails.TabIndex = 16;
		this.DGVPayslipDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVPayslipDetails_CellClick);
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
		this.title_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.title_col.DataPropertyName = "title";
		this.title_col.HeaderText = "Title";
		this.title_col.Name = "title_col";
		this.title_col.ReadOnly = true;
		this.payslipcategory_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.payslipcategory_col.DataPropertyName = "category";
		this.payslipcategory_col.HeaderText = "category";
		this.payslipcategory_col.Name = "payslipcategory_col";
		this.payslipcategory_col.ReadOnly = true;
		this.payslipcategory_col.Width = 116;
		this.amountpayslipdetails_col.DataPropertyName = "amount";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.amountpayslipdetails_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.amountpayslipdetails_col.HeaderText = "amount";
		this.amountpayslipdetails_col.Name = "amountpayslipdetails_col";
		this.amountpayslipdetails_col.ReadOnly = true;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Lime;
		this.editpaylisdetails_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.editpaylisdetails_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.editpaylisdetails_col.HeaderText = "action";
		this.editpaylisdetails_col.Name = "editpaylisdetails_col";
		this.editpaylisdetails_col.ReadOnly = true;
		this.editpaylisdetails_col.Text = "edit";
		this.editpaylisdetails_col.UseColumnTextForButtonValue = true;
		this.splitter2.Location = new System.Drawing.Point(3, 0);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(8, 538);
		this.splitter2.TabIndex = 19;
		this.splitter2.TabStop = false;
		this.splitter1.Location = new System.Drawing.Point(0, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(3, 538);
		this.splitter1.TabIndex = 18;
		this.splitter1.TabStop = false;
		this.btnDeductions.BackColor = System.Drawing.Color.Maroon;
		this.btnDeductions.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDeductions.FlatAppearance.BorderSize = 0;
		this.btnDeductions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDeductions.ForeColor = System.Drawing.Color.White;
		this.btnDeductions.Location = new System.Drawing.Point(180, 23);
		this.btnDeductions.Name = "btnDeductions";
		this.btnDeductions.Size = new System.Drawing.Size(160, 35);
		this.btnDeductions.TabIndex = 17;
		this.btnDeductions.Text = "Add Deductions";
		this.btnDeductions.UseVisualStyleBackColor = false;
		this.btnDeductions.Click += new System.EventHandler(btnDeductions_Click);
		this.btnEarnings.BackColor = System.Drawing.Color.Green;
		this.btnEarnings.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnEarnings.FlatAppearance.BorderSize = 0;
		this.btnEarnings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnEarnings.ForeColor = System.Drawing.Color.White;
		this.btnEarnings.Location = new System.Drawing.Point(22, 23);
		this.btnEarnings.Name = "btnEarnings";
		this.btnEarnings.Size = new System.Drawing.Size(143, 35);
		this.btnEarnings.TabIndex = 17;
		this.btnEarnings.Text = "Add Earnings";
		this.btnEarnings.UseVisualStyleBackColor = false;
		this.btnEarnings.Click += new System.EventHandler(btnEarnings_Click);
		this.tabPage1.Controls.Add(this.btnNew);
		this.tabPage1.Controls.Add(this.DGVGroupSet);
		this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabPage1.Location = new System.Drawing.Point(4, 34);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(1183, 544);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Master Settings";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnNew.Location = new System.Drawing.Point(688, 9);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(160, 38);
		this.btnNew.TabIndex = 18;
		this.btnNew.Text = "New Record";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.DGVGroupSet.AllowUserToAddRows = false;
		this.DGVGroupSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVGroupSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVGroupSet.Columns.AddRange(this.id_col, this.description_col, this.category_col, this.recordstatus_col, this.amount_col, this.payroll_edit_settings_col, this.payroll_remove_settings_col);
		this.DGVGroupSet.Location = new System.Drawing.Point(7, 53);
		this.DGVGroupSet.MultiSelect = false;
		this.DGVGroupSet.Name = "DGVGroupSet";
		this.DGVGroupSet.ReadOnly = true;
		this.DGVGroupSet.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVGroupSet.RowHeadersWidth = 20;
		this.DGVGroupSet.RowTemplate.Height = 24;
		this.DGVGroupSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVGroupSet.Size = new System.Drawing.Size(841, 475);
		this.DGVGroupSet.TabIndex = 17;
		this.DGVGroupSet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVGroupSet_CellClick);
		this.id_col.DataPropertyName = "id";
		this.id_col.HeaderText = "id";
		this.id_col.Name = "id_col";
		this.id_col.ReadOnly = true;
		this.id_col.Visible = false;
		this.description_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.description_col.DataPropertyName = "description";
		this.description_col.HeaderText = "description";
		this.description_col.Name = "description_col";
		this.description_col.ReadOnly = true;
		this.category_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.category_col.DataPropertyName = "category";
		this.category_col.HeaderText = "category";
		this.category_col.Name = "category_col";
		this.category_col.ReadOnly = true;
		this.category_col.Width = 102;
		this.recordstatus_col.DataPropertyName = "recordstatus";
		this.recordstatus_col.HeaderText = "recordstatus";
		this.recordstatus_col.Name = "recordstatus_col";
		this.recordstatus_col.ReadOnly = true;
		this.amount_col.DataPropertyName = "amount";
		this.amount_col.HeaderText = "amount";
		this.amount_col.Name = "amount_col";
		this.amount_col.ReadOnly = true;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Green;
		dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
		this.payroll_edit_settings_col.DefaultCellStyle = dataGridViewCellStyle5;
		this.payroll_edit_settings_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.payroll_edit_settings_col.HeaderText = "edit";
		this.payroll_edit_settings_col.Name = "payroll_edit_settings_col";
		this.payroll_edit_settings_col.ReadOnly = true;
		this.payroll_edit_settings_col.Text = "edit";
		this.payroll_edit_settings_col.UseColumnTextForButtonValue = true;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(3);
		this.payroll_remove_settings_col.DefaultCellStyle = dataGridViewCellStyle6;
		this.payroll_remove_settings_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.payroll_remove_settings_col.HeaderText = "remove";
		this.payroll_remove_settings_col.Name = "payroll_remove_settings_col";
		this.payroll_remove_settings_col.ReadOnly = true;
		this.payroll_remove_settings_col.Text = "remove";
		this.payroll_remove_settings_col.UseColumnTextForButtonValue = true;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(1115, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(80, 38);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Controls.Add(this.button1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1209, 46);
		this.panel1.TabIndex = 15;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(14, 10);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(150, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "Payroll information";
		this.button1.BackColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button1.FlatAppearance.BorderSize = 0;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.ForeColor = System.Drawing.Color.White;
		this.button1.Location = new System.Drawing.Point(959, 6);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(150, 35);
		this.button1.TabIndex = 17;
		this.button1.Text = "Finalized";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(btnEarnings_Click);
		this.btnPreview.BackColor = System.Drawing.Color.Teal;
		this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnPreview.FlatAppearance.BorderSize = 0;
		this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPreview.ForeColor = System.Drawing.Color.White;
		this.btnPreview.Location = new System.Drawing.Point(554, 23);
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(160, 35);
		this.btnPreview.TabIndex = 17;
		this.btnPreview.Text = "Preview/Print";
		this.btnPreview.UseVisualStyleBackColor = false;
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1209, 646);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.tabControl1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPayslipDetails";
		this.Text = "Payroll Information";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPayroll_Load);
		this.tabControl1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVEmployeeSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGVPayslipDetails).EndInit();
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVGroupSet).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
