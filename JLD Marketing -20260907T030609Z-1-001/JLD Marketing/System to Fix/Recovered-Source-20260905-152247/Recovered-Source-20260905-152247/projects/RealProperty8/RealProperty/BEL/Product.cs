namespace RealProperty.BEL;

public class Product : IEntity
{
	private string _code = "";

	private string _location = "";

	public int idproduct { get; set; }

	public string code
	{
		get
		{
			return _code;
		}
		set
		{
			_code = value;
		}
	}

	public string location
	{
		get
		{
			return _location;
		}
		set
		{
			_location = value;
		}
	}

	public int totalblockno { get; set; }

	public int totallotno { get; set; }

	public decimal totalarea { get; set; }

	public decimal cashprice { get; set; }

	public string[] getFields()
	{
		return new string[7] { "idproduct", "code", "location", "totalblockno", "totallotno", "totalarea", "cashprice" };
	}

	public string getTableName()
	{
		return "products";
	}
}
