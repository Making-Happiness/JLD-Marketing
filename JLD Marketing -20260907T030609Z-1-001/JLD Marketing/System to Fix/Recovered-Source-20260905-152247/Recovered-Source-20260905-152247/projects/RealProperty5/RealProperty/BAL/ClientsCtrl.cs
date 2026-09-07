using System;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class ClientsCtrl : AController, IDisposable
{
	public ClientsAdapter adapter { get; set; }

	public ClientsCtrl()
	{
		adapter = new ClientsAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal DataTable getTable()
	{
		return adapter.getTable("clients");
	}

	internal DataTable getcustomTable(string customtablename)
	{
		return adapter.getTable(customtablename);
	}

	internal int add(IEntity item)
	{
		return adapter.add(item, "clients");
	}

	internal int edit(IEntity item)
	{
		return adapter.edit(item, "clients");
	}

	internal DataTable getPurchaseDetailOrderTable(int clientID)
	{
		return adapter.getPurchaseDetailOrderTable(clientID);
	}

	internal DataTable get_view_agentsTable()
	{
		return adapter.get_view_agentsTable();
	}
}
