using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmpayslipinfo : Form
{
	private IContainer components = null;

	private Label lblEmployeename;

	private ComboBox cbTitle;

	private DateTimePicker dpDateTo;

	private DateTimePicker dpDateFrom;

	private Label label4;

	private Panel panel1;

	private Label label5;

	private TextBox txtID;

	private Button btnSave;

	private ComboBox cbRecordStatus;

	private Label label11;

	private Label label8;

	private TextBox txtRemarks;

	private ComboBox cboPayslipType;

	private Label label3;

	private ComboBox cbModeofPosting;

	private GroupBox groupBox1;

	private Label label2;

	private Label label1;

	private Label label6;

	private ComboBox cbForthemonthof;

	public string Transaction { get; set; }

	public bool CommitChanges { get; set; }

	public Payslip payslip { get; set; }

	public frmpayslipinfo(Payslip payslip, string transaction = "")
	{
		InitializeComponent();
		Transaction = transaction;
		this.payslip = payslip;
		loadCookies_forthemonthof();
	}

	private void loadCookies_forthemonthof()
	{
		cbForthemonthof.Items.Clear();
		DateTime dateTime = new DateTime(DateTime.Today.Year - 1, 12, 1);
		List<string> list = new List<string>();
		for (int i = 0; i < 14; i++)
		{
			list.Add(dateTime.ToString("MMMM yyyy"));
			dateTime = dateTime.AddMonths(1);
		}
		cbForthemonthof.Items.AddRange(list.ToArray());
		list = null;
	}

	private void bindData()
	{
		txtID.DataBindings.Add("Text", payslip, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbTitle.DataBindings.Add("Text", payslip, "title     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboPayslipType.DataBindings.Add("Text", payslip, "payslip_type        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbForthemonthof.DataBindings.Add("Text", payslip, "for_month_of   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbModeofPosting.DataBindings.Add("Text", payslip, "mode_of_posting        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", payslip, "remarks    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbRecordStatus.DataBindings.Add("Text", payslip, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		if (payslip.id == 0 && Transaction == "ADD")
		{
			payslip.recordstatus = "draft";
			int num = payrollCtrl.add_payslip(payslip);
			CommitChanges = false;
			if (num > 0)
			{
				payslip.id = num;
				CommitChanges = true;
				Close();
			}
		}
		else if (payslip.id > 0)
		{
			int num2 = payrollCtrl.edit_payslip(payslip);
			CommitChanges = false;
			if (num2 > 0)
			{
				CommitChanges = true;
				Close();
			}
		}
	}

	private void frmpayslipinfo_Load(object sender, EventArgs e)
	{
		bindData();
		if (Transaction == "ADD" && payslip != null)
		{
		}
	}

	private void cbForthemonthof_TextChanged(object sender, EventArgs e)
	{
		if (cbTitle.Text == "")
		{
			cbTitle.Text = cbForthemonthof.Text;
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
		this.lblEmployeename = new System.Windows.Forms.Label();
		this.cbTitle = new System.Windows.Forms.ComboBox();
		this.dpDateTo = new System.Windows.Forms.DateTimePicker();
		this.dpDateFrom = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label5 = new System.Windows.Forms.Label();
		this.txtID = new System.Windows.Forms.TextBox();
		this.btnSave = new System.Windows.Forms.Button();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.cboPayslipType = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cbModeofPosting = new System.Windows.Forms.ComboBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.cbForthemonthof = new System.Windows.Forms.ComboBox();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.lblEmployeename.AutoSize = true;
		this.lblEmployeename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEmployeename.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblEmployeename.Location = new System.Drawing.Point(14, 10);
		this.lblEmployeename.Name = "lblEmployeename";
		this.lblEmployeename.Size = new System.Drawing.Size(195, 25);
		this.lblEmployeename.TabIndex = 2;
		this.lblEmployeename.Text = "Payslip Information";
		this.cbTitle.FormattingEnabled = true;
		this.cbTitle.Location = new System.Drawing.Point(12, 150);
		this.cbTitle.Name = "cbTitle";
		this.cbTitle.Size = new System.Drawing.Size(526, 26);
		this.cbTitle.TabIndex = 88;
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
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.lblEmployeename);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(553, 46);
		this.panel1.TabIndex = 89;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(212, 27);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(30, 18);
		this.label5.TabIndex = 5;
		this.label5.Text = "To:";
		this.txtID.Location = new System.Drawing.Point(477, 47);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 87;
		this.txtID.TabStop = false;
		this.txtID.Visible = false;
		this.btnSave.Location = new System.Drawing.Point(414, 435);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 86;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.cbRecordStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "draft", "finalized" });
		this.cbRecordStatus.Location = new System.Drawing.Point(587, 292);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(526, 26);
		this.cbRecordStatus.TabIndex = 85;
		this.cbRecordStatus.Visible = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(584, 266);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 18);
		this.label11.TabIndex = 84;
		this.label11.Text = "Record Status";
		this.label11.Visible = false;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(6, 307);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 83;
		this.label8.Text = "Remarks";
		this.txtRemarks.Location = new System.Drawing.Point(9, 333);
		this.txtRemarks.MaxLength = 255;
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(526, 82);
		this.txtRemarks.TabIndex = 82;
		this.cboPayslipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboPayslipType.FormattingEnabled = true;
		this.cboPayslipType.Items.AddRange(new object[2] { "Master Payslip", "Customized" });
		this.cboPayslipType.Location = new System.Drawing.Point(12, 210);
		this.cboPayslipType.Name = "cboPayslipType";
		this.cboPayslipType.Size = new System.Drawing.Size(440, 26);
		this.cboPayslipType.TabIndex = 81;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 184);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(91, 18);
		this.label3.TabIndex = 80;
		this.label3.Text = "Payslip Type";
		this.cbModeofPosting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbModeofPosting.FormattingEnabled = true;
		this.cbModeofPosting.Items.AddRange(new object[2] { "Allow Auto Genarated", "Manual" });
		this.cbModeofPosting.Location = new System.Drawing.Point(12, 270);
		this.cbModeofPosting.Name = "cbModeofPosting";
		this.cbModeofPosting.Size = new System.Drawing.Size(440, 26);
		this.cbModeofPosting.TabIndex = 77;
		this.groupBox1.Controls.Add(this.dpDateTo);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dpDateFrom);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Location = new System.Drawing.Point(581, 352);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(440, 64);
		this.groupBox1.TabIndex = 76;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Effectivity";
		this.groupBox1.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 244);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(117, 18);
		this.label2.TabIndex = 74;
		this.label2.Text = "Mode of Posting";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 124);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(35, 18);
		this.label1.TabIndex = 75;
		this.label1.Text = "Title";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(9, 65);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(118, 18);
		this.label6.TabIndex = 74;
		this.label6.Text = "For the month of";
		this.cbForthemonthof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbForthemonthof.FormattingEnabled = true;
		this.cbForthemonthof.Location = new System.Drawing.Point(9, 91);
		this.cbForthemonthof.Name = "cbForthemonthof";
		this.cbForthemonthof.Size = new System.Drawing.Size(440, 26);
		this.cbForthemonthof.TabIndex = 77;
		this.cbForthemonthof.TextChanged += new System.EventHandler(cbForthemonthof_TextChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(553, 499);
		base.Controls.Add(this.cbTitle);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.txtID);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.cbRecordStatus);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.txtRemarks);
		base.Controls.Add(this.cboPayslipType);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.cbForthemonthof);
		base.Controls.Add(this.cbModeofPosting);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmpayslipinfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Payslip information";
		base.Load += new System.EventHandler(frmpayslipinfo_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
