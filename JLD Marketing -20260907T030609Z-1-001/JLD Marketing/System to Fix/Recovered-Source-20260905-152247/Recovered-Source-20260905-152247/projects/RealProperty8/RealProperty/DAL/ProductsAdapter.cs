using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class ProductsAdapter : AAdapter
{
	private string tablename;

	public ProductsAdapter()
	{
		Product product = new Product();
		tablename = product.getTableName();
		product = null;
	}

	internal override DataTable getTable()
	{
		return getTable(tablename);
	}

	internal DataTable getTableById(int id)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("?idproduct", id);
		return getTable("products", dictionary, 0, 0, "WHERE idproduct=?idproduct");
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
}
