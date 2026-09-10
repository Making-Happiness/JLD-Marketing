import re
with open('src/components/modals/StakeholderModal.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

# Add email state
code = code.replace("const [contactNo, setContactNo] = useState('');", "const [contactNo, setContactNo] = useState('');\n  const [email, setEmail] = useState('');")

# Load email on edit
code = code.replace("setContactNo(clientToEdit.contactno || '');", "setContactNo(clientToEdit.contactno || '');\n        setEmail(clientToEdit.email || '');")
code = code.replace("setContactNo('');", "setContactNo('');\n        setEmail('');")

# Add email to save payload
code = code.replace("contactno: contactNo,", "contactno: contactNo,\n      email,")

# Add email input in the form
input_html = """
          <div className="form-group flex-1">
            <label className="text-xs font-semibold text-slate-700 uppercase">Contact Number *</label>
            <input type="text" className="modal-input-field mt-1" value={contactNo} onChange={e => setContactNo(e.target.value)} required />
          </div>
          <div className="form-group flex-1">
            <label className="text-xs font-semibold text-slate-700 uppercase">Email Address</label>
            <input type="email" className="modal-input-field mt-1" value={email} onChange={e => setEmail(e.target.value)} />
          </div>
"""
code = re.sub(r'<div className="form-group">\s*<label className="text-xs font-semibold text-slate-700 uppercase">Contact Number \*</label>\s*<input type="text" className="modal-input-field mt-1" value=\{contactNo\} onChange=\{e => setContactNo\(e.target.value\)\} required />\s*</div>', input_html, code, flags=re.MULTILINE)

with open('src/components/modals/StakeholderModal.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
