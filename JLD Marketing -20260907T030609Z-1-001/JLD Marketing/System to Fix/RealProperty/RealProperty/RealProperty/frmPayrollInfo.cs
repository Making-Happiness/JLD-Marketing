using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPayrollInfo : Form
{
	private IContainer components = null;

	private ComboBox cbTitle;

	private Panel panel1;

	private Label lblEmployeename;

	private TextBox txtID;

	private Button btnSave;

	private Label label8;

	private ComboBox cbRecordStatus;

	private Label label1;

	private GroupBox groupBox1;

	private DateTimePicker dpDateTo;

	private Label label5;

	private DateTimePicker dpDateFrom;

	private Label label4;

	public string Transaction { get; set; }

	public Payroll payroll { get; set; }

	public bool CommitChanges { get; set; }

	public frmPayrollInfo()
	{
		InitializeComponent();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (payroll.id == 0 && Transaction == "ADD")
		{
			using (PayrollCtrl newPayrollCtrl = new PayrollCtrl())
			{
				payroll.recordstatus = "new";
				payroll.id = newPayrollCtrl.addPayroll(payroll);
				if (payroll.id > 0)
				{
					CommitChanges = true;
					Close();
				}
				else
				{
					CommitChanges = false;
				}
				return;
			}
		}
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		int num = payrollCtrl.editPayroll(payroll);
		if (num > 0)
		{
			CommitChanges = true;
			Close();
		}
		else
		{
			CommitChanges = false;
		}
	}

	private void frmPayrollInfo_Load(object sender, EventArgs e)
	{
		BindData();
		cbTitle.Items.Clear();
		DateTime dateTime = dpDateFrom.Value.AddDays(0.0);
		DateTime dateTime2 = dpDateTo.Value.AddDays(0.0);
		string text = dpDateFrom.Value.ToString("MMMM");
		string text2 = dpDateTo.Value.ToString("MMMM");
		string text3 = ((text == text2) ? "" : (text2 + " "));
		cbTitle.Items.Add(string.Format("{0} {1}-{2}{3} Payroll", dateTime.ToString("MMMM"), dateTime.Day, text3, dateTime2.Day));
		dateTime = dpDateFrom.Value.AddDays(-7.0);
		dateTime2 = dpDateTo.Value.AddDays(-7.0);
		text = dpDateFrom.Value.ToString("MMMM");
		text2 = dpDateTo.Value.ToString("MMMM");
		text3 = ((text == text2) ? "" : (text2 + " "));
		cbTitle.Items.Add(string.Format("{0} {1}-{2}{3} Payroll", dateTime.ToString("MMMM"), dateTime.Day, text3, dateTime2.Day));
		dateTime = dpDateFrom.Value.AddDays(7.0);
		dateTime2 = dpDateTo.Value.AddDays(7.0);
		text = dpDateFrom.Value.ToString("MMMM");
		text2 = dpDateTo.Value.ToString("MMMM");
		text3 = ((text == text2) ? "" : (text2 + " "));
		cbTitle.SelectedIndex = 0;
		cbTitle.Items.Add(string.Format("{0} {1}-{2}{3} Payroll", dateTime.ToString("MMMM"), dateTime.Day, text3, dateTime2.Day));
	}

	private void BindData()
	{
		txtID.DataBindings.Add("Text", payroll, "id", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbTitle.DataBindings.Add("Text", payroll, "title", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		dpDateFrom.DataBindings.Add("Text", payroll, "datefrom", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		dpDateTo.DataBindings.Add("Text", payroll, "dateto", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbRecordStatus.DataBindings.Add("text", payroll, "recordstatus", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
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
		this.cbTitle = new System.Windows.Forms.ComboBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblEmployeename = new System.Windows.Forms.Label();
		this.txtID = new System.Windows.Forms.TextBox();
		this.btnSave = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dpDateTo = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.dpDateFrom = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.cbTitle.FormattingEnabled = true;
		this.cbTitle.Location = new System.Drawing.Point(12, 91);
		this.cbTitle.Name = "cbTitle";
		this.cbTitle.Size = new System.Drawing.Size(526, 26);
		this.cbTitle.TabIndex = 101;
		this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.panel1.Controls.Add(this.lblEmployeename);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(555, 46);
		this.panel1.TabIndex = 102;
		this.lblEmployeename.AutoSize = true;
		this.lblEmployeename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEmployeename.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.lblEmployeename.Location = new System.Drawing.Point(14, 10);
		this.lblEmployeename.Name = "lblEmployeename";
		this.lblEmployeename.Size = new System.Drawing.Size(191, 25);
		this.lblEmployeename.TabIndex = 2;
		this.lblEmployeename.Text = "Payroll Information";
		this.txtID.Location = new System.Drawing.Point(477, 51);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 100;
		this.txtID.TabStop = false;
		this.txtID.Visible = false;
		this.btnSave.Location = new System.Drawing.Point(414, 284);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 99;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(12, 210);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(103, 18);
		this.label8.TabIndex = 98;
		this.label8.Text = "Record Status";
		this.cbRecordStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "active", "inactive" });
		this.cbRecordStatus.Location = new System.Drawing.Point(12, 236);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(151, 26);
		this.cbRecordStatus.TabIndex = 94;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 65);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(35, 18);
		this.label1.TabIndex = 92;
		this.label1.Text = "Title";
		this.groupBox1.Controls.Add(this.dpDateTo);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dpDateFrom);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Location = new System.Drawing.Point(12, 137);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(440, 64);
		this.groupBox1.TabIndex = 103;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Effectivity";
		this.dpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dpDateTo.Location = new System.Drawing.Point(260, 22);
		this.dpDateTo.Name = "dpDateTo";
		this.dpDateTo.Size = new System.Drawing.Size(140, 24);
		this.dpDateTo.TabIndex = 6;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(212, 27);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(30, 18);
		this.label5.TabIndex = 5;
		this.label5.Text = "To:";
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(555, 358);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.cbTitle);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.txtID);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.cbRecordStatus);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmPayrollInfo";
		this.Text = "frmPayrollInfo";
		base.Load += new System.EventHandler(frmPayrollInfo_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
