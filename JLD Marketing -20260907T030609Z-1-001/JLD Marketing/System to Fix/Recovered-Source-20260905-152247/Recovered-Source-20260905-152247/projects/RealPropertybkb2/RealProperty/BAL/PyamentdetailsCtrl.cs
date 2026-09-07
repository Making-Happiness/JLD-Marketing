using System;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class PyamentdetailsCtrl : AController, IDisposable
{
	private PaymentdetailsAdapter adapter = new PaymentdetailsAdapter();

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal DataTable get_proc_purchasedetails_fullTable()
	{
		return adapter.get_proc_purchasedetails_fullTable();
	}

	internal Paymentdetail getRecordbyID(int id)
	{
		return AController.DataRowToClass<Paymentdetail>(adapter.getTableByID("paymentdetails", "id", id).Rows[0]);
	}

	internal int edit(Paymentdetail pd)
	{
		return adapter.edit(pd);
	}
}
