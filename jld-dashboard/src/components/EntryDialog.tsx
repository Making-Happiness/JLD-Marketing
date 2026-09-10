import { ModalFrame } from './modals/ModalFrame';
import { FilePlus2 } from 'lucide-react';
import { useState } from 'react';
import { X } from 'lucide-react';
import type { Employee } from '../types';
export type EntryKind='agent'|'employee'|'loan'|'benefit'|'expense'|'payroll'|'commission';
export type EntryValues=Record<string,string>;
export function EntryDialog({kind,employees,onClose,onSave,claimName,claimBalance}:{kind:EntryKind;employees:Employee[];onClose:()=>void;onSave:(v:EntryValues)=>string|void;claimName?:string;claimBalance?:number}){
  const [error,setError]=useState('');

  const titles:Record<EntryKind,string>={
    agent:'Register agent',
    employee:'Add employee',
    loan:'Record loan / cash advance',
    benefit:'Add employee benefit',
    expense:'Record expense voucher',
    payroll:'Prepare payroll',
    commission:'Release commission'
  };

  const eyebrows:Record<EntryKind,string>={
    agent:'PERSONNEL & AGENTS',
    employee:'HUMAN RESOURCES',
    loan:'LOANS & ADVANCES',
    benefit:'EMPLOYEE BENEFITS',
    expense:'DISBURSEMENTS',
    payroll:'PAYROLL PROCESSING',
    commission:'COMMISSION REVENUE'
  };

  const field=(name:string,label:string,type='text',required=true,extra:Record<string,unknown>={})=>(
    <label key={name} className="entry-dialog-field-label">
      <span>{label} {required && <span className="req text-rose-500">*</span>}</span>
      <input className="entry-dialog-field-input" name={name} type={type} required={required} {...extra}/>
    </label>
  );

  return (
    <ModalFrame onClose={onClose} title={titles[kind]}>
      <div className="entry-dialog-shell form-shell">
        <header className="entry-dialog-header form-header">
          <div className="entry-dialog-icon-wrapper form-heading-icon">
            <FilePlus2 size={22}/>
          </div>
          <div className="entry-dialog-title-group">
            <span className="entry-dialog-eyebrow form-eyebrow">{eyebrows[kind]}</span>
            <h2 className="entry-dialog-title">{titles[kind]}</h2>
            <p className="entry-dialog-subtitle">Complete the details below to save this record.</p>
          </div>
          <button className="entry-dialog-close-btn form-close" onClick={onClose} aria-label="Close form">
            <X size={18}/>
          </button>
        </header>

        <form className="entry-dialog-form" onSubmit={e=>{
          e.preventDefault();
          const values=Object.fromEntries(new FormData(e.currentTarget).entries()) as EntryValues;
          const result=onSave(values);
          if(result) setError(result);
          else onClose();
        }}>
          <div className="entry-dialog-body form-body">
            <p className="entry-dialog-note form-note">
              {kind==='payroll'
                ? 'Prepare a draft for every active employee using a daily rate and the working days below. Review the results before approval. No cash is released.'
                : kind==='commission'
                ? `Release a voucher for ${claimName}. Available balance: ₱${claimBalance?.toLocaleString('en-PH')}. The release is recorded once in expenses.`
                : 'Saved entries appear immediately in the accounting ledgers and connected transactions.'}
            </p>

            <fieldset className="entry-dialog-fieldset">
              <legend className="entry-dialog-legend">
                {kind==='payroll'?'Payroll period':kind==='agent'||kind==='employee'?'Record details':'Transaction details'}
              </legend>
              <div className="entry-dialog-grid form-grid">
                {(kind==='agent'||kind==='employee')&&<>
                  {field('firstname','First name')}
                  {field('lastname','Last name')}
                  {field('contact','Contact number','tel')}
                  {field('role',kind==='agent'?'Role / designation':'Designation')}
                  {field('rate',kind==='agent'?'Commission rate (%)':'Daily rate (PHP)','number',true,{min:0,step:0.01,max:kind==='agent'?100:undefined})}
                </>}

                {(kind==='loan'||kind==='benefit'||kind==='expense')&&
                  <label className="entry-dialog-field-label full">
                    <span>{kind==='expense'?'Receiving employee':'Employee'} <span className="req text-rose-500">*</span></span>
                    <select className="entry-dialog-field-select" name="employee" required defaultValue="">
                      <option value="" disabled>Select an active employee</option>
                      {employees.filter(e=>e.status==='active').map(e=><option key={e.idemployee} value={e.idemployee}>{e.fullname}</option>)}
                    </select>
                  </label>
                }

                {(kind==='loan'||kind==='benefit'||kind==='expense')&&<>
                  {field('description','Description')}
                  {field('amount','Amount (PHP)','number',true,{min:0.01,step:0.01})}
                  {field('date','Transaction date','date',true,{defaultValue:new Date().toLocaleDateString('en-CA')})}
                </>}

                {kind==='loan'&&<>
                  {field('amortization','Deduction per cutoff (PHP)','number',true,{min:0.01,step:0.01})}
                  <label className="entry-dialog-field-label">
                    <span>Category <span className="req text-rose-500">*</span></span>
                    <select className="entry-dialog-field-select" name="category">
                      <option>CASH ADVANCE</option>
                      <option>SALARY LOAN</option>
                      <option>EMERGENCY LOAN</option>
                    </select>
                  </label>
                </>}

                {kind==='benefit'&&
                  <label className="entry-dialog-field-label">
                    <span>Category <span className="req text-rose-500">*</span></span>
                    <select className="entry-dialog-field-select" name="category">
                      <option>ALLOWANCE</option>
                      <option>BONUS</option>
                      <option>COMMISSION INCENTIVE</option>
                    </select>
                  </label>
                }

                {kind==='expense'&&field('purpose','Purpose / category')}

                {kind==='payroll'&&<>
                  {field('start','Period start','date')}
                  {field('end','Period end','date')}
                  {field('days','Working days','number',true,{min:1,max:31,defaultValue:13})}
                  <div className="entry-dialog-field-label full form-note" style={{margin:'4px 0 0'}}>
                    <strong style={{color:'#1e5539'}}>Rate basis notice:</strong>
                    <span>Employee salary is treated as a daily rate. Confirm rates in the Employees directory before processing.</span>
                  </div>
                </>}

                {kind==='commission'&&<>
                  {field('amount','Release amount (PHP)','number',true,{min:0.01,max:claimBalance,step:0.01,defaultValue:claimBalance})}
                  {field('date','Release date','date',true,{defaultValue:new Date().toLocaleDateString('en-CA')})}
                </>}
              </div>
            </fieldset>

            {error&&<p role="alert" className="entry-dialog-error-alert form-error">{error}</p>}
          </div>

          <div className="entry-dialog-actions form-actions">
            <button type="button" className="entry-dialog-cancel-btn secondary-button" onClick={onClose}>Cancel</button>
            <button className="entry-dialog-submit-btn primary-button" type="submit">
              {kind==='payroll'?'Prepare draft payroll':kind==='commission'?'Record release':'Save record'}
            </button>
          </div>
        </form>
      </div>
    </ModalFrame>
  );
}
