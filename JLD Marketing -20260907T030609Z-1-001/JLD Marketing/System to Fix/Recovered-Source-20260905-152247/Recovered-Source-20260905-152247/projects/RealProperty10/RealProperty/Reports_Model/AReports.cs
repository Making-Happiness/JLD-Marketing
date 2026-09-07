using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace RealProperty.Reports_Model;

public abstract class AReports
{
	private List<ReportDataSource> rptSource;

	private ReportParameter[] rptParams;

	public ReportParameter[] RptParams
	{
		get
		{
			return rptParams;
		}
		internal set
		{
			rptParams = value;
		}
	}

	public List<ReportDataSource> RptSource
	{
		get
		{
			return rptSource;
		}
		internal set
		{
			rptSource = value;
		}
	}

	public abstract string ReportEmbeddedResource { get; }
}
