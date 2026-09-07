using System;

namespace RealProperty.BEL;

public class Payrolldetails : IEntity, ICloneable
{
	public dynamic id { get; set; }

	public dynamic idemployee { get; set; }

	public dynamic idpayroll { get; set; }

	public decimal sun { get; set; }

	public decimal sat { get; set; }

	public decimal mon { get; set; }

	public decimal tue { get; set; }

	public decimal wed { get; set; }

	public decimal thu { get; set; }

	public decimal fri { get; set; }

	public dynamic ca { get; set; }

	public dynamic merienda { get; set; }

	public dynamic egg { get; set; }

	public dynamic others { get; set; }

	public dynamic undertime { get; set; }

	public dynamic rice { get; set; }

	public dynamic emergencyfund { get; set; }

	public dynamic recordstatus { get; set; }

	public string fullname { get; set; }

	public decimal daily_rate { get; set; }

	public string[] getFields()
	{
		return new string[18]
		{
			"id              ".Trim(),
			"idemployee      ".Trim(),
			"idpayroll       ".Trim(),
			"sun             ".Trim(),
			"sat             ".Trim(),
			"mon             ".Trim(),
			"tue             ".Trim(),
			"wed             ".Trim(),
			"thu             ".Trim(),
			"fri             ".Trim(),
			"ca              ".Trim(),
			"merienda        ".Trim(),
			"egg             ".Trim(),
			"others          ".Trim(),
			"undertime       ".Trim(),
			"rice            ".Trim(),
			"emergencyfund   ".Trim(),
			"recordstatus    ".Trim()
		};
	}

	public string getTableName()
	{
		return "payrolldetails";
	}

	public object Clone()
	{
		return MemberwiseClone();
	}
}
