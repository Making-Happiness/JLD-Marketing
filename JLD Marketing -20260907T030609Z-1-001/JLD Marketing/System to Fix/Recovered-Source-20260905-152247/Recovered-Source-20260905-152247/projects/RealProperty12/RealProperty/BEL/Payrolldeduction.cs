using System;

namespace RealProperty.BEL;

public class Payrolldeduction : IEntity
{
	public int id { get; set; }

	public DateTime datepaid { get; set; }

	public string description { get; set; }

	public decimal amount { get; set; }

	public string category { get; set; }

	public string remarks { get; set; }

	public string recordstatus { get; set; }

	public int idemployee { get; set; }

	public int idpayroll { get; set; }

	public int idloan { get; set; }

	public string[] getFields()
	{
		return new string[10]
		{
			"id             ".Trim(),
			"datepaid       ".Trim(),
			"description    ".Trim(),
			"amount         ".Trim(),
			"category       ".Trim(),
			"remarks        ".Trim(),
			"recordstatus   ".Trim(),
			"idemployee     ".Trim(),
			"idpayroll      ".Trim(),
			"idloan         ".Trim()
		};
	}

	public string getTableName()
	{
		return "payrolldeduction";
	}
}
