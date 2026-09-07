import React, { useState, useMemo } from 'react';
import { Agent } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { Users, Award, DollarSign, ArrowUpRight, CheckCircle2, Search, Plus } from 'lucide-react';

interface AgentsTableProps {
  agents: Agent[];
  onReleaseClaim?: (agent: Agent) => void;
  onAddAgent?: () => void;
}

export const AgentsTable: React.FC<AgentsTableProps> = ({
  agents,
  onReleaseClaim,
  onAddAgent
}) => {
  const [claimModalAgent, setClaimModalAgent] = useState<Agent | null>(null);
  const [claimAmount, setClaimAmount] = useState<number>(10000);
  const [claimDesc, setClaimDesc] = useState<string>('Commission Claim Release Voucher');
  const [searchTerm, setSearchTerm] = useState('');

  const totalSalesAll = agents.reduce((acc, a) => acc + a.totalSales, 0);
  const totalEarnedAll = agents.reduce((acc, a) => acc + a.totalEarned, 0);
  const totalClaimedAll = agents.reduce((acc, a) => acc + a.totalClaimed, 0);
  const totalBalanceAll = agents.reduce((acc, a) => acc + a.balance, 0);

  const filteredAgents = useMemo(() => {
    if (!searchTerm.trim()) return agents;
    const q = searchTerm.toLowerCase();
    return agents.filter(a => 
      a.fullname.toLowerCase().includes(q) ||
      a.role.toLowerCase().includes(q) ||
      a.contactno.toLowerCase().includes(q)
    );
  }, [agents, searchTerm]);

  return (
    <div className="px-8 py-5 space-y-4">
      {/* Commission Summary Cards */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-3.5">
        <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
          <span className="text-[11px] text-slate-500 font-medium">Total Network Sales</span>
          <div className="text-xl font-bold text-slate-900 mt-1">{formatCurrency(totalSalesAll)}</div>
        </div>
        <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
          <span className="text-[11px] text-slate-500 font-medium">Total Commissions Earned</span>
          <div className="text-xl font-bold text-emerald-800 mt-1">{formatCurrency(totalEarnedAll)}</div>
        </div>
        <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
          <span className="text-[11px] text-slate-500 font-medium">Total Claims Released</span>
          <div className="text-xl font-bold text-slate-900 mt-1">{formatCurrency(totalClaimedAll)}</div>
        </div>
        <div className="bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
          <span className="text-[11px] text-slate-500 font-medium">Pending Unclaimed Balance</span>
          <div className="text-xl font-bold text-amber-700 mt-1">{formatCurrency(totalBalanceAll)}</div>
        </div>
      </div>

      {/* Agents & Commission Unified Center Card */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-5 border-b border-slate-100 flex flex-wrap items-center justify-between gap-4">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Unified Agent &amp; Commission Center</h3>
            <p className="text-xs text-slate-500 mt-0.5">
              Corresponds to JLD C# <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmCommision.cs</code> &amp; <code className="text-emerald-700 bg-emerald-50 px-1 rounded">frmAgentDicer.cs</code>
            </p>
          </div>

          <div className="flex items-center gap-3">
            <div className="relative">
              <Search className="w-3.5 h-3.5 text-slate-400 absolute left-3 top-1/2 -translate-y-1/2" />
              <input
                type="text"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search agent or role..."
                className="pl-8 pr-3 py-1.5 rounded-xl border border-slate-200 text-xs focus:outline-hidden focus:ring-2 focus:ring-emerald-500 w-52"
              />
            </div>

            <button
              type="button"
              onClick={onAddAgent}
              className="px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer flex items-center gap-1.5"
            >
              <Plus className="w-3.5 h-3.5" />
              <span>+ New Agent</span>
            </button>
          </div>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-6">Agent / Dicer</th>
                <th className="py-3 px-4">Role</th>
                <th className="py-3 px-4">Contact #</th>
                <th className="py-3 px-4">Commission %</th>
                <th className="py-3 px-4">Gross Sales Volume</th>
                <th className="py-3 px-4">Earned Commission</th>
                <th className="py-3 px-4">Released Claims</th>
                <th className="py-3 px-4">Available Balance</th>
                <th className="py-3 px-6 text-right">Action</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {filteredAgents.length === 0 ? (
                <tr>
                  <td colSpan={9} className="py-8 text-center text-slate-400 text-xs">
                    No agents or dicers found matching your search.
                  </td>
                </tr>
              ) : (
                filteredAgents.map((agent) => (
                  <tr key={agent.id} className="hover:bg-slate-50/60 transition-colors">
                  <td className="py-3.5 px-6">
                    <div className="flex items-center gap-2.5">
                      <img
                        src={agent.avatarUrl}
                        alt={agent.fullname}
                        className="w-7 h-7 rounded-full object-cover ring-1 ring-slate-200"
                      />
                      <span className="font-bold text-slate-900">{agent.fullname}</span>
                    </div>
                  </td>
                  <td className="py-3.5 px-4 font-medium text-slate-700">
                    {agent.role}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {agent.contactno}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-emerald-800">
                    <span className="bg-emerald-50 border border-emerald-200 px-2 py-0.5 rounded-full">
                      {agent.commissionRate}%
                    </span>
                  </td>
                  <td className="py-3.5 px-4 font-semibold text-slate-900">
                    {formatCurrency(agent.totalSales)}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-slate-900">
                    {formatCurrency(agent.totalEarned)}
                  </td>
                  <td className="py-3.5 px-4 text-slate-600">
                    {formatCurrency(agent.totalClaimed)}
                  </td>
                  <td className="py-3.5 px-4 font-bold text-emerald-700">
                    {formatCurrency(agent.balance)}
                  </td>
                  <td className="py-3.5 px-6 text-right">
                    <button
                      onClick={() => setClaimModalAgent(agent)}
                      className="px-3 py-1.5 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-lg shadow-2xs transition-colors cursor-pointer"
                    >
                      Release Claim
                    </button>
                  </td>
                </tr>
              )))}
            </tbody>
          </table>
        </div>
      </div>

      {/* Claim Voucher Modal */}
      {claimModalAgent && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-900/40 backdrop-blur-xs p-4">
          <div className="bg-white rounded-2xl max-w-md w-full p-6 border border-slate-200 shadow-xl space-y-4 text-xs">
            <h3 className="text-base font-bold text-slate-900">Release Commission Claim</h3>
            <p className="text-slate-500">
              Agent: <strong className="text-slate-800">{claimModalAgent.fullname}</strong> • Available Balance: <strong className="text-emerald-700">{formatCurrency(claimModalAgent.balance)}</strong>
            </p>

            <div>
              <label className="block font-semibold text-slate-700 mb-1">Claim Amount (₱)</label>
              <input
                type="number"
                step="1000"
                max={claimModalAgent.balance}
                value={claimAmount}
                onChange={(e) => setClaimAmount(Number(e.target.value))}
                className="w-full border border-slate-200 rounded-xl px-3 py-2 font-bold text-slate-900 text-sm outline-none focus:border-emerald-600"
              />
            </div>

            <div>
              <label className="block font-semibold text-slate-700 mb-1">Description / Voucher Note</label>
              <input
                type="text"
                value={claimDesc}
                onChange={(e) => setClaimDesc(e.target.value)}
                className="w-full border border-slate-200 rounded-xl px-3 py-2 text-slate-800 outline-none focus:border-emerald-600"
              />
            </div>

            <div className="flex justify-end gap-2 pt-2">
              <button
                onClick={() => setClaimModalAgent(null)}
                className="px-4 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 font-semibold rounded-xl cursor-pointer"
              >
                Cancel
              </button>
              <button
                onClick={() => {
                  alert(`Commission voucher of ${formatCurrency(claimAmount)} successfully released to ${claimModalAgent.fullname}!`);
                  setClaimModalAgent(null);
                }}
                className="px-5 py-2 bg-[#00593B] hover:bg-[#004a31] text-white font-semibold rounded-xl cursor-pointer"
              >
                Confirm Release
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

