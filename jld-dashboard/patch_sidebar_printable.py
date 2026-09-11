import re
with open('src/components/layout/Sidebar.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = re.sub(r"\{\s*id: 'printables',.*?\}\s*\],?\s*\},", "", code, flags=re.MULTILINE|re.DOTALL)

with open('src/components/layout/Sidebar.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
