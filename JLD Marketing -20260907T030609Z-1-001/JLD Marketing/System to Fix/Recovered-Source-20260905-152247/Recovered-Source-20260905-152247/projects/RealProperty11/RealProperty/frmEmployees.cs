using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmEmployees : Form
{
	private EmployeeCtrl controller;

	private Employee sel_row;

	private DataTable dt_source;

	private IContainer components = null;

	private Panel panel1;

	private Label label2;

	private Button btnClose;

	private DataGridView DGV;

	private Button btnAdd;

	private Label label1;

	private TextBox txtSearch;

	private BindingSource employeeBindingSource;

	private DataGridViewTextBoxColumn idemployeeDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn middlenameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn genderDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dateofbirthDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn salaryDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn designationDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn civilstatusDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn contactnoDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn recordstatusDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn edit_col;

	public frmEmployees()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
		controller = new EmployeeCtrl();
		DGV_refresh();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		using frmEmployeesInfo frmEmployeesInfo2 = new frmEmployeesInfo();
		frmEmployeesInfo2.ShowDialog();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void DGV_refresh(string filter = null)
	{
		if (filter == null)
		{
			dt_source = controller.getTable();
			DGV.DataSource = dt_source;
			return;
		}
		if (dt_source == null)
		{
			dt_source = controller.getTable();
		}
		if (filter == "")
		{
			DGV.DataSource = dt_source;
			return;
		}
		string filterExpression = string.Format("firstname like '%{0}%' OR lastname like '%{0}%'", filter, filter);
		DataRow[] source = dt_source.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataSource = dt_source.Clone();
			DGV.DataSource = dataSource;
		}
		else
		{
			DGV.DataSource = source.CopyToDataTable();
		}
	}

	private void txtSearch_TextChanged(object sender, EventArgs e)
	{
		DGV_refresh(txtSearch.Text);
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0)
		{
			return;
		}
		sel_row = AController.DataRowToClass<Employee>(((DataRowView)DGV.CurrentRow.DataBoundItem).Row);
		if (columnIndex != DGV.Columns["edit_col"].Index)
		{
			return;
		}
		using frmEmployeesInfo frmEmployeesInfo2 = new frmEmployeesInfo();
		frmEmployeesInfo2.employee = sel_row;
		frmEmployeesInfo2.BackColor = BackColor;
		frmEmployeesInfo2.StartPosition = FormStartPosition.CenterParent;
		frmEmployeesInfo2.ShowDialog(this);
		if (!frmEmployeesInfo2.commitcahanges)
		{
			return;
		}
		sel_row = null;
		sel_row = frmEmployeesInfo2.employee;
		PropertyInfo[] properties = sel_row.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			try
			{
				DGV.CurrentRow.Cells[propertyInfo.Name + "DataGridViewTextBoxColumn"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
			}
			catch (Exception)
			{
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.btnAdd = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.idemployeeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.middlenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.genderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateofbirthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.salaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.designationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.civilstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contactnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.employeeBindingSource).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(933, 41);
		this.panel1.TabIndex = 14;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(12, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(172, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "Employee information";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(850, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.DGV.AllowUserToAddRows = false;
		this.DGV.AutoGenerateColumns = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idemployeeDataGridViewTextBoxColumn, this.firstnameDataGridViewTextBoxColumn, this.lastnameDataGridViewTextBoxColumn, this.middlenameDataGridViewTextBoxColumn, this.genderDataGridViewTextBoxColumn, this.dateofbirthDataGridViewTextBoxColumn, this.salaryDataGridViewTextBoxColumn, this.designationDataGridViewTextBoxColumn, this.civilstatusDataGridViewTextBoxColumn, this.contactnoDataGridViewTextBoxColumn, this.recordstatusDataGridViewTextBoxColumn, this.remarksDataGridViewTextBoxColumn, this.edit_col);
		this.DGV.DataSource = this.employeeBindingSource;
		this.DGV.Location = new System.Drawing.Point(14, 111);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGV.Size = new System.Drawing.Size(907, 513);
		this.DGV.TabIndex = 13;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.Lime;
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "EDIT";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Text = "EDIT";
		this.edit_col.UseColumnTextForButtonValue = true;
		this.btnAdd.Location = new System.Drawing.Point(773, 73);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(148, 32);
		this.btnAdd.TabIndex = 12;
		this.btnAdd.Text = "New Record";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(18, 53);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 11;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(14, 77);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(753, 28);
		this.txtSearch.TabIndex = 10;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.idemployeeDataGridViewTextBoxColumn.DataPropertyName = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.HeaderText = "idemployee";
		this.idemployeeDataGridViewTextBoxColumn.Name = "idemployeeDataGridViewTextBoxColumn";
		this.idemployeeDataGridViewTextBoxColumn.ReadOnly = true;
		this.idemployeeDataGridViewTextBoxColumn.Visible = false;
		this.firstnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "firstname";
		this.firstnameDataGridViewTextBoxColumn.HeaderText = "firstname";
		this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
		this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
		this.lastnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "lastname";
		this.lastnameDataGridViewTextBoxColumn.HeaderText = "lastname";
		this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
		this.lastnameDataGridViewTextBoxColumn.ReadOnly = true;
		this.middlenameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.middlenameDataGridViewTextBoxColumn.DataPropertyName = "middlename";
		this.middlenameDataGridViewTextBoxColumn.HeaderText = "middlename";
		this.middlenameDataGridViewTextBoxColumn.Name = "middlenameDataGridViewTextBoxColumn";
		this.middlenameDataGridViewTextBoxColumn.ReadOnly = true;
		this.genderDataGridViewTextBoxColumn.DataPropertyName = "gender";
		this.genderDataGridViewTextBoxColumn.HeaderText = "gender";
		this.genderDataGridViewTextBoxColumn.Name = "genderDataGridViewTextBoxColumn";
		this.genderDataGridViewTextBoxColumn.ReadOnly = true;
		this.genderDataGridViewTextBoxColumn.Visible = false;
		this.dateofbirthDataGridViewTextBoxColumn.DataPropertyName = "dateofbirth";
		this.dateofbirthDataGridViewTextBoxColumn.HeaderText = "dateofbirth";
		this.dateofbirthDataGridViewTextBoxColumn.Name = "dateofbirthDataGridViewTextBoxColumn";
		this.dateofbirthDataGridViewTextBoxColumn.ReadOnly = true;
		this.dateofbirthDataGridViewTextBoxColumn.Visible = false;
		this.salaryDataGridViewTextBoxColumn.DataPropertyName = "salary";
		this.salaryDataGridViewTextBoxColumn.HeaderText = "salary";
		this.salaryDataGridViewTextBoxColumn.Name = "salaryDataGridViewTextBoxColumn";
		this.salaryDataGridViewTextBoxColumn.ReadOnly = true;
		this.salaryDataGridViewTextBoxColumn.Visible = false;
		this.designationDataGridViewTextBoxColumn.DataPropertyName = "designation";
		this.designationDataGridViewTextBoxColumn.HeaderText = "designation";
		this.designationDataGridViewTextBoxColumn.Name = "designationDataGridViewTextBoxColumn";
		this.designationDataGridViewTextBoxColumn.ReadOnly = true;
		this.civilstatusDataGridViewTextBoxColumn.DataPropertyName = "civilstatus";
		this.civilstatusDataGridViewTextBoxColumn.HeaderText = "civilstatus";
		this.civilstatusDataGridViewTextBoxColumn.Name = "civilstatusDataGridViewTextBoxColumn";
		this.civilstatusDataGridViewTextBoxColumn.ReadOnly = true;
		this.civilstatusDataGridViewTextBoxColumn.Visible = false;
		this.contactnoDataGridViewTextBoxColumn.DataPropertyName = "contactno";
		this.contactnoDataGridViewTextBoxColumn.HeaderText = "contactno";
		this.contactnoDataGridViewTextBoxColumn.Name = "contactnoDataGridViewTextBoxColumn";
		this.contactnoDataGridViewTextBoxColumn.ReadOnly = true;
		this.recordstatusDataGridViewTextBoxColumn.DataPropertyName = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.HeaderText = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.Name = "recordstatusDataGridViewTextBoxColumn";
		this.recordstatusDataGridViewTextBoxColumn.ReadOnly = true;
		this.remarksDataGridViewTextBoxColumn.DataPropertyName = "remarks";
		this.remarksDataGridViewTextBoxColumn.HeaderText = "remarks";
		this.remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
		this.remarksDataGridViewTextBoxColumn.ReadOnly = true;
		this.remarksDataGridViewTextBoxColumn.Visible = false;
		this.employeeBindingSource.DataSource = typeof(RealProperty.BEL.Employee);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(933, 636);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.DGV);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Name = "frmEmployees";
		this.Text = "Employees Information";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.employeeBindingSource).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
