using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty.Reports_Model;

internal class rpt_paymenthistory : AReports
{
	[CompilerGenerated]
	private static class ctor_003Eo__SiteContainer0
	{
		public static CallSite<Func<CallSite, Type, string, object, ReportParameter>> _003C_003Ep__Site1;

		public static CallSite<Func<CallSite, object, object>> _003C_003Ep__Site2;
	}

	public override string ReportEmbeddedResource => "RealProperty.Reports.rptpaymenthistory.rdlc";

	public rpt_paymenthistory(Purchasedetail purchasedetail)
	{
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		PurchasedetailsCtrl purchasedetailsCtrl = new PurchasedetailsCtrl();
		DataTable tablePaymentHistory = purchasedetailsCtrl.getTablePaymentHistory(purchasedetail);
		string filterExpression = $"idclients={purchasedetail.idclients}";
		DataRow[] source = Program.fmain.view_clientsTable.Select(filterExpression);
		DataTable dataTable = source.CopyToDataTable();
		base.RptParams = (ReportParameter[])(object)new ReportParameter[1]
		{
			new ReportParameter("TotalPrice", purchasedetail.lotprice.ToString())
		};
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("dsPaymentHistory", tablePaymentHistory));
		base.RptSource.Add(new ReportDataSource("dsClient", dataTable));
	}
}
