using System;

namespace RealProperty.BEL;

public class Loan : IEntity
{
	public int id { get; set; }

	public DateTime dateapplied { get; set; }

	public string description { get; set; }

	public decimal amount { get; set; }

	public string category { get; set; }

	public decimal penalty { get; set; }

	public DateTime duedate { get; set; }

	public decimal amortization { get; set; }

	public dynamic remarks { get; set; }

	public string recordstatus { get; set; }

	public int idemployee { get; set; }

	public Loan()
	{
		id = 0;
		dateapplied = DateTime.Today;
		description = "";
		amount = 0m;
		category = "CASH ADVANCE";
		penalty = 0m;
		duedate = DateTime.Today;
		amortization = 0m;
		remarks = "";
		recordstatus = "active";
		idemployee = 0;
	}

	public string[] getFields()
	{
		return new string[11]
		{
			"id               ".Trim(),
			"dateapplied      ".Trim(),
			"description      ".Trim(),
			"amount           ".Trim(),
			"category         ".Trim(),
			"penalty          ".Trim(),
			"duedate          ".Trim(),
			"amortization     ".Trim(),
			"remarks          ".Trim(),
			"recordstatus     ".Trim(),
			"idemployee       ".Trim()
		};
	}

	public string getTableName()
	{
		return "loans";
	}
}
