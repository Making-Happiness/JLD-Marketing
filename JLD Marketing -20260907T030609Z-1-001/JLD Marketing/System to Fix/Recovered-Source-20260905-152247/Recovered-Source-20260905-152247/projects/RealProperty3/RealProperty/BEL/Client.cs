using System;

namespace RealProperty.BEL;

public class Client : IEntity
{
	private string[] fields = new string[9]
	{
		"idclients     ".Trim(),
		"firstname     ".Trim(),
		"lastname      ".Trim(),
		"middlename    ".Trim(),
		"gender        ".Trim(),
		"dateofbirth   ".Trim(),
		"placeofbirth  ".Trim(),
		"spousename    ".Trim(),
		"contactno     ".Trim()
	};

	private int _idclients = 0;

	private string _firstname = "";

	private string _lastname = "";

	private string _middlename = "";

	private string _gender = "";

	private string _dateofbirth = DateTime.Today.ToString("yyyy-MM-dd");

	private string _placeofbirth = "";

	private string _spousename = "";

	private string _contactno = "";

	public int idclients
	{
		get
		{
			return _idclients;
		}
		set
		{
			_idclients = value;
		}
	}

	public string firstname
	{
		get
		{
			return _firstname;
		}
		set
		{
			_firstname = value;
		}
	}

	public string lastname
	{
		get
		{
			return _lastname;
		}
		set
		{
			_lastname = value;
		}
	}

	public string middlename
	{
		get
		{
			return _middlename;
		}
		set
		{
			_middlename = value;
		}
	}

	public string gender
	{
		get
		{
			return _gender;
		}
		set
		{
			_gender = value;
		}
	}

	public string dateofbirth
	{
		get
		{
			return _dateofbirth;
		}
		set
		{
			_dateofbirth = value;
		}
	}

	public string placeofbirth
	{
		get
		{
			return _placeofbirth;
		}
		set
		{
			_placeofbirth = value;
		}
	}

	public string spousename
	{
		get
		{
			return _spousename;
		}
		set
		{
			_spousename = value;
		}
	}

	public string contactno
	{
		get
		{
			return _contactno;
		}
		set
		{
			_contactno = value;
		}
	}

	public string[] getFields()
	{
		return fields;
	}

	public string getTableName()
	{
		return "clients";
	}
}
