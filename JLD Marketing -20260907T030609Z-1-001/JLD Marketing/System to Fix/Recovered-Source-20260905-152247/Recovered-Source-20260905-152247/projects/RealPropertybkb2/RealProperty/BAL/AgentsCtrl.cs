using System;
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
}
