using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;

namespace RealProperty.Reports_Model;

internal class rpt_StatementOfCashflow : AReports
{
	public override string ReportEmbeddedResource => "RealProperty.Reports.rptstatementofcashflow.rdlc";

	public rpt_StatementOfCashflow()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		ReportsCtrl reportsCtrl = new ReportsCtrl();
		DateTime asof = new DateTime(2020, 11, 21);
		DataTable cashInflows = reportsCtrl.getCashInflows(asof);
		if (cashInflows == null)
		{
			throw new InvalidOperationException("The cash-flow report data could not be loaded. Restore the proc_cashinflows database procedure, then try again. See RealProperty.database.log for details.");
		}
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("dscashinflows", cashInflows));
	}
}
