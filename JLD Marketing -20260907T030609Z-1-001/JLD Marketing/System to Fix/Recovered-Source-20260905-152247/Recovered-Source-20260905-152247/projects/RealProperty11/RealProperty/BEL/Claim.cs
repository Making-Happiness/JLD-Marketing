using System;

namespace RealProperty.BEL;

public class Claim : IEntity
{
	public int id { get; set; }

	public int idagent { get; set; }

	public string claimby { get; set; }

	public DateTime dateofclaim { get; set; }

	public string description { get; set; }

	public decimal amount { get; set; }

	public string remarks { get; set; }

	public string recordstatus { get; set; }

	public string fullname { get; set; }

	public Claim()
	{
		remarks = "";
		recordstatus = "active";
	}

	public string[] getFields()
	{
		return new string[8]
		{
			"id             ".Trim(),
			"idagent        ".Trim(),
			"claimby        ".Trim(),
			"dateofclaim    ".Trim(),
			"description    ".Trim(),
			"amount         ".Trim(),
			"remarks        ".Trim(),
			"recordstatus   ".Trim()
		};
	}

	public string getTableName()
	{
		return "claims";
	}
}
