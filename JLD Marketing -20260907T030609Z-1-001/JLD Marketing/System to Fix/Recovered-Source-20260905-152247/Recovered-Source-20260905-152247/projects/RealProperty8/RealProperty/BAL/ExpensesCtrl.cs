using System;
using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class ExpensesCtrl : AController, IDisposable
{
	private ExpensesAdapter adapter { get; set; }

	public ExpensesCtrl()
	{
		adapter = new ExpensesAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal List<Expense_view> getexpenses_view()
	{
		DataTable table = adapter.getcustomTable("expenses_view");
		return AController.DataTableToList<Expense_view>(table);
	}

	internal int add(Expense_view expense_view)
	{
		return adapter.add(expense_view);
	}

	internal int edit(Expense_view expense_view)
	{
		return adapter.edit(expense_view);
	}

	internal Expense_view getRecordbyID(int p)
	{
		return AController.DataRowToClass<Expense_view>(adapter.getTableByID("expenses_view", "id", p).Rows[0]);
	}
}
