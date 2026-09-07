using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty.Reports_Model;

public class rpt_payroll : AReports
{
	public override string ReportEmbeddedResource => "RealProperty.Reports.rptPayroll.rdlc";

	public rpt_payroll(Payroll payroll)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		ReportsCtrl reportsCtrl = new ReportsCtrl();
		DataTable dataTable = reportsCtrl.get_rpt_Payroll(payroll.id);
		base.RptParams = (ReportParameter[])(object)new ReportParameter[0];
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("ds_rpt_Payroll", dataTable));
	}
}
