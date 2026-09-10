import { useEffect, useId, useRef, type ReactNode } from 'react';
import { createPortal } from 'react-dom';

interface ModalFrameProps {
  children: ReactNode;
  onClose: () => void;
  title: string;
}

export function ModalFrame({ children, onClose, title }: ModalFrameProps) {
  const ref = useRef<HTMLDialogElement>(null);
  const id = useId();
  const mouseDownOutsideRef = useRef<boolean>(false);

  useEffect(() => {
    const dialog = ref.current;
    if (!dialog) return;
    const previous = document.activeElement as HTMLElement | null;
    dialog.showModal();

    dialog.querySelectorAll('label').forEach((label, index) => {
      if (label.htmlFor || label.querySelector('input,select,textarea')) return;
      const control = label.parentElement?.querySelector('input,select,textarea') as HTMLElement | null;
      if (control) {
        control.id ||= `${id}-field-${index}`;
        label.htmlFor = control.id;
      }
    });

    document.body.classList.add('has-record-dialog');
    return () => {
      dialog.close();
      if (!document.querySelector('dialog[open]')) {
        document.body.classList.remove('has-record-dialog');
      }
      previous?.focus();
    };
  }, [id]);

  const handleMouseDown = (e: React.MouseEvent<HTMLDialogElement>) => {
    if (e.target === ref.current) {
      const rect = ref.current.getBoundingClientRect();
      mouseDownOutsideRef.current = (
        e.clientX < rect.left ||
        e.clientX > rect.right ||
        e.clientY < rect.top ||
        e.clientY > rect.bottom
      );
    } else {
      mouseDownOutsideRef.current = false;
    }
  };

  const handleClick = (e: React.MouseEvent<HTMLDialogElement>) => {
    if (e.target === ref.current && mouseDownOutsideRef.current) {
      const rect = ref.current.getBoundingClientRect();
      const isOutside = (
        e.clientX < rect.left ||
        e.clientX > rect.right ||
        e.clientY < rect.top ||
        e.clientY > rect.bottom
      );
      if (isOutside) {
        onClose();
      }
    }
    mouseDownOutsideRef.current = false;
  };

  return createPortal(
    <dialog
      ref={ref}
      className="modal-dialog-frame record-dialog"
      aria-label={title}
      onCancel={(e) => {
        e.preventDefault();
        onClose();
      }}
      onMouseDown={handleMouseDown}
      onClick={handleClick}
    >
      {children}
    </dialog>,
    document.body
  );
}



