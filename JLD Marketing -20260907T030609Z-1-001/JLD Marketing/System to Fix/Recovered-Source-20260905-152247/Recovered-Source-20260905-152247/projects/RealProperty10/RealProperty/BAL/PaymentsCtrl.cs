using System;
using System.Collections.Generic;
using System.Data;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class PaymentsCtrl : AController, IDisposable
{
	private PaymentsAdapter adapter { get; set; }

	public PaymentsCtrl()
	{
		adapter = new PaymentsAdapter();
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal int add(Payment payment, List<Paymentdetail> pamentDetailList)
	{
		payment.id = adapter.add(payment);
		for (int i = 0; i < pamentDetailList.Count; i++)
		{
			pamentDetailList[i].idpayment = payment.id;
		}
		int num = adapter.addBulkPaymentDetails(pamentDetailList);
		return payment.id;
	}

	internal DataTable get_view_paymentsTable()
	{
		return adapter.getTable();
	}

	internal Payment getRecordbyID(int p)
	{
		return AController.DataRowToClass<Payment>(adapter.getTableByID("payments", "id", p).Rows[0]);
	}

	internal int edit(Payment pd)
	{
		return adapter.edit(pd);
	}

	internal int deleterecordstatus(int idpayment, int idpaymentdetails)
	{
		return adapter.executeDeleteProcedure(idpayment, idpaymentdetails);
	}
}
