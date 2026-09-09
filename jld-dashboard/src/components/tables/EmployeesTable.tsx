import type { Employee } from '../../types';
import { formatCurrency } from '../../utils/calculations';
import { RecordTable } from './RecordTable';
interface EmployeesTableProps{employees:Employee[];onAddEmployee?:()=>void;onArchiveEmployee?:(r:Employee)=>void;onRestoreEmployee?:(r:Employee)=>void;onPermanentDeleteEmployee?:(r:Employee)=>void;onViewHistory?:(r:Employee)=>void;}
export function EmployeesTable(p:EmployeesTableProps){
  return (
    <div className="employees-table-view">
      <RecordTable
        rows={p.employees}
        rowKey={r=>r.idemployee}
        searchText={r=>`${r.idemployee} ${r.fullname} ${r.designation} ${r.contactno}`}
        addLabel="Add employee"
        onAdd={p.onAddEmployee}
        actions={{onArchive:p.onArchiveEmployee,onRestore:p.onRestoreEmployee,onDelete:p.onPermanentDeleteEmployee,onHistory:p.onViewHistory}}
        columns={[
          {label:'Employee ID',render:r=><span className="employee-id-badge">{`EMP-${r.idemployee}`}</span>},
          {label:'Employee name',render:r=><strong className="employee-fullname-text">{r.fullname}</strong>},
          {label:'Designation',render:r=><span className="employee-designation-text">{r.designation||'—'}</span>},
          {label:'Contact number',render:r=><span className="employee-contact-text">{r.contactno||'—'}</span>},
          {label:'Gender',render:r=><span className="employee-gender-text">{r.gender||'—'}</span>},
          {label:'Civil status',render:r=><span className="employee-status-text">{r.civilstatus||'—'}</span>},
          {label:'Daily rate (PHP)',numeric:true,render:r=><span className="employee-salary-text">{formatCurrency(r.salary)}</span>}
        ]}
      />
    </div>
  );
}
