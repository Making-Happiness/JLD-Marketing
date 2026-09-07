using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmClaimReleaseInfo : Form
{
	public string Transaction;

	public bool CommitChanges;

	public decimal amount_to_claim;

	private IContainer components = null;

	private DateTimePicker dtpDateOfClaim;

	private Label label4;

	private Label label1;

	private TextBox txtClaimBy;

	private NumericUpDown numAmount;

	private Label label7;

	private TextBox txtremarks;

	private Label label2;

	private Button btnSave;

	private TextBox txtID;

	public Claim claim { get; set; }

	public frmClaimReleaseInfo()
	{
		InitializeComponent();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (amount_to_claim - claim.amount <= 0m)
		{
			MessageBox.Show("The amount you are trying to release is greater than the Total claims.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		using AgentsCtrl agentsCtrl = new AgentsCtrl();
		if (Transaction == "ADD" && claim.id == 0)
		{
			int num = agentsCtrl.add_claim(claim);
			if (num > 0)
			{
				claim.id = num;
				CommitChanges = true;
				Close();
			}
		}
		else
		{
			int num2 = agentsCtrl.edit_claim(claim);
			if (num2 > 0)
			{
				CommitChanges = true;
				Close();
			}
		}
	}

	private void frmClaimReleaseInfo_Load(object sender, EventArgs e)
	{
		binddata();
	}

	private void binddata()
	{
		txtID.DataBindings.Add("Text", claim, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		if (claim.dateofclaim == DateTime.MinValue)
		{
			claim.dateofclaim = DateTime.Now;
		}
		dtpDateOfClaim.DataBindings.Add("Value", claim, "dateofclaim     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged, dtpDateOfClaim.MinDate);
		txtClaimBy.DataBindings.Add("Text", claim, "claimby     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtremarks.DataBindings.Add("Text", claim, "remarks      ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", claim, "amount    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
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
		this.dtpDateOfClaim = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtClaimBy = new System.Windows.Forms.TextBox();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.txtremarks = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.txtID = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		base.SuspendLayout();
		this.dtpDateOfClaim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDateOfClaim.Location = new System.Drawing.Point(24, 41);
		this.dtpDateOfClaim.Name = "dtpDateOfClaim";
		this.dtpDateOfClaim.Size = new System.Drawing.Size(223, 24);
		this.dtpDateOfClaim.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(21, 20);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(97, 18);
		this.label4.TabIndex = 5;
		this.label4.Text = "Release Date";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(20, 74);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 18);
		this.label1.TabIndex = 7;
		this.label1.Text = "Claim By";
		this.txtClaimBy.Location = new System.Drawing.Point(24, 96);
		this.txtClaimBy.Name = "txtClaimBy";
		this.txtClaimBy.Size = new System.Drawing.Size(493, 24);
		this.txtClaimBy.TabIndex = 6;
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(22, 146);
		this.numAmount.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(223, 26);
		this.numAmount.TabIndex = 48;
		this.numAmount.ThousandsSeparator = true;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(19, 125);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(115, 18);
		this.label7.TabIndex = 47;
		this.label7.Text = "Amount to claim";
		this.txtremarks.Location = new System.Drawing.Point(24, 205);
		this.txtremarks.Multiline = true;
		this.txtremarks.Name = "txtremarks";
		this.txtremarks.Size = new System.Drawing.Size(493, 55);
		this.txtremarks.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(20, 183);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 18);
		this.label2.TabIndex = 7;
		this.label2.Text = "Remarks";
		this.btnSave.Location = new System.Drawing.Point(393, 284);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 49;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.txtID.Location = new System.Drawing.Point(458, 12);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 50;
		this.txtID.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(531, 343);
		base.Controls.Add(this.txtID);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtremarks);
		base.Controls.Add(this.txtClaimBy);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.dtpDateOfClaim);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmClaimReleaseInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Dicer Releasing Information";
		base.Load += new System.EventHandler(frmClaimReleaseInfo_Load);
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
