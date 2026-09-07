using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPayslipDetailsInfo : Form
{
	private IContainer components = null;

	private TextBox txtID;

	private Button btnSave;

	private ComboBox cbRecordStatus;

	private Label label11;

	private Label label8;

	private TextBox txtRemarks;

	private ComboBox cboCategory;

	private Label label3;

	private NumericUpDown numAmount;

	private Label label5;

	private Label label7;

	private DateTimePicker dpDateTo;

	private DateTimePicker dpDateFrom;

	private Label label4;

	private ComboBox cbRecordTypes;

	private GroupBox groupBox1;

	private Label label2;

	private Label label1;

	private ComboBox cbTitle;

	private Panel panel1;

	private Label lblEmployeename;

	public string Transaction { get; set; }

	public Payslipdetail payslipdetail { get; set; }

	public dynamic Employeename { get; set; }

	public bool CommitChanges { get; set; }

	public string category_mode { get; set; }

	public frmPayslipDetailsInfo(Payslipdetail payslipdetail, string transaction = "")
	{
		InitializeComponent();
		this.payslipdetail = payslipdetail;
		Transaction = transaction;
	}

	private void load_PayrollSettingsCookies()
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable payrollSettingsCookies = payrollCtrl.getPayrollSettingsCookies();
		string text = "";
		if (Transaction != "ADD")
		{
			category_mode = ((payslipdetail.category.IndexOf("BENEFIT") > 0) ? "BENEFITS" : "");
		}
		text = ((!(category_mode == "BENEFITS")) ? string.Format("category not like '%{0}%' OR category not like '%{0}%'", "BENEFIT", "EARNING") : string.Format("category like '%{0}%' OR category like '%{0}%'", "BENEFIT", "EARNING"));
		DataRow[] source = payrollSettingsCookies.Select(text);
		payrollSettingsCookies = ((source.Count() <= 0) ? payrollSettingsCookies.Clone() : source.CopyToDataTable());
		cbTitle.DataSource = payrollSettingsCookies;
		cbTitle.DisplayMember = "description";
		cbTitle.ValueMember = "category";
	}

	private void bindData()
	{
		txtID.DataBindings.Add("Text", payslipdetail, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbTitle.DataBindings.Add("Text", payslipdetail, "title     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboCategory.DataBindings.Add("Text", payslipdetail, "category        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", payslipdetail, "amount   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", payslipdetail, "remarks    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbRecordStatus.DataBindings.Add("Text", payslipdetail, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void referesh_payrollsettings()
	{
	}

	private void frmPayslipDetailsInfo_Load(object sender, EventArgs e)
	{
		load_PayrollSettingsCookies();
		bindData();
		lblEmployeename.Text = Employeename;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		if (payslipdetail.id == 0 && Transaction == "ADD")
		{
			int num = payrollCtrl.add_payslipdetail(payslipdetail);
			CommitChanges = false;
			if (num > 0)
			{
				payslipdetail.id = num;
				CommitChanges = true;
				Close();
			}
		}
		else if (payslipdetail.id > 0)
		{
			int num2 = payrollCtrl.edit_payslipdetail(payslipdetail);
			CommitChanges = false;
			if (num2 > 0)
			{
				CommitChanges = true;
				Close();
			}
		}
	}

	private void cbTitle_SelectedValueChanged(object sender, EventArgs e)
	{
		cboCategory.Text = cbTitle.SelectedValue.ToString();
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
		this.txtID = new System.Windows.Forms.TextBox();
		this.btnSave = new System.Windows.Forms.Button();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.cboCategory = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.dpDateTo = new System.Windows.Forms.DateTimePicker();
		this.dpDateFrom = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.cbRecordTypes = new System.Windows.Forms.ComboBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cbTitle = new System.Windows.Forms.ComboBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblEmployeename = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		this.groupBox1.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.txtID.Location = new System.Drawing.Point(477, 56);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 71;
		this.txtID.TabStop = false;
		this.txtID.Visible = false;
		this.btnSave.Location = new System.Drawing.Point(414, 406);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 70;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.cbRecordStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "active", "inactive" });
		this.cbRecordStatus.Location = new System.Drawing.Point(12, 340);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(526, 26);
		this.cbRecordStatus.TabIndex = 69;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(9, 319);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 18);
		this.label11.TabIndex = 68;
		this.label11.Text = "Record Status";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(9, 213);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 67;
		this.label8.Text = "Remarks";
		this.txtRemarks.Location = new System.Drawing.Point(12, 234);
		this.txtRemarks.MaxLength = 255;
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(526, 82);
		this.txtRemarks.TabIndex = 66;
		this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboCategory.FormattingEnabled = true;
		this.cboCategory.Items.AddRange(new object[5] { "EARNINGS", "OTHER BENEFITS", "CONTRIBUTION", "CASH ADVANCE", "OTHER DEDUCTION" });
		this.cboCategory.Location = new System.Drawing.Point(12, 136);
		this.cboCategory.Name = "cboCategory";
		this.cboCategory.Size = new System.Drawing.Size(526, 26);
		this.cboCategory.TabIndex = 65;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(9, 115);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 18);
		this.label3.TabIndex = 64;
		this.label3.Text = "Category";
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(12, 186);
		this.numAmount.Maximum = new decimal(new int[4] { 10000001, 0, 0, 131072 });
		this.numAmount.Minimum = new decimal(new int[4] { 10000001, 0, 0, -2147352576 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(526, 24);
		this.numAmount.TabIndex = 63;
		this.numAmount.ThousandsSeparator = true;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(212, 27);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(30, 18);
		this.label5.TabIndex = 5;
		this.label5.Text = "To:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(9, 165);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(59, 18);
		this.label7.TabIndex = 62;
		this.label7.Text = "Amount";
		this.dpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dpDateTo.Location = new System.Drawing.Point(260, 22);
		this.dpDateTo.Name = "dpDateTo";
		this.dpDateTo.Size = new System.Drawing.Size(140, 24);
		this.dpDateTo.TabIndex = 6;
		this.dpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dpDateFrom.Location = new System.Drawing.Point(57, 22);
		this.dpDateFrom.Name = "dpDateFrom";
		this.dpDateFrom.Size = new System.Drawing.Size(140, 24);
		this.dpDateFrom.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(3, 27);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(48, 18);
		this.label4.TabIndex = 1;
		this.label4.Text = "From:";
		this.cbRecordTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordTypes.FormattingEnabled = true;
		this.cbRecordTypes.Items.AddRange(new object[3] { "FIXED", "BY DATE", "ONCE ONLY" });
		this.cbRecordTypes.Location = new System.Drawing.Point(587, 316);
		this.cbRecordTypes.Name = "cbRecordTypes";
		this.cbRecordTypes.Size = new System.Drawing.Size(440, 26);
		this.cbRecordTypes.TabIndex = 61;
		this.groupBox1.Controls.Add(this.dpDateTo);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dpDateFrom);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Location = new System.Drawing.Point(581, 361);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(440, 64);
		this.groupBox1.TabIndex = 60;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Effectivity";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(584, 295);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(48, 18);
		this.label2.TabIndex = 58;
		this.label2.Text = "Types";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 65);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(35, 18);
		this.label1.TabIndex = 59;
		this.label1.Text = "Title";
		this.cbTitle.FormattingEnabled = true;
		this.cbTitle.Location = new System.Drawing.Point(12, 86);
		this.cbTitle.Name = "cbTitle";
		this.cbTitle.Size = new System.Drawing.Size(526, 26);
		this.cbTitle.TabIndex = 72;
		this.cbTitle.SelectedValueChanged += new System.EventHandler(cbTitle_SelectedValueChanged);
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.lblEmployeename);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(550, 46);
		this.panel1.TabIndex = 73;
		this.lblEmployeename.AutoSize = true;
		this.lblEmployeename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEmployeename.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblEmployeename.Location = new System.Drawing.Point(14, 10);
		this.lblEmployeename.Name = "lblEmployeename";
		this.lblEmployeename.Size = new System.Drawing.Size(101, 25);
		this.lblEmployeename.TabIndex = 2;
		this.lblEmployeename.Text = "No Name";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(550, 470);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.cbTitle);
		base.Controls.Add(this.txtID);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.cbRecordStatus);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.txtRemarks);
		base.Controls.Add(this.cboCategory);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.cbRecordTypes);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPayslipDetailsInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmPayslipDetailsInfo";
		base.Load += new System.EventHandler(frmPayslipDetailsInfo_Load);
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
