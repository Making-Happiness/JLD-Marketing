using System;

namespace RealProperty.BEL;

public class Expense : IEntity
{
	public int id { get; set; }

	public int receiveby { get; set; }

	public string description { get; set; }

	public string purpose { get; set; }

	public decimal amount { get; set; }

	public int releaseby { get; set; }

	public DateTime daterelease { get; set; }

	public string remarks { get; set; }

	public Expense()
	{
		daterelease = DateTime.Now;
		remarks = "active";
		description = "";
	}

	public string[] getFields()
	{
		return new string[8]
		{
			"id            ".Trim(),
			"receiveby     ".Trim(),
			"description   ".Trim(),
			"purpose       ".Trim(),
			"amount        ".Trim(),
			"releaseby     ".Trim(),
			"daterelease   ".Trim(),
			"remarks       ".Trim()
		};
	}

	public string getTableName()
	{
		return "expenses";
	}
}
