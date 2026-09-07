using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;
using RealProperty.Reports_Model;

namespace RealProperty;

public class frmpaymenthistory : Form
{
	private PurchasedetailsCtrl purchasedetailsCtrl;

	private Paymentdetail sel_row;

	private IContainer components = null;

	private DataGridView DGVHistory;

	private Label lbltotal;

	private Label lblprice;

	private DateTimePicker dtpDateofPayment;

	private Label label4;

	private NumericUpDown numAmount;

	private Label label3;

	private Button btnSaveDateofPayment;

	private Button btnSaveAmount;

	private BindingSource paymenthistoryviewBindingSource;

	private Label lblOhterfees;

	private Button btnPrint;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idclientsDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idpaymentDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idpurchasedetailsDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dateofpaymentDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn paymenttypeDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn delete_col;

	public Purchasedetail purchasedetail { get; set; }

	public Client client { get; set; }

	public frmpaymenthistory()
	{
		InitializeComponent();
		purchasedetailsCtrl = new PurchasedetailsCtrl();
	}

	private void DGV_refresh()
	{
		DataTable tablePaymentHistory = purchasedetailsCtrl.getTablePaymentHistory(purchasedetail);
		DGVHistory.DataSource = tablePaymentHistory;
		calculateTotal();
	}

	private void calculateTotal()
	{
		dynamic val = 0;
		DataTable dataTable = (DataTable)DGVHistory.DataSource;
		val = ((dataTable.Rows.Count <= 0) ? ((object)0m) : dataTable.Compute("Sum(amount)", string.Empty));
		lbltotal.Text = string.Format("Total Paid: {0}", ((decimal)val).ToString("#,##0.00"));
	}

	private void frmpaymenthistory_Load(object sender, EventArgs e)
	{
		DGV_refresh();
		try
		{
			decimal num = Convert.ToDecimal(purchasedetail.lotprice);
			decimal penalty = purchasedetail.penalty;
			decimal otherfees = purchasedetail.otherfees;
			decimal num2 = num + penalty + otherfees;
			lblprice.Text = string.Format("Total Amount: {0}", num2.ToString("#,##0.00"));
			lblOhterfees.Text = string.Format("Penalty: {0} \nOtherfees: {1} \nLot Price: {2}", penalty.ToString("#,##0.00"), otherfees.ToString("#,##0.00"), num.ToString("#,##0.00"));
		}
		catch (Exception)
		{
		}
	}

