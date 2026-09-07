using System;

namespace RealProperty.BEL;

public class Payslip : IEntity
{
	public int id { get; set; }

	public dynamic title { get; set; }

	public dynamic payslip_type { get; set; }

	public dynamic mode_of_posting { get; set; }

	public dynamic for_month_of { get; set; }

	public dynamic dategenerated { get; set; }

	public dynamic remarks { get; set; }

	public dynamic recordstatus { get; set; }

	public dynamic datefinalized { get; set; }

	public Payslip()
	{
		id = 0;
		title = "";
		payslip_type = "MASTER PAYSLIP";
		mode_of_posting = "AUTOMATIC";
		for_month_of = DateTime.Today.AddMonths(1).ToString("MMMM yyyy");
		dategenerated = DateTime.Now;
		remarks = "";
		recordstatus = "new";
	}

	public string[] getFields()
	{
		return new string[9]
		{
			"id                 ".Trim(),
			"title              ".Trim(),
			"payslip_type       ".Trim(),
			"mode_of_posting    ".Trim(),
			"for_month_of       ".Trim(),
			"dategenerated      ".Trim(),
			"remarks            ".Trim(),
			"recordstatus       ".Trim(),
			"datefinalized      ".Trim()
		};
	}

	public string getTableName()
	{
		return "payslips";
	}
}
