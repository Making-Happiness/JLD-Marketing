using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmExpensesInfo : Form
{
	public string transaction = "ADD";

	private IContainer components = null;

	private NumericUpDown numAmount;

	private Label label3;

	private ComboBox cboDescription;

	private ComboBox cboReceiveBy;

	private Label label2;

	private Label label1;

	private Button btnSave;

	private Label label4;

	private ComboBox cboPurpose;

	private Label lblMessage;

	public Expense_view expense_view { get; set; }

	public DataTable employeeTable { get; set; }

	public bool commitchanges { get; set; }

	public frmExpensesInfo()
	{
		InitializeComponent();
		commitchanges = false;
	}

	private void binddata()
	{
		cboDescription.DataBindings.Add("Text", expense_view, "description        ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboPurpose.DataBindings.Add("Text", expense_view, "purpose   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cboReceiveBy.DataBindings.Add("SelectedValue", expense_view, "receiveby   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		numAmount.DataBindings.Add("Value", expense_view, "amount   ".Trim(), formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void initializeData()
	{
		cboReceiveBy.DataSource = employeeTable;
		cboReceiveBy.DisplayMember = "fullname";
		cboReceiveBy.ValueMember = "idemployee";
		cboReceiveBy.AutoCompleteSource = AutoCompleteSource.ListItems;
		cboReceiveBy.AutoCompleteMode = AutoCompleteMode.Suggest;
	}

	private void frmExpensesInfo_Load(object sender, EventArgs e)
	{
		if (transaction == "ADD")
		{
			expense_view = new Expense_view();
		}
		initializeData();
		binddata();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (transaction == "ADD")
		{
			using (ExpensesCtrl newExpensesCtrl = new ExpensesCtrl())
			{
				expense_view.daterelease = DateTime.Now;
				int num = newExpensesCtrl.add(expense_view);
				if (num > 0)
				{
					commitchanges = true;
					expense_view.id = num;
					lblMessage.ForeColor = Color.Green;
					lblMessage.Text = "Message:\n\tAdding records successfull.";
				}
				else
				{
					commitchanges = false;
					lblMessage.ForeColor = Color.Red;
					lblMessage.Text = "Message:\n\tAdding records failed!";
				}
				return;
			}
		}
		if (!(transaction == "EDIT"))
		{
			return;
		}
		using ExpensesCtrl expensesCtrl = new ExpensesCtrl();
		int num2 = expensesCtrl.edit(expense_view);
		if (num2 > 0)
		{
			commitchanges = true;
			lblMessage.ForeColor = Color.Green;
			lblMessage.Text = "Message:\n\tAdding records successfull.";
		}
		else
		{
			commitchanges = false;
			lblMessage.ForeColor = Color.Red;
			lblMessage.Text = "Message:\n\tAdding records failed!";
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
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.cboDescription = new System.Windows.Forms.ComboBox();
		this.cboReceiveBy = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnSave = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.cboPurpose = new System.Windows.Forms.ComboBox();
		this.lblMessage = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		base.SuspendLayout();
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.numAmount.Location = new System.Drawing.Point(124, 117);
		this.numAmount.Maximum = new decimal(new int[4] { 1000000001, 0, 0, 131072 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(151, 24);
		this.numAmount.TabIndex = 2;
		this.numAmount.ThousandsSeparator = true;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(53, 119);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(63, 18);
		this.label3.TabIndex = 18;
		this.label3.Text = "Amount:";
		this.cboDescription.BackColor = System.Drawing.Color.White;
		this.cboDescription.DisplayMember = "description";
		this.cboDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
		this.cboDescription.FormattingEnabled = true;
		this.cboDescription.Location = new System.Drawing.Point(124, 13);
		this.cboDescription.Margin = new System.Windows.Forms.Padding(4);
		this.cboDescription.Name = "cboDescription";
		this.cboDescription.Size = new System.Drawing.Size(504, 26);
		this.cboDescription.TabIndex = 1;
		this.cboDescription.ValueMember = "description";
		this.cboReceiveBy.BackColor = System.Drawing.Color.White;
		this.cboReceiveBy.FormattingEnabled = true;
		this.cboReceiveBy.Location = new System.Drawing.Point(124, 80);
		this.cboReceiveBy.Margin = new System.Windows.Forms.Padding(4);
		this.cboReceiveBy.Name = "cboReceiveBy";
		this.cboReceiveBy.Size = new System.Drawing.Size(504, 26);
		this.cboReceiveBy.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(29, 16);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(87, 18);
		this.label2.TabIndex = 12;
		this.label2.Text = "Description:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(40, 83);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 18);
		this.label1.TabIndex = 13;
		this.label1.Text = "Receiveby:";
		this.btnSave.Location = new System.Drawing.Point(540, 186);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(88, 59);
		this.btnSave.TabIndex = 19;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(48, 50);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 18);
		this.label4.TabIndex = 13;
		this.label4.Text = "Purpose:";
		this.cboPurpose.BackColor = System.Drawing.Color.White;
		this.cboPurpose.DisplayMember = "purpose";
		this.cboPurpose.FormattingEnabled = true;
		this.cboPurpose.Location = new System.Drawing.Point(124, 47);
		this.cboPurpose.Margin = new System.Windows.Forms.Padding(4);
		this.cboPurpose.Name = "cboPurpose";
		this.cboPurpose.Size = new System.Drawing.Size(504, 26);
		this.cboPurpose.TabIndex = 0;
		this.cboPurpose.ValueMember = "purpose";
		this.lblMessage.AutoSize = true;
		this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMessage.Location = new System.Drawing.Point(35, 170);
		this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(81, 18);
		this.lblMessage.TabIndex = 20;
		this.lblMessage.Text = "Message:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(652, 257);
		base.Controls.Add(this.lblMessage);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.cboDescription);
		base.Controls.Add(this.cboPurpose);
		base.Controls.Add(this.cboReceiveBy);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "frmExpensesInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Expenses Information";
		base.Load += new System.EventHandler(frmExpensesInfo_Load);
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
