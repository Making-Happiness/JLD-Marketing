using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace RealProperty.BAL;

internal abstract class AController
{
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

	internal static List<T> DataTableToList<T>(DataTable table) where T : class, new()
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
