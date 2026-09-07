using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmpayments : Form
{
	private IContainer components = null;

	private DataGridView DGV;

	private ComboBox cboFullname;

	private Label label1;

	private Label label2;

	private NumericUpDown numAmount;

	private ComboBox cboPaymentType;

	private Label label3;

	private TextBox txtReferenceNo;

	private Label label4;

	private Button btnAcceptPayment;

	private Label label5;

	private TextBox txtOR;

	private DateTimePicker dtpDateofPayment;

	private Label label6;

	private Label label7;

	private ComboBox cboRecieveByIncharge;

	private Panel panel1;

	private Label label8;

	private Button btnAdd;

	private Label label9;

	private ComboBox cboPaymentFor;

	private Label lblTotal;

	private Label label10;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem fileToolStripMenuItem;

	private ToolStripMenuItem SearchPaymentsToolStripMenuItem;

	private ComboBox cboPaidBy;

	private Label label11;

	private GroupBox groupBox1;

	private GroupBox groupBoxPayment;

	private TextBox txtPaymentNo;

	private Label label12;

	private DataGridViewTextBoxColumn idproduct_col;

	private DataGridViewTextBoxColumn description_col;

	private DataGridViewTextBoxColumn amount_col;

	private DataGridViewButtonColumn remove_col;

	public Client selected_client { get; set; }

	private DataTable purchasedetailsTable { get; set; }

	public DataTable view_clientsTable { get; set; }

	private Payment payment { get; set; }

	private Paymentdetail paymentdetail { get; set; }

	private List<Paymentdetail> paymentDetailList { get; set; }

	public frmpayments()
	{
		InitializeComponent();
		paymentdetail = new Paymentdetail();
		paymentDetailList = new List<Paymentdetail>();
		DGV.AutoGenerateColumns = false;
		dtpDateofPayment.Value = DateTime.Today;
		lblTotal.Font = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold);
		payment = new Payment();
	}

	private void BindData()
	{
		cboFullname.DataBindings.Add("SelectedValue", paymentdetail, "idpurchasedetails     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboPaymentFor.DataBindings.Add("Text", paymentdetail, "paymentfor     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", paymentdetail, "amount     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		dtpDateofPayment.DataBindings.Add("Value", payment, "dateofpayment     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtOR.DataBindings.Add("Text", payment, "orderreceipt     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtReferenceNo.DataBindings.Add("Text", payment, "referenceno     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboRecieveByIncharge.DataBindings.Add("SelectedValue", payment, "inchargeby     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboPaymentType.DataBindings.Add("Text", payment, "paymenttype     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtPaymentNo.DataBindings.Add("Text", payment, "paymentref     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboPaidBy.DataBindings.Add("SelectedValue", payment, "paidby     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void frmpayments_Load(object sender, EventArgs e)
	{
		groupBoxPayment.Visible = false;
		base.Height = 500;
		purchasedetailsTable = ((frmMain)base.ParentForm).view_purchasedetails_full;
		DataRow[] source = purchasedetailsTable.Select($"idclients={selected_client.idclients}");
		if (source.Count() > 0)
		{
			cboFullname.DataSource = source.CopyToDataTable();
			cboFullname.DisplayMember = "description";
			cboFullname.ValueMember = "id";
			cboFullname.AutoCompleteSource = AutoCompleteSource.ListItems;
			cboFullname.AutoCompleteMode = AutoCompleteMode.Suggest;
		}
		view_clientsTable = ((frmMain)base.ParentForm).view_clientsTable;
		cboPaidBy.DataSource = view_clientsTable;
		cboPaidBy.DisplayMember = "fullname";
		cboPaidBy.ValueMember = "idclients";
		cboPaidBy.AutoCompleteSource = AutoCompleteSource.ListItems;
		cboPaidBy.AutoCompleteMode = AutoCompleteMode.Suggest;
		cboRecieveByIncharge.DataSource = view_clientsTable.Copy();
		cboRecieveByIncharge.DisplayMember = "fullname";
		cboRecieveByIncharge.ValueMember = "idclients";
		cboRecieveByIncharge.AutoCompleteSource = AutoCompleteSource.ListItems;
		cboRecieveByIncharge.AutoCompleteMode = AutoCompleteMode.Suggest;
		BindData();
		if (cboFullname.Items.Count > 0)
		{
			cboFullname.SelectedIndex = 0;
		}
	}

	private void label5_Click(object sender, EventArgs e)
	{
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
	}

	private void cboPaymentFor_Enter(object sender, EventArgs e)
	{
		cboPaymentFor.DroppedDown = true;
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		if (this.paymentdetail.idpurchasedetails <= 0)
		{
			MessageBox.Show("Please select product again.");
			cboFullname.DroppedDown = true;
		}
		Paymentdetail paymentdetail = new Paymentdetail();
		paymentdetail.id = this.paymentdetail.id;
		paymentdetail.idpurchasedetails = this.paymentdetail.idpurchasedetails;
		paymentdetail.amount = this.paymentdetail.amount;
		paymentdetail.paymentfor = this.paymentdetail.paymentfor;
		paymentdetail.idpayment = this.paymentdetail.idpayment;
		paymentdetail.description = this.paymentdetail.description;
		paymentDetailList.Add(paymentdetail);
		DGV_Refresh();
		cboPaymentType.Focus();
		if (cboPaidBy.Text == "")
		{
			try
			{
				DataRowView dataRowView = (DataRowView)cboFullname.SelectedItem;
				Purchasedetail purchasedetail = AController.DataRowToClass<Purchasedetail>(dataRowView.Row);
				cboPaidBy.SelectedValue = purchasedetail.idclients;
			}
			catch (Exception)
			{
			}
		}
	}

	private void DGV_Refresh()
	{
		DGV.DataSource = null;
		DGV.DataSource = paymentDetailList;
		calculateTotal();
		if (DGV.Rows.Count > 0)
		{
			groupBoxPayment.Visible = true;
			base.Height = 650;
		}
		else
		{
			groupBoxPayment.Visible = false;
			base.Height = 500;
		}
	}

	private void cboFullname_SelectedValueChanged(object sender, EventArgs e)
	{
		paymentdetail.description = cboFullname.Text;
	}

	private void numAmount_Enter(object sender, EventArgs e)
	{
		numAmount.Select(0, numAmount.Text.Length);
	}

	private void calculateTotal()
	{
		decimal totalamount = 0m;
		foreach (Paymentdetail paymentDetail in paymentDetailList)
		{
			totalamount += paymentDetail.amount;
		}
		lblTotal.Text = string.Format("Total Amount: {0}", totalamount.ToString("#,##0.00"));
		payment.totalamount = totalamount;
	}

	private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex >= 0)
		{
			Paymentdetail item = (Paymentdetail)DGV.Rows[rowIndex].DataBoundItem;
			if (columnIndex == DGV.Columns["remove_col"].Index)
			{
				paymentDetailList.Remove(item);
				DGV_Refresh();
			}
		}
	}

	private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cboPaymentType.Text == "CASH")
		{
			txtReferenceNo.Enabled = false;
		}
		else
		{
			txtReferenceNo.Enabled = true;
		}
	}

	private void btnAcceptPayment_Click(object sender, EventArgs e)
	{
		using PaymentsCtrl paymentsCtrl = new PaymentsCtrl();
		if (cboRecieveByIncharge.Text == "")
		{
			payment.inchargeby = 0;
		}
		int num = paymentsCtrl.add(payment, paymentDetailList);
		if (num > 0)
		{
			MessageBox.Show("Payments Successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		else
		{
			MessageBox.Show("Payments Failed", "Somthing Wrong", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cboPaymentFor_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void lblTotal_Click(object sender, EventArgs e)
	{
	}

	private void lblTotal_TextChanged(object sender, EventArgs e)
	{
	}

	private void SearchPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		using frmSearchPayments frmSearchPayments2 = new frmSearchPayments();
		frmSearchPayments2.ShowDialog(base.ParentForm);
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
		this.DGV = new System.Windows.Forms.DataGridView();
		this.idproduct_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.description_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.remove_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.cboFullname = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.cboPaymentType = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtReferenceNo = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.btnAcceptPayment = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.txtOR = new System.Windows.Forms.TextBox();
		this.dtpDateofPayment = new System.Windows.Forms.DateTimePicker();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cboRecieveByIncharge = new System.Windows.Forms.ComboBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label8 = new System.Windows.Forms.Label();
		this.txtPaymentNo = new System.Windows.Forms.TextBox();
		this.btnAdd = new System.Windows.Forms.Button();
		this.label9 = new System.Windows.Forms.Label();
		this.cboPaymentFor = new System.Windows.Forms.ComboBox();
		this.lblTotal = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SearchPaymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.cboPaidBy = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBoxPayment = new System.Windows.Forms.GroupBox();
		this.label12 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		this.panel1.SuspendLayout();
		this.menuStrip1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBoxPayment.SuspendLayout();
		base.SuspendLayout();
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.idproduct_col, this.description_col, this.amount_col, this.remove_col);
		this.DGV.Location = new System.Drawing.Point(12, 219);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGV.Size = new System.Drawing.Size(632, 191);
		this.DGV.TabIndex = 1;
		this.DGV.TabStop = false;
		this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_CellClick);
		this.idproduct_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.idproduct_col.DataPropertyName = "idproduct";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.idproduct_col.DefaultCellStyle = dataGridViewCellStyle;
		this.idproduct_col.HeaderText = "ID";
		this.idproduct_col.Name = "idproduct_col";
		this.idproduct_col.ReadOnly = true;
		this.idproduct_col.Visible = false;
		this.description_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.description_col.DataPropertyName = "description";
		this.description_col.HeaderText = "Description";
		this.description_col.Name = "description_col";
		this.description_col.ReadOnly = true;
		this.amount_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.amount_col.DataPropertyName = "amount";
		this.amount_col.HeaderText = "amount";
		this.amount_col.Name = "amount_col";
		this.amount_col.ReadOnly = true;
		this.amount_col.Width = 87;
		this.remove_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.remove_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.remove_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.remove_col.HeaderText = "";
		this.remove_col.Name = "remove_col";
		this.remove_col.ReadOnly = true;
		this.remove_col.Text = "Remove";
		this.remove_col.UseColumnTextForButtonValue = true;
		this.remove_col.Width = 5;
		this.cboFullname.BackColor = System.Drawing.Color.White;
		this.cboFullname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboFullname.FormattingEnabled = true;
		this.cboFullname.Location = new System.Drawing.Point(10, 38);
		this.cboFullname.Margin = new System.Windows.Forms.Padding(4);
		this.cboFullname.Name = "cboFullname";
		this.cboFullname.Size = new System.Drawing.Size(609, 26);
		this.cboFullname.TabIndex = 1;
		this.cboFullname.SelectedValueChanged += new System.EventHandler(cboFullname_SelectedValueChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 17);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(115, 18);
		this.label1.TabIndex = 5;
		this.label1.Text = "Search Product:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(284, 75);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(59, 18);
		this.label2.TabIndex = 9;
		this.label2.Text = "Amount";
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(287, 96);
		this.numAmount.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(151, 24);
		this.numAmount.TabIndex = 4;
		this.numAmount.ThousandsSeparator = true;
		this.numAmount.Enter += new System.EventHandler(numAmount_Enter);
		this.cboPaymentType.FormattingEnabled = true;
		this.cboPaymentType.Items.AddRange(new object[4] { "CASH", "BANK TRANSFER", "GCASH", "CHECK" });
		this.cboPaymentType.Location = new System.Drawing.Point(372, 34);
		this.cboPaymentType.Margin = new System.Windows.Forms.Padding(4);
		this.cboPaymentType.Name = "cboPaymentType";
		this.cboPaymentType.Size = new System.Drawing.Size(247, 26);
		this.cboPaymentType.TabIndex = 2;
		this.cboPaymentType.Text = "CASH";
		this.cboPaymentType.SelectedIndexChanged += new System.EventHandler(cboPaymentType_SelectedIndexChanged);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(372, 12);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(102, 18);
		this.label3.TabIndex = 9;
		this.label3.Text = "Payment Type";
		this.txtReferenceNo.Enabled = false;
		this.txtReferenceNo.Location = new System.Drawing.Point(375, 86);
		this.txtReferenceNo.Name = "txtReferenceNo";
		this.txtReferenceNo.Size = new System.Drawing.Size(244, 24);
		this.txtReferenceNo.TabIndex = 3;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(372, 65);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 18);
		this.label4.TabIndex = 9;
		this.label4.Text = "Reference No";
		this.btnAcceptPayment.Location = new System.Drawing.Point(490, 141);
		this.btnAcceptPayment.Name = "btnAcceptPayment";
		this.btnAcceptPayment.Size = new System.Drawing.Size(132, 52);
		this.btnAcceptPayment.TabIndex = 8;
		this.btnAcceptPayment.TabStop = false;
		this.btnAcceptPayment.Text = "Accept Payment";
		this.btnAcceptPayment.UseVisualStyleBackColor = true;
		this.btnAcceptPayment.Click += new System.EventHandler(btnAcceptPayment_Click);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(364, 9);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(97, 18);
		this.label5.TabIndex = 9;
		this.label5.Text = "Payment Ref.";
		this.label5.Click += new System.EventHandler(label5_Click);
		this.txtOR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtOR.Location = new System.Drawing.Point(226, 156);
		this.txtOR.Name = "txtOR";
		this.txtOR.Size = new System.Drawing.Size(161, 24);
		this.txtOR.TabIndex = 45;
		this.txtOR.TabStop = false;
		this.txtOR.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.dtpDateofPayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDateofPayment.Location = new System.Drawing.Point(6, 156);
		this.dtpDateofPayment.Name = "dtpDateofPayment";
		this.dtpDateofPayment.Size = new System.Drawing.Size(202, 24);
		this.dtpDateofPayment.TabIndex = 47;
		this.dtpDateofPayment.TabStop = false;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(2, 134);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(121, 18);
		this.label6.TabIndex = 46;
		this.label6.Text = "Date Of Payment";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(7, 64);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(246, 18);
		this.label7.TabIndex = 9;
		this.label7.Text = "Recieve Payment by(area in-charge)";
		this.cboRecieveByIncharge.FormattingEnabled = true;
		this.cboRecieveByIncharge.Location = new System.Drawing.Point(7, 86);
		this.cboRecieveByIncharge.Margin = new System.Windows.Forms.Padding(4);
		this.cboRecieveByIncharge.Name = "cboRecieveByIncharge";
		this.cboRecieveByIncharge.Size = new System.Drawing.Size(339, 26);
		this.cboRecieveByIncharge.TabIndex = 6;
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.label8);
		this.panel1.Controls.Add(this.txtPaymentNo);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 31);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(664, 38);
		this.panel1.TabIndex = 48;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label8.Location = new System.Drawing.Point(12, 9);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(168, 18);
		this.label8.TabIndex = 2;
		this.label8.Text = "Payment Information.";
		this.txtPaymentNo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtPaymentNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPaymentNo.Location = new System.Drawing.Point(489, 6);
		this.txtPaymentNo.Name = "txtPaymentNo";
		this.txtPaymentNo.ReadOnly = true;
		this.txtPaymentNo.Size = new System.Drawing.Size(161, 24);
		this.txtPaymentNo.TabIndex = 45;
		this.txtPaymentNo.TabStop = false;
		this.txtPaymentNo.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.btnAdd.Location = new System.Drawing.Point(490, 81);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(132, 52);
		this.btnAdd.TabIndex = 7;
		this.btnAdd.Text = "Add";
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(10, 72);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(93, 18);
		this.label9.TabIndex = 9;
		this.label9.Text = "Payment For";
		this.cboPaymentFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboPaymentFor.FormattingEnabled = true;
		this.cboPaymentFor.Items.AddRange(new object[4] { "RESERVED", "DOWN PAYMENT", "INSTALLMENT", "FULL PAYMENT" });
		this.cboPaymentFor.Location = new System.Drawing.Point(10, 94);
		this.cboPaymentFor.Margin = new System.Windows.Forms.Padding(4);
		this.cboPaymentFor.Name = "cboPaymentFor";
		this.cboPaymentFor.Size = new System.Drawing.Size(210, 26);
		this.cboPaymentFor.TabIndex = 5;
		this.cboPaymentFor.SelectedIndexChanged += new System.EventHandler(cboPaymentFor_SelectedIndexChanged);
		this.cboPaymentFor.Enter += new System.EventHandler(cboPaymentFor_Enter);
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(315, 413);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(335, 31);
		this.lblTotal.TabIndex = 49;
		this.lblTotal.Text = "Total Amount: 0.00";
		this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTotal.TextChanged += new System.EventHandler(lblTotal_TextChanged);
		this.lblTotal.Click += new System.EventHandler(lblTotal_Click);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.Blue;
		this.label10.Location = new System.Drawing.Point(7, 112);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(145, 18);
		this.label10.TabIndex = 9;
		this.label10.Text = "Leave it blank if none";
		this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.fileToolStripMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(664, 31);
		this.menuStrip1.TabIndex = 50;
		this.menuStrip1.Text = "menuStrip1";
		this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.SearchPaymentsToolStripMenuItem });
		this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 27);
		this.fileToolStripMenuItem.Text = "File";
		this.SearchPaymentsToolStripMenuItem.Name = "SearchPaymentsToolStripMenuItem";
		this.SearchPaymentsToolStripMenuItem.Size = new System.Drawing.Size(208, 28);
		this.SearchPaymentsToolStripMenuItem.Text = "Search Payment";
		this.SearchPaymentsToolStripMenuItem.Click += new System.EventHandler(SearchPaymentsToolStripMenuItem_Click);
		this.cboPaidBy.BackColor = System.Drawing.Color.White;
		this.cboPaidBy.FormattingEnabled = true;
		this.cboPaidBy.Location = new System.Drawing.Point(9, 34);
		this.cboPaidBy.Margin = new System.Windows.Forms.Padding(4);
		this.cboPaidBy.Name = "cboPaidBy";
		this.cboPaidBy.Size = new System.Drawing.Size(337, 26);
		this.cboPaidBy.TabIndex = 1;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(6, 14);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(62, 18);
		this.label11.TabIndex = 9;
		this.label11.Text = "Paid By:";
		this.groupBox1.Controls.Add(this.numAmount);
		this.groupBox1.Controls.Add(this.cboPaymentFor);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.btnAdd);
		this.groupBox1.Controls.Add(this.cboFullname);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 72);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(632, 141);
		this.groupBox1.TabIndex = 51;
		this.groupBox1.TabStop = false;
		this.groupBoxPayment.Controls.Add(this.dtpDateofPayment);
		this.groupBoxPayment.Controls.Add(this.txtOR);
		this.groupBoxPayment.Controls.Add(this.label12);
		this.groupBoxPayment.Controls.Add(this.label6);
		this.groupBoxPayment.Controls.Add(this.txtReferenceNo);
		this.groupBoxPayment.Controls.Add(this.cboRecieveByIncharge);
		this.groupBoxPayment.Controls.Add(this.cboPaymentType);
		this.groupBoxPayment.Controls.Add(this.label10);
		this.groupBoxPayment.Controls.Add(this.label7);
		this.groupBoxPayment.Controls.Add(this.label4);
		this.groupBoxPayment.Controls.Add(this.label11);
		this.groupBoxPayment.Controls.Add(this.label3);
		this.groupBoxPayment.Controls.Add(this.btnAcceptPayment);
		this.groupBoxPayment.Controls.Add(this.cboPaidBy);
		this.groupBoxPayment.Location = new System.Drawing.Point(12, 447);
		this.groupBoxPayment.Name = "groupBoxPayment";
		this.groupBoxPayment.Size = new System.Drawing.Size(639, 199);
		this.groupBoxPayment.TabIndex = 52;
		this.groupBoxPayment.TabStop = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(225, 135);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(88, 18);
		this.label12.TabIndex = 46;
		this.label12.Text = "OR Number";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.ClientSize = new System.Drawing.Size(664, 653);
		base.Controls.Add(this.groupBoxPayment);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.lblTotal);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.DGV);
		base.Controls.Add(this.menuStrip1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.MainMenuStrip = this.menuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmpayments";
		this.Text = "frmpayments";
		base.Load += new System.EventHandler(frmpayments_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBoxPayment.ResumeLayout(false);
		this.groupBoxPayment.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
