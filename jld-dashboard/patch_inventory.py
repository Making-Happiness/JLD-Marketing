import re
with open('src/components/tables/InventoryTable.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = code.replace("{label:'Property code',render:r=><strong className=\"inventory-code-cell\">{r.code}</strong>},", "")
code = code.replace("label:'Location / project'", "label:'Location'")

with open('src/components/tables/InventoryTable.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
