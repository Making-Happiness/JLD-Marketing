using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPaySlip : Form
{
	private IContainer components = null;

	private DataGridView DGVPayslip;

	private Panel panel1;

	private Label label2;

	private Button btnClose;

	private Button btnAdd;

	private Label label1;

	private TextBox txtSearch;

	private DataGridViewTextBoxColumn idpayslip_col;

	private DataGridViewTextBoxColumn title_col;

	private DataGridViewTextBoxColumn for_month_of_col;

	private DataGridViewTextBoxColumn payslip_type_col;

	private DataGridViewTextBoxColumn recordstatus_col;

	private DataGridViewButtonColumn editpayslip_col;

	private DataGridViewButtonColumn open_col;

	public frmPaySlip()
	{
		InitializeComponent();
		DGVPayslip.AutoGenerateColumns = false;
	}

	private void frmPaySlip_Load(object sender, EventArgs e)
	{
		Refresh_DGVPayslip();
	}

	private void Refresh_DGVPayslip()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable payslipTable = payrollCtrl.getPayslipTable();
		DGVPayslip.DataSource = payslipTable;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		Payslip payslip = new Payslip();
		string transaction = "ADD";
		using frmpayslipinfo frmpayslipinfo2 = new frmpayslipinfo(payslip, transaction);
		frmpayslipinfo2.ShowDialog(this);
		if (frmpayslipinfo2.CommitChanges)
		{
			Refresh_DGVPayslip();
		}
	}

	private void DGVPayslip_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		int id = Convert.ToInt32(DGVPayslip.CurrentRow.Cells[idpayslip_col.Name].Value);
		Payslip payslipObject;
		if (columnIndex == DGVPayslip.Columns[editpayslip_col.Name].Index)
		{
			using (PayrollCtrl payrollCtrl = new PayrollCtrl())
			{
				payslipObject = payrollCtrl.getPayslipObject(id);
			}
			if (payslipObject == null)
			{
				return;
			}
			using frmpayslipinfo frmpayslipinfo2 = new frmpayslipinfo(payslipObject);
			frmpayslipinfo2.ShowDialog(this);
			if (frmpayslipinfo2.CommitChanges)
			{
				Refresh_DGVPayslip();
			}
			return;
		}
		if (columnIndex == DGVPayslip.Columns[open_col.Name].Index)
		{
			using (PayrollCtrl payrollCtrl = new PayrollCtrl())
			{
				DataTable dataTable = payrollCtrl.getneratePayslipdetails(id);
				object obj = dataTable.Rows[0].Field<object>(0);
				payslipObject = payrollCtrl.getPayslipObject(id);
			}
			using frmPayslipDetails frmPayslipDetails2 = new frmPayslipDetails();
			frmPayslipDetails2.payslip = payslipObject;
			frmPayslipDetails2.ShowDialog(this);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVPayslip = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnAdd = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.idpayslip_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.title_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.for_month_of_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.payslip_type_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.editpayslip_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.open_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGVPayslip).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.DGVPayslip.AllowUserToAddRows = false;
		this.DGVPayslip.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.DGVPayslip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVPayslip.Columns.AddRange(this.idpayslip_col, this.title_col, this.for_month_of_col, this.payslip_type_col, this.recordstatus_col, this.editpayslip_col, this.open_col);
		this.DGVPayslip.Location = new System.Drawing.Point(0, 104);
		this.DGVPayslip.MultiSelect = false;
		this.DGVPayslip.Name = "DGVPayslip";
		this.DGVPayslip.ReadOnly = true;
		this.DGVPayslip.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVPayslip.RowHeadersWidth = 20;
		this.DGVPayslip.RowTemplate.Height = 24;
		this.DGVPayslip.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGVPayslip.Size = new System.Drawing.Size(882, 440);
		this.DGVPayslip.TabIndex = 18;
		this.DGVPayslip.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVPayslip_CellClick);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(883, 46);
		this.panel1.TabIndex = 17;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label2.Location = new System.Drawing.Point(14, 10);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(150, 18);
		this.label2.TabIndex = 2;
		this.label2.Text = "Payroll information";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.Location = new System.Drawing.Point(802, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(80, 38);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnAdd.Location = new System.Drawing.Point(753, 64);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(129, 34);
		this.btnAdd.TabIndex = 21;
		this.btnAdd.Text = "New Record";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(4, 49);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 20;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(0, 70);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(714, 28);
		this.txtSearch.TabIndex = 19;
		this.idpayslip_col.DataPropertyName = "id";
		this.idpayslip_col.HeaderText = "id";
		this.idpayslip_col.Name = "idpayslip_col";
		this.idpayslip_col.ReadOnly = true;
		this.idpayslip_col.Visible = false;
		this.title_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.title_col.DataPropertyName = "title";
		this.title_col.HeaderText = "Title";
		this.title_col.Name = "title_col";
		this.title_col.ReadOnly = true;
		this.for_month_of_col.DataPropertyName = "for_month_of";
		this.for_month_of_col.HeaderText = "for_month_of";
		this.for_month_of_col.Name = "for_month_of_col";
		this.for_month_of_col.ReadOnly = true;
		this.payslip_type_col.DataPropertyName = "payslip_type";
		this.payslip_type_col.HeaderText = "payslip_type";
		this.payslip_type_col.Name = "payslip_type_col";
		this.payslip_type_col.ReadOnly = true;
		this.recordstatus_col.DataPropertyName = "recordstatus";
		this.recordstatus_col.HeaderText = "recordstatus";
		this.recordstatus_col.Name = "recordstatus_col";
		this.recordstatus_col.ReadOnly = true;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.Lime;
		this.editpayslip_col.DefaultCellStyle = dataGridViewCellStyle;
		this.editpayslip_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.editpayslip_col.HeaderText = "action";
		this.editpayslip_col.Name = "editpayslip_col";
		this.editpayslip_col.ReadOnly = true;
		this.editpayslip_col.Text = "edit";
		this.editpayslip_col.UseColumnTextForButtonValue = true;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
		this.open_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.open_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.open_col.HeaderText = "Open";
		this.open_col.Name = "open_col";
		this.open_col.ReadOnly = true;
		this.open_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.open_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.open_col.Text = "Open";
		this.open_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(883, 547);
		base.Controls.Add(this.btnAdd);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtSearch);
		base.Controls.Add(this.DGVPayslip);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPaySlip";
		this.Text = "Payslip List";
		base.Load += new System.EventHandler(frmPaySlip_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVPayslip).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
