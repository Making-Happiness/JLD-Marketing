import re
with open('src/components/tables/PurchaseDetailsTable.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = code.replace("label:'Buyer',", "label:'Stakeholder',")

with open('src/components/tables/PurchaseDetailsTable.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
