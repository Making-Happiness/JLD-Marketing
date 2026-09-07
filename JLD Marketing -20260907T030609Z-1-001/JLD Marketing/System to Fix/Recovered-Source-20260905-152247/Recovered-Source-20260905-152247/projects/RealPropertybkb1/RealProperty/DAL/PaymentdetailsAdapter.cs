using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class PaymentdetailsAdapter : AAdapter
{
	private string tablename;

	public PaymentdetailsAdapter()
	{
		Paymentdetail paymentdetail = new Paymentdetail();
		tablename = paymentdetail.getTableName();
		paymentdetail = null;
	}

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
		return add(item, item.getTableName());
	}

	internal override int edit(IEntity item)
	{
		return edit(item, item.getTableName());
	}

	internal override DataTable getTableByID(string PrimaryKeyName, int value)
	{
		return getTableByID(tablename, PrimaryKeyName, value);
	}

	internal DataTable get_proc_purchasedetails_fullTable()
	{
		return getTablebyProcedure("proc_purchasedetails_full");
	}
}
