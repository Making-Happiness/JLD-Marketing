using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class PurchasedetailsAdapter : AAdapter
{
	private string tablename = "purchasedetails";

	internal override DataTable getTable()
	{
		return getTable(tablename);
	}

	internal override DataTable getcustomTable(string customtablename)
	{
		return getcustomTable(customtablename);
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
}
