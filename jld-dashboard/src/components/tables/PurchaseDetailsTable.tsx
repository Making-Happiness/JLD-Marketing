import React, { useState, useMemo } from 'react';
import { PurchaseDetail } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { 
  Search, 
  Plus, 
  Edit3, 
  Trash2, 
  Receipt, 
  AlertTriangle, 
  MapPin, 
  User, 
  Calendar,
  Building2,
  ChevronLeft,
  ChevronRight
} from 'lucide-react';

interface PurchaseDetailsTableProps {
  purchases: PurchaseDetail[];
  onNewPurchase: () => void;
  onEditPurchase: (purchase: PurchaseDetail) => void;
  onDeletePurchase: (id: number) => void;
  onViewDetails: (purchase: PurchaseDetail) => void;
}

export const PurchaseDetailsTable: React.FC<PurchaseDetailsTableProps> = ({
  purchases,
  onNewPurchase,
  onEditPurchase,
  onDeletePurchase,
  onViewDetails
}) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [purchaseToDelete, setPurchaseToDelete] = useState<PurchaseDetail | null>(null);

  // Pagination
  const [currentPage, setCurrentPage] = useState(1);
  const rowsPerPage = 10;

  // Filter purchases by client name, block, lot, location, agent, remarks
  const filteredPurchases = useMemo(() => {
    if (!searchTerm.trim()) return purchases;
    const q = searchTerm.toLowerCase();
    return purchases.filter(
      (p) =>
        p.clientName.toLowerCase().includes(q) ||
        p.location.toLowerCase().includes(q) ||
        p.productCode.toLowerCase().includes(q) ||
        p.agentName.toLowerCase().includes(q) ||
        p.blockno.toString().includes(q) ||
        p.lotno.toString().includes(q) ||
        (p.remarks && p.remarks.toLowerCase().includes(q))
    );
  }, [purchases, searchTerm]);

  const totalPages = Math.ceil(filteredPurchases.length / rowsPerPage) || 1;
  const paginatedPurchases = filteredPurchases.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  return (
    <div className="px-8 py-6 space-y-4">
      {/* Top Section: Search and New Lot Purchase Button */}
      <div className="flex flex-col sm:flex-row items-stretch sm:items-center justify-between gap-3 bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
        {/* Search Input */}
        <div className="relative flex-1 max-w-md">
          <Search className="w-4 h-4 text-slate-400 absolute left-3.5 top-1/2 -translate-y-1/2" />
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => {
              setSearchTerm(e.target.value);
              setCurrentPage(1);
            }}
            placeholder="Search by buyer, block, lot, location, or agent..."
            className="w-full pl-9.5 pr-4 py-2 bg-slate-50/50 border border-slate-200 rounded-xl text-xs text-slate-800 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
          />
        </div>

        {/* New Purchase Button */}
        <button
          onClick={onNewPurchase}
          className="inline-flex items-center justify-center gap-1.5 px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer"
        >
          <Plus className="w-4 h-4 stroke-[2.5]" />
          <span>New Lot Purchase</span>
        </button>
      </div>

      {/* Main Table Format: Application & Purchase Information */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="p-4 border-b border-slate-100 flex items-center justify-between bg-slate-50/40">
          <div>
            <h3 className="text-sm font-bold text-slate-900">Application Information &amp; Purchase Records</h3>
            <p className="text-[11px] text-slate-500 mt-0.5">
              Comprehensive ledger of subdivision lot purchases, financing terms, and agent commissions.
            </p>
          </div>
          <span className="px-2.5 py-1 text-[11px] font-semibold text-emerald-800 bg-emerald-50 rounded-full border border-emerald-200/80">
            {filteredPurchases.length} Active Accounts
          </span>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-500 bg-slate-50/70">
                <th className="py-3 px-3">#</th>
                <th className="py-3 px-4">Buyer / Stakeholder</th>
                <th className="py-3 px-3">Location</th>
                <th className="py-3 px-3">Block No</th>
                <th className="py-3 px-3">Lot No</th>
                <th className="py-3 px-3">Area (sq.m.)</th>
                <th className="py-3 px-3">Lot Price</th>
                <th className="py-3 px-3">Terms [0=Cash]</th>
                <th className="py-3 px-4">Agent / Dicer</th>
                <th className="py-3 px-3">Agent Commision</th>
                <th className="py-3 px-3">Monthly Payment</th>
                <th className="py-3 px-3">Processing / Docs Fee</th>
                <th className="py-3 px-3">Penalty</th>
                <th className="py-3 px-3">Due Date</th>
                <th className="py-3 px-4">Remarks</th>
                <th className="py-3 px-6 text-right">Action</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {paginatedPurchases.length === 0 ? (
                <tr>
                  <td colSpan={16} className="py-12 text-center text-slate-400 text-xs">
                    No purchase records found matching your search.
                  </td>
                </tr>
              ) : (
                paginatedPurchases.map((item) => {
                  const commAmount = (item.lotprice * item.agentpercentage) / 100;

                  return (
                    <tr key={item.id} className="hover:bg-slate-50/70 transition-colors">
                      {/* ID */}
                      <td className="py-3.5 px-3 font-mono font-bold text-slate-400 text-[11px]">
                        #{item.id}
                      </td>

                      {/* Buyer / Stakeholder */}
                      <td className="py-3.5 px-4 font-bold text-slate-900">
                        <div className="flex items-center gap-2">
                          <div className="w-6 h-6 rounded-full bg-emerald-100 text-emerald-800 flex items-center justify-center font-bold text-[10px]">
                            {item.clientName.charAt(0)}
                          </div>
                          <div>
                            <span className="block leading-tight">{item.clientName}</span>
                            <span className="text-[10px] text-slate-400 font-normal">Buyer ID #{item.idclients}</span>
                          </div>
                        </div>
                      </td>

                      {/* Location */}
                      <td className="py-3.5 px-3 text-slate-700 font-medium">
                        <div className="flex items-center gap-1">
                          <MapPin className="w-3 h-3 text-emerald-600 shrink-0" />
                          <span className="truncate max-w-[130px]" title={item.location}>{item.location}</span>
                        </div>
                      </td>

                      {/* Block No */}
                      <td className="py-3.5 px-3 font-mono font-bold text-slate-800">
                        Blk {item.blockno}
                      </td>

                      {/* Lot No */}
                      <td className="py-3.5 px-3 font-mono font-bold text-slate-800">
                        Lot {item.lotno}
                      </td>

                      {/* Area (sq.m.) */}
                      <td className="py-3.5 px-3 text-slate-600">
                        {item.area.toLocaleString()} sq.m
                      </td>

                      {/* Lot Price */}
                      <td className="py-3.5 px-3 font-bold text-slate-900">
                        {formatCurrency(item.lotprice)}
                      </td>

                      {/* Terms [0=Cash] */}
                      <td className="py-3.5 px-3">
                        <span className={`px-2 py-0.5 rounded-md text-[10px] font-semibold border ${
                          item.terms === 0
                            ? 'bg-purple-50 text-purple-700 border-purple-200'
                            : 'bg-slate-100 text-slate-700 border-slate-200'
                        }`}>
                          {item.terms === 0 ? '0 (Cash)' : `${item.terms} yr${item.terms > 1 ? 's' : ''}`}
                        </span>
                      </td>

                      {/* Agent / Dicer */}
                      <td className="py-3.5 px-4 font-semibold text-slate-800">
                        {item.agentName}
                      </td>

                      {/* Agent Commision */}
                      <td className="py-3.5 px-3 text-slate-700 font-medium">
                        <div>{item.agentpercentage}%</div>
                        <span className="text-[10px] text-slate-400 font-normal">({formatCurrency(commAmount)})</span>
                      </td>

                      {/* Monthly Payment */}
                      <td className="py-3.5 px-3 font-bold text-emerald-950">
                        {item.terms === 0 ? '₱0 (Spot)' : formatCurrency(item.amortization)}
                      </td>

                      {/* Processing / Docs Fee */}
                      <td className="py-3.5 px-3 text-slate-600">
                        {formatCurrency(item.otherfees || 0)}
                      </td>

                      {/* Penalty */}
                      <td className="py-3.5 px-3">
                        {item.penalty > 0 ? (
                          <span className="text-rose-600 font-bold">{formatCurrency(item.penalty)}</span>
                        ) : (
                          <span className="text-slate-400">₱0</span>
                        )}
                      </td>

                      {/* Due Date */}
                      <td className="py-3.5 px-3 text-slate-600 font-mono text-[11px]">
                        {item.duedate || 'N/A'}
                      </td>

                      {/* Remarks */}
                      <td className="py-3.5 px-4 text-slate-500 text-[11px] max-w-xs truncate" title={item.remarks}>
                        {item.remarks || 'None'}
                      </td>

                      {/* Action (Edit, Details, Delete) */}
                      <td className="py-3.5 px-6 text-right">
                        <div className="flex items-center justify-end gap-1">
                          {/* EDIT = EDIT INFORMATION */}
                          <button
                            onClick={() => onEditPurchase(item)}
                            className="inline-flex items-center gap-1 px-2.5 py-1 text-xs font-semibold text-emerald-700 bg-emerald-50 hover:bg-emerald-100 border border-emerald-200/80 rounded-lg transition-colors cursor-pointer"
                            title="Edit Information"
                          >
                            <Edit3 className="w-3.5 h-3.5" />
                            <span>Edit</span>
                          </button>

                          {/* DETAILS = PAYMENT HISTORY */}
                          <button
                            onClick={() => onViewDetails(item)}
                            className="inline-flex items-center gap-1 px-2.5 py-1 text-xs font-semibold text-slate-700 bg-slate-100 hover:bg-slate-200 border border-slate-200 rounded-lg transition-colors cursor-pointer"
                            title="Details / Statement of Account & Ledger"
                          >
                            <Receipt className="w-3.5 h-3.5 text-slate-600" />
                            <span>Details</span>
                          </button>

                          {/* DELETE */}
                          <button
                            onClick={() => setPurchaseToDelete(item)}
                            className="inline-flex items-center gap-1 px-2 py-1 text-xs font-semibold text-rose-600 bg-rose-50 hover:bg-rose-100 border border-rose-200/80 rounded-lg transition-colors cursor-pointer"
                            title="Delete Purchase Account"
                          >
                            <Trash2 className="w-3.5 h-3.5" />
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

        {/* Pagination Footer */}
        <div className="px-6 py-3.5 border-t border-slate-100 flex items-center justify-between text-xs text-slate-500">
          <div>
            Showing {(currentPage - 1) * rowsPerPage + 1} to {Math.min(currentPage * rowsPerPage, filteredPurchases.length)} of {filteredPurchases.length} purchase records
          </div>
          <div className="flex items-center gap-1">
            <button
              onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
              disabled={currentPage === 1}
              className="p-1.5 rounded-lg border border-slate-200 hover:bg-slate-50 disabled:opacity-40 disabled:cursor-not-allowed cursor-pointer"
            >
              <ChevronLeft className="w-4 h-4" />
            </button>
            <span className="px-2 font-medium">Page {currentPage} of {totalPages}</span>
            <button
              onClick={() => setCurrentPage(p => Math.min(totalPages, p + 1))}
              disabled={currentPage === totalPages}
              className="p-1.5 rounded-lg border border-slate-200 hover:bg-slate-50 disabled:opacity-40 disabled:cursor-not-allowed cursor-pointer"
            >
              <ChevronRight className="w-4 h-4" />
            </button>
          </div>
        </div>
      </div>

      {/* Delete Confirmation Toast Dialog */}
      {purchaseToDelete && (
        <div className="fixed bottom-6 right-6 z-50 bg-slate-900/95 backdrop-blur-xs text-white p-4 rounded-2xl shadow-2xl border border-slate-700 max-w-md w-full flex flex-col gap-3 animate-in slide-in-from-bottom-5 duration-200">
          <div className="flex items-start gap-3">
            <div className="p-2 bg-rose-500/20 text-rose-400 rounded-xl shrink-0">
              <AlertTriangle className="w-5 h-5" />
            </div>
            <div className="flex-1">
              <h4 className="text-sm font-bold text-white">Delete Purchase Account</h4>
              <p className="text-xs text-slate-300 mt-1 leading-relaxed">
                Are you sure you want to delete purchase record for <span className="font-bold text-rose-400">{purchaseToDelete.clientName}</span> (Blk {purchaseToDelete.blockno} Lot {purchaseToDelete.lotno})?
              </p>
            </div>
          </div>
          <div className="flex items-center justify-end gap-2 pt-1 border-t border-slate-800">
            <button
              onClick={() => setPurchaseToDelete(null)}
              className="px-3 py-1.5 text-xs font-medium text-slate-300 hover:text-white hover:bg-slate-800 rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              onClick={() => {
                onDeletePurchase(purchaseToDelete.id);
                setPurchaseToDelete(null);
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

