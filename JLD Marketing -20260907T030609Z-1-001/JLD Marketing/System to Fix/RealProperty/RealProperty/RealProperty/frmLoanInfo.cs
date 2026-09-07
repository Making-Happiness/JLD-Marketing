using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmLoanInfo : Form
{
	private string Transaction;

	private IContainer components = null;

	private Label lblEmployeename;

	private ComboBox cbDescription;

	private DateTimePicker dpDateApplied;

	private Label label4;

	private Panel panel1;

	private TextBox txtID;

	private Button btnSave;

	private ComboBox cbRecordStatus;

	private Label label11;

	private Label label8;

	private TextBox txtRemarks;

	private ComboBox cboCategory;

	private Label label3;

	private NumericUpDown numAmount;

	private Label lblamount;

	private Label label1;

	private DateTimePicker dpDueDate;

	private Label lblduedate;

	private Label lblMonthlyPayment;

	private NumericUpDown numAmortization;

	private Label lblpenalty;

	private NumericUpDown numPenalty;

	private FlowLayoutPanel flowLayoutPanel1;

	private Panel panel10;

	private Panel panel2;

	private Panel panel3;

	private Panel panel4;

	private Panel panel5;

	private Panel panel6;

	private Panel panel7;

	private Panel panel8;

	private Panel panel9;

	private Panel panel11;

	public dynamic Employeename { get; set; }

	public bool CommitChanges { get; set; }

	public Loan loan { get; set; }

	public string Category { get; set; }

	public frmLoanInfo(Loan loan, string transaction = "")
	{
		InitializeComponent();
		Transaction = transaction;
		this.loan = loan;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		if (loan.id == 0 && Transaction == "ADD")
		{
			int num = payrollCtrl.add_loan(loan);
			CommitChanges = false;
			if (num > 0)
			{
				loan.id = num;
				CommitChanges = true;
				Close();
			}
		}
		else if (loan.id > 0)
		{
			int num2 = payrollCtrl.edit_loan(loan);
			CommitChanges = false;
			if (num2 > 0)
			{
				CommitChanges = true;
				Close();
			}
		}
	}

	private void frmLoanInfo_Load(object sender, EventArgs e)
	{
		lblEmployeename.Text = Employeename;
		if (Category == "DEDUCTIONS")
		{
			cboCategory.Items.AddRange(new string[4]
			{
				"CONTRIBUTION      ".Trim(),
				"CASH ADVANCE      ".Trim(),
				"PURCHASE          ".Trim(),
				"OTHER DEDUCTION   ".Trim()
			});
		}
		else
		{
			cboCategory.Items.AddRange(new string[2]
			{
				"EARNINGS           ".Trim(),
				"OTHER BENEFITS     ".Trim()
			});
			lblamount.Visible = false;
			numAmount.Visible = false;
			lblpenalty.Visible = false;
			numPenalty.Visible = false;
			lblduedate.Text = "Valid Until";
			lblMonthlyPayment.Text = "Monthly Amount";
		}
		BindData();
	}

	private void BindData()
	{
		txtID.DataBindings.Add("Text", loan, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbDescription.DataBindings.Add("Text", loan, "description     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		dpDateApplied.DataBindings.Add("Text", loan, "dateapplied   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		dpDueDate.DataBindings.Add("Text", loan, "duedate   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboCategory.DataBindings.Add("Text", loan, "category        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", loan, "amount   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numPenalty.DataBindings.Add("Value", loan, "penalty   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmortization.DataBindings.Add("Value", loan, "amortization   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", loan, "remarks    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbRecordStatus.DataBindings.Add("Text", loan, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
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
		this.lblEmployeename = new System.Windows.Forms.Label();
		this.cbDescription = new System.Windows.Forms.ComboBox();
		this.dpDateApplied = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtID = new System.Windows.Forms.TextBox();
		this.btnSave = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.cboCategory = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.lblamount = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dpDueDate = new System.Windows.Forms.DateTimePicker();
		this.lblduedate = new System.Windows.Forms.Label();
		this.lblMonthlyPayment = new System.Windows.Forms.Label();
		this.numAmortization = new System.Windows.Forms.NumericUpDown();
		this.lblpenalty = new System.Windows.Forms.Label();
		this.numPenalty = new System.Windows.Forms.NumericUpDown();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel11 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numAmortization).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numPenalty).BeginInit();
		this.flowLayoutPanel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel8.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel11.SuspendLayout();
		base.SuspendLayout();
		this.lblEmployeename.AutoSize = true;
		this.lblEmployeename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEmployeename.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblEmployeename.Location = new System.Drawing.Point(14, 10);
		this.lblEmployeename.Name = "lblEmployeename";
		this.lblEmployeename.Size = new System.Drawing.Size(101, 25);
		this.lblEmployeename.TabIndex = 2;
		this.lblEmployeename.Text = "No Name";
		this.cbDescription.FormattingEnabled = true;
		this.cbDescription.Location = new System.Drawing.Point(6, 33);
		this.cbDescription.Name = "cbDescription";
		this.cbDescription.Size = new System.Drawing.Size(526, 26);
		this.cbDescription.TabIndex = 88;
		this.dpDateApplied.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dpDateApplied.Location = new System.Drawing.Point(6, 21);
		this.dpDateApplied.Name = "dpDateApplied";
		this.dpDateApplied.Size = new System.Drawing.Size(526, 24);
		this.dpDateApplied.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(90, 18);
		this.label4.TabIndex = 1;
		this.label4.Text = "Date Applied";
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.lblEmployeename);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(555, 43);
		this.panel1.TabIndex = 89;
		this.panel10.AutoSize = true;
		this.panel10.Controls.Add(this.cbRecordStatus);
		this.panel10.Controls.Add(this.label11);
		this.panel10.Location = new System.Drawing.Point(3, 511);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(535, 50);
		this.panel10.TabIndex = 93;
		this.cbRecordStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "active", "inactive" });
		this.cbRecordStatus.Location = new System.Drawing.Point(6, 21);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(526, 26);
		this.cbRecordStatus.TabIndex = 85;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(3, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 18);
		this.label11.TabIndex = 84;
		this.label11.Text = "Record Status";
		this.txtID.Location = new System.Drawing.Point(468, 3);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 87;
		this.txtID.TabStop = false;
		this.txtID.Visible = false;
		this.btnSave.Location = new System.Drawing.Point(405, 19);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 86;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(6, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 83;
		this.label8.Text = "Remarks";
		this.txtRemarks.Location = new System.Drawing.Point(6, 21);
		this.txtRemarks.MaxLength = 255;
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(526, 82);
		this.txtRemarks.TabIndex = 82;
		this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboCategory.FormattingEnabled = true;
		this.cboCategory.Location = new System.Drawing.Point(6, 21);
		this.cboCategory.Name = "cboCategory";
		this.cboCategory.Size = new System.Drawing.Size(526, 26);
		this.cboCategory.TabIndex = 81;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 18);
		this.label3.TabIndex = 80;
		this.label3.Text = "Category";
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(6, 23);
		this.numAmount.Maximum = new decimal(new int[4] { 10000001, 0, 0, 131072 });
		this.numAmount.Minimum = new decimal(new int[4] { 10000001, 0, 0, -2147352576 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(526, 24);
		this.numAmount.TabIndex = 79;
		this.numAmount.ThousandsSeparator = true;
		this.lblamount.AutoSize = true;
		this.lblamount.Location = new System.Drawing.Point(6, 2);
		this.lblamount.Name = "lblamount";
		this.lblamount.Size = new System.Drawing.Size(59, 18);
		this.lblamount.TabIndex = 78;
		this.lblamount.Text = "Amount";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 18);
		this.label1.TabIndex = 75;
		this.label1.Text = "Description";
		this.dpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dpDueDate.Location = new System.Drawing.Point(6, 21);
		this.dpDueDate.Name = "dpDueDate";
		this.dpDueDate.Size = new System.Drawing.Size(526, 24);
		this.dpDueDate.TabIndex = 91;
		this.lblduedate.AutoSize = true;
		this.lblduedate.Location = new System.Drawing.Point(6, 0);
		this.lblduedate.Name = "lblduedate";
		this.lblduedate.Size = new System.Drawing.Size(70, 18);
		this.lblduedate.TabIndex = 90;
		this.lblduedate.Text = "Due Date";
		this.lblMonthlyPayment.AutoSize = true;
		this.lblMonthlyPayment.Location = new System.Drawing.Point(6, 0);
		this.lblMonthlyPayment.Name = "lblMonthlyPayment";
		this.lblMonthlyPayment.Size = new System.Drawing.Size(214, 18);
		this.lblMonthlyPayment.TabIndex = 78;
		this.lblMonthlyPayment.Text = "Monthly Payment / Contribution";
		this.numAmortization.DecimalPlaces = 2;
		this.numAmortization.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmortization.Location = new System.Drawing.Point(9, 21);
		this.numAmortization.Maximum = new decimal(new int[4] { 10000001, 0, 0, 131072 });
		this.numAmortization.Minimum = new decimal(new int[4] { 10000001, 0, 0, -2147352576 });
		this.numAmortization.Name = "numAmortization";
		this.numAmortization.Size = new System.Drawing.Size(526, 24);
		this.numAmortization.TabIndex = 79;
		this.numAmortization.ThousandsSeparator = true;
		this.lblpenalty.AutoSize = true;
		this.lblpenalty.Location = new System.Drawing.Point(6, 0);
		this.lblpenalty.Name = "lblpenalty";
		this.lblpenalty.Size = new System.Drawing.Size(56, 18);
		this.lblpenalty.TabIndex = 78;
		this.lblpenalty.Text = "Penalty";
		this.numPenalty.DecimalPlaces = 2;
		this.numPenalty.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numPenalty.Location = new System.Drawing.Point(9, 21);
		this.numPenalty.Maximum = new decimal(new int[4] { 10000001, 0, 0, 131072 });
		this.numPenalty.Minimum = new decimal(new int[4] { 10000001, 0, 0, -2147352576 });
		this.numPenalty.Name = "numPenalty";
		this.numPenalty.Size = new System.Drawing.Size(526, 24);
		this.numPenalty.TabIndex = 79;
		this.numPenalty.ThousandsSeparator = true;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.flowLayoutPanel1.Controls.Add(this.panel2);
		this.flowLayoutPanel1.Controls.Add(this.panel3);
		this.flowLayoutPanel1.Controls.Add(this.panel4);
		this.flowLayoutPanel1.Controls.Add(this.panel5);
		this.flowLayoutPanel1.Controls.Add(this.panel6);
		this.flowLayoutPanel1.Controls.Add(this.panel7);
		this.flowLayoutPanel1.Controls.Add(this.panel8);
		this.flowLayoutPanel1.Controls.Add(this.panel9);
		this.flowLayoutPanel1.Controls.Add(this.panel10);
		this.flowLayoutPanel1.Controls.Add(this.panel11);
		this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 49);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 635);
		this.flowLayoutPanel1.TabIndex = 92;
		this.panel2.AutoSize = true;
		this.panel2.Controls.Add(this.cbDescription);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.txtID);
		this.panel2.Location = new System.Drawing.Point(3, 3);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(535, 62);
		this.panel2.TabIndex = 0;
		this.panel3.AutoSize = true;
		this.panel3.Controls.Add(this.dpDateApplied);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Location = new System.Drawing.Point(3, 71);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(535, 48);
		this.panel3.TabIndex = 1;
		this.panel4.AutoSize = true;
		this.panel4.Controls.Add(this.cboCategory);
		this.panel4.Controls.Add(this.label3);
		this.panel4.Location = new System.Drawing.Point(3, 125);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(535, 50);
		this.panel4.TabIndex = 2;
		this.panel5.AutoSize = true;
		this.panel5.Controls.Add(this.numAmount);
		this.panel5.Controls.Add(this.lblamount);
		this.panel5.Location = new System.Drawing.Point(3, 181);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(535, 50);
		this.panel5.TabIndex = 3;
		this.panel6.AutoSize = true;
		this.panel6.Controls.Add(this.dpDueDate);
		this.panel6.Controls.Add(this.lblduedate);
		this.panel6.Location = new System.Drawing.Point(3, 237);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(535, 48);
		this.panel6.TabIndex = 4;
		this.panel7.AutoSize = true;
		this.panel7.Controls.Add(this.numAmortization);
		this.panel7.Controls.Add(this.lblMonthlyPayment);
		this.panel7.Location = new System.Drawing.Point(3, 291);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(538, 48);
		this.panel7.TabIndex = 5;
		this.panel8.AutoSize = true;
		this.panel8.Controls.Add(this.numPenalty);
		this.panel8.Controls.Add(this.lblpenalty);
		this.panel8.Location = new System.Drawing.Point(3, 345);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(538, 48);
		this.panel8.TabIndex = 6;
		this.panel9.AutoSize = true;
		this.panel9.Controls.Add(this.txtRemarks);
		this.panel9.Controls.Add(this.label8);
		this.panel9.Location = new System.Drawing.Point(3, 399);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(535, 106);
		this.panel9.TabIndex = 7;
		this.panel11.Controls.Add(this.btnSave);
		this.panel11.Location = new System.Drawing.Point(3, 567);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(535, 65);
		this.panel11.TabIndex = 94;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		base.ClientSize = new System.Drawing.Size(555, 714);
		base.Controls.Add(this.flowLayoutPanel1);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmLoanInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmLoanInfo";
		base.Load += new System.EventHandler(frmLoanInfo_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel10.ResumeLayout(false);
		this.panel10.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numAmortization).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numPenalty).EndInit();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		this.panel8.ResumeLayout(false);
		this.panel8.PerformLayout();
		this.panel9.ResumeLayout(false);
		this.panel9.PerformLayout();
		this.panel11.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
