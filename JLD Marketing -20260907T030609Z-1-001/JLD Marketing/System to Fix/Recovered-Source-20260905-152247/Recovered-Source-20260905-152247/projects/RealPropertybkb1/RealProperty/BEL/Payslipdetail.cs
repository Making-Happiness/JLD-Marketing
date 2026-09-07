using System;

namespace RealProperty.BEL;

public class Payslipdetail : IEntity
{
	public int id { get; set; }

	public int idemployee { get; set; }

	public int idpayslip { get; set; }

	public string title { get; set; }

	public decimal amount { get; set; }

	public string category { get; set; }

	public string mode { get; set; }

	public DateTime datefrom { get; set; }

	public DateTime dateto { get; set; }

	public string remarks { get; set; }

	public string recordstatus { get; set; }

	public Payslipdetail()
	{
		title = "";
		category = "OTHER DEDUCTION";
		amount = 0.0m;
		remarks = "";
		recordstatus = "active";
		mode = "FIXED";
	}

	public string[] getFields()
	{
		return new string[11]
		{
			"id           ".Trim(),
			"idemployee   ".Trim(),
			"idpayslip   ".Trim(),
			"title        ".Trim(),
			"amount       ".Trim(),
			"category     ".Trim(),
			"mode         ".Trim(),
			"datefrom         ".Trim(),
			"dateto         ".Trim(),
			"remarks      ".Trim(),
			"recordstatus ".Trim()
		};
	}

	public string getTableName()
	{
		return "payslipdetails";
	}
}
