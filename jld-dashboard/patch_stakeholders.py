import re
with open('src/components/tables/StakeholdersTable.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = code.replace("addLabel=\"Add buyer\"", "addLabel=\"Add stakeholder\"")
code = code.replace("label:'Buyer ID',render:r=><span className=\"buyer-id-badge\">{BUY-}</span>", "label:'Stakeholder ID',render:r=><span className=\"buyer-id-badge\">{String(r.idclients).padStart(2, '0')}</span>")
code = code.replace("label:'Buyer name'", "label:'Stakeholder name'")

with open('src/components/tables/StakeholdersTable.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
