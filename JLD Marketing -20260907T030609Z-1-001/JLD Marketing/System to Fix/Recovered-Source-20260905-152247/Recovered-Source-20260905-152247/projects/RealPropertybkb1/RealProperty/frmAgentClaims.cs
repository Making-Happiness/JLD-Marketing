using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RealProperty;

public class frmAgentClaims : Form
{
	private IContainer components = null;

	private DataGridView DGV;

	private DataGridViewTextBoxColumn id_col;

	private DataGridViewTextBoxColumn fullname_col;

	private DataGridViewTextBoxColumn contactno_col;

	private DataGridViewTextBoxColumn recordstatus_col;

	private DataGridViewButtonColumn calims_col;

	private DataGridViewButtonColumn edit_col;

	private DataGridViewButtonColumn delete_col;

	private Label label1;

	private Label lblAgentName;

	private Label label2;

	private Label label5;

	private Button button1;

	private Button button2;

	public frmAgentClaims()
	{
		InitializeComponent();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.id_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contactno_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatus_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.calims_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.edit_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.delete_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.label1 = new System.Windows.Forms.Label();
		this.lblAgentName = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		base.SuspendLayout();
		this.DGV.AllowUserToAddRows = false;
		this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV.Columns.AddRange(this.id_col, this.fullname_col, this.contactno_col, this.recordstatus_col, this.calims_col, this.edit_col, this.delete_col);
		this.DGV.Location = new System.Drawing.Point(14, 79);
		this.DGV.MultiSelect = false;
		this.DGV.Name = "DGV";
		this.DGV.ReadOnly = true;
		this.DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGV.RowHeadersWidth = 20;
		this.DGV.RowTemplate.Height = 24;
		this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGV.Size = new System.Drawing.Size(973, 460);
		this.DGV.TabIndex = 10;
		this.id_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.id_col.DataPropertyName = "id";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.id_col.DefaultCellStyle = dataGridViewCellStyle;
		this.id_col.HeaderText = "ID";
		this.id_col.Name = "id_col";
		this.id_col.ReadOnly = true;
		this.id_col.Visible = false;
		this.fullname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.fullname_col.DataPropertyName = "fullname";
		this.fullname_col.HeaderText = "Agent/Dicer Name";
		this.fullname_col.Name = "fullname_col";
		this.fullname_col.ReadOnly = true;
		this.contactno_col.DataPropertyName = "contactno";
		this.contactno_col.HeaderText = "ContactNo";
		this.contactno_col.Name = "contactno_col";
		this.contactno_col.ReadOnly = true;
		this.recordstatus_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.recordstatus_col.DataPropertyName = "recordstatus";
		this.recordstatus_col.HeaderText = "status";
		this.recordstatus_col.Name = "recordstatus_col";
		this.recordstatus_col.ReadOnly = true;
		this.recordstatus_col.Width = 77;
		this.calims_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(255, 255, 192);
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
		this.calims_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.calims_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.calims_col.HeaderText = "Claims";
		this.calims_col.Name = "calims_col";
		this.calims_col.ReadOnly = true;
		this.calims_col.Text = "claims";
		this.calims_col.UseColumnTextForButtonValue = true;
		this.calims_col.Width = 60;
		this.edit_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
		this.edit_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.edit_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.edit_col.HeaderText = "Edit";
		this.edit_col.Name = "edit_col";
		this.edit_col.ReadOnly = true;
		this.edit_col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.edit_col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.edit_col.Text = "Edit";
		this.edit_col.UseColumnTextForButtonValue = true;
		this.edit_col.Width = 62;
		this.delete_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
		this.delete_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.delete_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.delete_col.HeaderText = "Delete";
		this.delete_col.Name = "delete_col";
		this.delete_col.ReadOnly = true;
		this.delete_col.Text = "Delete";
		this.delete_col.UseColumnTextForButtonValue = true;
		this.delete_col.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(14, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(110, 18);
		this.label1.TabIndex = 11;
		this.label1.Text = "AGENT NAME:";
		this.lblAgentName.AutoSize = true;
		this.lblAgentName.Location = new System.Drawing.Point(136, 45);
		this.lblAgentName.Name = "lblAgentName";
		this.lblAgentName.Size = new System.Drawing.Size(110, 18);
		this.lblAgentName.TabIndex = 11;
		this.lblAgentName.Text = "AGENT NAME:";
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(9, 560);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(371, 33);
		this.label2.TabIndex = 11;
		this.label2.Text = "Total Commision: 0,000,000.00";
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(533, 556);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(454, 29);
		this.label5.TabIndex = 11;
		this.label5.Text = "Remaining Claims: 0.00";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button1.Location = new System.Drawing.Point(386, 560);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(111, 29);
		this.button1.TabIndex = 12;
		this.button1.Text = "View Details";
		this.button1.UseVisualStyleBackColor = true;
		this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.Location = new System.Drawing.Point(803, 37);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(184, 36);
		this.button2.TabIndex = 12;
		this.button2.Text = "ADD CLAIMS";
		this.button2.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1012, 616);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.lblAgentName);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.DGV);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmAgentClaims";
		this.Text = "frmAgentClaims";
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
