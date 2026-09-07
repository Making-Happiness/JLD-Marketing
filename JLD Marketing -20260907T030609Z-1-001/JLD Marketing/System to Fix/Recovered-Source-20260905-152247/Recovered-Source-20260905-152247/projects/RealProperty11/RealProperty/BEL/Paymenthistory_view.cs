using System;

namespace RealProperty.BEL;

public class Paymenthistory_view
{
	public int id { get; set; }

	public int idpurchasedetails { get; set; }

	public int idclients { get; set; }

	public DateTime dateofpayment { get; set; }

	public int idpayment { get; set; }

	public string description { get; set; }

	public decimal amount { get; set; }

	public string paymenttype { get; set; }
}
