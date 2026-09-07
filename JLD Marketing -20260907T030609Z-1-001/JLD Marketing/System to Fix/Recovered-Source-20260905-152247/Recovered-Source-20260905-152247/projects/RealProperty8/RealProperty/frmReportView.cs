using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using RealProperty.BEL;
using RealProperty.Reports_Model;

namespace RealProperty;

public class frmReportView : Form
{
	private IContainer components = null;

	private ReportViewer rptViewer;

	private BindingSource Expense_viewBindingSource;

	public AReports reports { get; set; }

	public frmReportView()
	{
		InitializeComponent();
		((ContainerControl)(object)rptViewer).AutoScaleMode = AutoScaleMode.Dpi;
	}

	private void frmReportView_Load(object sender, EventArgs e)
	{
		if (reports == null)
		{
			return;
		}
		rptViewer.Reset();
		foreach (ReportDataSource item in reports.RptSource)
		{
			((Collection<ReportDataSource>)(object)rptViewer.LocalReport.DataSources).Add(item);
		}
		rptViewer.LocalReport.ReportEmbeddedResource = reports.ReportEmbeddedResource;
		if (reports.RptParams != null)
		{
			try
			{
				((Report)rptViewer.LocalReport).SetParameters((IEnumerable<ReportParameter>)reports.RptParams);
			}
			catch (Exception)
			{
			}
		}
		((Report)rptViewer.LocalReport).Refresh();
		rptViewer.RefreshReport();
	}

	private void SetCustomPaperSize(ReportViewer reportViewer, float width, float height)
	{
		PageSettings pageSettings = new PageSettings();
		pageSettings.PaperSize = new PaperSize("Custom Size", (int)width, (int)height);
		pageSettings.Margins = new Margins(0, 0, 0, 0);
		reportViewer.SetPageSettings(pageSettings);
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		ReportDataSource val = new ReportDataSource();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RealProperty.frmReportView));
		this.Expense_viewBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.rptViewer = new ReportViewer();
		((System.ComponentModel.ISupportInitialize)this.Expense_viewBindingSource).BeginInit();
		base.SuspendLayout();
		this.Expense_viewBindingSource.DataSource = typeof(RealProperty.BEL.Expense_view);
		((System.Windows.Forms.Control)(object)this.rptViewer).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		((System.Windows.Forms.UserControl)(object)this.rptViewer).AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		val.Name = "DataSet1";
		val.Value = this.Expense_viewBindingSource;
		((System.Collections.ObjectModel.Collection<ReportDataSource>)(object)this.rptViewer.LocalReport.DataSources).Add(val);
		this.rptViewer.LocalReport.ReportEmbeddedResource = "RealProperty.Report1.rdlc";
		((System.Windows.Forms.Control)(object)this.rptViewer).Location = new System.Drawing.Point(3, 12);
		((System.Windows.Forms.Control)(object)this.rptViewer).Name = "rptViewer";
		((System.Windows.Forms.Control)(object)this.rptViewer).Size = new System.Drawing.Size(1110, 576);
		((System.Windows.Forms.Control)(object)this.rptViewer).TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1117, 593);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.rptViewer);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmReportView";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Report Viewer";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmReportView_Load);
		((System.ComponentModel.ISupportInitialize)this.Expense_viewBindingSource).EndInit();
		base.ResumeLayout(false);
	}
}
