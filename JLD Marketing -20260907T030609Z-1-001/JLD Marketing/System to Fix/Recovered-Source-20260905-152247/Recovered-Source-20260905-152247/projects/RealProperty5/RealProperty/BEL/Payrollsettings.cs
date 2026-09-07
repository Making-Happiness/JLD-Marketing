using System;

namespace RealProperty.BEL;

public class Payrollsettings : IEntity
{
	public int id { get; set; }

	public string description { get; set; }

	public decimal amount { get; set; }

	public string category { get; set; }

	public string remarks { get; set; }

	public string recordstatus { get; set; }

	public DateTime daterecorded { get; set; }

	public Payrollsettings()
	{
		id = 0;
		amount = 0m;
		description = "";
		category = "ALL";
		daterecorded = DateTime.Now;
		recordstatus = "active";
	}

	public string[] getFields()
	{
		return new string[7]
		{
			"id             ".Trim(),
			"description    ".Trim(),
			"amount         ".Trim(),
			"category       ".Trim(),
			"remarks        ".Trim(),
			"recordstatus   ".Trim(),
			"daterecorded   ".Trim()
		};
	}

	public string getTableName()
	{
		return "payrollsettings";
	}
}
