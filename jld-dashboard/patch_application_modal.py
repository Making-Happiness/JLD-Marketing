import re
with open('src/components/modals/ApplicationModal.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

# Update onSave prop type
code = code.replace("onSave: (application: PurchaseDetail) => string | void;", "onSave: (application: any) => Promise<string | void> | string | void;")

# Update terms state
code = code.replace("const [terms, setTerms] = useState<number>(2);", "const [terms, setTerms] = useState<number>(2);\n  const [isOtherTerm, setIsOtherTerm] = useState(false);")
code = code.replace("setTerms(initialData.terms);", "setTerms(initialData.terms);\n        setIsOtherTerm(![1,2,3,4,5].includes(initialData.terms));")

# Update agent state
code = code.replace("const [agentId, setAgentId] = useState<number | ''>('');", "const [agentId, setAgentId] = useState<number | string>('');\n  const [otherAgentName, setOtherAgentName] = useState('');")

# Update submit handler to await
code = code.replace("const handleSubmit = (e: React.FormEvent) => {", "const handleSubmit = async (e: React.FormEvent) => {")
code = code.replace("const result=onSave(application);if(result){setError(result);return;} setError('');onClose();", "const result=await onSave(application);if(result){setError(result);return;} setError('');onClose();")

# Inject agentName into payload if using Other
code = code.replace("idagent: Number(agentId),", "idagent: agentId === 'other' ? -1 : Number(agentId),\n        _newAgentName: agentId === 'other' ? otherAgentName : undefined,")
# Use the correct agentName for existing agent
code = code.replace("agentName: agents.find(a => a.id === Number(agentId))?.fullname || '',", "agentName: agentId === 'other' ? otherAgentName : (agents.find(a => a.id === Number(agentId))?.fullname || ''),")

# Replace terms dropdown
terms_html = """                  <span>Payment Term (Years)</span>
                    <div style={{display:'flex', gap: '8px'}}>
                      <select
                        value={isOtherTerm ? 'other' : terms}
                        onChange={(e) => {
                          if (e.target.value === 'other') {
                            setIsOtherTerm(true);
                            setTerms(0);
                          } else {
                            setIsOtherTerm(false);
                            setTerms(Number(e.target.value));
                          }
                        }}
                        className="application-field-select"
                        style={{flex: 1}}
                      >
                        <option value={1}>1 Year (12 months)</option>
                        <option value={2}>2 Years (24 months)</option>
                        <option value={3}>3 Years (36 months)</option>
                        <option value={4}>4 Years (48 months)</option>
                        <option value={5}>5 Years (60 months)</option>
                        <option value="other">Other</option>
                      </select>
                      {isOtherTerm && (
                        <input type="number" min="1" max="50" step="0.5" className="application-field-input" style={{width: '80px'}} value={terms || ''} onChange={e => setTerms(Number(e.target.value))} placeholder="Years" required />
                      )}
                    </div>"""
code = re.sub(r'<span>Payment Term</span>\s*<select\s*value=\{terms\}\s*onChange=\{\(e\) => setTerms\(Number\(e\.target\.value\)\)\}\s*className="application-field-select"\s*>\s*<option value=\{1\}>1 Year \(12 months\)</option>\s*<option value=\{2\}>2 Years \(24 months\)</option>\s*<option value=\{3\}>3 Years \(36 months\)</option>\s*<option value=\{4\}>4 Years \(48 months\)</option>\s*<option value=\{5\}>5 Years \(60 months\)</option>\s*</select>', terms_html, code, flags=re.MULTILINE|re.DOTALL)

# Add min="0" to Down Payment input
code = code.replace('onChange={(e) => setDownpayment(Number(e.target.value))}\n                      className="application-field-input"\n                    />', 'onChange={(e) => setDownpayment(Number(e.target.value))}\n                      className="application-field-input"\n                      min="0"\n                    />')

# Replace Agent dropdown
agent_html = """                  <span>Assigned Agent / Dicer</span>
                    <div style={{display:'flex', gap: '8px', flexDirection: 'column'}}>
                      <select
                        value={agentId}
                        onChange={(e) => setAgentId(e.target.value)}
                        className="application-field-select"
                        required
                      >
                        <option value="" disabled>Select agent</option>
                        {agents.map(a => (
                          <option key={a.id} value={a.id}>{a.fullname}</option>
                        ))}
                        <option value="other">Other (Type name)</option>
                      </select>
                      {agentId === 'other' && (
                        <input type="text" className="application-field-input" value={otherAgentName} onChange={e => setOtherAgentName(e.target.value)} placeholder="Full name of agent" required />
                      )}
                    </div>"""
code = re.sub(r'<span>Assigned Agent / Dicer</span>\s*<select\s*value=\{agentId\}\s*onChange=\{\(e\) => setAgentId\(Number\(e\.target\.value\)\)\}\s*className="application-field-select"\s*required\s*>\s*<option value="" disabled>Select agent</option>\s*\{agents\.map\(a => \(\s*<option key=\{a\.id\} value=\{a\.id\}>\{a\.fullname\}</option>\s*\)\)\}\s*</select>', agent_html, code, flags=re.MULTILINE|re.DOTALL)

with open('src/components/modals/ApplicationModal.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
