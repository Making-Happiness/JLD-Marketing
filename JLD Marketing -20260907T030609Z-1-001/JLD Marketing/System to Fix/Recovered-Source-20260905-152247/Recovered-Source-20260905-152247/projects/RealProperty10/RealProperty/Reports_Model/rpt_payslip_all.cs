using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty.Reports_Model;

public class rpt_payslip_all : AReports
{
	public override string ReportEmbeddedResource => "RealProperty.Reports.rptAll_Payslips.rdlc";

	public rpt_payslip_all(Payslip payslip)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		ReportsCtrl reportsCtrl = new ReportsCtrl();
		DataTable payslipdetailsALL = reportsCtrl.getPayslipdetailsALL(payslip.id);
		base.RptParams = (ReportParameter[])(object)new ReportParameter[0];
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("DS_Payslipdetail", payslipdetailsALL));
	}
}
