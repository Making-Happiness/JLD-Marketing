import re
with open('src/components/layout/Sidebar.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

target = """  {
    id: 'printables',
    title: 'DATA & PRINTABLES',
    items: [
      { id: 'printable-accounts', label: 'Printable accounts', icon: Files },
    ],
  },"""

code = code.replace(target, "")

with open('src/components/layout/Sidebar.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
