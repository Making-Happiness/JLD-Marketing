using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty.Reports_Model;

public class rpt_soa_source : AReports
{
	[CompilerGenerated]
	private static class ctor_003Eo__SiteContainer0
	{
		public static CallSite<Func<CallSite, Type, string, object, ReportParameter>> _003C_003Ep__Site1;

		public static CallSite<Func<CallSite, object, object>> _003C_003Ep__Site2;
	}

	public override string ReportEmbeddedResource => "RealProperty.Reports.rptSOA.rdlc";

	public rpt_soa_source(Purchasedetail purchasedetail)
	{
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		ReportsCtrl reportsCtrl = new ReportsCtrl();
		int id = purchasedetail.id;
		DataTable dataTable = reportsCtrl.getsoadetails(id);
		object sOA = reportsCtrl.getSOA(id);
		base.RptParams = (ReportParameter[])(object)new ReportParameter[1]
		{
			new ReportParameter("TotalPrice", purchasedetail.lotprice.ToString())
		};
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("DS_SOADetails", dataTable));
		base.RptSource.Add(new ReportDataSource("DS_SOA", sOA));
	}
}
