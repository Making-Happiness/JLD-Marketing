import re

with open('src/App.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

# Replace Agents
code = re.sub(
    r"setAgents\(prev=>\[\.\.\.prev,\{id,fullname,contactno:v\.contact,role:v\.role,commissionRate:Number\(v\.rate\),totalSales:0,totalEarned:0,totalClaimed:0,balance:0,recordstatus:'active',status:'active'\}\]\);",
    r"const newAgent = {fullname,contactno:v.contact,role:v.role,commissionRate:Number(v.rate),totalSales:0,totalEarned:0,totalClaimed:0,balance:0,recordstatus:'active',status:'active'}; API.saveAgent(newAgent).then(res => res.data && setAgents(prev => [res.data, ...prev]));",
    code
)

# Replace Employees
code = re.sub(
    r"setEmployees\(prev=>\[\.\.\.prev,\{idemployee:id,firstname:v\.firstname,lastname:v\.lastname,middlename:'',gender:'',dateofbirth:'',salary:Number\(v\.rate\),designation:v\.role,civilstatus:'',contactno:v\.contact,recordstatus:'active',status:'active',fullname\}\]\);",
    r"const newEmp = {firstname:v.firstname,lastname:v.lastname,middlename:'',gender:'',dateofbirth:'',salary:Number(v.rate),designation:v.role,civilstatus:'',contactno:v.contact,recordstatus:'active',status:'active',fullname}; API.saveEmployee(newEmp).then(res => res.data && setEmployees(prev => [res.data, ...prev]));",
    code
)

# Replace Loans
code = re.sub(
    r"\(entryKind==='loan'\?setLoans:setBenefits\)\(prev=>\[record,\.\.\.prev\]\);",
    r"const newRec = {...record, id: undefined}; API.saveEmployeeLoan(newRec).then(res => { if (res.data) (entryKind === 'loan' ? setLoans : setBenefits)(prev => [res.data, ...prev]); });",
    code
)

# Replace Expense
code = re.sub(
    r"setExpenses\(prev=>\[\{id,receiveby:employee!\.idemployee,receivebyName:employee!\.fullname,releaseby:0,releasebyName:'Accounting workspace',description:v\.description,purpose:v\.purpose,amount,daterelease:v\.date,remarks:'',status:'active'\},...prev\]\);",
    r"const newExp = {receiveby:employee!.idemployee,receivebyName:employee!.fullname,releaseby:0,releasebyName:'Accounting workspace',description:v.description,purpose:v.purpose,amount,daterelease:v.date,remarks:'',status:'active'}; API.saveExpense(newExp).then(res => res.data && setExpenses(prev => [res.data, ...prev]));",
    code
)

# Replace Commission (Expense + Agent update)
code = re.sub(
    r"setExpenses\(prev=>\[\{id,receiveby:0,receivebyName:agent\.fullname,releaseby:0,releasebyName:'Accounting workspace',description:Commission release A.*? \$\{agent\.fullname},purpose:'Agent commission',amount,daterelease:v\.date,remarks:Agent ID \$\{agent\.id},status:'active'\},...prev\]\);",
    r"const newExp2 = {receiveby:0,receivebyName:agent.fullname,releaseby:0,releasebyName:'Accounting workspace',description:Commission release ,purpose:'Agent commission',amount,daterelease:v.date,remarks:Agent ID ,status:'active'}; API.saveExpense(newExp2).then(res => res.data && setExpenses(prev => [res.data, ...prev])); API.saveAgent({...agent, totalClaimed: Math.round((agent.totalClaimed+amount)*100)/100, balance: Math.round((agent.balance-amount)*100)/100});",
    code
)

# Replace Payroll
code = re.sub(
    r"try \{const records=preparePayroll\(dbState,v\.start,v\.end,Number\(v\.days\)\);setPayrollRecords\(prev=>\[\.\.\.records,\.\.\.prev\]\);records\.forEach\(p=>logActivity\('Payroll',p\.id,p\.employeeName,'CREATE',Prepared draft for \$\{p\.period\}\.\)\);\} catch\(error\)\{return \(error as Error\)\.message;\}",
    r"try { const records=preparePayroll(dbState,v.start,v.end,Number(v.days)); records.forEach(r => { const {id, ...recData} = r; API.savePayroll(recData).then(res => { if (res.data) setPayrollRecords(prev => [res.data, ...prev]); }); logActivity('Payroll',r.id,r.employeeName,'CREATE',Prepared draft for .); }); } catch(error){return (error as Error).message;}",
    code
)

# Replace Payslips
code = re.sub(
    r"setPayslips\(prev=>\[\.\.\.eligible\.map\(payslipFromPayroll\),\.\.\.prev\]\);eligible\.forEach\(p=>logActivity\('Payslip',p\.id,p\.employeeName,'CREATE',Generated draft payslip for \$\{p\.period\}\.\)\);",
    r"eligible.forEach(p => { const slip = payslipFromPayroll(p); const {id, ...slipData} = slip; API.savePayslip(slipData).then(res => { if (res.data) setPayslips(prev => [res.data, ...prev]); }); logActivity('Payslip',p.id,p.employeeName,'CREATE',Generated draft payslip for .); });",
    code
)

# Archive
code = re.sub(
    r"const confirmArchive = \(reason: string\) => \{\n\s+if \(!archiveTarget\) return;\n\s+const \{ entityType, id, recordTitle \} = archiveTarget;\n\s+const now = new Date\(\)\.toISOString\(\);\n\s+const idNum = Number\(id\);",
    r"const confirmArchive = async (reason: string) => {\n      if (!archiveTarget) return;\n      const { entityType, id, recordTitle } = archiveTarget;\n      const now = new Date().toISOString();\n      const idNum = Number(id);\n      const tableMap: Record<string, string> = { Product: 'products', Stakeholder: 'clients', Purchase: 'purchase_details', Payment: 'payment_transactions', Agent: 'agents', Employee: 'employees', Loan: 'employee_loans', Benefit: 'employee_loans', Payroll: 'payroll_records', Payslip: 'payslips', Expense: 'expenses' };\n      const idColMap: Record<string, string> = { Product: 'idproduct', Stakeholder: 'idclients', Employee: 'idemployee' };\n      const tableName = tableMap[entityType];\n      const idColName = idColMap[entityType] || 'id';\n      if (tableName) { await API.supabase.from(tableName).update({status: 'archived', deleted_at: now}).eq(idColName, idNum); }",
    code
)

# Restore
code = re.sub(
    r"const handleRestore = \(entityType: string, id: number \| string, recordTitle: string\) => \{\n\s+const idNum = Number\(id\);",
    r"const handleRestore = async (entityType: string, id: number | string, recordTitle: string) => {\n      const idNum = Number(id);\n      const tableMap: Record<string, string> = { Product: 'products', Stakeholder: 'clients', Purchase: 'purchase_details', Payment: 'payment_transactions', Agent: 'agents', Employee: 'employees', Loan: 'employee_loans', Benefit: 'employee_loans', Payroll: 'payroll_records', Payslip: 'payslips', Expense: 'expenses' };\n      const idColMap: Record<string, string> = { Product: 'idproduct', Stakeholder: 'idclients', Employee: 'idemployee' };\n      const tableName = tableMap[entityType];\n      const idColName = idColMap[entityType] || 'id';\n      if (tableName) { await API.supabase.from(tableName).update({status: 'active', deleted_at: null}).eq(idColName, idNum); }",
    code
)

# Permanent Delete
code = re.sub(
    r"const confirmPermanentDelete = \(\) => \{\n\s+if \(!deleteTarget\) return;\n\s+const \{ entityType, id, recordTitle \} = deleteTarget;\n\s+const idNum = Number\(id\);",
    r"const confirmPermanentDelete = async () => {\n      if (!deleteTarget) return;\n      const { entityType, id, recordTitle } = deleteTarget;\n      const idNum = Number(id);\n      const tableMap: Record<string, string> = { Product: 'products', Stakeholder: 'clients', Purchase: 'purchase_details', Payment: 'payment_transactions', Agent: 'agents', Employee: 'employees', Loan: 'employee_loans', Benefit: 'employee_loans', Payroll: 'payroll_records', Payslip: 'payslips', Expense: 'expenses' };\n      const idColMap: Record<string, string> = { Product: 'idproduct', Stakeholder: 'idclients', Employee: 'idemployee' };\n      const tableName = tableMap[entityType];\n      const idColName = idColMap[entityType] || 'id';\n      if (tableName) { await API.supabase.from(tableName).delete().eq(idColName, idNum); }",
    code
)

with open('src/App.tsx', 'w', encoding='utf-8') as f:
    f.write(code)

