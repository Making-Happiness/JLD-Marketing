using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmSearchClient : Form
{
	private ClientsCtrl controller;

	private IContainer components = null;

	private DataGridView DGV;

	private TextBox txtSearch;

	private Label label1;

	private DataGridViewTextBoxColumn lastname_col;

	private DataGridViewTextBoxColumn firstname_col;

	private DataGridViewTextBoxColumn middlename_col;

	private DataGridViewButtonColumn select_col;

	private DataTable clientstable { get; set; }

	public Client sel_row { get; set; }

	public frmSearchClient()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
	}

	private void frmSearchClient_Load(object sender, EventArgs e)
	{
		controller = new ClientsCtrl();
		DGV_refresh();
	}

	private void DGV_refresh(string filter = null)
	{
		if (filter == null)
		{
			clientstable = controller.getcustomTable("clientswithfullname");
			DGV.DataSource = clientstable;
			return;
		}
		if (clientstable == null)
		{
			clientstable = controller.getcustomTable("clientswithfullname");
		}
		if (filter == "")
		{
			DGV.DataSource = clientstable;
			return;
		}
		string filterExpression = string.Format("firstname like '%{0}%' OR lastname like '%{0}%'", filter, filter);
		DataRow[] source = clientstable.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataTable = clientstable.Clone();
			dataTable.Clear();
			DGV.DataSource = dataTable;
		}
		else
		{
			DGV.DataSource = source.CopyToDataTable();
		}
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (columnIndex == DGV.Columns["select_col"].Index)
		{
			sel_row = AController.DataRowToClass<Client>(((DataRowView)DGV.CurrentRow.DataBoundItem).Row);
			Close();
		}
	}

	private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		this.DGV = new System.Windows.Forms.DataGridView();
		this.lastname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.firstname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.middlename_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.select_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		base.SuspendLayout();
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.lastname_col, this.firstname_col, this.middlename_col, this.select_col);
		this.DGV.Location = new System.Drawing.Point(13, 71);
		this.DGV.Margin = new System.Windows.Forms.Padding(4);
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.Size = new System.Drawing.Size(691, 494);
		this.DGV.TabIndex = 0;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellContentClick);
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
		this.txtSearch.Location = new System.Drawing.Point(14, 36);
		this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(689, 26);
		this.txtSearch.TabIndex = 1;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 11);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(248, 20);
		this.label1.TabIndex = 2;
		this.label1.Text = "Search (Firsname or Lastname)";
		base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(721, 598);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmSearchClient";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Search Client";
		base.Load += new System.EventHandler(frmSearchClient_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
