using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

	internal int setEmployeModeSalary(Employee emp, int mode_of_salary)
	{
		emp.mode_of_salary = mode_of_salary;
		return Adapter.edit(emp);
	}

	internal DataTable getWeeklyEmployees()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("mode_of_salary", 1);
		return Adapter.getTable("view_employeeswithfullname", dictionary, 0, 0, "where mode_of_salary=?mode_of_salary");
	}

	internal Employee getEmployeeObject(int id)
	{
		DataTable tableByID = Adapter.getTableByID("employees", "idemployee", id);
		if (tableByID.Rows.Count > 0)
		{
			return AController.DataTableToList<Employee>(tableByID).First();
		}
		return null;
	}

	internal DataTable view_payrolldetailswithemployee(int idpayroll)
	{
		return Adapter.getTableByID("view_payrolldetailswithemployee", "idpayroll", idpayroll);
	}
}
