using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmExpenses : Form
{
	private ExpensesCtrl controller;

	private Expense_view sel_row;

	private IContainer components = null;

	private DataGridView DGVExpenses;

	private Button btnAdd;

	private Panel panel1;

	private Label lblTitle;

	private BindingSource expense_viewBindingSource;

	private DataGridViewTextBoxColumn daterelease_col;

	private DataGridViewTextBoxColumn description_col;

	private DataGridViewTextBoxColumn purpose_col;

	private DataGridViewTextBoxColumn amount_col;

	private DataGridViewTextBoxColumn id_col;

	private DataGridViewTextBoxColumn receiveby_str_col;

	private DataGridViewTextBoxColumn remarks_col;

	private DataGridViewTextBoxColumn receiveby_col;

	private DataGridViewTextBoxColumn releaseby_col;

	private DataGridViewTextBoxColumn releaseby_str_col;

	private DataGridViewButtonColumn edit_col;

	public List<Expense_view> view_expensestable { get; set; }

	public frmExpenses()
	{
		InitializeComponent();
		DGVExpenses.AutoGenerateColumns = false;
		controller = new ExpensesCtrl();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		Expense_view expense_view = new Expense_view();
		using frmExpensesInfo frmExpensesInfo2 = new frmExpensesInfo();
		frmExpensesInfo2.transaction = "ADD";
		frmExpensesInfo2.employeeTable = Program.fmain.view_EmployeesTable;
		frmExpensesInfo2.expense_view = expense_view;
		frmExpensesInfo2.ShowDialog(this);
		if (frmExpensesInfo2.commitchanges)
		{
			DGV_refresh();
			if (DGVExpenses.Rows.Count > 0)
			{
				DGVExpenses.Rows[DGVExpenses.Rows.Count - 1].Cells[0].Selected = true;
			}
		}
	}

	private void DGV_refresh()
	{
		view_expensestable = controller.getexpenses_view();
		Program.fmain.view_expensesTable = view_expensestable;
		expense_viewBindingSource.DataSource = view_expensestable;
	}

	private void frmExpenses_Load(object sender, EventArgs e)
	{
		lblTitle.Text = $"List of Expenses as of {DateTime.Today.ToShortDateString()}";
		DGV_refresh();
	}

	private void DGVExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		sel_row = (Expense_view)DGVExpenses.Rows[DGVExpenses.CurrentRow.Index].DataBoundItem;
		if (columnIndex != DGVExpenses.Columns["edit_col"].Index)
		{
			return;
		}
		using frmExpensesInfo frmExpensesInfo2 = new frmExpensesInfo();
		sel_row = controller.getRecordbyID(sel_row.id);
		frmExpensesInfo2.transaction = "EDIT";
		frmExpensesInfo2.employeeTable = Program.fmain.view_EmployeesTable;
		frmExpensesInfo2.expense_view = sel_row;
		frmExpensesInfo2.ShowDialog(this);
		if (frmExpensesInfo2.commitchanges)
		{
			sel_row = frmExpensesInfo2.expense_view;
			PropertyInfo[] properties = sel_row.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				DGVExpenses.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVExpenses = new System.Windows.Forms.DataGridView();
		this.expense_viewBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.btnAdd = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblTitle = new System.Windows.Forms.Label();
		this.daterelease_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.description_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.purpose_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.id_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.receiveby_str_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.remarks_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.receiveby_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.releaseby_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.releaseby_str_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGVExpenses).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.expense_viewBindingSource).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.DGVExpenses.AllowUserToAddRows = false;
		this.DGVExpenses.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVExpenses.AutoGenerateColumns = false;
		this.DGVExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVExpenses.Columns.AddRange(this.daterelease_col, this.description_col, this.purpose_col, this.amount_col, this.id_col, this.receiveby_str_col, this.remarks_col, this.receiveby_col, this.releaseby_col, this.releaseby_str_col, this.edit_col);
		this.DGVExpenses.DataSource = this.expense_viewBindingSource;
		this.DGVExpenses.Location = new System.Drawing.Point(0, 44);
		this.DGVExpenses.MultiSelect = false;
		this.DGVExpenses.Name = "DGVExpenses";
		this.DGVExpenses.ReadOnly = true;
		this.DGVExpenses.RowHeadersWidth = 21;
		this.DGVExpenses.RowTemplate.Height = 24;
		this.DGVExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVExpenses.Size = new System.Drawing.Size(1042, 428);
		this.DGVExpenses.TabIndex = 12;
		this.DGVExpenses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVExpenses_CellClick);
		this.expense_viewBindingSource.DataSource = typeof(RealProperty.BEL.Expense_view);
		this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAdd.Location = new System.Drawing.Point(968, 3);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(74, 35);
		this.btnAdd.TabIndex = 13;
		this.btnAdd.Text = "+ADD";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.lblTitle);
		this.panel1.Controls.Add(this.btnAdd);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1045, 41);
		this.panel1.TabIndex = 14;
		this.lblTitle.AutoSize = true;
		this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblTitle.Location = new System.Drawing.Point(12, 9);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(133, 18);
		this.lblTitle.TabIndex = 2;
		this.lblTitle.Text = "List of Expenses";
		this.daterelease_col.DataPropertyName = "daterelease";
		dataGridViewCellStyle.Format = "d";
		dataGridViewCellStyle.NullValue = null;
		this.daterelease_col.DefaultCellStyle = dataGridViewCellStyle;
		this.daterelease_col.HeaderText = "Daterelease";
		this.daterelease_col.Name = "daterelease_col";
		this.daterelease_col.ReadOnly = true;
		this.description_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.description_col.DataPropertyName = "description";
		this.description_col.HeaderText = "Description";
		this.description_col.Name = "description_col";
		this.description_col.ReadOnly = true;
		this.purpose_col.DataPropertyName = "purpose";
		this.purpose_col.HeaderText = "Purpose";
		this.purpose_col.Name = "purpose_col";
		this.purpose_col.ReadOnly = true;
		this.amount_col.DataPropertyName = "amount";
		this.amount_col.HeaderText = "Amount";
		this.amount_col.Name = "amount_col";
		this.amount_col.ReadOnly = true;
		this.id_col.DataPropertyName = "id";
		this.id_col.HeaderText = "id";
		this.id_col.Name = "id_col";
		this.id_col.ReadOnly = true;
		this.id_col.Visible = false;
		this.receiveby_str_col.DataPropertyName = "receiveby_str";
		this.receiveby_str_col.HeaderText = "Receiveby";
		this.receiveby_str_col.Name = "receiveby_str_col";
		this.receiveby_str_col.ReadOnly = true;
		this.remarks_col.DataPropertyName = "remarks";
		this.remarks_col.HeaderText = "Remarks";
		this.remarks_col.Name = "remarks_col";
		this.remarks_col.ReadOnly = true;
		this.receiveby_col.DataPropertyName = "receiveby";
		this.receiveby_col.HeaderText = "Receiveby";
		this.receiveby_col.Name = "receiveby_col";
		this.receiveby_col.ReadOnly = true;
		this.receiveby_col.Visible = false;
		this.releaseby_col.DataPropertyName = "releaseby";
		this.releaseby_col.HeaderText = "Releaseby";
		this.releaseby_col.Name = "releaseby_col";
		this.releaseby_col.ReadOnly = true;
		this.releaseby_col.Visible = false;
		this.releaseby_str_col.DataPropertyName = "releaseby_str";
		this.releaseby_str_col.HeaderText = "Releaseby_str";
		this.releaseby_str_col.Name = "releaseby_str_col";
		this.releaseby_str_col.ReadOnly = true;
		this.releaseby_str_col.Visible = false;
		this.edit_col.DataPropertyName = "releaseby_str";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.edit_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.edit_col.Text = "Edit";
		this.edit_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1045, 484);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.DGVExpenses);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmExpenses";
		this.Text = "Expenses";
		base.Load += new System.EventHandler(frmExpenses_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVExpenses).EndInit();
		((System.ComponentModel.ISupportInitialize)this.expense_viewBindingSource).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
