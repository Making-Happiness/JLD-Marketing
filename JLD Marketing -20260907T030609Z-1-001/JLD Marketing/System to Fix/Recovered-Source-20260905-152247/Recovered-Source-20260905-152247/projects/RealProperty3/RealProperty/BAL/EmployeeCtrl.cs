using System;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class EmployeeCtrl : AController, IDisposable
{
	private EmployeeAdapter Adapter;

	public EmployeeCtrl()
	{
		Adapter = new EmployeeAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal int add(Employee employee)
	{
		return Adapter.add(employee);
	}

	internal int edit(Employee employee)
	{
		return Adapter.edit(employee);
	}

	internal DataTable getTable()
	{
		return Adapter.getTable();
	}

	internal DataTable getobcategoryCookies()
	{
		return Adapter.getobcategoryCookies();
	}

	internal DataTable jobDesignationCookies()
	{
		return Adapter.jobDesignationCookies();
	}

	internal DataTable getcustomTable(string p)
	{
		return Adapter.getcustomTable(p);
	}
}
