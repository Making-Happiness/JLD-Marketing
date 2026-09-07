namespace RealProperty.BEL;

public class Agent : IEntity
{
	public int id { get; set; }

	public string fullname { get; set; }

	public string recordstatus { get; set; }

	public string contactno { get; set; }

	public Agent()
	{
		id = 0;
		contactno = "";
		fullname = "";
		recordstatus = "active";
	}

	public string[] getFields()
	{
		return new string[4]
		{
			"id         ".Trim(),
			"contactno       ".Trim(),
			"fullname        ".Trim(),
			"recordstatus    ".Trim()
		};
	}

	public string getTableName()
	{
		return "dicers";
	}
}
