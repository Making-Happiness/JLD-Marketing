using System;
using System.Data;
using RealProperty.BEL;

namespace RealProperty.DAL;

internal class ReportsAdapter : AAdapter
{
	internal override DataTable getTable()
	{
		throw new NotImplementedException();
	}

	internal override DataTable getcustomTable(string customtablename)
	{
		throw new NotImplementedException();
	}

	internal override int add(IEntity item)
	{
		throw new NotImplementedException();
	}

	internal override int edit(IEntity item)
	{
		throw new NotImplementedException();
	}

	internal override DataTable getTableByID(string PrimaryKeyName, int value)
	{
		throw new NotImplementedException();
	}

	internal DataTable getsoadetails(int idpurchasedetail)
	{
		return getTableByID("rpt_soadetails", "idpurchasedetails", idpurchasedetail);
	}

	internal object getSOA(int idpurchasedetail)
	{
		return getTableByID("rpt_soa", "id", idpurchasedetail);
	}
}
