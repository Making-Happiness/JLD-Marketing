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

public class frmClients : Form
{
	private ClientsCtrl controller;

	private Client sel_row;

	private Purchasedetail sel_row_purchase_details;

	private DataTable dt_source;

	private IContainer components = null;

	private Button btnAdd;

	private Label label1;

	private TextBox txtSearch;

	private Panel panel1;

	private Label label2;

	private Button btnClose;

	private DataGridView DGV;

	private DataGridView DGVPurchase;

	private Label label3;

	private DataGridViewTextBoxColumn code_col;

	private DataGridViewTextBoxColumn location_col;

	private DataGridViewTextBoxColumn status_col;

	private DataGridViewButtonColumn editpucahse_col;

	private DataGridViewButtonColumn Details_col;

	private DataGridViewTextBoxColumn idclients_col;

	private DataGridViewTextBoxColumn firstname_col;

	private DataGridViewTextBoxColumn middlename_col;

	private DataGridViewTextBoxColumn lastname_col;

	private DataGridViewTextBoxColumn gender_col;

	private DataGridViewTextBoxColumn dateofbirth_col;

	private DataGridViewTextBoxColumn placeofbirth_col;

	private DataGridViewTextBoxColumn spousename_col;

	private DataGridViewTextBoxColumn contactno_col;

	private DataGridViewButtonColumn edit_col;

	private DataGridViewButtonColumn delete_col;

	private DataGridViewButtonColumn apply_col;

