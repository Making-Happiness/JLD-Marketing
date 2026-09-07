using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPayroll : Form
{
	private DataTable dt_sourse;

	private IContainer components = null;

	private Panel panel1;

	private Label label1;

	private TextBox txtSearch;

	private DataGridView DGV;

	private BindingSource payrollBindingSource;

	private Button btnAdd;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn datefromDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn datetoDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn recordstatusDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn edit_col;

	private DataGridViewButtonColumn open_col;

	public frmPayroll()
	{
		InitializeComponent();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		using frmPayrollInfo frmPayrollInfo2 = new frmPayrollInfo();
		Payroll payroll = new Payroll();
		frmPayrollInfo2.payroll = payroll;
		frmPayrollInfo2.Transaction = "ADD";
		frmPayrollInfo2.ShowDialog(this);
		if (frmPayrollInfo2.CommitChanges)
		{
			Refresh_DGV();
		}
	}

	private void Refresh_DGV()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		dt_sourse = payrollCtrl.getPayrollTable();
		DGV.DataSource = dt_sourse;
	}

	private void frmPayroll_Load(object sender, EventArgs e)
	{
		Refresh_DGV();
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		int id = Convert.ToInt32(DGV.CurrentRow.Cells[idDataGridViewTextBoxColumn.Name].Value);
		Payroll payrollTableByID;
		if (columnIndex == DGV.Columns[edit_col.Name].Index)
		{
			using (PayrollCtrl payrollCtrl = new PayrollCtrl())
			{
				payrollTableByID = payrollCtrl.getPayrollTableByID(id);
			}
			if (payrollTableByID == null)
			{
				return;
			}
			using frmPayrollInfo frmPayrollInfo2 = new frmPayrollInfo();
			frmPayrollInfo2.payroll = payrollTableByID;
			frmPayrollInfo2.ShowDialog(this);
			if (frmPayrollInfo2.CommitChanges)
			{
				Refresh_DGV();
			}
			return;
		}
		if (columnIndex != DGV.Columns[open_col.Name].Index)
		{
			return;
		}
		using (PayrollCtrl payrollCtrl = new PayrollCtrl())
		{
			payrollTableByID = payrollCtrl.getPayrollTableByID(id);
			if (payrollTableByID.recordstatus == "new")
			{
				DataTable dataTable = payrollCtrl.generate_payrolldetails(payrollTableByID.id);
			}
		}
		using frmPayrollDetails frmPayrollDetails2 = new frmPayrollDetails();
		frmPayrollDetails2.payroll = payrollTableByID;
		frmPayrollDetails2.ShowDialog(this);
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnAdd = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.open_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.datefromDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.datetoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payrollBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.payrollBindingSource).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.btnAdd);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.txtSearch);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(773, 67);
		this.panel1.TabIndex = 20;
		this.btnAdd.Location = new System.Drawing.Point(632, 27);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(129, 34);
		this.btnAdd.TabIndex = 22;
		this.btnAdd.Text = "New Record";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(55, 18);
		this.label1.TabIndex = 14;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(3, 33);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(409, 28);
		this.txtSearch.TabIndex = 13;
		this.DGV.AllowUserToAddRows = false;
		this.DGV.AutoGenerateColumns = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.titleDataGridViewTextBoxColumn, this.datefromDataGridViewTextBoxColumn, this.datetoDataGridViewTextBoxColumn, this.recordstatusDataGridViewTextBoxColumn, this.edit_col, this.open_col);
		this.DGV.DataSource = this.payrollBindingSource;
		this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV.Location = new System.Drawing.Point(0, 67);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGV.Size = new System.Drawing.Size(773, 486);
		this.DGV.TabIndex = 21;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.edit_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.Green;
		dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(3);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "edit";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Text = "Edit";
		this.edit_col.ToolTipText = "edit payroll info";
		this.edit_col.UseColumnTextForButtonValue = true;
		this.edit_col.Width = 37;
		this.open_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.open_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.open_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.open_col.HeaderText = "open";
		this.open_col.Name = "open_col";
		this.open_col.ReadOnly = true;
		this.open_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.open_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.open_col.Text = "open";
		this.open_col.ToolTipText = "open payroll";
		this.open_col.UseColumnTextForButtonValue = true;
		this.open_col.Width = 70;
		this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
		this.idDataGridViewTextBoxColumn.HeaderText = "id";
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.titleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.titleDataGridViewTextBoxColumn.DataPropertyName = "title";
		this.titleDataGridViewTextBoxColumn.HeaderText = "title";
		this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
		this.titleDataGridViewTextBoxColumn.ReadOnly = true;
		this.datefromDataGridViewTextBoxColumn.DataPropertyName = "datefrom";
		this.datefromDataGridViewTextBoxColumn.HeaderText = "datefrom";
		this.datefromDataGridViewTextBoxColumn.Name = "datefromDataGridViewTextBoxColumn";
		this.datefromDataGridViewTextBoxColumn.ReadOnly = true;
		this.datetoDataGridViewTextBoxColumn.DataPropertyName = "dateto";
		this.datetoDataGridViewTextBoxColumn.HeaderText = "dateto";
		this.datetoDataGridViewTextBoxColumn.Name = "datetoDataGridViewTextBoxColumn";
		this.datetoDataGridViewTextBoxColumn.ReadOnly = true;
		this.recordstatusDataGridViewTextBoxColumn.DataPropertyName = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.HeaderText = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.Name = "recordstatusDataGridViewTextBoxColumn";
		this.recordstatusDataGridViewTextBoxColumn.ReadOnly = true;
		this.payrollBindingSource.DataSource = typeof(RealProperty.BEL.Payroll);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(773, 553);
		base.Controls.Add(this.DGV);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPayroll";
		this.Text = "Payroll list";
		base.Load += new System.EventHandler(frmPayroll_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.payrollBindingSource).EndInit();
		base.ResumeLayout(false);
	}
}
