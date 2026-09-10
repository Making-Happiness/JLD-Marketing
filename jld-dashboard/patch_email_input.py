import re
with open('src/components/modals/StakeholderModal.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

contact_html = """                  <label className="form-field-label">
                    <span>Contact Number <span className="req">*</span></span>
                    <input
                      type="text"
                      value={contactNo}
                      onChange={e => setContactNo(e.target.value)}
                      placeholder="e.g. 0917-123-4567"
                      required
                    />
                  </label>
                  
                  <label className="form-field-label">
                    <span>Email Address</span>
                    <input
                      type="email"
                      value={email}
                      onChange={e => setEmail(e.target.value)}
                      placeholder="e.g. buyer@example.com"
                    />
                  </label>"""
                  
code = re.sub(r'<label className="form-field-label">\s*<span>Contact Number.*?required\s*/>\s*</label>', contact_html, code, flags=re.MULTILINE|re.DOTALL)

with open('src/components/modals/StakeholderModal.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
