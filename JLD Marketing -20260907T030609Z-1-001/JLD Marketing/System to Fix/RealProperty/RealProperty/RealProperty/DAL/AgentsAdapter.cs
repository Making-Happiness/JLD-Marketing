using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class AgentsAdapter : AAdapter
{
	private string tablename;

	public AgentsAdapter()
	{
		Agent agent = new Agent();
		tablename = agent.getTableName();
		agent = null;
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
}
