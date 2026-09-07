using System;
using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class PayrollAdapter : AAdapter
{
	private string tablename;

	public PayrollAdapter()
	{
		Payrollsettings payrollsettings = new Payrollsettings();
		tablename = payrollsettings.getTableName();
		payrollsettings = null;
	}

	internal override DataTable getTable()
	{
		throw new NotImplementedException();
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
		throw new NotImplementedException();
	}

	internal int deletePayslipDetails(int id)
	{
		return delete("payslipdetails", "id", id);
	}
}
