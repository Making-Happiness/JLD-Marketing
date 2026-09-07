using System;
using System.Globalization;

namespace RealProperty.BEL;

public class Payroll : IEntity
{
	public int id { get; set; }

	public string title { get; set; }

	public DateTime datefrom { get; set; }

	public DateTime dateto { get; set; }

	public string recordstatus { get; set; }

	public Payroll()
	{
		title = "";
		DateTime today = DateTime.Today;
		DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
		int num = (7 + (today.DayOfWeek - firstDayOfWeek)) % 7;
		DateTime date = today.AddDays(-1 * num).Date;
		datefrom = date.AddDays(1.0);
		dateto = date.AddDays(6.0);
		recordstatus = "active";
	}

	public string[] getFields()
	{
		return new string[5]
		{
			"id                 ".Trim(),
			"title              ".Trim(),
			"datefrom           ".Trim(),
			"dateto             ".Trim(),
			"recordstatus       ".Trim()
		};
	}

	public string getTableName()
	{
		return "payrolls";
	}
}
