import re
with open('src/components/layout/Sidebar.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = code.replace("label: 'Properties & lots'", "label: 'Properties'")
code = code.replace("label: 'Buyers'", "label: 'Stakeholders'")
code = code.replace("label: 'Collections'", "label: 'Payments'")

with open('src/components/layout/Sidebar.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