	private void btnSaveDateofPayment_Click(object sender, EventArgs e)
	{
		using PaymentsCtrl paymentsCtrl = new PaymentsCtrl();
		Payment payment = new Payment();
		dynamic value = DGVHistory.CurrentRow.Cells["idpaymentDataGridViewTextBoxColumn"].Value;
		try
		{
			payment.id = value;
		}
		catch (Exception)
		{
			return;
		}
		payment = paymentsCtrl.getRecordbyID(value);
		payment.dateofpayment = dtpDateofPayment.Value;
		int num = paymentsCtrl.edit(payment);
		if (num > 0)
		{
			DGVHistory.CurrentRow.Cells["dateofpaymentDataGridViewTextBoxColumn"].Value = payment.dateofpayment;
			calculateTotal();
			MessageBox.Show("Date of Payment Successfully Saved..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("Date of Payment Failed to Saved..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void DGVHistory_SelectionChanged(object sender, EventArgs e)
	{
		dynamic value = DGVHistory.CurrentRow.Cells["dateofpaymentDataGridViewTextBoxColumn"].Value;
		dynamic value2 = DGVHistory.CurrentRow.Cells["amountDataGridViewTextBoxColumn"].Value;
		try
		{
			dtpDateofPayment.Value = value;
		}
		catch (Exception)
		{
		}
		try
		{
			numAmount.Value = value2;
		}
		catch (Exception)
		{
		}
	}

	private void btnSaveAmount_Click(object sender, EventArgs e)
	{
		using PyamentdetailsCtrl pyamentdetailsCtrl = new PyamentdetailsCtrl();
		Paymentdetail paymentdetail = new Paymentdetail();
		dynamic value = DGVHistory.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value;
		try
		{
			paymentdetail.id = value;
		}
		catch (Exception)
		{
			return;
		}
		paymentdetail = pyamentdetailsCtrl.getRecordbyID(value);
		paymentdetail.amount = numAmount.Value;
		int num = pyamentdetailsCtrl.edit(paymentdetail);
		if (num > 0)
		{
			DGVHistory.CurrentRow.Cells["amountDataGridViewTextBoxColumn"].Value = paymentdetail.amount;
			calculateTotal();
			MessageBox.Show("Amout Successfully Saved..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("Amount Failed to Saved..", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		AReports reports = new rpt_soa_source(purchasedetail);
		frmReportView frmReportView2 = new frmReportView();
		frmReportView2.reports = reports;
		frmReportView2.ShowDialog(this);
	}

	private void DGVHistory_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex < 0)
		{
			return;
		}
		sel_row = AController.DataRowToClass<Paymentdetail>(((DataRowView)DGVHistory.CurrentRow.DataBoundItem).Row);
		if (columnIndex != DGVHistory.Columns["delete_col"].Index)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("Deleting this record cannot be reveted. \nYou want to delete this record?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		DialogResult dialogResult2 = MessageBox.Show("Are you sure?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult2 != DialogResult.Yes)
		{
			return;
		}
		using PaymentsCtrl paymentsCtrl = new PaymentsCtrl();
		int idpayment = sel_row.idpayment;
		int id = sel_row.id;
		int num = paymentsCtrl.deleterecordstatus(idpayment, id);
		if (num > 0)
		{
			DGV_refresh();
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
		this.DGVHistory = new System.Windows.Forms.DataGridView();
		this.lbltotal = new System.Windows.Forms.Label();
		this.lblprice = new System.Windows.Forms.Label();
		this.dtpDateofPayment = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.btnSaveDateofPayment = new System.Windows.Forms.Button();
		this.btnSaveAmount = new System.Windows.Forms.Button();
		this.lblOhterfees = new System.Windows.Forms.Label();
		this.btnPrint = new System.Windows.Forms.Button();
		this.delete_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idclientsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idpaymentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idpurchasedetailsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateofpaymentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.paymenttypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.paymenthistoryviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
		((System.ComponentModel.ISupportInitialize)this.DGVHistory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.paymenthistoryviewBindingSource).BeginInit();
		base.SuspendLayout();
		this.DGVHistory.AllowUserToAddRows = false;
		this.DGVHistory.AllowUserToDeleteRows = false;
		this.DGVHistory.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVHistory.AutoGenerateColumns = false;
		this.DGVHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVHistory.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.idclientsDataGridViewTextBoxColumn, this.idpaymentDataGridViewTextBoxColumn, this.idpurchasedetailsDataGridViewTextBoxColumn, this.dateofpaymentDataGridViewTextBoxColumn, this.descriptionDataGridViewTextBoxColumn, this.amountDataGridViewTextBoxColumn, this.paymenttypeDataGridViewTextBoxColumn, this.delete_col);
		this.DGVHistory.DataSource = this.paymenthistoryviewBindingSource;
		this.DGVHistory.Location = new System.Drawing.Point(5, 59);
		this.DGVHistory.MultiSelect = false;
		this.DGVHistory.Name = "DGVHistory";
		this.DGVHistory.ReadOnly = true;
		this.DGVHistory.RowHeadersWidth = 21;
		this.DGVHistory.RowTemplate.Height = 24;
		this.DGVHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGVHistory.Size = new System.Drawing.Size(996, 436);
		this.DGVHistory.TabIndex = 14;
		this.DGVHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVHistory_CellClick);
		this.DGVHistory.SelectionChanged += new System.EventHandler(DGVHistory_SelectionChanged);
		this.lbltotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbltotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbltotal.Location = new System.Drawing.Point(534, 564);
		this.lbltotal.Name = "lbltotal";
		this.lbltotal.Size = new System.Drawing.Size(459, 36);
		this.lbltotal.TabIndex = 15;
		this.lbltotal.Text = "Total Paid: 0.00";
		this.lbltotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblprice.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblprice.Location = new System.Drawing.Point(12, 564);
		this.lblprice.Name = "lblprice";
		this.lblprice.Size = new System.Drawing.Size(459, 36);
		this.lblprice.TabIndex = 16;
		this.lblprice.Text = "Total Price: 0.00";
		this.lblprice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.dtpDateofPayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDateofPayment.Location = new System.Drawing.Point(143, 31);
		this.dtpDateofPayment.Name = "dtpDateofPayment";
		this.dtpDateofPayment.Size = new System.Drawing.Size(223, 24);
		this.dtpDateofPayment.TabIndex = 18;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(12, 36);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(125, 18);
		this.label4.TabIndex = 17;
		this.label4.Text = "Date Of Payment:";
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(708, 29);
		this.numAmount.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(151, 24);
		this.numAmount.TabIndex = 19;
		this.numAmount.ThousandsSeparator = true;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(637, 31);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(63, 18);
		this.label3.TabIndex = 20;
		this.label3.Text = "Amount:";
		this.btnSaveDateofPayment.Location = new System.Drawing.Point(372, 31);
		this.btnSaveDateofPayment.Name = "btnSaveDateofPayment";
		this.btnSaveDateofPayment.Size = new System.Drawing.Size(88, 24);
		this.btnSaveDateofPayment.TabIndex = 21;
		this.btnSaveDateofPayment.Text = "Save";
		this.btnSaveDateofPayment.UseVisualStyleBackColor = true;
		this.btnSaveDateofPayment.Click += new System.EventHandler(btnSaveDateofPayment_Click);
		this.btnSaveAmount.Location = new System.Drawing.Point(865, 29);
		this.btnSaveAmount.Name = "btnSaveAmount";
		this.btnSaveAmount.Size = new System.Drawing.Size(88, 24);
		this.btnSaveAmount.TabIndex = 21;
		this.btnSaveAmount.Text = "Save";
		this.btnSaveAmount.UseVisualStyleBackColor = true;
		this.btnSaveAmount.Click += new System.EventHandler(btnSaveAmount_Click);
		this.lblOhterfees.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblOhterfees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblOhterfees.Location = new System.Drawing.Point(11, 498);
		this.lblOhterfees.Name = "lblOhterfees";
		this.lblOhterfees.Size = new System.Drawing.Size(459, 66);
		this.lblOhterfees.TabIndex = 16;
		this.lblOhterfees.Text = "Penalty: 0.00 OtherFees: 0.00";
		this.lblOhterfees.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnPrint.BackColor = System.Drawing.Color.RoyalBlue;
		this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPrint.ForeColor = System.Drawing.Color.WhiteSmoke;
		this.btnPrint.Location = new System.Drawing.Point(836, 513);
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(157, 37);
		this.btnPrint.TabIndex = 22;
		this.btnPrint.Text = "Print SOA";
		this.btnPrint.UseVisualStyleBackColor = false;
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.delete_col.DefaultCellStyle = dataGridViewCellStyle;
		this.delete_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_col.HeaderText = "action";
		this.delete_col.Name = "delete_col";
		this.delete_col.ReadOnly = true;
		this.delete_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.delete_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.delete_col.Text = "Delete";
		this.delete_col.UseColumnTextForButtonValue = true;
		this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
		this.idDataGridViewTextBoxColumn.HeaderText = "id";
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.ReadOnly = true;
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.idclientsDataGridViewTextBoxColumn.DataPropertyName = "idclients";
		this.idclientsDataGridViewTextBoxColumn.HeaderText = "idclients";
		this.idclientsDataGridViewTextBoxColumn.Name = "idclientsDataGridViewTextBoxColumn";
		this.idclientsDataGridViewTextBoxColumn.ReadOnly = true;
		this.idclientsDataGridViewTextBoxColumn.Visible = false;
		this.idpaymentDataGridViewTextBoxColumn.DataPropertyName = "idpayment";
		this.idpaymentDataGridViewTextBoxColumn.HeaderText = "idpayment";
		this.idpaymentDataGridViewTextBoxColumn.Name = "idpaymentDataGridViewTextBoxColumn";
		this.idpaymentDataGridViewTextBoxColumn.ReadOnly = true;
		this.idpaymentDataGridViewTextBoxColumn.Visible = false;
		this.idpurchasedetailsDataGridViewTextBoxColumn.DataPropertyName = "idpurchasedetails";
		this.idpurchasedetailsDataGridViewTextBoxColumn.HeaderText = "idpurchasedetails";
		this.idpurchasedetailsDataGridViewTextBoxColumn.Name = "idpurchasedetailsDataGridViewTextBoxColumn";
		this.idpurchasedetailsDataGridViewTextBoxColumn.ReadOnly = true;
		this.idpurchasedetailsDataGridViewTextBoxColumn.Visible = false;
		this.dateofpaymentDataGridViewTextBoxColumn.DataPropertyName = "dateofpayment";
		this.dateofpaymentDataGridViewTextBoxColumn.HeaderText = "dateofpayment";
		this.dateofpaymentDataGridViewTextBoxColumn.Name = "dateofpaymentDataGridViewTextBoxColumn";
		this.dateofpaymentDataGridViewTextBoxColumn.ReadOnly = true;
		this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
		this.descriptionDataGridViewTextBoxColumn.HeaderText = "description";
		this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
		this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
		this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
		this.amountDataGridViewTextBoxColumn.HeaderText = "amount";
		this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
		this.amountDataGridViewTextBoxColumn.ReadOnly = true;
		this.paymenttypeDataGridViewTextBoxColumn.DataPropertyName = "paymenttype";
		this.paymenttypeDataGridViewTextBoxColumn.HeaderText = "paymenttype";
		this.paymenttypeDataGridViewTextBoxColumn.Name = "paymenttypeDataGridViewTextBoxColumn";
		this.paymenttypeDataGridViewTextBoxColumn.ReadOnly = true;
		this.paymenthistoryviewBindingSource.DataSource = typeof(RealProperty.BEL.Paymenthistory_view);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1005, 620);
		base.Controls.Add(this.btnPrint);
		base.Controls.Add(this.btnSaveAmount);
		base.Controls.Add(this.btnSaveDateofPayment);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.dtpDateofPayment);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.lblOhterfees);
		base.Controls.Add(this.lblprice);
		base.Controls.Add(this.lbltotal);
		base.Controls.Add(this.DGVHistory);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmpaymenthistory";
		this.Text = "Payment History";
		base.Load += new System.EventHandler(frmpaymenthistory_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVHistory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.paymenthistoryviewBindingSource).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
