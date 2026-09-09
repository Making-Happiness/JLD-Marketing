import type { PayslipRecord } from '../../types';
import { PayrollTable } from './PayrollTable';
export function PayslipTable({payslips,onGeneratePayslip}:{payslips:PayslipRecord[];onGeneratePayslip?:()=>void}){
  return (
    <div className="payslip-directory-view">
      <PayrollTable payrollRecords={[]} payslips={payslips} onGeneratePayslip={onGeneratePayslip} initialMode="payslips"/>
    </div>
  );
}
