import React, { useState, useMemo } from 'react';
import { Client } from '../../types';
import { 
  Search, 
  Plus, 
  Edit3, 
  Trash2, 
  AlertTriangle, 
  Phone, 
  User, 
  MapPin, 
  Heart,
  ChevronRight,
  FileText
} from 'lucide-react';

interface StakeholdersTableProps {
  clients: Client[];
  onNewRecord: () => void;
  onEditClient: (client: Client) => void;
  onDeleteClient: (idclients: number) => void;
  onApply: (client: Client) => void;
  onSelectClient: (client: Client) => void;
}

export const StakeholdersTable: React.FC<StakeholdersTableProps> = ({
  clients,
  onNewRecord,
  onEditClient,
  onDeleteClient,
  onApply,
  onSelectClient
}) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [clientToDelete, setClientToDelete] = useState<Client | null>(null);

  // Filter clients by search query
  const filteredClients = useMemo(() => {
    if (!searchTerm.trim()) return clients;
    const q = searchTerm.toLowerCase();
    return clients.filter((c) =>
      c.firstname.toLowerCase().includes(q) ||
      c.lastname.toLowerCase().includes(q) ||
      c.contactno.toLowerCase().includes(q) ||
      (c.placeofbirth && c.placeofbirth.toLowerCase().includes(q)) ||
      c.idclients.toString().includes(q)
    );
  }, [clients, searchTerm]);

  return (
    <div className="px-8 py-6 space-y-4">
      {/* Top Section: Search and New Record Button */}
      <div className="flex flex-col sm:flex-row items-stretch sm:items-center justify-between gap-3 bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
        {/* Search Input */}
        <div className="relative flex-1 max-w-md">
          <Search className="w-4 h-4 text-slate-400 absolute left-3.5 top-1/2 -translate-y-1/2" />
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search stakeholder by name, contact, place, or ID..."
            className="w-full pl-9.5 pr-4 py-2 bg-slate-50/50 border border-slate-200 rounded-xl text-xs text-slate-800 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
          />
        </div>

        {/* New Record Button */}
        <button
          onClick={onNewRecord}
          className="inline-flex items-center justify-center gap-1.5 px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer"
        >
          <Plus className="w-4 h-4 stroke-[2.5]" />
          <span>New Record</span>
        </button>
      </div>

      {/* Stakeholder Information Table: Strictly 9 Columns Only */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/40">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Stakeholder Information</h3>
            <p className="text-[11px] text-slate-500 mt-0.5">
              Click on any stakeholder row to open their Purchase Details in a fill-up form.
            </p>
          </div>
          <span className="px-2.5 py-1 text-[11px] font-semibold text-emerald-800 bg-emerald-50 rounded-full border border-emerald-200/80">
            {filteredClients.length} Registered Stakeholders
          </span>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-500 bg-slate-50/70">
                <th className="py-3 px-4">ID</th>
                <th className="py-3 px-4">First Name</th>
                <th className="py-3 px-4">LastName</th>
                <th className="py-3 px-4">DOB</th>
                <th className="py-3 px-4">Gender</th>
                <th className="py-3 px-4">Spouse Name</th>
                <th className="py-3 px-4">Place of Birth</th>
                <th className="py-3 px-4">Contact No.</th>
                <th className="py-3 px-6 text-right">Action</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {filteredClients.length === 0 ? (
                <tr>
                  <td colSpan={9} className="py-12 text-center text-slate-400 text-xs">
                    No stakeholders found matching your search.
                  </td>
                </tr>
              ) : (
                filteredClients.map((client) => {
                  return (
                    <tr
                      key={client.idclients}
                      onClick={() => onSelectClient(client)}
                      className="hover:bg-slate-50/80 transition-colors cursor-pointer group"
                      title="Click to view Purchase Details form"
                    >
                      {/* ID */}
                      <td className="py-3.5 px-4 font-mono font-bold text-slate-700">
                        #{client.idclients}
                      </td>

                      {/* First Name */}
                      <td className="py-3.5 px-4 font-semibold text-slate-900 group-hover:text-emerald-800 transition-colors">
                        <div className="flex items-center gap-1.5">
                          <span>{client.firstname}</span>
                          <span className="text-[10px] text-emerald-700 font-normal opacity-0 group-hover:opacity-100 transition-opacity flex items-center">
                            (view form)
                          </span>
                        </div>
                      </td>

                      {/* LastName */}
                      <td className="py-3.5 px-4 font-bold text-slate-900 group-hover:text-emerald-900 transition-colors">
                        {client.lastname}
                      </td>

                      {/* DOB */}
                      <td className="py-3.5 px-4 text-slate-600">
                        {client.dateofbirth || 'N/A'}
                      </td>

                      {/* Gender */}
                      <td className="py-3.5 px-4">
                        <span className={`px-2 py-0.5 rounded-md text-[10px] font-semibold border ${
                          client.gender === 'Female'
                            ? 'bg-rose-50 text-rose-700 border-rose-200'
                            : 'bg-sky-50 text-sky-700 border-sky-200'
                        }`}>
                          {client.gender || 'Male'}
                        </span>
                      </td>

                      {/* Spouse Name */}
                      <td className="py-3.5 px-4 text-slate-600">
                        {client.spousename || 'N/A'}
                      </td>

                      {/* Place of Birth */}
                      <td className="py-3.5 px-4 text-slate-600">
                        {client.placeofbirth || 'South Cotabato'}
                      </td>

                      {/* Contact No. */}
                      <td className="py-3.5 px-4 font-medium text-slate-800">
                        <div className="flex items-center gap-1.5">
                          <Phone className="w-3.5 h-3.5 text-slate-400" />
                          <span>{client.contactno || 'N/A'}</span>
                        </div>
                      </td>

                      {/* Action (EDIT, DELETE, APPLY) */}
                      <td 
                        className="py-3.5 px-6 text-right"
                        onClick={(e) => e.stopPropagation()}
                      >
                        <div className="flex items-center justify-end gap-1.5">
                          {/* EDIT INFORMATION */}
                          <button
                            onClick={() => onEditClient(client)}
                            className="inline-flex items-center gap-1 px-2.5 py-1.5 text-xs font-semibold text-slate-700 bg-white hover:bg-slate-100 border border-slate-200 rounded-lg shadow-2xs transition-colors cursor-pointer"
                            title="Edit Information"
                          >
                            <Edit3 className="w-3.5 h-3.5 text-slate-500" />
                            <span>Edit</span>
                          </button>

                          {/* DELETE */}
                          <button
                            onClick={() => setClientToDelete(client)}
                            className="inline-flex items-center gap-1 px-2.5 py-1.5 text-xs font-semibold text-rose-600 bg-rose-50 hover:bg-rose-100 border border-rose-200/80 rounded-lg transition-colors cursor-pointer"
                            title="Delete Stakeholder"
                          >
                            <Trash2 className="w-3.5 h-3.5" />
                            <span>Delete</span>
                          </button>

                          {/* APPLY */}
                          <button
                            onClick={() => onApply(client)}
                            className="inline-flex items-center gap-1 px-3 py-1.5 text-xs font-semibold text-white bg-[#00593B] hover:bg-[#004a31] rounded-lg shadow-2xs transition-colors cursor-pointer"
                            title="Apply for Lot Purchase (Select Product/Location)"
                          >
                            <Plus className="w-3.5 h-3.5 stroke-[2.5]" />
                            <span>Apply</span>
                          </button>
                        </div>
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* Confirmation Toast Dialog: Delete Stakeholder */}
      {clientToDelete && (
        <div className="fixed bottom-6 right-6 z-50 bg-slate-900/95 backdrop-blur-xs text-white p-4 rounded-2xl shadow-2xl border border-slate-700 max-w-md w-full flex flex-col gap-3 animate-in slide-in-from-bottom-5 duration-200">
          <div className="flex items-start gap-3">
            <div className="p-2 bg-rose-500/20 text-rose-400 rounded-xl shrink-0">
              <AlertTriangle className="w-5 h-5" />
            </div>
            <div className="flex-1">
              <h4 className="text-sm font-bold text-white">Delete Stakeholder Confirmation</h4>
              <p className="text-xs text-slate-300 mt-1 leading-relaxed">
                Are you sure you want to delete <span className="font-bold text-rose-400">{clientToDelete.firstname} {clientToDelete.lastname}</span> (ID #{clientToDelete.idclients})?
              </p>
            </div>
          </div>
          <div className="flex items-center justify-end gap-2 pt-1 border-t border-slate-800">
            <button
              onClick={() => setClientToDelete(null)}
              className="px-3 py-1.5 text-xs font-medium text-slate-300 hover:text-white hover:bg-slate-800 rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              onClick={() => {
                onDeleteClient(clientToDelete.idclients);
                setClientToDelete(null);
              }}
              className="px-3.5 py-1.5 text-xs font-semibold bg-rose-600 hover:bg-rose-700 text-white rounded-xl shadow-xs transition-colors cursor-pointer"
            >
              Yes, Delete
            </button>
          </div>
        </div>
      )}
    </div>
  );
};
