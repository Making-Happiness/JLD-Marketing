using System.Collections;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using RealProperty.BAL;
using RealProperty.BEL;

namespace RealProperty.Reports_Model;

internal class rpt_listofexpenses : AReports
{
	public override string ReportEmbeddedResource => "RealProperty.Reports.rptlistofexpenses.rdlc";

	public rpt_listofexpenses()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		ExpensesCtrl expensesCtrl = new ExpensesCtrl();
		List<Expense_view> list = expensesCtrl.getexpenses_view();
		base.RptSource = new List<ReportDataSource>();
		base.RptSource.Add(new ReportDataSource("DataSet1", (IEnumerable)list));
	}
}
