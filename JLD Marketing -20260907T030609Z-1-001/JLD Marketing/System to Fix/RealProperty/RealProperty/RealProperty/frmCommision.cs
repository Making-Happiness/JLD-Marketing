using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty;

public class frmCommision : Form
{
	private DataTable dt_source;

	private object totalclaims;

	private object totalrelease;

	private IContainer components = null;

	private DataGridView DGVDicer;

	private SplitContainer splitContainer1;

	private TextBox txtSearch;

	private Label label1;

	private DataGridView dgvDicePayment;

	private DataGridViewTextBoxColumn iddicer_col;

	private DataGridViewTextBoxColumn fullname_col;

	private SplitContainer splitContainer2;

	private Label lblTotalClaims;

	private Label lblEstimatedClaims;

	private DataGridViewTextBoxColumn id_col;

	private DataGridViewTextBoxColumn description_col;

	private DataGridViewTextBoxColumn lotprice_col;

	private DataGridViewTextBoxColumn amount_col;

	private DataGridViewTextBoxColumn idagent_col;

	private DataGridViewTextBoxColumn agentpercentage_col;

	private DataGridViewTextBoxColumn otherfees_col;

	private DataGridViewTextBoxColumn penalty_col;

	private DataGridViewTextBoxColumn claim_amount_col;

	private DataGridViewTextBoxColumn estimated_claims_col;

	private DataGridView DGV_release_claims;

	private Button btnNew;

	private Label label2;

	private BindingSource claimBindingSource;

	private Label lblTotalRelease;

	private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn idagentDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn dateofclaimDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn claimbyDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;

	private DataGridViewTextBoxColumn recordstatusDataGridViewTextBoxColumn;

	private DataGridViewButtonColumn print_col;

	private DataGridViewButtonColumn remove_col;

	public frmCommision()
	{
		InitializeComponent();
		DGVDicer.AutoGenerateColumns = false;
		dgvDicePayment.AutoGenerateColumns = false;
	}

	public int? TargetAgentId { get; set; }

