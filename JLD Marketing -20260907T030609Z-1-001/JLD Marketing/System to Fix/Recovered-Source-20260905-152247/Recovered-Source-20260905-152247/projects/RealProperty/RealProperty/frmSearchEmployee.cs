using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmSearchEmployee : Form
{
	private EmployeeCtrl controller;

	private IContainer components = null;

	private Label label1;

	private TextBox txtSearch;

	private DataGridView DGV;

	private DataGridViewTextBoxColumn lastname_col;

	private DataGridViewTextBoxColumn firstname_col;

	private DataGridViewTextBoxColumn middlename_col;

	private DataGridViewButtonColumn select_col;

	private DataTable employeestable { get; set; }

	private bool employeeLoadAttempted;

	private bool employeeLoadErrorShown;

	public Employee sel_row { get; set; }

	public frmSearchEmployee()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
	}

	private void frmSearchEmployee_Load(object sender, EventArgs e)
	{
		controller = new EmployeeCtrl();
		DGV_refresh();
	}

	private void DGV_refresh(string filter = null)
	{
		if (controller == null)
		{
			DGV.DataSource = CreateEmptyEmployeeTable();
			return;
		}

		if (!employeeLoadAttempted)
		{
			employeeLoadAttempted = true;
			employeestable = controller.getcustomTable("view_employeeswithfullname");
		}

		if (employeestable == null)
		{
			DGV.DataSource = CreateEmptyEmployeeTable();
			if (!employeeLoadErrorShown)
			{
				employeeLoadErrorShown = true;
				MessageBox.Show("Employee data could not be loaded. Check the MySQL connection and the view_employeeswithfullname view, then reopen this window.", "Database unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return;
		}

		string text = (filter ?? string.Empty).Trim();
		if (text.Length == 0)
		{
			DGV.DataSource = employeestable;
			return;
		}

		DataRow[] array = employeestable.AsEnumerable().Where(delegate(DataRow row)
		{
			string text2 = row.Field<string>("firstname") ?? string.Empty;
			string text3 = row.Field<string>("lastname") ?? string.Empty;
			return text2.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0 || text3.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
		}).ToArray();
		DGV.DataSource = ((array.Length == 0) ? employeestable.Clone() : array.CopyToDataTable());
	}

	private static DataTable CreateEmptyEmployeeTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("lastname", typeof(string));
		dataTable.Columns.Add("firstname", typeof(string));
		dataTable.Columns.Add("middlename", typeof(string));
		return dataTable;
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0 || e.ColumnIndex != DGV.Columns["select_col"].Index)
		{
			return;
		}
		DataRowView dataRowView = DGV.CurrentRow?.DataBoundItem as DataRowView;
		if (dataRowView != null)
		{
			sel_row = AController.DataRowToClass<Employee>(dataRowView.Row);
			Close();
		}
	}

	private void txtSearch_TextChanged(object sender, EventArgs e)
	{
		DGV_refresh(txtSearch.Text);
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
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.lastname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.firstname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.middlename_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.select_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 11);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(218, 18);
		this.label1.TabIndex = 5;
		this.label1.Text = "Search (Firsname or Lastname)";
		this.txtSearch.Location = new System.Drawing.Point(14, 36);
		this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(689, 24);
		this.txtSearch.TabIndex = 4;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.lastname_col, this.firstname_col, this.middlename_col, this.select_col);
		this.DGV.Location = new System.Drawing.Point(13, 71);
		this.DGV.Margin = new System.Windows.Forms.Padding(4);
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.Size = new System.Drawing.Size(691, 494);
		this.DGV.TabIndex = 3;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.lastname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.lastname_col.DataPropertyName = "lastname";
		this.lastname_col.HeaderText = "lastname";
		this.lastname_col.Name = "lastname_col";
		this.lastname_col.ReadOnly = true;
		this.firstname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.firstname_col.DataPropertyName = "firstname";
		this.firstname_col.HeaderText = "firstname";
		this.firstname_col.Name = "firstname_col";
		this.firstname_col.ReadOnly = true;
		this.middlename_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.middlename_col.DataPropertyName = "middlename";
		this.middlename_col.HeaderText = "middlename";
		this.middlename_col.Name = "middlename_col";
		this.middlename_col.ReadOnly = true;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(1);
		this.select_col.DefaultCellStyle = dataGridViewCellStyle;
		this.select_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.select_col.HeaderText = "Action";
		this.select_col.Name = "select_col";
		this.select_col.ReadOnly = true;
		this.select_col.Text = "select";
		this.select_col.ToolTipText = "click select to choose client";
		this.select_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(714, 583);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmSearchEmployee";
		this.Text = "frmSearchEmployee";
		base.Load += new System.EventHandler(frmSearchEmployee_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
