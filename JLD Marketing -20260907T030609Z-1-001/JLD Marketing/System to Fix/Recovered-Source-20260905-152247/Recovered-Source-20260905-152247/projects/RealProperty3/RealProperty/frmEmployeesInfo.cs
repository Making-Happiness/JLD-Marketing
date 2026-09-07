using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmEmployeesInfo : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private Label lblMessage;

	private Button btnSave;

	private DateTimePicker dtpDateofbirth;

	private ComboBox cboGender;

	private TextBox txtID;

	private Label label4;

	private Label label9;

	private Label label3;

	private Label label6;

	private Label label5;

	private Label label2;

	private Label label1;

	private TextBox txtContactno;

	private TextBox txtMiddlename;

	private TextBox txtLastname;

	private TextBox txtFirstname;

	private TextBox txtDateofbirth;

	private NumericUpDown numSalary;

	private Label label7;

	private ComboBox cbCivistatus;

	private Label label10;

	private Label label8;

	private ComboBox cbRecordStatus;

	private Label label11;

	private TextBox txtRemarks;

	private ComboBox cbJobCategory;

	private Label label12;

	private Button button1;

	private Button button2;

	private ComboBox cbDesignation;

	public Employee employee { get; set; }

	public bool commitcahanges { get; set; }

	public frmEmployeesInfo()
	{
		InitializeComponent();
		if (employee == null)
		{
			employee = new Employee();
		}
	}

	private void bindData()
	{
		DataTable dt_jobcategory = getdt_jobcategory();
		BindJobCategory(dt_jobcategory);
		DataTable dt_jobDesignation = getdt_jobDesignation();
		BindjobDesignation(dt_jobDesignation);
		txtID.DataBindings.Add("Text", employee, "idemployee    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtFirstname.DataBindings.Add("Text", employee, "firstname     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtLastname.DataBindings.Add("Text", employee, "lastname      ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtMiddlename.DataBindings.Add("Text", employee, "middlename    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboGender.DataBindings.Add("Text", employee, "gender        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		try
		{
			dtpDateofbirth.DataBindings.Add("Value", employee, "dateofbirth   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		}
		catch (Exception)
		{
			employee.dateofbirth = DateTime.Today.AddYears(-10);
			dtpDateofbirth.DataBindings.Add("Value", employee, "dateofbirth   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		}
		numSalary.DataBindings.Add("Value", employee, "salary   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbDesignation.DataBindings.Add("Text", employee, "designation  ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbCivistatus.DataBindings.Add("Text", employee, "civilstatus    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtContactno.DataBindings.Add("Text", employee, "contactno     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtRemarks.DataBindings.Add("Text", employee, "remarks     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cbRecordStatus.DataBindings.Add("Text", employee, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void BindjobDesignation(DataTable dt_jobDesignation)
	{
		cbDesignation.DataSource = dt_jobDesignation;
		cbDesignation.ValueMember = "designation";
	}

	private void BindJobCategory(DataTable dt_jobcategory)
	{
		cbJobCategory.DataSource = dt_jobcategory;
		cbJobCategory.ValueMember = "category";
	}

	private DataTable getdt_jobDesignation()
	{
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		return employeeCtrl.jobDesignationCookies();
	}

	private DataTable getdt_jobcategory()
	{
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		return employeeCtrl.getobcategoryCookies();
	}

	private void frmEmployeesInfo_Load(object sender, EventArgs e)
	{
		bindData();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (employee.idemployee <= 0)
		{
			using (EmployeeCtrl employeeCtrl = new EmployeeCtrl())
			{
				int num = employeeCtrl.add(employee);
				if (num > 0)
				{
					commitcahanges = true;
					lblMessage.Text = "Message!\nAdding Record Successfull!";
				}
				else
				{
					commitcahanges = false;
					lblMessage.Text = "Message!\nAdding Record failed!";
				}
				return;
			}
		}
		using EmployeeCtrl employeeCtrl = new EmployeeCtrl();
		int num2 = employeeCtrl.edit(employee);
		if (num2 > 0)
		{
			commitcahanges = true;
			lblMessage.Text = "Message!\nChanging Record Successfull!";
		}
		else
		{
			commitcahanges = false;
			lblMessage.Text = "Message!\nChanging Record Failed!";
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbRecordStatus = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.numSalary = new System.Windows.Forms.NumericUpDown();
		this.lblMessage = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.dtpDateofbirth = new System.Windows.Forms.DateTimePicker();
		this.cbCivistatus = new System.Windows.Forms.ComboBox();
		this.cboGender = new System.Windows.Forms.ComboBox();
		this.txtID = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtRemarks = new System.Windows.Forms.TextBox();
		this.txtContactno = new System.Windows.Forms.TextBox();
		this.txtMiddlename = new System.Windows.Forms.TextBox();
		this.txtLastname = new System.Windows.Forms.TextBox();
		this.txtFirstname = new System.Windows.Forms.TextBox();
		this.txtDateofbirth = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.cbJobCategory = new System.Windows.Forms.ComboBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.cbDesignation = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numSalary).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbRecordStatus);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.numSalary);
		this.groupBox1.Controls.Add(this.lblMessage);
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.btnSave);
		this.groupBox1.Controls.Add(this.dtpDateofbirth);
		this.groupBox1.Controls.Add(this.cbDesignation);
		this.groupBox1.Controls.Add(this.cbJobCategory);
		this.groupBox1.Controls.Add(this.cbCivistatus);
		this.groupBox1.Controls.Add(this.cboGender);
		this.groupBox1.Controls.Add(this.txtID);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtRemarks);
		this.groupBox1.Controls.Add(this.txtContactno);
		this.groupBox1.Controls.Add(this.txtMiddlename);
		this.groupBox1.Controls.Add(this.txtLastname);
		this.groupBox1.Controls.Add(this.txtFirstname);
		this.groupBox1.Controls.Add(this.txtDateofbirth);
		this.groupBox1.Location = new System.Drawing.Point(14, 14);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(849, 537);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Employee Information";
		this.cbRecordStatus.FormattingEnabled = true;
		this.cbRecordStatus.Items.AddRange(new object[2] { "active", "inactive" });
		this.cbRecordStatus.Location = new System.Drawing.Point(39, 408);
		this.cbRecordStatus.Name = "cbRecordStatus";
		this.cbRecordStatus.Size = new System.Drawing.Size(250, 26);
		this.cbRecordStatus.TabIndex = 50;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(36, 386);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 18);
		this.label11.TabIndex = 49;
		this.label11.Text = "Record Status";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(345, 329);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(69, 18);
		this.label8.TabIndex = 47;
		this.label8.Text = "Remarks";
		this.numSalary.DecimalPlaces = 2;
		this.numSalary.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numSalary.Location = new System.Drawing.Point(39, 175);
		this.numSalary.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numSalary.Name = "numSalary";
		this.numSalary.Size = new System.Drawing.Size(250, 24);
		this.numSalary.TabIndex = 46;
		this.numSalary.ThousandsSeparator = true;
		this.lblMessage.AutoSize = true;
		this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMessage.Location = new System.Drawing.Point(39, 462);
		this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(81, 18);
		this.lblMessage.TabIndex = 8;
		this.lblMessage.Text = "Message:";
		this.btnSave.Location = new System.Drawing.Point(704, 474);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(124, 43);
		this.btnSave.TabIndex = 2;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.dtpDateofbirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDateofbirth.Location = new System.Drawing.Point(38, 122);
		this.dtpDateofbirth.Name = "dtpDateofbirth";
		this.dtpDateofbirth.Size = new System.Drawing.Size(250, 24);
		this.dtpDateofbirth.TabIndex = 3;
		this.cbCivistatus.FormattingEnabled = true;
		this.cbCivistatus.Items.AddRange(new object[3] { "Single", "Married", "Widow" });
		this.cbCivistatus.Location = new System.Drawing.Point(39, 290);
		this.cbCivistatus.Name = "cbCivistatus";
		this.cbCivistatus.Size = new System.Drawing.Size(250, 26);
		this.cbCivistatus.TabIndex = 4;
		this.cboGender.FormattingEnabled = true;
		this.cboGender.Items.AddRange(new object[2] { "Male", "Female" });
		this.cboGender.Location = new System.Drawing.Point(297, 119);
		this.cboGender.Name = "cboGender";
		this.cboGender.Size = new System.Drawing.Size(250, 26);
		this.cboGender.TabIndex = 4;
		this.txtID.Location = new System.Drawing.Point(760, 2);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(68, 24);
		this.txtID.TabIndex = 0;
		this.txtID.TabStop = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(34, 97);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(93, 18);
		this.label4.TabIndex = 1;
		this.label4.Text = "Date Of Birth";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(34, 329);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(110, 18);
		this.label9.TabIndex = 1;
		this.label9.Text = "Tel/CP Number";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(554, 39);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(164, 18);
		this.label3.TabIndex = 1;
		this.label3.Text = "Middle Name (Optional)";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(36, 154);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(49, 18);
		this.label7.TabIndex = 1;
		this.label7.Text = "Salary";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(36, 268);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 18);
		this.label10.TabIndex = 1;
		this.label10.Text = "Civil Status";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(428, 206);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(173, 18);
		this.label6.TabIndex = 1;
		this.label6.Text = "Job Position/Designation";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(294, 97);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(57, 18);
		this.label5.TabIndex = 1;
		this.label5.Text = "Gender";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(294, 39);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(80, 18);
		this.label2.TabIndex = 1;
		this.label2.Text = "Last Name";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(34, 39);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(81, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "First Name";
		this.txtRemarks.Location = new System.Drawing.Point(348, 354);
		this.txtRemarks.Multiline = true;
		this.txtRemarks.Name = "txtRemarks";
		this.txtRemarks.Size = new System.Drawing.Size(250, 80);
		this.txtRemarks.TabIndex = 7;
		this.txtContactno.Location = new System.Drawing.Point(37, 354);
		this.txtContactno.Name = "txtContactno";
		this.txtContactno.Size = new System.Drawing.Size(250, 24);
		this.txtContactno.TabIndex = 7;
		this.txtMiddlename.Location = new System.Drawing.Point(557, 64);
		this.txtMiddlename.Name = "txtMiddlename";
		this.txtMiddlename.Size = new System.Drawing.Size(250, 24);
		this.txtMiddlename.TabIndex = 2;
		this.txtLastname.Location = new System.Drawing.Point(297, 64);
		this.txtLastname.Name = "txtLastname";
		this.txtLastname.Size = new System.Drawing.Size(250, 24);
		this.txtLastname.TabIndex = 1;
		this.txtFirstname.Location = new System.Drawing.Point(38, 64);
		this.txtFirstname.Name = "txtFirstname";
		this.txtFirstname.Size = new System.Drawing.Size(250, 24);
		this.txtFirstname.TabIndex = 0;
		this.txtDateofbirth.Location = new System.Drawing.Point(39, 93);
		this.txtDateofbirth.Name = "txtDateofbirth";
		this.txtDateofbirth.ReadOnly = true;
		this.txtDateofbirth.Size = new System.Drawing.Size(250, 24);
		this.txtDateofbirth.TabIndex = 0;
		this.txtDateofbirth.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(36, 208);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(97, 18);
		this.label12.TabIndex = 1;
		this.label12.Text = "Job Category";
		this.cbJobCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbJobCategory.FormattingEnabled = true;
		this.cbJobCategory.Items.AddRange(new object[3] { "Single", "Married", "Widow" });
		this.cbJobCategory.Location = new System.Drawing.Point(39, 230);
		this.cbJobCategory.Name = "cbJobCategory";
		this.cbJobCategory.Size = new System.Drawing.Size(250, 26);
		this.cbJobCategory.TabIndex = 4;
		this.button1.Location = new System.Drawing.Point(293, 230);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(61, 26);
		this.button1.TabIndex = 2;
		this.button1.TabStop = false;
		this.button1.Text = "add";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(btnSave_Click);
		this.button2.Location = new System.Drawing.Point(760, 231);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(61, 26);
		this.button2.TabIndex = 2;
		this.button2.TabStop = false;
		this.button2.Text = "add";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(btnSave_Click);
		this.cbDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbDesignation.FormattingEnabled = true;
		this.cbDesignation.Items.AddRange(new object[3] { "Single", "Married", "Widow" });
		this.cbDesignation.Location = new System.Drawing.Point(431, 231);
		this.cbDesignation.Name = "cbDesignation";
		this.cbDesignation.Size = new System.Drawing.Size(318, 26);
		this.cbDesignation.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(870, 577);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmEmployeesInfo";
		this.Text = "Employees Information";
		base.Load += new System.EventHandler(frmEmployeesInfo_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numSalary).EndInit();
		base.ResumeLayout(false);
	}
}
