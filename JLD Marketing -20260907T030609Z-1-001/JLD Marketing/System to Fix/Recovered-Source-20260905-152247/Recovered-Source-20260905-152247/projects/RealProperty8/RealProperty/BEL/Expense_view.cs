using System;

namespace RealProperty.BEL;

public class Expense_view : Expense, ICloneable
{
	public string releaseby_str { get; set; }

	public string receiveby_str { get; set; }

	public object Clone()
	{
		return MemberwiseClone();
	}
}
