import React, { useState, useMemo } from 'react';
import { Product } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { Search, Plus, Edit3, Trash2, AlertTriangle, MapPin } from 'lucide-react';

interface InventoryTableProps {
  products: Product[];
  onAddProduct: () => void;
  onEditProduct: (product: Product) => void;
  onDeleteProduct: (idproduct: number) => void;
}

export const InventoryTable: React.FC<InventoryTableProps> = ({
  products,
  onAddProduct,
  onEditProduct,
  onDeleteProduct
}) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [productToDelete, setProductToDelete] = useState<Product | null>(null);

  // Filter products by Code or Location
  const filteredProducts = useMemo(() => {
    if (!searchTerm.trim()) return products;
    const q = searchTerm.toLowerCase();
    return products.filter(
      (p) =>
        p.code.toLowerCase().includes(q) ||
        p.location.toLowerCase().includes(q) ||
        p.idproduct.toString().includes(q)
    );
  }, [products, searchTerm]);

  return (
    <div className="px-8 py-6 space-y-4">
      {/* Top Section: Search and Add Button */}
      <div className="flex flex-col sm:flex-row items-stretch sm:items-center justify-between gap-3 bg-white p-4 rounded-2xl border border-slate-200/80 shadow-2xs">
        {/* Search Input */}
        <div className="relative flex-1 max-w-md">
          <Search className="w-4 h-4 text-slate-400 absolute left-3.5 top-1/2 -translate-y-1/2" />
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Search by code, location, or ID..."
            className="w-full pl-9.5 pr-4 py-2 bg-slate-50/50 border border-slate-200 rounded-xl text-xs text-slate-800 placeholder-slate-400 focus:bg-white focus:outline-none focus:ring-2 focus:ring-emerald-600/20 focus:border-emerald-600 transition-all"
          />
        </div>

        {/* Add Product Button */}
        <button
          onClick={onAddProduct}
          className="inline-flex items-center justify-center gap-1.5 px-4 py-2 bg-[#00593B] hover:bg-[#004a31] text-white text-xs font-semibold rounded-xl shadow-xs transition-all cursor-pointer"
        >
          <Plus className="w-4 h-4 stroke-[2.5]" />
          <span>Add Product</span>
        </button>
      </div>

      {/* Main Table: Strictly ONLY ID | Code | Location | Total Block | Total Lot | Total Area | Cash Price | Action */}
      <div className="bg-white rounded-2xl border border-slate-200/80 shadow-2xs overflow-hidden">
        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse text-xs">
            <thead>
              <tr className="border-b border-slate-100 text-[11px] font-semibold text-slate-400 bg-slate-50/50">
                <th className="py-3 px-4 font-semibold text-slate-500">ID</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Code</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Location</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Total Block</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Total Lot</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Total Area</th>
                <th className="py-3 px-4 font-semibold text-slate-500">Cash Price</th>
                <th className="py-3 px-6 text-right font-semibold text-slate-500">Action</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {filteredProducts.length === 0 ? (
                <tr>
                  <td colSpan={8} className="py-12 text-center text-slate-400 text-xs">
                    No products found matching your search.
                  </td>
                </tr>
              ) : (
                filteredProducts.map((prod) => (
                  <tr key={prod.idproduct} className="hover:bg-slate-50/60 transition-colors">
                    {/* ID */}
                    <td className="py-3.5 px-4 font-mono font-bold text-slate-700">
                      #{prod.idproduct}
                    </td>

                    {/* Code */}
                    <td className="py-3.5 px-4 font-mono font-bold text-slate-900">
                      <span className="px-2 py-0.5 rounded-md bg-emerald-50 text-emerald-800 border border-emerald-200/80 text-[11px]">
                        {prod.code}
                      </span>
                    </td>

                    {/* Location */}
                    <td className="py-3.5 px-4 font-medium text-slate-800">
                      <div className="flex items-center gap-1.5">
                        <MapPin className="w-3.5 h-3.5 text-emerald-600 shrink-0" />
                        <span>{prod.location}</span>
                      </div>
                    </td>

                    {/* Total Block */}
                    <td className="py-3.5 px-4 text-slate-600 font-medium">
                      {prod.totalblockno}
                    </td>

                    {/* Total Lot */}
                    <td className="py-3.5 px-4 text-slate-600 font-medium">
                      {prod.totallotno}
                    </td>

                    {/* Total Area */}
                    <td className="py-3.5 px-4 text-slate-600">
                      {prod.totalarea.toLocaleString()} sq.m
                    </td>

                    {/* Cash Price */}
                    <td className="py-3.5 px-4 font-bold text-slate-900">
                      {formatCurrency(prod.cashprice)}
                    </td>

                    {/* Action (EDIT and DELETE) */}
                    <td className="py-3.5 px-6 text-right">
                      <div className="flex items-center justify-end gap-1.5">
                        {/* EDIT */}
                        <button
                          onClick={() => onEditProduct(prod)}
                          className="inline-flex items-center gap-1 px-2.5 py-1.5 text-xs font-semibold text-emerald-700 bg-emerald-50 hover:bg-emerald-100/80 border border-emerald-200/70 rounded-lg transition-colors cursor-pointer"
                          title="Edit Product"
                        >
                          <Edit3 className="w-3.5 h-3.5" />
                          <span>Edit</span>
                        </button>

                        {/* DELETE */}
                        <button
                          onClick={() => setProductToDelete(prod)}
                          className="inline-flex items-center gap-1 px-2.5 py-1.5 text-xs font-semibold text-rose-600 bg-rose-50 hover:bg-rose-100/80 border border-rose-200/70 rounded-lg transition-colors cursor-pointer"
                          title="Delete Product"
                        >
                          <Trash2 className="w-3.5 h-3.5" />
                          <span>Delete</span>
                        </button>
                      </div>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* Delete Confirmation Toast Dialog */}
      {productToDelete && (
        <div className="fixed bottom-6 right-6 z-50 bg-slate-900/95 backdrop-blur-xs text-white p-4 rounded-2xl shadow-2xl border border-slate-700 max-w-md w-full flex flex-col gap-3 animate-in slide-in-from-bottom-5 duration-200">
          <div className="flex items-start gap-3">
            <div className="p-2 bg-rose-500/20 text-rose-400 rounded-xl shrink-0">
              <AlertTriangle className="w-5 h-5" />
            </div>
            <div className="flex-1">
              <h4 className="text-sm font-bold text-white">Delete Product Confirmation</h4>
              <p className="text-xs text-slate-300 mt-1 leading-relaxed">
                Are you sure you want to delete <span className="font-bold text-rose-400">{productToDelete.code}</span> ({productToDelete.location})?
              </p>
            </div>
          </div>
          <div className="flex items-center justify-end gap-2 pt-1 border-t border-slate-800">
            <button
              onClick={() => setProductToDelete(null)}
              className="px-3 py-1.5 text-xs font-medium text-slate-300 hover:text-white hover:bg-slate-800 rounded-xl transition-colors cursor-pointer"
            >
              Cancel
            </button>
            <button
              onClick={() => {
                onDeleteProduct(productToDelete.idproduct);
                setProductToDelete(null);
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
