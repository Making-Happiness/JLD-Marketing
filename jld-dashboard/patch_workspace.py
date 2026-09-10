import re
with open('src/components/AccountantWorkspace.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

code = re.sub(r'<section className="workspace-workflow-panel workflow-panel">.*?</section>', '', code, flags=re.MULTILINE|re.DOTALL)

with open('src/components/AccountantWorkspace.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
