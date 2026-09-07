using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmApplicationForm : Form
{
	private enum messageEnum
	{
		NO_MESSAGE,
		ENTRY_REQUIRED,
		SUCCESS_INSERT,
		SUCCESS_EDIT,
		FAILED_INSERT,
		FAILED_EDIT
	}

	[CompilerGenerated]
	private static class ctor_003Eo__SiteContainer0
	{
		public static CallSite<Func<CallSite, object, Client>> _003C_003Ep__Site1;

		public static CallSite<Func<CallSite, object, Purchasedetail>> _003C_003Ep__Site2;

		public static CallSite<Func<CallSite, object, Product>> _003C_003Ep__Site3;
	}

	private int transaction = 3;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label10;

	private Label label1;

	private GroupBox groupBoxApplication;

	private ComboBox cboProducts;

	private Label label20;

	private Label label17;

	private Label label15;

	private Label label14;

	private Label label13;

	private Label label12;

	private Label label11;

	private ComboBox cboFullname;

	private Label label2;

	private Button btnProductSearch;

	private Button btnClientSearch;

	private Button btnSave;

	private Label lblMessage;

	private ComboBox cboBlockNo;

	private ComboBox cboLotNo;

	private NumericUpDown numAreasqm;

	private NumericUpDown numLotprice;

	private ComboBox cboTerms;

	private NumericUpDown numMonthlypayment;

	private ComboBox cboAgent;

	private NumericUpDown numAgentCommission;

	private Label label3;

	private Button btnCalculate;

	private NumericUpDown numpenalty;

	private NumericUpDown numotherfees;

	private Label label5;

	private Label label4;

	private DateTimePicker dtpDueDate;

	private Label label6;

	private Label label8;

	private TextBox txtRemarks;

	internal Client client { get; set; }

	internal Product product { get; set; }

	public bool commitchanges { get; set; }

	internal Purchasedetail purchasedetail { get; set; }

	public DataTable clientTable { get; set; }

	public DataTable productTable { get; set; }

	public frmApplicationForm(int trans = 3, Client _client = null, Purchasedetail _purchasedetail = null)
	{
		InitializeComponent();
		transaction = trans;
		if (_client != null)
		{
			client = (dynamic)_client;
		}
		if (_purchasedetail != null)
		{
			purchasedetail = (dynamic)_purchasedetail;
			using ProductsCtrl productsCtrl = new ProductsCtrl();
			DataTable tablebyId = productsCtrl.getTablebyId(purchasedetail.idproducts);
			if (tablebyId.Rows.Count > 0)
			{
				product = (dynamic)AController.DataRowToClass<Product>(tablebyId.Rows[0]);
			}
		}
		LoadBlockandLotNumber(100, 100);
	}

	private void LoadBlockandLotNumber(int MaxBlockNo, int MaxLotNo)
	{
		string[] array = new string[MaxBlockNo];
		for (int i = 0; i < MaxBlockNo; i++)
		{
			array[i] = string.Concat(i + 1);
		}
		cboBlockNo.Items.Clear();
		cboBlockNo.Items.AddRange(array);
		array = new string[MaxLotNo];
		for (int i = 0; i < MaxLotNo; i++)
		{
			array[i] = string.Concat(i + 1);
		}
		cboLotNo.Items.Clear();
		cboLotNo.Items.AddRange(array);
	}

	private void bindDataClient()
	{
		cboFullname.Text = $"{client.lastname}, {client.firstname} {client.middlename}".ToString().ToUpper();
	}

	private void bindDataProducts()
	{
		if (cboProducts.SelectedIndex > -1)
		{
			if (product.idproduct > 0)
			{
				cboProducts.Text = product.location;
			}
			else
			{
				product = AController.DataRowToClass<Product>(((DataRowView)cboProducts.SelectedItem).Row);
			}
		}
		else if (product.idproduct > 0)
		{
			cboProducts.Text = product.location;
		}
	}

	private void bindDataPurchasedetails()
	{
		cboBlockNo.DataBindings.Add("Text", purchasedetail, "blockno", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboLotNo.DataBindings.Add("Text", purchasedetail, "lotno", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAreasqm.DataBindings.Add("Value", purchasedetail, "area", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numLotprice.DataBindings.Add("Value", purchasedetail, "lotprice", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboTerms.DataBindings.Add("Text", purchasedetail, "terms", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numMonthlypayment.DataBindings.Add("Text", purchasedetail, "amortization", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboAgent.DataBindings.Add("SelectedValue", purchasedetail, "idagent", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAgentCommission.DataBindings.Add("Value", purchasedetail, "agentpercentage", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numotherfees.DataBindings.Add("Value", purchasedetail, "otherfees", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numpenalty.DataBindings.Add("Value", purchasedetail, "penalty", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		purchasedetail.duedate = (object)((purchasedetail.duedate == null) ? ((object)dtpDueDate.MinDate) : purchasedetail.duedate);
		dtpDueDate.DataBindings.Add("Value", purchasedetail, "duedate", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", purchasedetail, "remarks", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void numericUpDown1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void frmApplicationForm_Load(object sender, EventArgs e)
	{
		if (transaction == 1)
		{
			setComboBoxReadOnly(cboFullname, value: true);
			using ProductsCtrl productsCtrl = new ProductsCtrl();
			if (productTable == null)
			{
				productTable = productsCtrl.getTable();
			}
			cboProducts.DataSource = productTable;
			cboProducts.DisplayMember = "location";
			cboProducts.AutoCompleteMode = AutoCompleteMode.Suggest;
			cboProducts.AutoCompleteSource = AutoCompleteSource.ListItems;
			if (product == null)
			{
				cboProducts.SelectedIndex = -1;
			}
		}
		else if (transaction == 2)
		{
			using ClientsCtrl clientsCtrl = new ClientsCtrl();
			setComboBoxReadOnly(cboProducts, value: true);
			if (clientTable == null)
			{
				clientTable = clientsCtrl.getcustomTable("clientswithfullname");
			}
			cboFullname.DataSource = clientTable;
			cboFullname.DisplayMember = "fullname";
			cboFullname.AutoCompleteMode = AutoCompleteMode.Suggest;
			cboFullname.AutoCompleteSource = AutoCompleteSource.ListItems;
			if (client == null)
			{
				cboFullname.SelectedIndex = -1;
			}
		}
		else if (transaction == 3)
		{
			setComboBoxReadOnly(cboFullname, value: true);
			setComboBoxReadOnly(cboProducts, value: true);
		}
		dgvAgent_Refresh();
		bindDataClient();
		bindDataProducts();
		if (purchasedetail == null)
		{
			purchasedetail = new Purchasedetail();
			cboAgent.SelectedIndex = -1;
		}
		bindDataPurchasedetails();
	}

	private void dgvAgent_Refresh()
	{
		cboAgent.DataSource = Program.fmain.view_agentsTable;
		cboAgent.DisplayMember = "fullname";
		cboAgent.ValueMember = "id";
		cboAgent.AutoCompleteMode = AutoCompleteMode.Suggest;
		cboAgent.AutoCompleteSource = AutoCompleteSource.ListItems;
	}

	internal void setComboBoxReadOnly(ComboBox cbo, bool value)
	{
		if (value)
		{
			cbo.FlatStyle = FlatStyle.Flat;
			cbo.Enabled = false;
			cbo.DropDownStyle = ComboBoxStyle.Simple;
		}
		else
		{
			cbo.FlatStyle = FlatStyle.Standard;
			cbo.Enabled = false;
			cbo.DropDownStyle = ComboBoxStyle.DropDown;
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!checkrequiredfileds())
		{
			return;
		}
		using PurchasedetailsCtrl purchasedetailsCtrl = new PurchasedetailsCtrl();
		purchasedetail.idclients = client.idclients;
		purchasedetail.idproducts = product.idproduct;
		purchasedetail.duedate = (object)((purchasedetail.duedate <= dtpDueDate.MinDate) ? null : purchasedetail.duedate);
		int num;
		if (purchasedetail.id > 0)
		{
			num = purchasedetailsCtrl.edit(purchasedetail);
			if (num > 0)
			{
				showmessage(messageEnum.SUCCESS_EDIT);
				commitchanges = true;
			}
			else
			{
				showmessage(messageEnum.FAILED_EDIT);
			}
			return;
		}
		num = purchasedetailsCtrl.add(purchasedetail);
		if (num > 0)
		{
			purchasedetail.id = num;
			showmessage(messageEnum.SUCCESS_INSERT);
			commitchanges = true;
		}
		else
		{
			showmessage(messageEnum.FAILED_INSERT);
		}
	}

	private bool checkrequiredfileds()
	{
		int num = 0;
		foreach (object control in groupBoxApplication.Controls)
		{
			if (control.GetType().Name.ToString() == "ComboBox")
			{
				dynamic val = (ComboBox)control;
				bool flag = val.Name == "cboAgent";
				if (val.Text == "" && !flag)
				{
					val.DroppedDown = true;
					showmessage(messageEnum.ENTRY_REQUIRED);
					num++;
					break;
				}
			}
			else if (control.GetType().Name.ToString() == "NumericUpDown")
			{
				dynamic val = (NumericUpDown)control;
				if (val.Value < 0)
				{
					val.Focus();
					showmessage(messageEnum.ENTRY_REQUIRED);
					num++;
					break;
				}
			}
			else if (control.GetType().Name.ToString() == "TextBox")
			{
				((TextBox)control).Focus();
				dynamic val = (TextBox)control;
				if (val.Text == "")
				{
					val.Focus();
					showmessage(messageEnum.ENTRY_REQUIRED);
					num++;
					break;
				}
			}
		}
		if (num <= 0)
		{
			showmessage(messageEnum.NO_MESSAGE);
			return true;
		}
		return false;
	}

	private void showmessage(messageEnum status)
	{
		switch (status)
		{
		case messageEnum.ENTRY_REQUIRED:
			lblMessage.ForeColor = Color.Red;
			lblMessage.Text = $"Message: \n\n\t Entry Required...";
			break;
		case messageEnum.SUCCESS_INSERT:
			lblMessage.ForeColor = Color.Green;
			lblMessage.Text = $"Message: \n\n\t New Entry Successfully Saved.";
			break;
		case messageEnum.SUCCESS_EDIT:
			lblMessage.ForeColor = Color.Green;
			lblMessage.Text = $"Message: \n\n\t Changes Successfully Saved.";
			break;
		case messageEnum.FAILED_INSERT:
			lblMessage.ForeColor = Color.Red;
			lblMessage.Text = $"Message: \n\n\t New Entry Failed to Saved.";
			break;
		case messageEnum.FAILED_EDIT:
			lblMessage.ForeColor = Color.Red;
			lblMessage.Text = $"Message: \n\n\t Changes Failed to Saved.";
			break;
		default:
			lblMessage.ForeColor = Color.Green;
			lblMessage.Text = "Message:";
			break;
		}
	}

	private void cboProducts_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cboProducts.SelectedItem != null)
		{
			product = AController.DataRowToClass<Product>(((DataRowView)cboProducts.SelectedItem).Row);
			LoadBlockandLotNumber(product.totalblockno, product.totallotno);
		}
	}

	private void cboTerms_SelectedIndexChanged(object sender, EventArgs e)
	{
		updateMonthlyPayment();
	}

	private void updateMonthlyPayment()
	{
		decimal downpayment = 0m;
		numMonthlypayment.Value = purchasedetail.getAmortization(downpayment);
	}

	private void btnCalculate_Click(object sender, EventArgs e)
	{
		using frmamortizationcalculator frmamortizationcalculator2 = new frmamortizationcalculator();
		frmamortizationcalculator2.price = numLotprice.Value;
		frmamortizationcalculator2.terms = Convert.ToInt32(cboTerms.Text);
		frmamortizationcalculator2.dp = 0.00m;
		frmamortizationcalculator2.Result = numMonthlypayment.Value;
		frmamortizationcalculator2.ShowDialog(this);
		numMonthlypayment.Value = frmamortizationcalculator2.Result;
		cboTerms.Text = string.Concat(frmamortizationcalculator2.terms);
		numLotprice.Value = frmamortizationcalculator2.price;
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnProductSearch = new System.Windows.Forms.Button();
		this.btnClientSearch = new System.Windows.Forms.Button();
		this.cboFullname = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cboProducts = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBoxApplication = new System.Windows.Forms.GroupBox();
		this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
		this.btnCalculate = new System.Windows.Forms.Button();
		this.cboBlockNo = new System.Windows.Forms.ComboBox();
		this.cboLotNo = new System.Windows.Forms.ComboBox();
		this.numpenalty = new System.Windows.Forms.NumericUpDown();
		this.numotherfees = new System.Windows.Forms.NumericUpDown();
		this.numAreasqm = new System.Windows.Forms.NumericUpDown();
		this.numAgentCommission = new System.Windows.Forms.NumericUpDown();
		this.numLotprice = new System.Windows.Forms.NumericUpDown();
		this.cboTerms = new System.Windows.Forms.ComboBox();
		this.numMonthlypayment = new System.Windows.Forms.NumericUpDown();
		this.cboAgent = new System.Windows.Forms.ComboBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.lblMessage = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		this.groupBoxApplication.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numpenalty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numotherfees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numAreasqm).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numAgentCommission).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numLotprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numMonthlypayment).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnProductSearch);
		this.groupBox1.Controls.Add(this.btnClientSearch);
		this.groupBox1.Controls.Add(this.cboFullname);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cboProducts);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox1.Size = new System.Drawing.Size(755, 178);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Client Information";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.btnProductSearch.Location = new System.Drawing.Point(649, 118);
		this.btnProductSearch.Name = "btnProductSearch";
		this.btnProductSearch.Size = new System.Drawing.Size(75, 26);
		this.btnProductSearch.TabIndex = 4;
		this.btnProductSearch.Text = "search";
		this.btnProductSearch.UseVisualStyleBackColor = true;
		this.btnProductSearch.Visible = false;
		this.btnClientSearch.Location = new System.Drawing.Point(649, 61);
		this.btnClientSearch.Name = "btnClientSearch";
		this.btnClientSearch.Size = new System.Drawing.Size(75, 26);
		this.btnClientSearch.TabIndex = 4;
		this.btnClientSearch.Text = "search";
		this.btnClientSearch.UseVisualStyleBackColor = true;
		this.cboFullname.BackColor = System.Drawing.Color.White;
		this.cboFullname.FormattingEnabled = true;
		this.cboFullname.Location = new System.Drawing.Point(38, 61);
		this.cboFullname.Margin = new System.Windows.Forms.Padding(4);
		this.cboFullname.Name = "cboFullname";
		this.cboFullname.Size = new System.Drawing.Size(604, 26);
		this.cboFullname.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(31, 39);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(75, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "Full Name";
		this.cboProducts.FormattingEnabled = true;
		this.cboProducts.Location = new System.Drawing.Point(38, 118);
		this.cboProducts.Margin = new System.Windows.Forms.Padding(4);
		this.cboProducts.Name = "cboProducts";
		this.cboProducts.Size = new System.Drawing.Size(604, 26);
		this.cboProducts.TabIndex = 3;
		this.cboProducts.SelectedIndexChanged += new System.EventHandler(cboProducts_SelectedIndexChanged);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(29, 96);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 18);
		this.label11.TabIndex = 1;
		this.label11.Text = "Location";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(492, 98);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(84, 18);
		this.label10.TabIndex = 1;
		this.label10.Text = "Agent/Dicer";
		this.groupBoxApplication.Controls.Add(this.dtpDueDate);
		this.groupBoxApplication.Controls.Add(this.btnCalculate);
		this.groupBoxApplication.Controls.Add(this.cboBlockNo);
		this.groupBoxApplication.Controls.Add(this.cboLotNo);
		this.groupBoxApplication.Controls.Add(this.numpenalty);
		this.groupBoxApplication.Controls.Add(this.numotherfees);
		this.groupBoxApplication.Controls.Add(this.numAreasqm);
		this.groupBoxApplication.Controls.Add(this.numAgentCommission);
		this.groupBoxApplication.Controls.Add(this.numLotprice);
		this.groupBoxApplication.Controls.Add(this.cboTerms);
		this.groupBoxApplication.Controls.Add(this.numMonthlypayment);
		this.groupBoxApplication.Controls.Add(this.cboAgent);
		this.groupBoxApplication.Controls.Add(this.label10);
		this.groupBoxApplication.Controls.Add(this.label20);
		this.groupBoxApplication.Controls.Add(this.label3);
		this.groupBoxApplication.Controls.Add(this.label2);
		this.groupBoxApplication.Controls.Add(this.label6);
		this.groupBoxApplication.Controls.Add(this.label5);
		this.groupBoxApplication.Controls.Add(this.label17);
		this.groupBoxApplication.Controls.Add(this.label4);
		this.groupBoxApplication.Controls.Add(this.label15);
		this.groupBoxApplication.Controls.Add(this.label14);
		this.groupBoxApplication.Controls.Add(this.label13);
		this.groupBoxApplication.Controls.Add(this.label12);
		this.groupBoxApplication.Location = new System.Drawing.Point(13, 199);
		this.groupBoxApplication.Margin = new System.Windows.Forms.Padding(4);
		this.groupBoxApplication.Name = "groupBoxApplication";
		this.groupBoxApplication.Padding = new System.Windows.Forms.Padding(4);
		this.groupBoxApplication.Size = new System.Drawing.Size(756, 276);
		this.groupBoxApplication.TabIndex = 1;
		this.groupBoxApplication.TabStop = false;
		this.groupBoxApplication.Text = "Application Information";
		this.dtpDueDate.CustomFormat = "";
		this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDueDate.Location = new System.Drawing.Point(524, 245);
		this.dtpDueDate.Name = "dtpDueDate";
		this.dtpDueDate.Size = new System.Drawing.Size(200, 24);
		this.dtpDueDate.TabIndex = 49;
		this.btnCalculate.Location = new System.Drawing.Point(414, 180);
		this.btnCalculate.Name = "btnCalculate";
		this.btnCalculate.Size = new System.Drawing.Size(75, 23);
		this.btnCalculate.TabIndex = 48;
		this.btnCalculate.Text = "calculate";
		this.btnCalculate.UseVisualStyleBackColor = true;
		this.btnCalculate.Click += new System.EventHandler(btnCalculate_Click);
		this.cboBlockNo.FormattingEnabled = true;
		this.cboBlockNo.Location = new System.Drawing.Point(31, 67);
		this.cboBlockNo.Margin = new System.Windows.Forms.Padding(4);
		this.cboBlockNo.Name = "cboBlockNo";
		this.cboBlockNo.Size = new System.Drawing.Size(224, 26);
		this.cboBlockNo.TabIndex = 47;
		this.cboLotNo.FormattingEnabled = true;
		this.cboLotNo.Location = new System.Drawing.Point(265, 66);
		this.cboLotNo.Margin = new System.Windows.Forms.Padding(4);
		this.cboLotNo.Name = "cboLotNo";
		this.cboLotNo.Size = new System.Drawing.Size(224, 26);
		this.cboLotNo.TabIndex = 46;
		this.numpenalty.DecimalPlaces = 2;
		this.numpenalty.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numpenalty.Location = new System.Drawing.Point(268, 245);
		this.numpenalty.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numpenalty.Name = "numpenalty";
		this.numpenalty.Size = new System.Drawing.Size(221, 24);
		this.numpenalty.TabIndex = 45;
		this.numpenalty.ThousandsSeparator = true;
		this.numotherfees.DecimalPlaces = 2;
		this.numotherfees.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numotherfees.Location = new System.Drawing.Point(34, 245);
		this.numotherfees.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numotherfees.Name = "numotherfees";
		this.numotherfees.Size = new System.Drawing.Size(221, 24);
		this.numotherfees.TabIndex = 45;
		this.numotherfees.ThousandsSeparator = true;
		this.numAreasqm.DecimalPlaces = 2;
		this.numAreasqm.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAreasqm.Location = new System.Drawing.Point(495, 67);
		this.numAreasqm.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numAreasqm.Name = "numAreasqm";
		this.numAreasqm.Size = new System.Drawing.Size(221, 24);
		this.numAreasqm.TabIndex = 45;
		this.numAreasqm.ThousandsSeparator = true;
		this.numAgentCommission.DecimalPlaces = 2;
		this.numAgentCommission.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAgentCommission.Location = new System.Drawing.Point(34, 179);
		this.numAgentCommission.Maximum = new decimal(new int[4] { 30, 0, 0, 0 });
		this.numAgentCommission.Name = "numAgentCommission";
		this.numAgentCommission.Size = new System.Drawing.Size(87, 24);
		this.numAgentCommission.TabIndex = 44;
		this.numLotprice.DecimalPlaces = 2;
		this.numLotprice.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numLotprice.Location = new System.Drawing.Point(31, 120);
		this.numLotprice.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numLotprice.Name = "numLotprice";
		this.numLotprice.Size = new System.Drawing.Size(224, 24);
		this.numLotprice.TabIndex = 44;
		this.numLotprice.ThousandsSeparator = true;
		this.cboTerms.FormattingEnabled = true;
		this.cboTerms.Items.AddRange(new object[11]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.cboTerms.Location = new System.Drawing.Point(262, 119);
		this.cboTerms.Margin = new System.Windows.Forms.Padding(4);
		this.cboTerms.Name = "cboTerms";
		this.cboTerms.Size = new System.Drawing.Size(221, 26);
		this.cboTerms.TabIndex = 42;
		this.cboTerms.SelectedIndexChanged += new System.EventHandler(cboTerms_SelectedIndexChanged);
		this.numMonthlypayment.DecimalPlaces = 2;
		this.numMonthlypayment.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numMonthlypayment.Location = new System.Drawing.Point(184, 179);
		this.numMonthlypayment.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numMonthlypayment.Name = "numMonthlypayment";
		this.numMonthlypayment.Size = new System.Drawing.Size(224, 24);
		this.numMonthlypayment.TabIndex = 41;
		this.numMonthlypayment.ThousandsSeparator = true;
		this.cboAgent.FormattingEnabled = true;
		this.cboAgent.Items.AddRange(new object[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
		this.cboAgent.Location = new System.Drawing.Point(495, 120);
		this.cboAgent.Margin = new System.Windows.Forms.Padding(4);
		this.cboAgent.Name = "cboAgent";
		this.cboAgent.Size = new System.Drawing.Size(224, 26);
		this.cboAgent.TabIndex = 40;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(259, 96);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(115, 18);
		this.label20.TabIndex = 1;
		this.label20.Text = "Terms [0=Cash]";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(128, 184);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(21, 18);
		this.label3.TabIndex = 1;
		this.label3.Text = "%";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(29, 154);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(126, 18);
		this.label2.TabIndex = 1;
		this.label2.Text = "Agent Commision";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(521, 224);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 18);
		this.label6.TabIndex = 1;
		this.label6.Text = "Due Date";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(263, 221);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(56, 18);
		this.label5.TabIndex = 1;
		this.label5.Text = "Penalty";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(182, 158);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(122, 18);
		this.label17.TabIndex = 1;
		this.label17.Text = "Monthly Payment";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(29, 221);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(152, 18);
		this.label4.TabIndex = 1;
		this.label4.Text = "Processing/Docs Fee";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(28, 98);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(67, 18);
		this.label15.TabIndex = 1;
		this.label15.Text = "Lot Price";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(490, 43);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(89, 18);
		this.label14.TabIndex = 1;
		this.label14.Text = "Area (sq.m.)";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(259, 44);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(53, 18);
		this.label13.TabIndex = 1;
		this.label13.Text = "Lot No";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(28, 43);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(70, 18);
		this.label12.TabIndex = 1;
		this.label12.Text = "Block No";
		this.btnSave.Location = new System.Drawing.Point(658, 616);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(110, 34);
		this.btnSave.TabIndex = 5;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMessage.Location = new System.Drawing.Point(21, 598);
		this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(296, 52);
		this.lblMessage.TabIndex = 9;
		this.lblMessage.Text = "Message:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(48, 479);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 49;
		this.label8.Text = "Remarks";
		this.txtRemarks.Location = new System.Drawing.Point(124, 481);
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(378, 100);
		this.txtRemarks.TabIndex = 48;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(782, 659);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.txtRemarks);
		base.Controls.Add(this.lblMessage);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.groupBoxApplication);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmApplicationForm";
		this.Text = "frmApplicationForm";
		base.Load += new System.EventHandler(frmApplicationForm_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBoxApplication.ResumeLayout(false);
		this.groupBoxApplication.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numpenalty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numotherfees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numAreasqm).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numAgentCommission).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numLotprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numMonthlypayment).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
