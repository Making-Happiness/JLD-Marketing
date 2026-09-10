import re
with open('src/App.tsx', 'r', encoding='utf-8') as f:
    code = f.read()

# Replace handleSaveApplication
new_handler = """  const handleSaveApplication = async (newApp: any): Promise<string|void> => {
      let agentId = newApp.idagent;
      if (agentId === -1 && newApp._newAgentName) {
        // Create new agent on the fly
        const newAgentObj = {fullname: newApp._newAgentName, contactno: '', role: 'Agent', commissionRate: newApp.agentpercentage, totalSales:0, totalEarned:0, totalClaimed:0, balance:0, recordstatus:'active', status:'active' as any};
        const res = await API.saveAgent(newAgentObj);
        if (res.data) {
          setAgents(prev => [res.data, ...prev]);
          agentId = res.data.id;
          newApp.idagent = agentId;
          newApp.agentName = res.data.fullname;
        } else {
          return 'Failed to create new agent.';
        }
      }

      const product=products.find(p=>p.idproduct===newApp.idproducts&&p.status==='active');
      if(!product||!clients.some(c=>c.idclients===newApp.idclients&&c.status==='active')||!agents.some(a=>a.id===newApp.idagent&&a.status==='active')) {
          if(agentId !== -1) return 'Select an active property, buyer and agent.';
      }
      if(leads.some(l=>l.id!==newApp.id&&l.status==='active'&&l.idproducts===newApp.idproducts&&l.blockno===newApp.blockno&&l.lotno===newApp.lotno))return 'This lot already has an active contract. Select an available lot.';
      if(!Number.isInteger(newApp.blockno)||!Number.isInteger(newApp.lotno)||newApp.blockno<1||newApp.lotno<1||newApp.blockno>product.totalblockno||newApp.lotno>product.totallotno)return 'Block or lot number is outside this property inventory.';
      if(newApp.lotprice<=0||newApp.area<=0||newApp.downpayment<0||newApp.downpayment>newApp.lotprice||newApp.agentpercentage<0||newApp.agentpercentage>100)return 'Review the price, area, downpayment and commission rate.';
      if(editingLead&&payments.some(p=>p.items.some(i=>i.idpurchasedetails===newApp.id))&&(editingLead.idclients!==newApp.idclients||editingLead.idproducts!==newApp.idproducts||editingLead.idagent!==newApp.idagent||editingLead.agentpercentage!==newApp.agentpercentage||editingLead.blockno!==newApp.blockno||editingLead.lotno!==newApp.lotno))return 'A contract with receipts cannot change buyer, property, lot or commission assignment.';
      
      delete newApp._newAgentName;
      
      if (editingLead) {
        const statement=calculateStatement(newApp,payments);
        if(statement.balance<0||statement.downPaymentOutstanding<0)return 'The revised price or down payment conflicts with existing receipts.';
        setLeads(leads.map(l => l.id === newApp.id ? newApp : l));
        void API.savePurchaseDetail(newApp);
        logActivity('Purchase', newApp.id, ${newApp.clientName} (Blk  Lot ), 'EDIT', Updated purchase terms for );
        showToast(Purchase details for  successfully updated!);
      } else {
        const appWithStatus = {
          ...newApp,
          status: 'active',
          deleted_at: null
        };
        setLeads([appWithStatus, ...leads]);
        void API.savePurchaseDetail(appWithStatus);
        logActivity('Purchase', newApp.id, ${newApp.clientName} (Blk  Lot ), 'CREATE', Executed lot purchase application for );
        showToast(New lot purchase for  created!);
      }
    };"""

code = re.sub(r'const handleSaveApplication = \(newApp: PurchaseDetail\):string\|void => \{.*?(?=\s+const handleEditPurchase =)', new_handler, code, flags=re.MULTILINE|re.DOTALL)

with open('src/App.tsx', 'w', encoding='utf-8') as f:
    f.write(code)
