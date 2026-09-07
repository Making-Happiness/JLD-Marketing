using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class EmployeeAdapter : AAdapter
{
	private string tablename;

	public EmployeeAdapter()
	{
		Employee employee = new Employee();
		tablename = employee.getTableName();
		employee = null;
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

	internal DataTable getobcategoryCookies()
	{
		return getTable("jobcategory");
	}

	internal DataTable jobDesignationCookies()
	{
		return getTable("jobdesignation");
	}
}
