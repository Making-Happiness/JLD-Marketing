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

public class frmAgentDicer : Form
{
	private ClientsCtrl controller;

	private Agent sel_row;

	private DataTable dt_source;

	private IContainer components = null;

	private DataGridView DGV;

	private Panel panel1;

	private Label label2;

	private Button btnClose;

	private Button btnAdd;

	private Button btnCommissionCenter;

	private Label label1;

	private TextBox txtSearch;

	private DataGridViewTextBoxColumn id_col;

	private DataGridViewTextBoxColumn fullname_col;

	private DataGridViewTextBoxColumn contactno_col;

	private DataGridViewTextBoxColumn recordstatus_col;

	private DataGridViewButtonColumn calims_col;

	private DataGridViewButtonColumn edit_col;

	private DataGridViewButtonColumn delete_col;

	public frmAgentDicer()
	{
		InitializeComponent();
		DGV.AutoGenerateColumns = false;
		controller = new ClientsCtrl();
		DGV_refresh();
	}

	private void DGV_refresh(string filter = null)
	{
		if (filter == null)
		{
			dt_source = controller.get_view_agentsTable();
			DGV.DataSource = controller.get_view_agentsTable();
			return;
		}
		if (dt_source == null)
		{
			dt_source = controller.get_view_agentsTable();
		}
		if (filter == "")
		{
			DGV.DataSource = dt_source;
			return;
		}
		string filterExpression = $"fullname like '%{filter}%'";
		DataRow[] source = dt_source.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataTable = dt_source.Clone();
			dataTable.Clear();
			DGV.DataSource = dataTable;
		}
		else
		{
			DGV.DataSource = source.CopyToDataTable();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCommissionCenter_Click(object sender, EventArgs e)
	{
		frmCommision commForm = new frmCommision();
		Program.fmain.showForm(commForm);
	}

	private void frmAgentDicer_Load(object sender, EventArgs e)
	{
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0)
		{
			return;
		}
		sel_row = AController.DataRowToClass<Agent>(((DataRowView)DGV.CurrentRow.DataBoundItem).Row);
		if (columnIndex == DGV.Columns["calims_col"].Index)
		{
			frmCommision commForm = new frmCommision();
			commForm.TargetAgentId = sel_row.id;
			Program.fmain.showForm(commForm);
			return;
		}
		if (columnIndex == DGV.Columns["edit_col"].Index)
		{
			using (frmAgentInfo frmAgentInfo2 = new frmAgentInfo(sel_row))
			{
				frmAgentInfo2.BackColor = BackColor;
				frmAgentInfo2.StartPosition = FormStartPosition.CenterParent;
				frmAgentInfo2.ShowDialog(Program.fmain);
				if (frmAgentInfo2.commitchanges)
				{
					sel_row = null;
					sel_row = frmAgentInfo2.agent;
					PropertyInfo[] properties = sel_row.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						try
						{
							DGV.CurrentRow.Cells[propertyInfo.Name + "_col"].Value = sel_row.GetType().GetProperty(propertyInfo.Name).GetValue(sel_row, null);
						}
						catch (Exception)
						{
						}
					}
				}
				return;
			}
		}
		if (columnIndex == DGV.Columns["delete_col"].Index)
		{
			MessageBox.Show("Delete agent record");
		}
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		using frmAgentInfo frmAgentInfo2 = new frmAgentInfo();
		frmAgentInfo2.BackColor = BackColor;
		frmAgentInfo2.StartPosition = FormStartPosition.CenterParent;
		frmAgentInfo2.ShowDialog(this);
		if (frmAgentInfo2.commitchanges)
		{
			DGV_refresh();
			DGV.Rows[DGV.Rows.Count - 1].Cells[0].Selected = true;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnAdd = new System.Windows.Forms.Button();
		this.btnCommissionCenter = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.id_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contactno_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.calims_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.delete_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.DGV.AllowUserToAddRows = false;
		this.DGV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.id_col, this.fullname_col, this.contactno_col, this.recordstatus_col, this.calims_col, this.edit_col, this.delete_col);
		this.DGV.Location = new System.Drawing.Point(7, 105);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGV.Size = new System.Drawing.Size(717, 383);
		this.DGV.TabIndex = 9;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(732, 41);
		this.panel1.TabIndex = 10;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(12, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(185, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "Agent / Dicer Information";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(649, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
		this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAdd.Location = new System.Drawing.Point(559, 67);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(165, 36);
		this.btnAdd.TabIndex = 13;
		this.btnAdd.Text = "+ New Agent";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
		this.btnCommissionCenter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCommissionCenter.Location = new System.Drawing.Point(375, 67);
		this.btnCommissionCenter.Name = "btnCommissionCenter";
		this.btnCommissionCenter.Size = new System.Drawing.Size(175, 36);
		this.btnCommissionCenter.TabIndex = 14;
		this.btnCommissionCenter.Text = "Commissions & Claims";
		this.btnCommissionCenter.BackColor = System.Drawing.Color.FromArgb(0, 89, 59);
		this.btnCommissionCenter.ForeColor = System.Drawing.Color.White;
		this.btnCommissionCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnCommissionCenter.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCommissionCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCommissionCenter.UseVisualStyleBackColor = false;
		this.btnCommissionCenter.Click += new System.EventHandler(this.btnCommissionCenter_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(11, 47);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 12;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(7, 71);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(355, 28);
		this.txtSearch.TabIndex = 11;
		this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
		this.id_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.id_col.DataPropertyName = "id";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.id_col.DefaultCellStyle = dataGridViewCellStyle;
		this.id_col.HeaderText = "ID";
		this.id_col.Name = "id_col";
		this.id_col.ReadOnly = true;
		this.id_col.Visible = false;
		this.id_col.Width = 27;
		this.fullname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.fullname_col.DataPropertyName = "fullname";
		this.fullname_col.HeaderText = "Agent / Dicer Name";
		this.fullname_col.Name = "fullname_col";
		this.fullname_col.ReadOnly = true;
		this.contactno_col.DataPropertyName = "contactno";
		this.contactno_col.HeaderText = "Contact No.";
		this.contactno_col.Name = "contactno_col";
		this.contactno_col.ReadOnly = true;
		this.recordstatus_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.recordstatus_col.DataPropertyName = "recordstatus";
		this.recordstatus_col.HeaderText = "Status";
		this.recordstatus_col.Name = "recordstatus_col";
		this.recordstatus_col.ReadOnly = true;
		this.recordstatus_col.Width = 75;
		this.calims_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(255, 255, 192);
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.calims_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.calims_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.calims_col.HeaderText = "Claims";
		this.calims_col.Name = "calims_col";
		this.calims_col.ReadOnly = true;
		this.calims_col.Text = "Claims";
		this.calims_col.ToolTipText = "View agent commissions and release claim vouchers";
		this.calims_col.UseColumnTextForButtonValue = true;
		this.calims_col.Width = 55;
		this.edit_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "Edit";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.edit_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.edit_col.Text = "Edit";
		this.edit_col.UseColumnTextForButtonValue = true;
		this.edit_col.Width = 61;
		this.delete_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
		this.delete_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.delete_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_col.HeaderText = "Delete";
		this.delete_col.Name = "delete_col";
		this.delete_col.ReadOnly = true;
		this.delete_col.Text = "Delete";
		this.delete_col.UseColumnTextForButtonValue = true;
		this.delete_col.Visible = false;
		this.delete_col.Width = 55;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(732, 496);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.btnCommissionCenter);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmAgentDicer";
		this.Text = "Agents & Property Specialists Directory";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(this.frmAgentDicer_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
