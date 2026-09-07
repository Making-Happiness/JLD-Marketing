using System;

namespace RealProperty.BEL;

internal class Payment : IEntity
{
	public int id { get; set; }

	public DateTime dateofpayment { get; set; }

	public string orderreceipt { get; set; }

	public decimal totalamount { get; set; }

	public int paidby { get; set; }

	public string paymenttype { get; set; }

	public string referenceno { get; set; }

	public string paymentref { get; set; }

	public int inchargeby { get; set; }

	public int recordedby { get; set; }

	public DateTime dateencoded { get; set; }

	public DateTime? dateedited { get; set; }

	public string recordstatus { get; set; }

	public Payment()
	{
		id = 0;
		dateofpayment = DateTime.Today;
		orderreceipt = "";
		totalamount = 0m;
		paidby = 0;
		paymenttype = "CASH";
		referenceno = "";
		paymentref = generateRefNo();
		inchargeby = 0;
		recordedby = 0;
		dateencoded = DateTime.Today;
		dateedited = null;
		recordstatus = "active";
	}

	public string[] getFields()
	{
		return new string[13]
		{
			"id             ".Trim(),
			"dateofpayment  ".Trim(),
			"orderreceipt   ".Trim(),
			"totalamount    ".Trim(),
			"paidby         ".Trim(),
			"paymenttype    ".Trim(),
			"referenceno    ".Trim(),
			"paymentref     ".Trim(),
			"inchargeby     ".Trim(),
			"recordedby     ".Trim(),
			"dateencoded    ".Trim(),
			"dateedited     ".Trim(),
			"recordstatus   ".Trim()
		};
	}

	public string getTableName()
	{
		return "payments";
	}

	private string generateRefNo()
	{
		char[] array = new char[10] { '0', '1', 'N', '3', 'V', 'E', 'R', 'S', 'A', 'L' };
		char[] array2 = Guid.NewGuid().ToString().ToCharArray();
		string text = "";
		for (int i = 0; i < 11; i++)
		{
			try
			{
				string value = Convert.ToString(array2[i]);
				int num = Convert.ToInt32(value);
				text += array[num];
			}
			catch (Exception)
			{
				text += array2[i];
			}
		}
		return DateTime.Today.ToString("yyMM") + text.ToUpper();
	}
}