	private void frmCommision_Load(object sender, EventArgs e)
	{
		this.Text = "Commissions & Claims Center";
		DGVDicer_refresh();
		if (TargetAgentId.HasValue && DGVDicer.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in DGVDicer.Rows)
			{
				if (row.Cells[iddicer_col.Name].Value != null && Convert.ToInt32(row.Cells[iddicer_col.Name].Value) == TargetAgentId.Value)
				{
					row.Selected = true;
					DGVDicer.CurrentCell = row.Cells[fullname_col.Name];
					break;
				}
			}
		}
		if (DGVDicer.CurrentRow != null)
		{
			int iddicer = (int)DGVDicer.CurrentRow.Cells[iddicer_col.Name].Value;
			dgvDicePayment_refresh(iddicer);
			DGV_release_claims_refresh(iddicer);
		}
	}

	private void DGV_release_claims_refresh(int iddicer = 0)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable dataTable = payrollCtrl.get_view_releasedclaims(iddicer);
		lblTotalRelease.Text = string.Format("Total Amount Release : {0}", "0.00");
		if (iddicer > 0)
		{
			set_dgv_source(DGV_release_claims, dataTable);
		}
		else
		{
			set_dgv_source(DGV_release_claims, dataTable.Clone());
		}
		totalrelease = dataTable.Compute("SUM(amount)", "");
		if (totalrelease != DBNull.Value)
		{
			lblTotalRelease.Text = string.Format("Total Amount Release : {0}", ((decimal)totalrelease).ToString("#,##0.00"));
		}
	}

	private void set_dgv_source(DataGridView dgv, DataTable dt)
	{
		if (dgvDicePayment.InvokeRequired)
		{
			dgv.Invoke((MethodInvoker)delegate
			{
				set_dgv_source(dgv, dt);
			});
		}
		else
		{
			dgv.DataSource = dt;
		}
	}

	private void dgvDicePayment_refresh(int iddicer = 0)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		DataTable dataTable = payrollCtrl.get_view_paidbyclient_dice(iddicer);
		lblTotalClaims.Text = string.Format("Total Claims: {0}", "0.00");
		lblEstimatedClaims.Text = string.Format("Total Estimated Claims: {0}", "0.00");
		if (iddicer > 0)
		{
			set_dgv_source(dgvDicePayment, dataTable);
		}
		else
		{
			set_dgv_source(dgvDicePayment, dataTable.Clone());
		}
		totalclaims = dataTable.Compute("SUM(claim_amount)", "");
		if (totalclaims != DBNull.Value)
		{
			lblTotalClaims.Text = string.Format("Total Claims: {0}", ((decimal)totalclaims).ToString("#,##0.00"));
		}
		object obj = dataTable.Compute("SUM(estimated_claims)", "");
		if (obj != DBNull.Value)
		{
			lblEstimatedClaims.Text = string.Format("Total Estimated Claims: {0}", ((decimal)obj).ToString("#,##0.00"));
		}
	}

	private void DGVDicer_refresh(string filter = null)
	{
		using PayrollCtrl payrollCtrl = new PayrollCtrl();
		if (filter == null)
		{
			dt_source = payrollCtrl.getDIcerTableView();
			DGVDicer.DataSource = dt_source;
			return;
		}
		if (dt_source == null)
		{
			dt_source = payrollCtrl.getDIcerTableView();
		}
		if (filter == "")
		{
			DGVDicer.DataSource = dt_source;
			return;
		}
		string filterExpression = $"fullname like '%{filter}%'";
		DataRow[] source = dt_source.Select(filterExpression);
		if (source.Count() <= 0)
		{
			DataTable dataTable = dt_source.Clone();
			dataTable.Clear();
			DGVDicer.DataSource = dataTable;
		}
		else
		{
			DGVDicer.DataSource = source.CopyToDataTable();
		}
	}

	private void txtSearch_TextChanged(object sender, EventArgs e)
	{
		DGVDicer_refresh(txtSearch.Text);
	}

	private void DGVDicer_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (rowIndex >= 0)
		{
			int iddicer = (int)DGVDicer.CurrentRow.Cells[iddicer_col.Name].Value;
			dgvDicePayment_refresh(iddicer);
			DGV_release_claims_refresh(iddicer);
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		using frmClaimReleaseInfo frmClaimReleaseInfo2 = new frmClaimReleaseInfo();
		int num = (int)DGVDicer.CurrentRow.Cells[iddicer_col.Name].Value;
		Claim claim = new Claim();
		claim.idagent = num;
		frmClaimReleaseInfo2.claim = claim;
		frmClaimReleaseInfo2.Transaction = "ADD";
		frmClaimReleaseInfo2.amount_to_claim = ((totalclaims != DBNull.Value) ? ((decimal)totalclaims) : 0m) - ((totalrelease != DBNull.Value) ? ((decimal)totalrelease) : 0m);
		frmClaimReleaseInfo2.ShowDialog(this);
		if (frmClaimReleaseInfo2.CommitChanges)
		{
			DGV_release_claims_refresh(num);
		}
	}

	private void DGV_release_claims_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		object value = DGVDicer.CurrentRow.Cells[iddicer_col.Name].Value;
		dynamic value2 = DGV_release_claims.CurrentRow.Cells[idDataGridViewTextBoxColumn.Name].Value;
		if (columnIndex != DGV_release_claims.Columns[remove_col.Name].Index)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("Do you wnat to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.No)
		{
			using (AgentsCtrl agentsCtrl = new AgentsCtrl())
			{
				Claim claimbyID = agentsCtrl.getClaimbyID((int)value2);
				claimbyID.recordstatus = "deleted";
				int num = agentsCtrl.edit_claim(claimbyID);
				DGV_release_claims_refresh(claimbyID.idagent);
			}
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGVDicer = new System.Windows.Forms.DataGridView();
		this.iddicer_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullname_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSearch = new System.Windows.Forms.TextBox();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.lblEstimatedClaims = new System.Windows.Forms.Label();
		this.lblTotalClaims = new System.Windows.Forms.Label();
		this.dgvDicePayment = new System.Windows.Forms.DataGridView();
		this.id_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.description_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lotprice_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idagent_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.agentpercentage_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.otherfees_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.penalty_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.claim_amount_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estimated_claims_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnNew = new System.Windows.Forms.Button();
		this.lblTotalRelease = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.DGV_release_claims = new System.Windows.Forms.DataGridView();
		this.claimBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idagentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fullnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dateofclaimDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.claimbyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recordstatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.print_col = new System.Windows.Forms.DataGridViewButtonColumn();
		this.remove_col = new System.Windows.Forms.DataGridViewButtonColumn();
		((System.ComponentModel.ISupportInitialize)this.DGVDicer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDicePayment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGV_release_claims).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.claimBindingSource).BeginInit();
		base.SuspendLayout();
		this.DGVDicer.AllowUserToAddRows = false;
		this.DGVDicer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGVDicer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGVDicer.Columns.AddRange(this.iddicer_col, this.fullname_col);
		this.DGVDicer.Location = new System.Drawing.Point(3, 43);
		this.DGVDicer.MultiSelect = false;
		this.DGVDicer.Name = "DGVDicer";
		this.DGVDicer.ReadOnly = true;
		this.DGVDicer.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.DGVDicer.RowHeadersWidth = 20;
		this.DGVDicer.RowTemplate.Height = 24;
		this.DGVDicer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGVDicer.Size = new System.Drawing.Size(404, 679);
		this.DGVDicer.TabIndex = 17;
		this.DGVDicer.TabStop = false;
		this.DGVDicer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVDicer_CellClick);
		this.iddicer_col.DataPropertyName = "id";
		this.iddicer_col.HeaderText = "iddicer";
		this.iddicer_col.Name = "iddicer_col";
		this.iddicer_col.ReadOnly = true;
		this.iddicer_col.Visible = false;
		this.fullname_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.fullname_col.DataPropertyName = "fullname";
		this.fullname_col.HeaderText = "Name";
		this.fullname_col.Name = "fullname_col";
		this.fullname_col.ReadOnly = true;
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer1.Location = new System.Drawing.Point(12, 3);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.label1);
		this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
		this.splitContainer1.Panel1.Controls.Add(this.DGVDicer);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(1228, 725);
		this.splitContainer1.SplitterDistance = 409;
		this.splitContainer1.TabIndex = 18;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(3, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 18);
		this.label1.TabIndex = 19;
		this.label1.Text = "Search";
		this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSearch.Location = new System.Drawing.Point(69, 9);
		this.txtSearch.Name = "txtSearch";
		this.txtSearch.Size = new System.Drawing.Size(293, 28);
		this.txtSearch.TabIndex = 18;
		this.txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
		this.splitContainer2.Panel1.Controls.Add(this.lblEstimatedClaims);
		this.splitContainer2.Panel1.Controls.Add(this.lblTotalClaims);
		this.splitContainer2.Panel1.Controls.Add(this.dgvDicePayment);
		this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.splitContainer2.Panel2.Controls.Add(this.btnNew);
		this.splitContainer2.Panel2.Controls.Add(this.lblTotalRelease);
		this.splitContainer2.Panel2.Controls.Add(this.label2);
		this.splitContainer2.Panel2.Controls.Add(this.DGV_release_claims);
		this.splitContainer2.Size = new System.Drawing.Size(815, 725);
		this.splitContainer2.SplitterDistance = 361;
		this.splitContainer2.TabIndex = 1;
		this.lblEstimatedClaims.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblEstimatedClaims.AutoSize = true;
		this.lblEstimatedClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEstimatedClaims.Location = new System.Drawing.Point(12, 326);
		this.lblEstimatedClaims.Name = "lblEstimatedClaims";
		this.lblEstimatedClaims.Size = new System.Drawing.Size(318, 29);
		this.lblEstimatedClaims.TabIndex = 2;
		this.lblEstimatedClaims.Text = "Total Estimated Claims: 0.00";
		this.lblEstimatedClaims.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblTotalClaims.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotalClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalClaims.Location = new System.Drawing.Point(467, 322);
		this.lblTotalClaims.Name = "lblTotalClaims";
		this.lblTotalClaims.Size = new System.Drawing.Size(343, 38);
		this.lblTotalClaims.TabIndex = 1;
		this.lblTotalClaims.Text = "Total Claims: 0.00";
		this.lblTotalClaims.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dgvDicePayment.AllowUserToAddRows = false;
		this.dgvDicePayment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDicePayment.ColumnHeadersHeight = 32;
		this.dgvDicePayment.Columns.AddRange(this.id_col, this.description_col, this.lotprice_col, this.amount_col, this.idagent_col, this.agentpercentage_col, this.otherfees_col, this.penalty_col, this.claim_amount_col, this.estimated_claims_col);
		this.dgvDicePayment.Location = new System.Drawing.Point(10, 43);
		this.dgvDicePayment.Name = "dgvDicePayment";
		this.dgvDicePayment.RowHeadersWidth = 22;
		this.dgvDicePayment.RowTemplate.Height = 24;
		this.dgvDicePayment.Size = new System.Drawing.Size(802, 276);
		this.dgvDicePayment.TabIndex = 0;
		this.id_col.DataPropertyName = "id";
		this.id_col.HeaderText = "id";
		this.id_col.Name = "id_col";
		this.id_col.Visible = false;
		this.description_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.description_col.DataPropertyName = "description";
		this.description_col.HeaderText = "description";
		this.description_col.Name = "description_col";
		this.lotprice_col.DataPropertyName = "lotprice";
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle.Format = "N2";
		this.lotprice_col.DefaultCellStyle = dataGridViewCellStyle;
		this.lotprice_col.HeaderText = "lot Price";
		this.lotprice_col.Name = "lotprice_col";
		this.amount_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.amount_col.DataPropertyName = "amount";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		this.amount_col.DefaultCellStyle = dataGridViewCellStyle2;
		this.amount_col.HeaderText = "amount";
		this.amount_col.Name = "amount_col";
		this.amount_col.Width = 87;
		this.idagent_col.DataPropertyName = "idagent";
		this.idagent_col.HeaderText = "idagent";
		this.idagent_col.Name = "idagent_col";
		this.idagent_col.Visible = false;
		this.agentpercentage_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
		this.agentpercentage_col.DataPropertyName = "agentpercentage";
		this.agentpercentage_col.HeaderText = "%";
		this.agentpercentage_col.Name = "agentpercentage_col";
		this.agentpercentage_col.Width = 5;
		this.otherfees_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.otherfees_col.DataPropertyName = "otherfees";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		this.otherfees_col.DefaultCellStyle = dataGridViewCellStyle3;
		this.otherfees_col.HeaderText = "otherfees";
		this.otherfees_col.Name = "otherfees_col";
		this.otherfees_col.Width = 5;
		this.penalty_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.penalty_col.DataPropertyName = "penalty";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.penalty_col.DefaultCellStyle = dataGridViewCellStyle4;
		this.penalty_col.HeaderText = "penalty";
		this.penalty_col.Name = "penalty_col";
		this.penalty_col.Width = 5;
		this.claim_amount_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.claim_amount_col.DataPropertyName = "claim_amount";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		dataGridViewCellStyle5.Format = "N2";
		dataGridViewCellStyle5.NullValue = null;
		this.claim_amount_col.DefaultCellStyle = dataGridViewCellStyle5;
		this.claim_amount_col.HeaderText = "claims";
		this.claim_amount_col.Name = "claim_amount_col";
		this.claim_amount_col.Width = 80;
		this.estimated_claims_col.DataPropertyName = "estimated_claims";
		this.estimated_claims_col.HeaderText = "estimated_claims";
		this.estimated_claims_col.Name = "estimated_claims_col";
		this.estimated_claims_col.Visible = false;
		this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnNew.BackColor = System.Drawing.Color.Red;
		this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNew.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.btnNew.Location = new System.Drawing.Point(662, 2);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(147, 45);
		this.btnNew.TabIndex = 3;
		this.btnNew.Text = "New Release";
		this.btnNew.UseVisualStyleBackColor = false;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.lblTotalRelease.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotalRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalRelease.Location = new System.Drawing.Point(440, 321);
		this.lblTotalRelease.Name = "lblTotalRelease";
		this.lblTotalRelease.Size = new System.Drawing.Size(370, 38);
		this.lblTotalRelease.TabIndex = 1;
		this.lblTotalRelease.Text = "Total Released: 0.00";
		this.lblTotalRelease.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(5, 7);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(228, 29);
		this.label2.TabIndex = 2;
		this.label2.Text = "Claim Released List";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DGV_release_claims.AllowUserToAddRows = false;
		this.DGV_release_claims.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV_release_claims.AutoGenerateColumns = false;
		this.DGV_release_claims.ColumnHeadersHeight = 32;
		this.DGV_release_claims.Columns.AddRange(this.idDataGridViewTextBoxColumn, this.idagentDataGridViewTextBoxColumn, this.fullnameDataGridViewTextBoxColumn, this.dateofclaimDataGridViewTextBoxColumn, this.descriptionDataGridViewTextBoxColumn, this.claimbyDataGridViewTextBoxColumn, this.remarksDataGridViewTextBoxColumn, this.amountDataGridViewTextBoxColumn, this.recordstatusDataGridViewTextBoxColumn, this.print_col, this.remove_col);
		this.DGV_release_claims.DataSource = this.claimBindingSource;
		this.DGV_release_claims.Location = new System.Drawing.Point(6, 47);
		this.DGV_release_claims.Name = "DGV_release_claims";
		this.DGV_release_claims.RowHeadersWidth = 22;
		this.DGV_release_claims.RowTemplate.Height = 24;
		this.DGV_release_claims.Size = new System.Drawing.Size(802, 273);
		this.DGV_release_claims.TabIndex = 1;
		this.DGV_release_claims.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGV_release_claims_CellClick);
		this.claimBindingSource.DataSource = typeof(RealProperty.BEL.Claim);
		this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
		this.idDataGridViewTextBoxColumn.HeaderText = "id";
		this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
		this.idDataGridViewTextBoxColumn.Visible = false;
		this.idagentDataGridViewTextBoxColumn.DataPropertyName = "idagent";
		this.idagentDataGridViewTextBoxColumn.HeaderText = "idagent";
		this.idagentDataGridViewTextBoxColumn.Name = "idagentDataGridViewTextBoxColumn";
		this.idagentDataGridViewTextBoxColumn.Visible = false;
		this.fullnameDataGridViewTextBoxColumn.DataPropertyName = "fullname";
		this.fullnameDataGridViewTextBoxColumn.HeaderText = "dicer";
		this.fullnameDataGridViewTextBoxColumn.Name = "fullnameDataGridViewTextBoxColumn";
		this.dateofclaimDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.dateofclaimDataGridViewTextBoxColumn.DataPropertyName = "dateofclaim";
		this.dateofclaimDataGridViewTextBoxColumn.HeaderText = "dateofclaim";
		this.dateofclaimDataGridViewTextBoxColumn.Name = "dateofclaimDataGridViewTextBoxColumn";
		this.dateofclaimDataGridViewTextBoxColumn.Width = 5;
		this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
		this.descriptionDataGridViewTextBoxColumn.HeaderText = "description";
		this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
		this.descriptionDataGridViewTextBoxColumn.Visible = false;
		this.claimbyDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.claimbyDataGridViewTextBoxColumn.DataPropertyName = "claimby";
		this.claimbyDataGridViewTextBoxColumn.HeaderText = "claimby";
		this.claimbyDataGridViewTextBoxColumn.Name = "claimbyDataGridViewTextBoxColumn";
		this.remarksDataGridViewTextBoxColumn.DataPropertyName = "remarks";
		this.remarksDataGridViewTextBoxColumn.HeaderText = "remarks";
		this.remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
		this.amountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		this.amountDataGridViewTextBoxColumn.DataPropertyName = "amount";
		this.amountDataGridViewTextBoxColumn.HeaderText = "amount";
		this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
		this.amountDataGridViewTextBoxColumn.Width = 5;
		this.recordstatusDataGridViewTextBoxColumn.DataPropertyName = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.HeaderText = "recordstatus";
		this.recordstatusDataGridViewTextBoxColumn.Name = "recordstatusDataGridViewTextBoxColumn";
		this.recordstatusDataGridViewTextBoxColumn.Visible = false;
		this.print_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(1);
		this.print_col.DefaultCellStyle = dataGridViewCellStyle6;
		this.print_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.print_col.HeaderText = "print";
		this.print_col.Name = "print_col";
		this.print_col.Text = "Print";
		this.print_col.UseColumnTextForButtonValue = true;
		this.print_col.Width = 5;
		this.remove_col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Red;
		dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(1);
		this.remove_col.DefaultCellStyle = dataGridViewCellStyle7;
		this.remove_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.remove_col.HeaderText = "remove";
		this.remove_col.Name = "remove_col";
		this.remove_col.Text = "remove";
		this.remove_col.UseColumnTextForButtonValue = true;
		this.remove_col.Width = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1246, 732);
		base.Controls.Add(this.splitContainer1);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmCommision";
		this.Text = "Agent Commision";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCommision_Load);
		((System.ComponentModel.ISupportInitialize)this.DGVDicer).EndInit();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel1.PerformLayout();
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDicePayment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGV_release_claims).EndInit();
		((System.ComponentModel.ISupportInitialize)this.claimBindingSource).EndInit();
		base.ResumeLayout(false);
	}
}
