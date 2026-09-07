using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmProducts : Form
{
	private ProductsCtrl controller;

	private Product sel_row;

	private IContainer components = null;

	private DataGridView DGV;

	private TextBox txtSearch;

	private Label label1;

	private Button btnAdd;

	private Button btnClose;

	private DataGridViewTextBoxColumn idproduct_col;

	private DataGridViewTextBoxColumn code_col;

	private DataGridViewTextBoxColumn location_col;

	private DataGridViewTextBoxColumn totalblockno_col;

	private DataGridViewTextBoxColumn totallotno_col;

	private DataGridViewTextBoxColumn totalarea_col;

	private DataGridViewTextBoxColumn cashprice_col;

	private DataGridViewButtonColumn edit_col;

	private DataGridViewButtonColumn delete_col;

	private DataGridViewButtonColumn details_col;

	private Label label2;

	private Panel panel1;

	public frmProducts()
	{
		InitializeComponent();
		controller = new ProductsCtrl();
		DGV_refresh();
	}

	private void DGV_refresh()
	{
		DataTable table = controller.getTable();
		DGV.DataSource = table;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		using frmproductinfo frmproductinfo2 = new frmproductinfo();
		frmproductinfo2.BackColor = BackColor;
		frmproductinfo2.StartPosition = FormStartPosition.CenterParent;
		frmproductinfo2.ShowDialog(this);
		DGV_refresh();
		DGV.Rows[DGV.Rows.Count - 1].Cells["idproduct_col"].Selected = true;
	}

	private void frmProducts_Load(object sender, EventArgs e)
	{
		DGV.AutoGenerateColumns = false;
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (columnIndex == DGV.Columns["edit_col"].Index)
		{
			sel_row = AController.DataRowToClass<Product>(((DataRowView)DGV.CurrentRow.DataBoundItem).Row);
			using frmproductinfo frmproductinfo2 = new frmproductinfo(sel_row);
			frmproductinfo2.BackColor = BackColor;
			frmproductinfo2.StartPosition = FormStartPosition.CenterParent;
			frmproductinfo2.ShowDialog(this);
			if (frmproductinfo2.commitchanges)
			{
				sel_row = null;
				sel_row = frmproductinfo2.Product;
				PropertyInfo[] properties = sel_row.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					DGV.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
				}
			}
			return;
		}
		if (columnIndex == DGV.Columns["delete_col"].Index)
		{
			MessageBox.Show("delete");
		}
		else if (columnIndex == DGV.Columns["details_col"].Index)
		{
			MessageBox.Show("show details");
		}
	}

	private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		this.DGV = new System.Windows.Forms.DataGridView();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnAdd = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();
		this.idproduct_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.code_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.location_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalblockno_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totallotno_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.totalarea_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cashprice_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.delete_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.details_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.label2 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idproduct_col, this.code_col, this.location_col, this.totalblockno_col, this.totallotno_col, this.totalarea_col, this.cashprice_col, this.edit_col, this.delete_col, this.details_col);
		this.DGV.Location = new System.Drawing.Point(12, 113);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGV.Size = new System.Drawing.Size(924, 503);
		this.DGV.TabIndex = 0;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellContentClick);
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(12, 83);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(720, 28);
		this.txtSearch.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(16, 62);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 2;
		this.label1.Text = "Search";
		this.btnAdd.Location = new System.Drawing.Point(865, 73);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(71, 34);
		this.btnAdd.TabIndex = 3;
		this.btnAdd.Text = "Add";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.btnClose.Location = new System.Drawing.Point(869, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.idproduct_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.idproduct_col.DataPropertyName = "idproduct";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.idproduct_col.DefaultCellStyle = dataGridViewCellStyle;
		this.idproduct_col.HeaderText = "ID";
		this.idproduct_col.Name = "idproduct_col";
		this.idproduct_col.ReadOnly = true;
		this.idproduct_col.Width = 5;
		this.code_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.code_col.DataPropertyName = "code";
		this.code_col.HeaderText = "Code";
		this.code_col.Name = "code_col";
		this.code_col.ReadOnly = true;
		this.code_col.Width = 5;
		this.location_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.location_col.DataPropertyName = "location";
		this.location_col.HeaderText = "Location";
		this.location_col.Name = "location_col";
		this.location_col.ReadOnly = true;
		this.totalblockno_col.DataPropertyName = "totalblockno";
		this.totalblockno_col.HeaderText = "Total block";
		this.totalblockno_col.Name = "totalblockno_col";
		this.totalblockno_col.ReadOnly = true;
		this.totalblockno_col.Width = 60;
		this.totallotno_col.DataPropertyName = "totallotno";
		this.totallotno_col.HeaderText = "Total Lot";
		this.totallotno_col.Name = "totallotno_col";
		this.totallotno_col.ReadOnly = true;
		this.totallotno_col.Width = 60;
		this.totalarea_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.totalarea_col.DataPropertyName = "totalarea";
		this.totalarea_col.HeaderText = "Total area";
		this.totalarea_col.Name = "totalarea_col";
		this.totalarea_col.ReadOnly = true;
		this.totalarea_col.Width = 5;
		this.cashprice_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.cashprice_col.DataPropertyName = "cashprice";
		this.cashprice_col.HeaderText = "Cash price";
		this.cashprice_col.Name = "cashprice_col";
		this.cashprice_col.ReadOnly = true;
		this.cashprice_col.Width = 5;
		this.edit_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "Edit";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.edit_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.edit_col.Text = "Edit";
		this.edit_col.UseColumnTextForButtonValue = true;
		this.edit_col.Width = 62;
		this.delete_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
		this.delete_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.delete_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_col.HeaderText = "Delete";
		this.delete_col.Name = "delete_col";
		this.delete_col.ReadOnly = true;
		this.delete_col.Text = "Delete";
		this.delete_col.UseColumnTextForButtonValue = true;
		this.delete_col.Width = 56;
		this.details_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Teal;
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
		this.details_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.details_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.details_col.HeaderText = "Details";
		this.details_col.Name = "details_col";
		this.details_col.ReadOnly = true;
		this.details_col.Text = "Details";
		this.details_col.UseColumnTextForButtonValue = true;
		this.details_col.Visible = false;
		this.details_col.Width = 59;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(12, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(128, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "List of Products";
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(952, 46);
		this.panel1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(952, 628);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmProducts";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmProducts";
		base.Load += new System.EventHandler(frmProducts_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
