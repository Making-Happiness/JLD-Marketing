using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class ClientsAdapter : AAdapter
{
	private string tablename;

	public ClientsAdapter()
	{
		Client client = new Client();
		tablename = client.getTableName();
		client = null;
	}

	internal override DataTable getTable()
	{
		return getTable("clients");
	}

	internal override DataTable getcustomTable(string customtablename)
	{
		return getTable(customtablename);
	}

	internal override int add(IEntity item)
	{
		return add(item, "clients");
	}

	internal override int edit(IEntity item)
	{
		return edit(item, "clients");
	}

	internal DataTable getPurchaseDetailOrderTable(int clientID)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("?idclients", clientID);
		return getTable("view_productdeatailswithproduct", dictionary, 0, 0, "WHERE idclients=?idclients");
	}

	internal override DataTable getTableByID(string PrimaryKeyName, int value)
	{
		return getTableByID(tablename, PrimaryKeyName, value);
	}

	internal DataTable get_view_agentsTable()
	{
		return getTable("view_agents");
	}
}
