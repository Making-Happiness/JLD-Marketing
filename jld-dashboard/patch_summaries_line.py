import re

files = [
    'src/components/tables/AgentsTable.tsx',
    'src/components/tables/ExpensesTable.tsx',
    'src/components/tables/InventoryTable.tsx',
    'src/components/tables/LoansTable.tsx',
    'src/components/tables/PaymentsTable.tsx',
    'src/components/tables/PayrollTable.tsx'
]

for file in files:
    with open(file, 'r', encoding='utf-8') as f:
        lines = f.readlines()
    
    with open(file, 'w', encoding='utf-8') as f:
        for line in lines:
            if 'summary={' not in line:
                f.write(line)
