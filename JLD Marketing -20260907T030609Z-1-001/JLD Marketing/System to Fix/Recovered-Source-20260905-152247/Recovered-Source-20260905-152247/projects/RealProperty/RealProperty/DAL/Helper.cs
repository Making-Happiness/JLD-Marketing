using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace RealProperty.DAL;

public static class Helper
{
	public static string ConnectionString => DatabaseConnection.ConnectionString;

	public static int executeNonQuery(string cmdText, Dictionary<string, dynamic> parameters = null)
	{
		int result = 0;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			if (parameters != null)
			{
				foreach (KeyValuePair<string, object> parameter in parameters)
				{
					mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
				}
			}
			result = executeNonQuery(mySqlCommand);
		}
		return result;
	}

	public static DataTable getTable(string cmdText, Dictionary<string, dynamic> parameters = null)
	{
		DataTable result = null;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			if (parameters != null)
			{
				foreach (KeyValuePair<string, object> parameter in parameters)
				{
					mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
				}
			}
			result = getTable(mySqlCommand);
		}
		return result;
	}

	public static object ExecuteScalar(string cmdText, Dictionary<string, dynamic> parameters = null)
	{
		object result = null;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			if (parameters != null)
			{
				foreach (KeyValuePair<string, object> parameter in parameters)
				{
					mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
				}
			}
			result = ExecuteScalar(mySqlCommand);
		}
		return result;
	}

	public static int executeNonQuery(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		int result = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			cmd.CommandTimeout = DatabaseConnection.CommandTimeoutSeconds;
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				result = cmd.ExecuteNonQuery();
				mySqlTransaction.Commit();
			}
			catch (Exception exception)
			{
				DatabaseConnection.Log(exception, "Helper.executeNonQuery");
				if (mySqlTransaction != null)
				{
					mySqlTransaction.Rollback();
				}
			}
			return result;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	public static object ExecuteScalar(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		object result = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			cmd.CommandTimeout = DatabaseConnection.CommandTimeoutSeconds;
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				result = cmd.ExecuteScalar();
				mySqlTransaction.Commit();
			}
			catch (Exception exception)
			{
				DatabaseConnection.Log(exception, "Helper.ExecuteScalar");
				if (mySqlTransaction != null)
				{
					mySqlTransaction.Rollback();
				}
			}
			return result;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	public static DataTable getTable(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		DataTable dataTable = new DataTable();
		try
		{
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			dataTable.Load(cmd.ExecuteReader());
			return dataTable;
		}
		catch (Exception exception)
		{
			DatabaseConnection.Log(exception, "Helper.getTable");
			return null;
		}
	}

	public static DataTable getTable<T>(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		DataTable dataTable = new DataTable();
		try
		{
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			dataTable.Load(cmd.ExecuteReader());
			return dataTable;
		}
		catch (Exception exception)
		{
			DatabaseConnection.Log(exception, "Helper.getTable<T>");
			return null;
		}
	}

	public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
	{
		try
		{
			List<T> list = new List<T>();
			foreach (DataRow item in table.AsEnumerable())
			{
				T val = new T();
				PropertyInfo[] properties = val.GetType().GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					try
					{
						PropertyInfo property = val.GetType().GetProperty(propertyInfo.Name);
						property.SetValue(val, Convert.ChangeType(item[propertyInfo.Name], property.PropertyType), null);
					}
					catch
					{
					}
				}
				list.Add(val);
			}
			return list;
		}
		catch
		{
			return null;
		}
	}
}
