using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmPayrolldeductionEditAmount : Form
{
	private IContainer components = null;

	private Button btnSave;

	private NumericUpDown numAmount;

	private Label label2;

	private Label label1;

	private Label lblDescription;

	public Payrolldeduction payrolldeduction { get; set; }

	public bool commitchanges { get; set; }

	public frmPayrolldeductionEditAmount()
	{
		InitializeComponent();
	}

	private void bindData()
	{
		numAmount.DataBindings.Add("Value", payrolldeduction, "amount", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		lblDescription.DataBindings.Add("Text", payrolldeduction, "description", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		int num = payrollCtrl.edit_payrolldeduction(payrolldeduction);
		if (num > 0)
		{
			commitchanges = true;
			Close();
		}
		else
		{
			MessageBox.Show("Changes failed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			commitchanges = false;
		}
	}

	private void frmPayrolldeductionEditAmount_Load(object sender, EventArgs e)
	{
		bindData();
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
		this.btnSave = new System.Windows.Forms.Button();
		this.numAmount = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.lblDescription = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.numAmount).BeginInit();
		base.SuspendLayout();
		this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSave.Location = new System.Drawing.Point(250, 156);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(110, 58);
		this.btnSave.TabIndex = 17;
		this.btnSave.Text = "SAVE CHANGES";
		this.btnSave.UseVisualStyleBackColor = false;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.numAmount.DecimalPlaces = 2;
		this.numAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.numAmount.Location = new System.Drawing.Point(53, 89);
		this.numAmount.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.numAmount.Name = "numAmount";
		this.numAmount.Size = new System.Drawing.Size(307, 34);
		this.numAmount.TabIndex = 19;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(50, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(86, 25);
		this.label2.TabIndex = 18;
		this.label2.Text = "Amount:";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(157, 25);
		this.label1.TabIndex = 20;
		this.label1.Text = "Title Description:";
		this.lblDescription.AutoSize = true;
		this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblDescription.Location = new System.Drawing.Point(175, 9);
		this.lblDescription.Name = "lblDescription";
		this.lblDescription.Size = new System.Drawing.Size(33, 25);
		this.lblDescription.TabIndex = 18;
		this.lblDescription.Text = "ca";
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(384, 226);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.numAmount);
		base.Controls.Add(this.lblDescription);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.btnSave);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmPayrolldeductionEditAmount";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Payroll Deduction Edit Amount";
		base.Load += new System.EventHandler(frmPayrolldeductionEditAmount_Load);
		((System.ComponentModel.ISupportInitialize)this.numAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
