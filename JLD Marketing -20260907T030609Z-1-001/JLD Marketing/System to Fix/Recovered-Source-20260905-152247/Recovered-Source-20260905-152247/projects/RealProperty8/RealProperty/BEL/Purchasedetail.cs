using System;

namespace RealProperty.BEL;

public class Purchasedetail : IEntity
{
	public int id { get; set; }

	public dynamic blockno { get; set; }

	public dynamic lotno { get; set; }

	public dynamic area { get; set; }

	public dynamic lotprice { get; set; }

	public dynamic amortization { get; set; }

	public decimal terms { get; set; }

	public dynamic agentpercentage { get; set; }

	public int idclients { get; set; }

	public int idproducts { get; set; }

	public string remarks { get; set; }

	public string recordstatus { get; set; }

	public int idagent { get; set; }

	public decimal otherfees { get; set; }

	public decimal penalty { get; set; }

	public DateTime duedate { get; set; }

	public Purchasedetail()
	{
		setdefaultvalues();
	}

	private void setdefaultvalues()
	{
		id = 0;
		blockno = 1;
		lotno = 0;
		area = 100;
		lotprice = 76000;
		terms = 2m;
		idagent = 0;
		agentpercentage = 7;
		idclients = 0;
		idproducts = 0;
		remarks = "pending";
		recordstatus = "active";
		amortization = 0;
		otherfees = 0m;
		penalty = 0m;
	}

	public decimal getAmortization(decimal downpayment)
	{
		try
		{
			decimal num = (lotprice - downpayment) * 0.15m * terms;
			return Math.Round((lotprice + num - downpayment) / (12m * terms), 0);
		}
		catch (Exception)
		{
			return 0m;
		}
	}

	public string[] getFields()
	{
		return new string[16]
		{
			"id               ".Trim(),
			"blockno          ".Trim(),
			"lotno            ".Trim(),
			"area             ".Trim(),
			"lotprice         ".Trim(),
			"amortization     ".Trim(),
			"terms            ".Trim(),
			"idagent          ".Trim(),
			"agentpercentage  ".Trim(),
			"idclients        ".Trim(),
			"idproducts       ".Trim(),
			"recordstatus       ".Trim(),
			"remarks          ".Trim(),
			"otherfees       ".Trim(),
			"penalty          ".Trim(),
			"duedate          ".Trim()
		};
	}

	public string getTableName()
	{
		return "purchasedetails";
	}
}
