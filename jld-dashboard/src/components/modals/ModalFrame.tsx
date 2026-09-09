import { useEffect, useId, useRef, type ReactNode } from 'react';
import { createPortal } from 'react-dom';
export function ModalFrame({children,onClose,title}:{children:ReactNode;onClose:()=>void;title:string}){
 const ref=useRef<HTMLDialogElement>(null);const id=useId();
 useEffect(()=>{const dialog=ref.current;if(!dialog)return;const previous=document.activeElement as HTMLElement|null;dialog.showModal();
 dialog.querySelectorAll('label').forEach((label,index)=>{if(label.htmlFor||label.querySelector('input,select,textarea'))return;const control=label.parentElement?.querySelector('input,select,textarea') as HTMLElement|null;if(control){control.id ||= `${id}-field-${index}`;label.htmlFor=control.id;}});
 document.body.classList.add('has-record-dialog');return()=>{dialog.close();if(!document.querySelector('dialog[open]'))document.body.classList.remove('has-record-dialog');previous?.focus();};},[id]);
 return createPortal(<dialog ref={ref} className="modal-dialog-frame record-dialog" aria-label={title} onCancel={e=>{e.preventDefault();onClose();}}>{children}</dialog>,document.body);
}

