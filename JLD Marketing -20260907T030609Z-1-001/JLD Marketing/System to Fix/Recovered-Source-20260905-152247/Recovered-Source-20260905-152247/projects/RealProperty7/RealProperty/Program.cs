using System;
using System.Windows.Forms;

namespace RealProperty;

internal static class Program
{
	public static frmMain fmain;

	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		fmain = new frmMain();
		Application.Run(fmain);
	}
}
