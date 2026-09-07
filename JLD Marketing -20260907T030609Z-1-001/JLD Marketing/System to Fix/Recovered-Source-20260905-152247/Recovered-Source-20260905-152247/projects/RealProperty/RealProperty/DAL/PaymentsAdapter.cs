using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class PaymentsAdapter : AAdapter
{
	private string tablename = "payments";

	internal override DataTable getTable()
	{
		return getTable(tablename);
	}

	internal override DataTable getcustomTable(string customtablename)
	{
		return getTable(customtablename);
	}

	internal override int add(IEntity item)
	{
		return add(item, tablename);
	}

	internal override int edit(IEntity item)
	{
		return edit(item, tablename);
	}

	internal override DataTable getTableByID(string PrimaryKeyName, int value)
	{
		return getTableByID(tablename, PrimaryKeyName, value);
	}

	internal int addBulkPaymentDetails(List<Paymentdetail> pamentDetailList)
	{
		return addBulk(pamentDetailList);
	}

	internal int executeDeleteProcedure(int idpayment, int idpaymentdetails)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("?vidpayments", idpayment);
		dictionary.Add("?vidpaymentsdetails", idpaymentdetails);
		string procName = "proc_deletepaymentdetails";
		return executeProcedure(procName, dictionary);
	}
}
