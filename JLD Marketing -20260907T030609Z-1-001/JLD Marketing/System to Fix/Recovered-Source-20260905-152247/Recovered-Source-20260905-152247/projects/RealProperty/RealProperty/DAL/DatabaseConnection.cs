using System;
using System.Configuration;
using System.IO;
using MySql.Data.MySqlClient;

namespace RealProperty.DAL;

/// <summary>
/// Centralizes database configuration, connection checks, and safe diagnostic logging.
/// Connection strings and passwords are intentionally never written to the log.
/// </summary>
internal static class DatabaseConnection
{
	internal const int CommandTimeoutSeconds = 30;

	private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RealProperty.database.log");

	internal static string ConnectionString
	{
		get
		{
			ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["ConStr"];
			if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
			{
				throw new ConfigurationErrorsException("The ConStr database connection string is missing from RealProperty.exe.config.");
			}

			if (ContainsPlaceholder(setting.ConnectionString))
			{
				throw new ConfigurationErrorsException("The database password in RealProperty.exe.config is still a placeholder. Configure a valid database account before starting the application.");
			}

			// The original configuration had no bounded timeout. Appending these values makes
			// a disconnected server fail promptly even when an older config is still in use.
			return setting.ConnectionString + ";Connection Timeout=5;Default Command Timeout=" + CommandTimeoutSeconds + ";";
		}
	}

	internal static bool TryConnect(out string message)
	{
		try
		{
			using (MySqlConnection connection = new MySqlConnection(ConnectionString))
			{
				connection.Open();
			}

			message = null;
			return true;
		}
		catch (Exception exception)
		{
			Log(exception, "Database connection check");
			message = GetSafeMessage(exception);
			return false;
		}
	}

	internal static void Log(Exception exception, string operation)
	{
		try
		{
			File.AppendAllText(LogFilePath, string.Format("{0:yyyy-MM-dd HH:mm:ss} [{1}] {2}: {3}{4}", DateTime.Now, operation, exception.GetType().Name, exception.Message, Environment.NewLine));
		}
		catch
		{
			// Diagnostics must never create another application error.
		}
	}

	private static bool ContainsPlaceholder(string connectionString)
	{
		return connectionString.IndexOf("REDACTED", StringComparison.OrdinalIgnoreCase) >= 0 || connectionString.IndexOf("REPLACE_WITH_SECRET", StringComparison.OrdinalIgnoreCase) >= 0;
	}

	private static string GetSafeMessage(Exception exception)
	{
		if (exception is ConfigurationErrorsException)
		{
			return exception.Message;
		}

		MySqlException mysqlException = exception as MySqlException;
		if (mysqlException != null)
		{
			return string.Format("MySQL connection failed (error {0}). Check the server address, network/firewall access, database account, and password.", mysqlException.Number);
		}

		return "The database connection could not be opened. Check the application log for technical details.";
	}
}
