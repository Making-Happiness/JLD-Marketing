namespace RealProperty.BEL;

public class Paymentdetail : IEntity
{
	public int id { get; set; }

	public int idpurchasedetails { get; set; }

	public decimal amount { get; set; }

	public string paymentfor { get; set; }

	public int idpayment { get; set; }

	public string description { get; set; }

	public Paymentdetail()
	{
		id = 0;
		idpurchasedetails = 0;
		description = "";
		amount = 0m;
		paymentfor = "INSTALLMENT";
		idpayment = 0;
	}

	public string[] getFields()
	{
		return new string[6]
		{
			"id                 ".Trim(),
			"description        ".Trim(),
			"idpurchasedetails  ".Trim(),
			"amount             ".Trim(),
			"paymentfor         ".Trim(),
			"idpayment          ".Trim()
		};
	}

	public string getTableName()
	{
		return "paymentdetails";
	}
}
