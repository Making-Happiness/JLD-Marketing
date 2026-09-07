using System;
using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class PurchasedetailsCtrl : AController, IDisposable
{
	private PurchasedetailsAdapter adapter { get; set; }

	public PurchasedetailsCtrl()
	{
		adapter = new PurchasedetailsAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal DataTable getTable()
	{
		return adapter.getTable("purchasedetails");
	}

	internal DataTable getcustomTable(string customtablename)
	{
		return adapter.getcustomTable(customtablename);
	}

	internal int add(IEntity item)
	{
		return adapter.add(item, item.getTableName());
	}

	internal int edit(IEntity item)
	{
		return adapter.edit(item, item.getTableName());
	}

	internal DataTable getTablePaymentHistory(Purchasedetail purchasedetail)
	{
		return adapter.getTableByID("view_paymenthistory", "idpurchasedetails", purchasedetail.id);
	}

	internal int delete_purchasedetails(int p)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("vidpurchasedetails", p);
		return adapter.executeProcedure("proc_delete_purchasedetails", dictionary);
	}
}
