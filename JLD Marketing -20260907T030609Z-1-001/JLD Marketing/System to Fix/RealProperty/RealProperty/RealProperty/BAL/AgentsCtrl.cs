using System;
using System.Data;
using System.Linq;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class AgentsCtrl : AController, IDisposable
{
	public AgentsAdapter adapter { get; set; }

	public AgentsCtrl()
	{
		adapter = new AgentsAdapter();
	}

	internal int add(Agent item)
	{
		return adapter.add(item);
	}

	internal int edit(Agent item)
	{
		return adapter.edit(item);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal int add_claim(Claim claim)
	{
		return adapter.add(claim, "claims");
	}

	internal int edit_claim(Claim claim)
	{
		return adapter.edit(claim, "claims");
	}

	internal Claim getClaimbyID(int idclaim)
	{
		DataTable tableByID = adapter.getTableByID("claims", "id", idclaim);
		return AController.DataRowToClass<Claim>(tableByID.Select().FirstOrDefault());
	}
}
