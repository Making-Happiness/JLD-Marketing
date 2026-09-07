using System;
using System.Collections.Generic;
using System.Data;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class ReportsCtrl : AController
{
	private ReportsAdapter Adapter;

	public ReportsCtrl()
	{
		Adapter = new ReportsAdapter();
	}

	internal DataTable getsoadetails(int idpurchasedetail)
	{
		return Adapter.getsoadetails(idpurchasedetail);
	}

	internal object getSOA(int idpurchasedetail)
	{
		return Adapter.getSOA(idpurchasedetail);
	}

	internal object getCashInflows(DateTime asof)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("vdateasof", asof);
		return Adapter.getTablebyProcedure("proc_cashinflows", dictionary);
	}

	internal DataTable getPayslipdetailsALL(int idpayslip)
	{
		return Adapter.getTableByID("view_rpt_payslipdetails", "idpayslip", idpayslip);
	}

	internal DataTable getPayslipTable(int id)
	{
		return Adapter.getTableByID("payslips", "id", id);
	}

	internal DataTable get_rpt_Payroll(int idpayroll)
	{
		return Adapter.getTableByID("rpt_payroll", "idpayroll", idpayroll);
	}
}
