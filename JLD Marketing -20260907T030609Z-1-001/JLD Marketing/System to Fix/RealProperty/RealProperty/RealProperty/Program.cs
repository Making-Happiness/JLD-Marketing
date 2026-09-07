using System;
using System.Threading;
using System.Windows.Forms;
using RealProperty.DAL;

namespace RealProperty;

internal static class Program
{
	public static frmMain fmain;

	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.ThreadException += Application_ThreadException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

		string message;
		if (!DatabaseConnection.TryConnect(out message))
		{
			MessageBox.Show("Real Property System cannot start because its database is unavailable.\r\n\r\n" + message + "\r\n\r\nUpdate RealProperty.exe.config, restore the database, or contact the database administrator.", "Database unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		fmain = new frmMain();
		Application.Run(fmain);
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs exceptionEventArgs)
	{
		DatabaseConnection.Log(exceptionEventArgs.Exception, "Windows Forms unhandled exception");
		MessageBox.Show("An unexpected error occurred. The technical details were recorded in RealProperty.database.log.", "Real Property System", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs exceptionEventArgs)
	{
		Exception exception = exceptionEventArgs.ExceptionObject as Exception;
		if (exception != null)
		{
			DatabaseConnection.Log(exception, "Application-domain unhandled exception");
		}
	}
}
