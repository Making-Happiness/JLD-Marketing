using System;

namespace RealProperty.BEL.ReoprtsObject;

public class rpt_SOA
{
	public int idclients { get; set; }

	public string firstname { get; set; }

	public string lastname { get; set; }

	public string middlename { get; set; }

	public string gender { get; set; }

	public DateTime dateofbirth { get; set; }

	public string placeofbirth { get; set; }

	public string spousename { get; set; }

	public string contactno { get; set; }

	public int id { get; set; }

	public int blockno { get; set; }

	public string lotno { get; set; }

	public decimal area { get; set; }

	public decimal lotprice { get; set; }

	public decimal amortization { get; set; }

	public string terms { get; set; }

	public int idagent { get; set; }

	public decimal agentpercentage { get; set; }

	public int idproducts { get; set; }

	public decimal otherfees { get; set; }

	public decimal penalty { get; set; }

	public string remarks { get; set; }

	public string location { get; set; }

	public DateTime duedate { get; set; }
}
