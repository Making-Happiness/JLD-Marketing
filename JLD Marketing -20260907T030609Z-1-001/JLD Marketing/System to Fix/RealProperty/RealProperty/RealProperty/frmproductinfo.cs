using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmproductinfo : Form
{
	public Product Product;

	public bool commitchanges = false;

	private IContainer components = null;

	private GroupBox groupBox2;

	private TextBox txtLocation;

	private Label label15;

	private Label label14;

	private Label label13;

	private Label label12;

	private Label label11;

	private Button btnSave;

	private TextBox txtidProduct;

	private TextBox txtcode;

	private Label label1;

	private Label lblMessage;

	private NumericUpDown numArea;

	private NumericUpDown numCashprice;

	private NumericUpDown numLotno;

	private NumericUpDown numBlockno;

	public frmproductinfo(Product product = null)
	{
		InitializeComponent();
		lblMessage.Font = new Font(Font.FontFamily, Font.Size + 1f);
		if (product != null)
		{
			Product = product;
		}
		else
		{
			Product = new Product();
			Product.location = "Sample";
		}
		bindData();
	}

	private void bindData()
	{
		txtidProduct.DataBindings.Add("Text", Product, "idproduct", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtcode.DataBindings.Add("Text", Product, "code", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtLocation.DataBindings.Add("Text", Product, "location", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numBlockno.DataBindings.Add("Value", Product, "totalblockno", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numLotno.DataBindings.Add("Value", Product, "totallotno", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numArea.DataBindings.Add("Value", Product, "totalarea", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numCashprice.DataBindings.Add("Value", Product, "cashprice", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Product product = Product;
		if (product.idproduct == 0)
		{
			using (ProductsCtrl newProductsCtrl = new ProductsCtrl())
			{
				int newProductId = newProductsCtrl.add(product);
				if (newProductId > 0)
				{
					Product.idproduct = newProductId;
					txtidProduct.Text = string.Concat(newProductId);
					commitchanges = true;
					lblMessage.Text = "Message:\nNew record successfully saved.";
				}
				else
				{
					lblMessage.Text = "Message:\nNew Record not saved.";
				}
				return;
			}
		}
		using ProductsCtrl productsCtrl = new ProductsCtrl();
		int num = productsCtrl.edit(product);
		if (num > 0)
		{
			commitchanges = true;
			lblMessage.Text = "Message:\nChanges successfully saved.";
		}
		else
		{
			lblMessage.Text = "Message:\nChanges not saved.";
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
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.numArea = new System.Windows.Forms.NumericUpDown();
		this.numLotno = new System.Windows.Forms.NumericUpDown();
		this.numBlockno = new System.Windows.Forms.NumericUpDown();
		this.numCashprice = new System.Windows.Forms.NumericUpDown();
		this.txtLocation = new System.Windows.Forms.TextBox();
		this.txtidProduct = new System.Windows.Forms.TextBox();
		this.txtcode = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.lblMessage = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numLotno).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numBlockno).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numCashprice).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.numArea);
		this.groupBox2.Controls.Add(this.numLotno);
		this.groupBox2.Controls.Add(this.numBlockno);
		this.groupBox2.Controls.Add(this.numCashprice);
		this.groupBox2.Controls.Add(this.txtLocation);
		this.groupBox2.Controls.Add(this.txtidProduct);
		this.groupBox2.Controls.Add(this.txtcode);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Location = new System.Drawing.Point(15, 15);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox2.Size = new System.Drawing.Size(850, 302);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Property Information";
		this.numArea.DecimalPlaces = 2;
		this.numArea.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numArea.Location = new System.Drawing.Point(544, 175);
		this.numArea.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numArea.Name = "numArea";
		this.numArea.Size = new System.Drawing.Size(252, 24);
		this.numArea.TabIndex = 2;
		this.numLotno.Location = new System.Drawing.Point(280, 175);
		this.numLotno.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.numLotno.Name = "numLotno";
		this.numLotno.Size = new System.Drawing.Size(252, 24);
		this.numLotno.TabIndex = 2;
		this.numBlockno.Location = new System.Drawing.Point(22, 175);
		this.numBlockno.Name = "numBlockno";
		this.numBlockno.Size = new System.Drawing.Size(252, 24);
		this.numBlockno.TabIndex = 2;
		this.numCashprice.DecimalPlaces = 2;
		this.numCashprice.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numCashprice.Location = new System.Drawing.Point(22, 230);
		this.numCashprice.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numCashprice.Name = "numCashprice";
		this.numCashprice.Size = new System.Drawing.Size(252, 24);
		this.numCashprice.TabIndex = 2;
		this.txtLocation.Location = new System.Drawing.Point(25, 114);
		this.txtLocation.Margin = new System.Windows.Forms.Padding(4);
		this.txtLocation.Name = "txtLocation";
		this.txtLocation.Size = new System.Drawing.Size(771, 24);
		this.txtLocation.TabIndex = 0;
		this.txtidProduct.Location = new System.Drawing.Point(742, 34);
		this.txtidProduct.Margin = new System.Windows.Forms.Padding(4);
		this.txtidProduct.Name = "txtidProduct";
		this.txtidProduct.ReadOnly = true;
		this.txtidProduct.Size = new System.Drawing.Size(96, 24);
		this.txtidProduct.TabIndex = 0;
		this.txtcode.Location = new System.Drawing.Point(26, 61);
		this.txtcode.Margin = new System.Windows.Forms.Padding(4);
		this.txtcode.Name = "txtcode";
		this.txtcode.Size = new System.Drawing.Size(248, 24);
		this.txtcode.TabIndex = 0;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(21, 209);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(198, 18);
		this.label15.TabIndex = 1;
		this.label15.Text = "Spot/Cash Price per 100sqm";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(541, 151);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(126, 18);
		this.label14.TabIndex = 1;
		this.label14.Text = "Total Area (sq.m.)";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(281, 152);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(66, 18);
		this.label13.TabIndex = 1;
		this.label13.Text = "Total Lot";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(21, 151);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(83, 18);
		this.label12.TabIndex = 1;
		this.label12.Text = "Total Block";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(21, 37);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(44, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "Code";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(21, 90);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(123, 18);
		this.label11.TabIndex = 1;
		this.label11.Text = "Location/Address";
		this.btnSave.Location = new System.Drawing.Point(757, 348);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(107, 44);
		this.btnSave.TabIndex = 3;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.lblMessage.AutoSize = true;
		this.lblMessage.Location = new System.Drawing.Point(40, 332);
		this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(73, 18);
		this.lblMessage.TabIndex = 1;
		this.lblMessage.Text = "Message:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(878, 416);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.lblMessage);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmproductinfo";
		this.Text = "Product Information";
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numLotno).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numBlockno).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numCashprice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