	public frmClients()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
		DGVPurchase.AutoGenerateColumns = false;
		controller = new ClientsCtrl();
		DGV_refresh();
	}

	private void DGV_Purchase_refresh(int clientID)
	{
		DGVPurchase.DataSource = controller.getPurchaseDetailOrderTable(clientID);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		using frmClientinfo frmClientinfo2 = new frmClientinfo();
		frmClientinfo2.BackColor = BackColor;
		frmClientinfo2.StartPosition = FormStartPosition.CenterParent;
		frmClientinfo2.ShowDialog(this);
		if (frmClientinfo2.commitchanges)
		{
			DGV_refresh();
			DGV.Rows[DGV.Rows.Count - 1].Cells[0].Selected = true;
		}
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0)
		{
			return;
		}
		sel_row = AController.DataRowToClass<Client>(((DataRowView)DGV.CurrentRow.DataBoundItem).Row);
		if (columnIndex == DGV.Columns["edit_col"].Index)
		{
			using frmClientinfo frmClientinfo2 = new frmClientinfo(sel_row);
			frmClientinfo2.BackColor = BackColor;
			frmClientinfo2.StartPosition = FormStartPosition.CenterParent;
			frmClientinfo2.ShowDialog(this);
			if (frmClientinfo2.commitchanges)
			{
				sel_row = null;
				sel_row = frmClientinfo2.client;
				PropertyInfo[] properties = sel_row.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					DGV.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
				}
			}
		}
		else if (columnIndex == DGV.Columns["delete_col"].Index)
		{
			MessageBox.Show("delete");
		}
		else if (columnIndex == DGV.Columns["apply_col"].Index)
		{
			using frmApplicationForm frmApplicationForm2 = new frmApplicationForm(1, sel_row);
			frmApplicationForm2.clientTable = (DataTable)DGV.DataSource;
			frmApplicationForm2.productTable = null;
			frmApplicationForm2.BackColor = BackColor;
			frmApplicationForm2.StartPosition = FormStartPosition.CenterParent;
			frmApplicationForm2.ShowDialog(this);
		}
		DGV_Purchase_refresh(sel_row.idclients);
	}

	private void DGV_refresh(string filter = null)
	{
		if (filter == null)
		{
			dt_source = controller.getcustomTable("clientswithfullname");
			DGV.DataSource = dt_source;
			return;
		}
		if (dt_source == null)
		{
			dt_source = controller.getcustomTable("clientswithfullname");
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

	private void frmClients_Load(object sender, EventArgs e)
	{
	}

	private void DGVPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0)
		{
			return;
		}
		sel_row_purchase_details = AController.DataRowToClass<Purchasedetail>(((DataRowView)DGVPurchase.CurrentRow.DataBoundItem).Row);
		if (columnIndex == DGVPurchase.Columns["editpucahse_col"].Index)
		{
			using (frmApplicationForm frmApplicationForm2 = new frmApplicationForm(3, sel_row, sel_row_purchase_details))
			{
				frmApplicationForm2.clientTable = (DataTable)DGV.DataSource;
				frmApplicationForm2.productTable = (DataTable)DGVPurchase.DataSource;
				frmApplicationForm2.BackColor = BackColor;
				frmApplicationForm2.StartPosition = FormStartPosition.CenterParent;
				frmApplicationForm2.ShowDialog(this);
				if (frmApplicationForm2.commitchanges)
				{
					DGV_Purchase_refresh(sel_row.idclients);
				}
				return;
			}
		}
		if (columnIndex != DGVPurchase.Columns["Details_col"].Index)
		{
			return;
		}
		using frmpaymenthistory frmpaymenthistory2 = new frmpaymenthistory();
		frmpaymenthistory2.purchasedetail = sel_row_purchase_details;
		frmpaymenthistory2.BackColor = BackColor;
		frmpaymenthistory2.StartPosition = FormStartPosition.CenterParent;
		frmpaymenthistory2.ShowDialog(this);
	}

	private void DGVPurchase_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.btnAdd = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.DGVPurchase = new System.Windows.Forms.DataGridView();
		this.code_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.location_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.status_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.editpucahse_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.Details_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.idclients_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.firstname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.middlename_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lastname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gender_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateofbirth_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.placeofbirth_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.spousename_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contactno_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.delete_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.apply_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVPurchase).BeginInit();
		base.SuspendLayout();
		this.btnAdd.Location = new System.Drawing.Point(553, 75);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(148, 32);
		this.btnAdd.TabIndex = 6;
		this.btnAdd.Text = "New Record";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(5, 55);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 5;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(1, 79);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(539, 28);
		this.txtSearch.TabIndex = 4;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idclients_col, this.firstname_col, this.middlename_col, this.lastname_col, this.gender_col, this.dateofbirth_col, this.placeofbirth_col, this.spousename_col, this.contactno_col, this.edit_col, this.delete_col, this.apply_col);
		this.DGV.Location = new System.Drawing.Point(1, 113);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGV.Size = new System.Drawing.Size(776, 402);
		this.DGV.TabIndex = 8;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellContentClick);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1187, 41);
		this.panel1.TabIndex = 9;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(12, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(188, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "Stakeholder information";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(1104, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.DGVPurchase.AllowUserToAddRows = false;
		this.DGVPurchase.AllowUserToDeleteRows = false;
		this.DGVPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVPurchase.Columns.AddRange(this.code_col, this.location_col, this.status_col, this.editpucahse_col, this.Details_col);
		this.DGVPurchase.Location = new System.Drawing.Point(783, 113);
		this.DGVPurchase.Name = "DGVPurchase";
		this.DGVPurchase.ReadOnly = true;
		this.DGVPurchase.RowHeadersWidth = 20;
		this.DGVPurchase.RowTemplate.Height = 24;
		this.DGVPurchase.Size = new System.Drawing.Size(402, 402);
		this.DGVPurchase.TabIndex = 10;
		this.DGVPurchase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVPurchase_CellClick);
		this.DGVPurchase.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVPurchase_CellContentClick);
		this.code_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.code_col.DataPropertyName = "code";
		this.code_col.HeaderText = "Lot Code";
		this.code_col.Name = "code_col";
		this.code_col.ReadOnly = true;
		this.code_col.Width = 5;
		this.location_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.location_col.DataPropertyName = "location";
		this.location_col.HeaderText = "Location";
		this.location_col.Name = "location_col";
		this.location_col.ReadOnly = true;
		this.status_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.status_col.DataPropertyName = "remarks";
		this.status_col.HeaderText = "Status";
		this.status_col.Name = "status_col";
		this.status_col.ReadOnly = true;
		this.status_col.Width = 79;
		this.editpucahse_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(1);
		this.editpucahse_col.DefaultCellStyle = dataGridViewCellStyle;
		this.editpucahse_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.editpucahse_col.HeaderText = "Edit";
		this.editpucahse_col.Name = "editpucahse_col";
		this.editpucahse_col.ReadOnly = true;
		this.editpucahse_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.editpucahse_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.editpucahse_col.Text = "Edit";
		this.editpucahse_col.UseColumnTextForButtonValue = true;
		this.editpucahse_col.Width = 5;
		this.Details_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Green;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.Details_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.Details_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Details_col.HeaderText = "Details";
		this.Details_col.Name = "Details_col";
		this.Details_col.ReadOnly = true;
		this.Details_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Details_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.Details_col.Text = "Details";
		this.Details_col.UseColumnTextForButtonValue = true;
		this.Details_col.Width = 5;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(791, 85);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(136, 18);
		this.label3.TabIndex = 11;
		this.label3.Text = "Purchase Details";
		this.idclients_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.idclients_col.DataPropertyName = "idclients";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.idclients_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.idclients_col.HeaderText = "ID";
		this.idclients_col.Name = "idclients_col";
		this.idclients_col.ReadOnly = true;
		this.idclients_col.Width = 5;
		this.firstname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.firstname_col.DataPropertyName = "firstname";
		this.firstname_col.HeaderText = "FirstName";
		this.firstname_col.Name = "firstname_col";
		this.firstname_col.ReadOnly = true;
		this.firstname_col.Width = 106;
		this.middlename_col.DataPropertyName = "middlename";
		this.middlename_col.HeaderText = "MiddleName";
		this.middlename_col.Name = "middlename_col";
		this.middlename_col.ReadOnly = true;
		this.lastname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.lastname_col.DataPropertyName = "lastname";
		this.lastname_col.HeaderText = "LastName";
		this.lastname_col.Name = "lastname_col";
		this.lastname_col.ReadOnly = true;
		this.lastname_col.Width = 105;
		this.gender_col.DataPropertyName = "gender";
		this.gender_col.HeaderText = "Gender";
		this.gender_col.Name = "gender_col";
		this.gender_col.ReadOnly = true;
		this.gender_col.Width = 60;
		this.dateofbirth_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.dateofbirth_col.DataPropertyName = "dateofbirth";
		this.dateofbirth_col.HeaderText = "DateOfBirth";
		this.dateofbirth_col.Name = "dateofbirth_col";
		this.dateofbirth_col.ReadOnly = true;
		this.dateofbirth_col.Width = 5;
		this.placeofbirth_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.placeofbirth_col.DataPropertyName = "placeofbirth";
		this.placeofbirth_col.HeaderText = "PlaceOfBirth";
		this.placeofbirth_col.Name = "placeofbirth_col";
		this.placeofbirth_col.ReadOnly = true;
		this.placeofbirth_col.Visible = false;
		this.placeofbirth_col.Width = 5;
		this.spousename_col.DataPropertyName = "spousename";
		this.spousename_col.HeaderText = "SpouseName";
		this.spousename_col.Name = "spousename_col";
		this.spousename_col.ReadOnly = true;
		this.spousename_col.Visible = false;
		this.contactno_col.DataPropertyName = "contactno";
		this.contactno_col.HeaderText = "ContactNo";
		this.contactno_col.Name = "contactno_col";
		this.contactno_col.ReadOnly = true;
		this.edit_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle4;
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
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(1);
		this.delete_col.DefaultCellStyle = dataGridViewCellStyle5;
		this.delete_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_col.HeaderText = "Delete";
		this.delete_col.Name = "delete_col";
		this.delete_col.ReadOnly = true;
		this.delete_col.Text = "Delete";
		this.delete_col.UseColumnTextForButtonValue = true;
		this.delete_col.Width = 56;
		this.apply_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Teal;
		dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(1);
		this.apply_col.DefaultCellStyle = dataGridViewCellStyle6;
		this.apply_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.apply_col.HeaderText = "Apply";
		this.apply_col.Name = "apply_col";
		this.apply_col.ReadOnly = true;
		this.apply_col.Text = "Apply";
		this.apply_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1187, 519);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.DGVPurchase);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.DGV);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmClients";
		this.Text = "Stakeholders";
		base.Load += new System.EventHandler(frmClients_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVPurchase).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
