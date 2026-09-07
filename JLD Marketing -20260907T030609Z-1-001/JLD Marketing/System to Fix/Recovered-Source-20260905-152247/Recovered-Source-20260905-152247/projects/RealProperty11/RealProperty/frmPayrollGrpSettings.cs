using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPayrollGrpSettings : Form
{
	private PayrollCtrl controller;

	private IContainer components = null;

	private TextBox txtDescription;

	private Label label1;

	private ComboBox cboCategory;

	private Label label3;

	private Label label8;

	private TextBox txtRemarks;

	private ComboBox cbRecordStatus;

	private Label label11;

	private Button btnSave;

	private TextBox txtID;

	private NumericUpDown numAmount;

	private Label label7;

	public string Tansaction { get; set; }

	public bool commitcahnges { get; set; }

	public Payrollsettings payrollsettings { get; set; }

	public frmPayrollGrpSettings(Payrollsettings payrollsettings)
	{
		InitializeComponent();
		controller = new PayrollCtrl();
		this.payrollsettings = payrollsettings;
	}

	private void frmPayrollGrpSettings_Load(object sender, EventArgs e)
	{
		loadCategoryCookies();
		BindData();
	}

	private void BindData()
	{
		txtID.DataBindings.Add("Text", payrollsettings, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtDescription.DataBindings.Add("Text", payrollsettings, "description     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboCategory.DataBindings.Add("Text", payrollsettings, "category        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", payrollsettings, "amount   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", payrollsettings, "remarks    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		if (payrollsettings.recordstatus == "deleted")
		{
			cbRecordStatus.Items.Add("deleted");
		}
		cbRecordStatus.DataBindings.Add("Text", payrollsettings, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void loadCategoryCookies()
	{
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (payrollsettings.id <= 0 && Tansaction == "ADD")
		{
			payrollsettings.daterecorded = DateTime.Now;
			int num = controller.add(payrollsettings);
			if (num > 0)
			{
				payrollsettings.id = num;
				commitcahnges = true;
				Close();
			}
			else
			{
				commitcahnges = false;
				MessageBox.Show("Record failed to add record.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else if (payrollsettings.id > 0)
		{
			int num = controller.edit(payrollsettings);
			if (num > 0)
			{
				commitcahnges = true;
				Close();
			}
			else
			{
				commitcahnges = false;
				MessageBox.Show("Record failed to edit record.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
		this.txtDescription = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cboCategory = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.txtID = new System.Windows.Forms.TextBox();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		base.SuspendLayout();
		this.txtDescription.Location = new System.Drawing.Point(12, 30);
		this.txtDescription.MaxLength = 45;
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(440, 24);
		this.txtDescription.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "Description";
		this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboCategory.FormattingEnabled = true;
		this.cboCategory.Items.AddRange(new object[5] { "EARNINGS", "OTHER BENEFITS", "CONTRIBUTION", "CASH ADVANCE", "OTHER DEDUCTION" });
		this.cboCategory.Location = new System.Drawing.Point(12, 78);
		this.cboCategory.Name = "cboCategory";
		this.cboCategory.Size = new System.Drawing.Size(440, 26);
		this.cboCategory.TabIndex = 50;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(9, 57);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 18);
		this.label3.TabIndex = 49;
		this.label3.Text = "Category";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(9, 170);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 52;
		this.label8.Text = "Remarks";
		this.txtRemarks.Location = new System.Drawing.Point(12, 191);
		this.txtRemarks.MaxLength = 255;
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(440, 80);
		this.txtRemarks.TabIndex = 51;
		this.cbRecordStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "active", "inactive" });
		this.cbRecordStatus.Location = new System.Drawing.Point(12, 295);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(440, 26);
		this.cbRecordStatus.TabIndex = 54;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(9, 274);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 18);
		this.label11.TabIndex = 53;
		this.label11.Text = "Record Status";
		this.btnSave.Location = new System.Drawing.Point(328, 340);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 55;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.txtID.Location = new System.Drawing.Point(409, 0);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 56;
		this.txtID.TabStop = false;
		this.txtID.Visible = false;
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(12, 132);
		this.numAmount.Maximum = new decimal(new int[4] { 10000001, 0, 0, 131072 });
		this.numAmount.Minimum = new decimal(new int[4] { 10000001, 0, 0, -2147352576 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(440, 24);
		this.numAmount.TabIndex = 65;
		this.numAmount.ThousandsSeparator = true;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(9, 111);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(59, 18);
		this.label7.TabIndex = 64;
		this.label7.Text = "Amount";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(477, 398);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.txtID);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.cbRecordStatus);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.txtRemarks);
		base.Controls.Add(this.cboCategory);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtDescription);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.MaximizeBox = false;
		base.Name = "frmPayrollGrpSettings";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Payroll Settings";
		base.Load += new System.EventHandler(frmPayrollGrpSettings_Load);
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
