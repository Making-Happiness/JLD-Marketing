using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RealProperty;

public class frmSearchPayments : Form
{
	private IContainer components = null;

	private TextBox textBox1;

	private ComboBox cbofilter;

	private Label label1;

	private DataGridView DGV;

	private DataGridViewButtonColumn remove_col;

	private DataTable paymentstable { get; set; }

	public frmSearchPayments()
	{
		InitializeComponent();
	}

	private void frmSearchPayments_Load(object sender, EventArgs e)
	{
		cbofilter.SelectedIndex = 0;
		paymentstable = Program.fmain.view_paymentsTable;
		DGV_refresh();
	}

	private void DGV_refresh()
	{
		DGV.DataSource = paymentstable;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.cbofilter = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.remove_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		base.SuspendLayout();
		this.textBox1.Location = new System.Drawing.Point(12, 66);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(801, 24);
		this.textBox1.TabIndex = 0;
		this.cbofilter.BackColor = System.Drawing.SystemColors.HighlightText;
		this.cbofilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbofilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cbofilter.ForeColor = System.Drawing.SystemColors.WindowText;
		this.cbofilter.FormattingEnabled = true;
		this.cbofilter.Items.AddRange(new object[3] { "Payment Ref", "Client Lastname", "Client First Name" });
		this.cbofilter.Location = new System.Drawing.Point(12, 34);
		this.cbofilter.Name = "cbofilter";
		this.cbofilter.Size = new System.Drawing.Size(230, 26);
		this.cbofilter.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 13);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 18);
		this.label1.TabIndex = 2;
		this.label1.Text = "Search By:";
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.remove_col);
		this.DGV.Location = new System.Drawing.Point(12, 106);
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersWidth = 21;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.Size = new System.Drawing.Size(801, 435);
		this.DGV.TabIndex = 3;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.remove_col.DefaultCellStyle = dataGridViewCellStyle;
		this.remove_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.remove_col.HeaderText = "Action";
		this.remove_col.Name = "remove_col";
		this.remove_col.ReadOnly = true;
		this.remove_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.remove_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.remove_col.Text = "Remove";
		this.remove_col.UseColumnTextForButtonValue = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(834, 590);
		base.Controls.Add(this.DGV);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.cbofilter);
		base.Controls.Add(this.textBox1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmSearchPayments";
		this.Text = "frmSearchPayments";
		base.Load += new System.EventHandler(frmSearchPayments_Load);
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
