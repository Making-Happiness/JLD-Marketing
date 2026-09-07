using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal abstract class AAdapter
{
	private string ConnectionString => ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

	internal abstract DataTable getTable();

	internal abstract DataTable getcustomTable(string customtablename);

	internal abstract int add(IEntity item);

	internal abstract int edit(IEntity item);

	internal abstract DataTable getTableByID(string PrimaryKeyName, int value);

	internal static T DataRowToClass<T>(DataRow row) where T : class, new()
	{
		T val = new T();
		PropertyInfo[] properties = val.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			try
			{
				PropertyInfo property = val.GetType().GetProperty(propertyInfo.Name);
				property.SetValue(val, Convert.ChangeType(row[propertyInfo.Name], property.PropertyType), null);
			}
			catch
			{
			}
		}
		return val;
	}

	internal DataTable getTableByID(string tablename, string PrimaryKeyName, int value)
	{
		string text = string.Format(" WHERE {0} = ?{0}", PrimaryKeyName);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add(PrimaryKeyName, value);
		return getTable(tablename, dictionary, 0, 0, text);
	}

	internal DataTable getTable(string tablename, Dictionary<string, dynamic> parameters = null, int StartAt = 0, int countlimit = 0, string where = "")
	{
		string text = "";
		text = ((countlimit != 0) ? $" LIMIT {StartAt},{countlimit}" : "");
		string text2 = $"SELECT * FROM {tablename} {where} ";
		text2 += text;
		return getTable(text2, parameters);
	}

	internal DataTable getTablebyProcedure(string procName, Dictionary<string, dynamic> param = null)
	{
		using MySqlCommand mySqlCommand = new MySqlCommand();
		string commandText = $"{procName}";
		if (param != null)
		{
			foreach (KeyValuePair<string, object> item in param)
			{
				mySqlCommand.Parameters.AddWithValue(item.Key, (dynamic)item.Value);
			}
		}
		mySqlCommand.CommandText = commandText;
		mySqlCommand.CommandType = CommandType.StoredProcedure;
		return getTable(mySqlCommand);
	}

	internal int executeProcedure(string procName, Dictionary<string, dynamic> param)
	{
		using MySqlCommand mySqlCommand = new MySqlCommand();
		string commandText = $"{procName}";
		foreach (KeyValuePair<string, object> item in param)
		{
			mySqlCommand.Parameters.AddWithValue(item.Key, (dynamic)item.Value);
		}
		mySqlCommand.CommandText = commandText;
		mySqlCommand.CommandType = CommandType.StoredProcedure;
		return executeNonQuery(mySqlCommand);
	}

	internal int delete(string tablename, string PrimaryKeyName, int value)
	{
		string cmdText = $"DELETE FROM {tablename} WHERE {PrimaryKeyName} = ?{PrimaryKeyName}";
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("?" + PrimaryKeyName, value);
		return Convert.ToInt32(ExecuteDeleteQuery(cmdText, dictionary));
	}

	internal int add(IEntity item, string tablename)
	{
		string text = $"INSERT INTO {tablename} ( ";
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		string[] fields = item.getFields();
		string text2 = "";
		for (int i = 1; i < fields.Count(); i++)
		{
			dictionary.Add("?" + fields[i].Trim(), (dynamic)item.GetType().GetProperty(fields[i].Trim()).GetValue(item, null));
			text += $" {fields[i].Trim()} ,";
			text2 += $" ?{fields[i].Trim()} ,";
		}
		text = text.Trim(',') + ") VALUES(";
		text = text + text2.Trim(',') + ")";
		return Convert.ToInt32(ExecuteInsertQuery(text, dictionary));
	}

	private MySqlCommand getInsertCMD(IEntity item, int index)
	{
		string text = $"INSERT INTO {item.getTableName()} ( ";
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		string[] fields = item.getFields();
		string text2 = "";
		for (int i = 1; i < fields.Count(); i++)
		{
			dictionary.Add("?" + fields[i].Trim(), (dynamic)item.GetType().GetProperty(fields[i].Trim()).GetValue(item, null));
			text += $" {fields[i].Trim()} ,";
			text2 += $" ?{fields[i].Trim()}{index} ,";
		}
		text = text.Trim(',') + ") VALUES(";
		text = text + text2.Trim(',') + ")";
		MySqlCommand mySqlCommand = new MySqlCommand();
		mySqlCommand.CommandType = CommandType.Text;
		mySqlCommand.CommandText = text;
		if (dictionary != null)
		{
			foreach (KeyValuePair<string, object> item2 in dictionary)
			{
				mySqlCommand.Parameters.AddWithValue(item2.Key + index, (dynamic)item2.Value);
			}
		}
		return mySqlCommand;
	}

	internal int addBulk(dynamic listEntity)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		MySqlCommand mySqlCommand = new MySqlCommand();
		int num = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			mySqlCommand.CommandTimeout = 0;
			mySqlConnection.Open();
			mySqlCommand.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				int num2 = 0;
				foreach (dynamic item in listEntity)
				{
					num2++;
					MySqlCommand mySqlCommand2 = getInsertCMD(item, num2);
					mySqlCommand.CommandText = mySqlCommand2.CommandText;
					mySqlCommand.CommandType = mySqlCommand2.CommandType;
					MySqlParameter[] array = new MySqlParameter[mySqlCommand2.Parameters.Count];
					mySqlCommand2.Parameters.CopyTo(array, 0);
					mySqlCommand.Parameters.AddRange(array);
					num += mySqlCommand.ExecuteNonQuery();
				}
				mySqlTransaction.Commit();
			}
			catch (Exception)
			{
				mySqlTransaction.Rollback();
			}
			return num;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	internal int edit(IEntity item, string tablename)
	{
		string text = $"UPDATE {tablename} set ";
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		string[] fields = item.getFields();
		for (int i = 1; i < fields.Count(); i++)
		{
			dictionary.Add("?" + fields[i].Trim(), (dynamic)item.GetType().GetProperty(fields[i].Trim()).GetValue(item, null));
			text += string.Format(" {0} = ?{0},", fields[i].Trim());
		}
		text = text.Trim(',');
		dictionary.Add("?" + fields[0].Trim(), (dynamic)item.GetType().GetProperty(fields[0].Trim()).GetValue(item, null));
		text += string.Format(" WHERE {0} = ?{0}", fields[0].Trim());
		return Convert.ToInt32(executeNonQuery(text, dictionary));
	}

	private int executeNonQuery(string cmdText, Dictionary<string, dynamic> parameters)
	{
		int result = 0;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
			}
			result = executeNonQuery(mySqlCommand);
		}
		return result;
	}

	private DataTable getTable(string cmdText, Dictionary<string, dynamic> parameters)
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

	private object ExecuteScalar(string cmdText, Dictionary<string, dynamic> parameters)
	{
		object result = null;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
			}
			result = ExecuteScalar(mySqlCommand);
		}
		return result;
	}

	private object ExecuteInsertQuery(string cmdText, Dictionary<string, dynamic> parameters)
	{
		object result = null;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
			}
			result = ExecuteInsertQuery(mySqlCommand);
		}
		return result;
	}

	private object ExecuteDeleteQuery(string cmdText, Dictionary<string, dynamic> parameters)
	{
		object result = null;
		using (MySqlCommand mySqlCommand = new MySqlCommand())
		{
			mySqlCommand.CommandText = cmdText;
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				mySqlCommand.Parameters.AddWithValue(parameter.Key, (dynamic)parameter.Value);
			}
			result = ExecuteInsertQuery(mySqlCommand);
		}
		return result;
	}

	private int executeNonQuery(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		int result = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			cmd.CommandTimeout = 0;
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				result = cmd.ExecuteNonQuery();
				mySqlTransaction.Commit();
			}
			catch (Exception)
			{
				mySqlTransaction.Rollback();
			}
			return result;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	private object ExecuteScalar(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		object result = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			cmd.CommandTimeout = 0;
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				result = cmd.ExecuteScalar();
				mySqlTransaction.Commit();
			}
			catch (MySqlException)
			{
				mySqlTransaction.Rollback();
			}
			return result;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	private object ExecuteInsertQuery(MySqlCommand cmd)
	{
		using MySqlConnection mySqlConnection = new MySqlConnection(ConnectionString);
		object result = 0;
		MySqlTransaction mySqlTransaction = null;
		try
		{
			cmd.CommandTimeout = 0;
			mySqlConnection.Open();
			cmd.Connection = mySqlConnection;
			mySqlTransaction = mySqlConnection.BeginTransaction();
			try
			{
				result = cmd.ExecuteScalar();
				mySqlTransaction.Commit();
				result = cmd.LastInsertedId;
			}
			catch (MySqlException)
			{
				mySqlTransaction.Rollback();
			}
			return result;
		}
		finally
		{
			mySqlTransaction = null;
		}
	}

	private DataTable getTable(MySqlCommand cmd)
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
		catch (Exception)
		{
			return null;
		}
	}

	private DataTable getTable<T>(MySqlCommand cmd)
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
		catch (Exception)
		{
			return null;
		}
	}

	private List<T> DataTableToList<T>(DataTable table) where T : class, new()
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
