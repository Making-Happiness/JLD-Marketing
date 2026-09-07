using System;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class ProductsCtrl : AController, IDisposable
{
	private ProductsAdapter adapter { get; set; }

	public ProductsCtrl()
	{
		adapter = new ProductsAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal DataTable getTable()
	{
		return adapter.getTable("products");
	}

	internal DataTable getcustomTable(string customtablename)
	{
		return adapter.getTable(customtablename);
	}

	internal int add(IEntity item)
	{
		return adapter.add(item, "products");
	}

	internal int edit(IEntity item)
	{
		return adapter.edit(item, "products");
	}

	internal DataTable getTablebyId(int id)
	{
		return adapter.getTableById(id);
	}
}
