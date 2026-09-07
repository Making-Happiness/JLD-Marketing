using System;

namespace RealProperty.BEL;

public class Employee : IEntity
{
	public int idemployee { get; set; }

	public string firstname { get; set; }

	public string lastname { get; set; }

	public string middlename { get; set; }

	public string gender { get; set; }

	public DateTime dateofbirth { get; set; }

	public decimal salary { get; set; }

	public string designation { get; set; }

	public string civilstatus { get; set; }

	public string contactno { get; set; }

	public string jobcategory { get; set; }

	public string recordstatus { get; set; }

	public string remarks { get; set; }

	public string[] getFields()
	{
		return new string[13]
		{
			"idemployee        ".Trim(),
			"firstname         ".Trim(),
			"lastname          ".Trim(),
			"middlename        ".Trim(),
			"gender            ".Trim(),
			"dateofbirth       ".Trim(),
			"salary            ".Trim(),
			"designation       ".Trim(),
			"civilstatus       ".Trim(),
			"contactno         ".Trim(),
			"jobcategory       ".Trim(),
			"recordstatus      ".Trim(),
			"remarks           ".Trim()
		};
	}

	public string getTableName()
	{
		return "employees";
	}
}
