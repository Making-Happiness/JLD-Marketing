using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmClientinfo : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private ComboBox cboGender;

	private DateTimePicker dtpDateofbirth;

	private Label label4;

	private Label label9;

	private Label label3;

	private Label label8;

	private Label label6;

	private Label label5;

	private Label label2;

	private Label label1;

	private TextBox txtSpousename;

	private TextBox txtPlaceofbirth;

	private TextBox txtContactno;

	private TextBox txtMiddlename;

	private TextBox txtLastname;

	private TextBox txtFirstname;

	private Button btnSave;

	private TextBox txtID;

	private TextBox txtDateofbirth;

	private Label lblMessage;

	public Client client { get; set; }

	public bool commitchanges { get; set; }

	public frmClientinfo(Client client = null)
	{
		InitializeComponent();
		lblMessage.Font = new Font(Font.FontFamily, Font.Size + 1f);
		if (client != null)
		{
			this.client = client;
		}
		else
		{
			this.client = new Client();
		}
		bindData();
	}

	private void bindData()
	{
		txtID.DataBindings.Add("Text", client, "idclients     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtFirstname.DataBindings.Add("Text", client, "firstname     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtLastname.DataBindings.Add("Text", client, "lastname      ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtMiddlename.DataBindings.Add("Text", client, "middlename    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboGender.DataBindings.Add("Text", client, "gender        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtDateofbirth.DataBindings.Add("Text", client, "dateofbirth   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtPlaceofbirth.DataBindings.Add("Text", client, "placeofbirth  ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtSpousename.DataBindings.Add("Text", client, "spousename    ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtContactno.DataBindings.Add("Text", client, "contactno     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Client client = this.client;
		if (client.idclients == 0)
		{
			using (ClientsCtrl newClientsCtrl = new ClientsCtrl())
			{
				int newClientId = newClientsCtrl.add(client);
				if (newClientId > 0)
				{
					this.client.idclients = newClientId;
					txtID.Text = string.Concat(newClientId);
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
		using ClientsCtrl clientsCtrl = new ClientsCtrl();
		int num = clientsCtrl.edit(client);
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

	private void txtDateofbirth_TextChanged(object sender, EventArgs e)
	{
		try
		{
			dtpDateofbirth.Value = Convert.ToDateTime(txtDateofbirth.Text);
		}
		catch (Exception)
		{
		}
	}

	private void dtpDateofbirth_ValueChanged(object sender, EventArgs e)
	{
		txtDateofbirth.Text = dtpDateofbirth.Value.ToString("yyyy-MM-dd");
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
		this.lblMessage = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.dtpDateofbirth = new System.Windows.Forms.DateTimePicker();
		this.cboGender = new System.Windows.Forms.ComboBox();
		this.txtID = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSpousename = new System.Windows.Forms.TextBox();
		this.txtPlaceofbirth = new System.Windows.Forms.TextBox();
		this.txtContactno = new System.Windows.Forms.TextBox();
		this.txtMiddlename = new System.Windows.Forms.TextBox();
		this.txtLastname = new System.Windows.Forms.TextBox();
		this.txtFirstname = new System.Windows.Forms.TextBox();
		this.txtDateofbirth = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.lblMessage);
		this.groupBox1.Controls.Add(this.btnSave);
		this.groupBox1.Controls.Add(this.dtpDateofbirth);
		this.groupBox1.Controls.Add(this.cboGender);
		this.groupBox1.Controls.Add(this.txtID);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtSpousename);
		this.groupBox1.Controls.Add(this.txtPlaceofbirth);
		this.groupBox1.Controls.Add(this.txtContactno);
		this.groupBox1.Controls.Add(this.txtMiddlename);
		this.groupBox1.Controls.Add(this.txtLastname);
		this.groupBox1.Controls.Add(this.txtFirstname);
		this.groupBox1.Controls.Add(this.txtDateofbirth);
		this.groupBox1.Location = new System.Drawing.Point(14, 14);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(755, 328);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Personal Information";
		this.lblMessage.AutoSize = true;
		this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMessage.Location = new System.Drawing.Point(32, 249);
		this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(81, 18);
		this.lblMessage.TabIndex = 8;
		this.lblMessage.Text = "Message:";
		this.btnSave.Location = new System.Drawing.Point(639, 284);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(110, 38);
		this.btnSave.TabIndex = 2;
		this.btnSave.TabStop = false;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.dtpDateofbirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDateofbirth.Location = new System.Drawing.Point(34, 108);
		this.dtpDateofbirth.Name = "dtpDateofbirth";
		this.dtpDateofbirth.Size = new System.Drawing.Size(223, 24);
		this.dtpDateofbirth.TabIndex = 3;
		this.dtpDateofbirth.ValueChanged += new System.EventHandler(dtpDateofbirth_ValueChanged);
		this.cboGender.FormattingEnabled = true;
		this.cboGender.Items.AddRange(new object[2] { "Male", "Female" });
		this.cboGender.Location = new System.Drawing.Point(264, 106);
		this.cboGender.Name = "cboGender";
		this.cboGender.Size = new System.Drawing.Size(223, 26);
		this.cboGender.TabIndex = 4;
		this.txtID.Location = new System.Drawing.Point(676, 2);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 24);
		this.txtID.TabIndex = 0;
		this.txtID.TabStop = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(30, 86);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(93, 18);
		this.label4.TabIndex = 1;
		this.label4.Text = "Date Of Birth";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(492, 187);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(110, 18);
		this.label9.TabIndex = 1;
		this.label9.Text = "Tel/CP Number";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(492, 35);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(164, 18);
		this.label3.TabIndex = 1;
		this.label3.Text = "Middle Name (Optional)";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(30, 187);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(103, 18);
		this.label8.TabIndex = 1;
		this.label8.Text = "Spouse Name";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(30, 136);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(96, 18);
		this.label6.TabIndex = 1;
		this.label6.Text = "Place of Birth";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(261, 86);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(57, 18);
		this.label5.TabIndex = 1;
		this.label5.Text = "Gender";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(261, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(80, 18);
		this.label2.TabIndex = 1;
		this.label2.Text = "Last Name";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(30, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(81, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "First Name";
		this.txtSpousename.Location = new System.Drawing.Point(34, 209);
		this.txtSpousename.Name = "txtSpousename";
		this.txtSpousename.Size = new System.Drawing.Size(454, 24);
		this.txtSpousename.TabIndex = 6;
		this.txtPlaceofbirth.Location = new System.Drawing.Point(34, 159);
		this.txtPlaceofbirth.Name = "txtPlaceofbirth";
		this.txtPlaceofbirth.Size = new System.Drawing.Size(685, 24);
		this.txtPlaceofbirth.TabIndex = 5;
		this.txtContactno.Location = new System.Drawing.Point(495, 209);
		this.txtContactno.Name = "txtContactno";
		this.txtContactno.Size = new System.Drawing.Size(223, 24);
		this.txtContactno.TabIndex = 7;
		this.txtMiddlename.Location = new System.Drawing.Point(495, 57);
		this.txtMiddlename.Name = "txtMiddlename";
		this.txtMiddlename.Size = new System.Drawing.Size(223, 24);
		this.txtMiddlename.TabIndex = 2;
		this.txtLastname.Location = new System.Drawing.Point(264, 57);
		this.txtLastname.Name = "txtLastname";
		this.txtLastname.Size = new System.Drawing.Size(223, 24);
		this.txtLastname.TabIndex = 1;
		this.txtFirstname.Location = new System.Drawing.Point(34, 57);
		this.txtFirstname.Name = "txtFirstname";
		this.txtFirstname.Size = new System.Drawing.Size(223, 24);
		this.txtFirstname.TabIndex = 0;
		this.txtDateofbirth.Location = new System.Drawing.Point(35, 83);
		this.txtDateofbirth.Name = "txtDateofbirth";
		this.txtDateofbirth.ReadOnly = true;
		this.txtDateofbirth.Size = new System.Drawing.Size(223, 24);
		this.txtDateofbirth.TabIndex = 0;
		this.txtDateofbirth.Visible = false;
		this.txtDateofbirth.TextChanged += new System.EventHandler(txtDateofbirth_TextChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(781, 354);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmClientinfo";
		this.Text = "Stakeholder Information";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
