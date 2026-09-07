using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmAgentInfo : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private Label lblMessage;

	private Button btnSave;

	private TextBox txtID;

	private Label label9;

	private Label label1;

	private TextBox txtContactno;

	private TextBox txtFullname;

	private ComboBox cborecordstatus;

	private Label label2;

	public Agent agent { get; set; }

	public bool commitchanges { get; set; }

	public frmAgentInfo(Agent agent = null)
	{
		InitializeComponent();
		if (agent != null)
		{
			this.agent = agent;
		}
		else
		{
			this.agent = new Agent();
		}
		bindData();
	}

	private void bindData()
	{
		txtID.DataBindings.Add("Text", agent, "id     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtFullname.DataBindings.Add("Text", agent, "fullname     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txtContactno.DataBindings.Add("Text", agent, "contactno     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cborecordstatus.DataBindings.Add("Text", agent, "recordstatus     ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Agent agent = this.agent;
		if (agent.id == 0)
		{
			using (AgentsCtrl agentsCtrl = new AgentsCtrl())
			{
				int num = agentsCtrl.add(agent);
				if (num > 0)
				{
					this.agent.id = num;
					txtID.Text = string.Concat(num);
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
		using AgentsCtrl agentsCtrl = new AgentsCtrl();
		int num = agentsCtrl.edit(agent);
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblMessage = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.txtID = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtContactno = new System.Windows.Forms.TextBox();
		this.txtFullname = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cborecordstatus = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cborecordstatus);
		this.groupBox1.Controls.Add(this.lblMessage);
		this.groupBox1.Controls.Add(this.btnSave);
		this.groupBox1.Controls.Add(this.txtID);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtContactno);
		this.groupBox1.Controls.Add(this.txtFullname);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(755, 328);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Agent/Dicer Information";
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
		this.txtID.Location = new System.Drawing.Point(676, 2);
		this.txtID.Name = "txtID";
		this.txtID.ReadOnly = true;
		this.txtID.Size = new System.Drawing.Size(61, 22);
		this.txtID.TabIndex = 0;
		this.txtID.TabStop = false;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(32, 87);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(104, 17);
		this.label9.TabIndex = 1;
		this.label9.Text = "Tel/CP Number";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(30, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(121, 17);
		this.label1.TabIndex = 1;
		this.label1.Text = "Agent/Dicer name";
		this.txtContactno.Location = new System.Drawing.Point(35, 109);
		this.txtContactno.Name = "txtContactno";
		this.txtContactno.Size = new System.Drawing.Size(223, 22);
		this.txtContactno.TabIndex = 7;
		this.txtFullname.Location = new System.Drawing.Point(34, 57);
		this.txtFullname.Name = "txtFullname";
		this.txtFullname.Size = new System.Drawing.Size(684, 22);
		this.txtFullname.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(32, 140);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(102, 17);
		this.label2.TabIndex = 1;
		this.label2.Text = "Record Status:";
		this.cborecordstatus.FormattingEnabled = true;
		this.cborecordstatus.Items.AddRange(new object[2] { "active", "in-active" });
		this.cborecordstatus.Location = new System.Drawing.Point(35, 161);
		this.cborecordstatus.Name = "cborecordstatus";
		this.cborecordstatus.Size = new System.Drawing.Size(223, 24);
		this.cborecordstatus.TabIndex = 9;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(779, 361);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmAgentInfo";
		this.Text = "frmAgentInfo";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
