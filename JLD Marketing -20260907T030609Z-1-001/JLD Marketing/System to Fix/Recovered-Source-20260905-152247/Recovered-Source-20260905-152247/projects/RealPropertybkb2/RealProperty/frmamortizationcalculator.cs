using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BEL;

namespace RealProperty;

public class frmamortizationcalculator : Form
{
	private bool done_startup = false;

	private IContainer components = null;

	private NumericUpDown numDownpayment;

	private Label label16;

	private NumericUpDown numLotprice;

	private ComboBox cboTerms;

	private Label label20;

	private Label label15;

	private TextBox txt_estMonthlyFee;

	private Label label1;

	private Button btnOK;

	private Button btnCancel;

	public decimal Result { get; set; }

	public decimal price { get; set; }

	public int terms { get; set; }

	public decimal dp { get; set; }

	public frmamortizationcalculator()
	{
		InitializeComponent();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Result = Convert.ToDecimal(txt_estMonthlyFee.Text);
		price = numLotprice.Value;
		terms = Convert.ToInt32(cboTerms.Text);
		dp = numDownpayment.Value;
		Close();
	}

	private void numLotprice_ValueChanged(object sender, EventArgs e)
	{
		calculate();
	}

	private void calculate()
	{
		if (done_startup)
		{
			decimal value = numLotprice.Value;
			int num = Convert.ToInt32(cboTerms.Text);
			decimal value2 = numDownpayment.Value;
			txt_estMonthlyFee.Text = new Purchasedetail
			{
				lotprice = value,
				terms = num
			}.getAmortization(value2).ToString("#,###.00");
		}
	}

	private void cboTerms_SelectedIndexChanged(object sender, EventArgs e)
	{
		calculate();
	}

	private void numDownpayment_ValueChanged(object sender, EventArgs e)
	{
		calculate();
	}

	private void frmamortizationcalculator_Load(object sender, EventArgs e)
	{
		startup();
	}

	private void startup()
	{
		binddata();
	}

	private void binddata()
	{
		numLotprice.Value = price;
		cboTerms.Text = string.Concat(terms);
		numDownpayment.Value = dp;
		done_startup = true;
		calculate();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void numLotprice_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void numDownpayment_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void num_MouseUp(object sender, MouseEventArgs e)
	{
		NumericUpDown numericUpDown = sender as NumericUpDown;
		numericUpDown.Select(0, (numericUpDown.Text ?? "").Length);
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
		this.numDownpayment = new System.Windows.Forms.NumericUpDown();
		this.label16 = new System.Windows.Forms.Label();
		this.numLotprice = new System.Windows.Forms.NumericUpDown();
		this.cboTerms = new System.Windows.Forms.ComboBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txt_estMonthlyFee = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.numDownpayment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numLotprice).BeginInit();
		base.SuspendLayout();
		this.numDownpayment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.numDownpayment.DecimalPlaces = 2;
		this.numDownpayment.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numDownpayment.Location = new System.Drawing.Point(32, 151);
		this.numDownpayment.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numDownpayment.Name = "numDownpayment";
		this.numDownpayment.Size = new System.Drawing.Size(297, 24);
		this.numDownpayment.TabIndex = 45;
		this.numDownpayment.ThousandsSeparator = true;
		this.numDownpayment.Value = new decimal(new int[4] { 20000, 0, 0, 0 });
		this.numDownpayment.ValueChanged += new System.EventHandler(numDownpayment_ValueChanged);
		this.numDownpayment.KeyUp += new System.Windows.Forms.KeyEventHandler(numDownpayment_KeyUp);
		this.numDownpayment.MouseUp += new System.Windows.Forms.MouseEventHandler(num_MouseUp);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(26, 130);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(173, 18);
		this.label16.TabIndex = 44;
		this.label16.Text = "Estimated Downpayment";
		this.numLotprice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.numLotprice.DecimalPlaces = 2;
		this.numLotprice.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numLotprice.Location = new System.Drawing.Point(32, 29);
		this.numLotprice.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numLotprice.Name = "numLotprice";
		this.numLotprice.Size = new System.Drawing.Size(298, 24);
		this.numLotprice.TabIndex = 49;
		this.numLotprice.ThousandsSeparator = true;
		this.numLotprice.ValueChanged += new System.EventHandler(numLotprice_ValueChanged);
		this.numLotprice.KeyUp += new System.Windows.Forms.KeyEventHandler(numLotprice_KeyUp);
		this.numLotprice.MouseUp += new System.Windows.Forms.MouseEventHandler(num_MouseUp);
		this.cboTerms.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTerms.FormattingEnabled = true;
		this.cboTerms.Items.AddRange(new object[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
		this.cboTerms.Location = new System.Drawing.Point(32, 92);
		this.cboTerms.Margin = new System.Windows.Forms.Padding(4);
		this.cboTerms.Name = "cboTerms";
		this.cboTerms.Size = new System.Drawing.Size(295, 26);
		this.cboTerms.TabIndex = 48;
		this.cboTerms.SelectedIndexChanged += new System.EventHandler(cboTerms_SelectedIndexChanged);
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(29, 69);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(51, 18);
		this.label20.TabIndex = 46;
		this.label20.Text = "Terms";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(29, 7);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(67, 18);
		this.label15.TabIndex = 47;
		this.label15.Text = "Lot Price";
		this.txt_estMonthlyFee.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txt_estMonthlyFee.Location = new System.Drawing.Point(32, 214);
		this.txt_estMonthlyFee.Name = "txt_estMonthlyFee";
		this.txt_estMonthlyFee.ReadOnly = true;
		this.txt_estMonthlyFee.Size = new System.Drawing.Size(297, 24);
		this.txt_estMonthlyFee.TabIndex = 50;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(29, 193);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(151, 18);
		this.label1.TabIndex = 44;
		this.label1.Text = "Estimated Mothly Fee";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.Location = new System.Drawing.Point(242, 252);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(85, 50);
		this.btnOK.TabIndex = 51;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancel.Location = new System.Drawing.Point(151, 252);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(85, 50);
		this.btnCancel.TabIndex = 51;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(355, 314);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.txt_estMonthlyFee);
		base.Controls.Add(this.numLotprice);
		base.Controls.Add(this.cboTerms);
		base.Controls.Add(this.label20);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.numDownpayment);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label16);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmamortizationcalculator";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Amortization Calculator";
		base.Load += new System.EventHandler(frmamortizationcalculator_Load);
		((System.ComponentModel.ISupportInitialize)this.numDownpayment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numLotprice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
