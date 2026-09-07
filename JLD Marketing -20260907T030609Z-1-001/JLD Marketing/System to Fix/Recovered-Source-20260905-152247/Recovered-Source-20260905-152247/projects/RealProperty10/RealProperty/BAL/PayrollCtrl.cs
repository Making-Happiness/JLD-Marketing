using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using RealProperty.BEL;
using RealProperty.DAL;

namespace RealProperty.BAL;

internal class PayrollCtrl : AController, IDisposable
{
	private PayrollAdapter Adapter;

	public PayrollCtrl()
	{
		Adapter = new PayrollAdapter();
	}

	internal DataTable getCategoryCookies()
	{
		return Adapter.getcustomTable("view_category_set");
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	internal int add(Payrollsettings payrollsettings)
	{
		return Adapter.add(payrollsettings);
	}

	internal int edit(Payrollsettings payrollsettings)
	{
		return Adapter.edit(payrollsettings);
	}

	internal DataTable getPayrollsettingsTable()
	{
		return Adapter.getcustomTable("payrollsettings");
	}

	internal DataTable getEmployeeSalaryTable()
	{
		return Adapter.getcustomTable("view_employee_salary");
	}

	internal DataTable getPayrollSettingsCookies()
	{
		return Adapter.getcustomTable("view_payrollsettingscookies");
	}

	internal int add_payslipdetail(Payslipdetail payslipdetail)
	{
		return Adapter.add(payslipdetail, "payslipdetails");
	}

	internal int edit_payslipdetail(Payslipdetail payslipdetail)
	{
		return Adapter.edit(payslipdetail, "payslipdetails");
	}

	internal DataTable getPayslipdetailsTable(int idpayslip, int idemplyee)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("idpayslip", idpayslip);
		dictionary.Add("idemployee", idemplyee);
		return Adapter.getTable("payslipdetails", dictionary, 0, 0, "WHERE idpayslip=?idpayslip and idemployee=?idemployee");
	}

	internal Payslipdetail getPayslipdetailsObject(int id)
	{
		DataTable tableByID = Adapter.getTableByID("payslipdetails", "id", id);
		if (tableByID.Rows.Count > 0)
		{
			return AController.DataTableToList<Payslipdetail>(tableByID).First();
		}
		return null;
	}

	internal DataTable get_viewLoansTable(int idemployee)
	{
		return Adapter.getTableByID("view_loans", "idemployee", idemployee);
	}

	internal int add_loan(Loan loan)
	{
		return Adapter.add(loan, loan.getTableName());
	}

	internal int edit_loan(Loan loan)
	{
		return Adapter.edit(loan, loan.getTableName());
	}

	internal Loan getLoansObject(dynamic id)
	{
		DataTable dataTable = Adapter.getTableByID("loans", "id", id);
		if (dataTable.Rows.Count > 0)
		{
			return AController.DataTableToList<Loan>(dataTable).First();
		}
		return null;
	}

	internal DataTable getPayslipTable()
	{
		return Adapter.getcustomTable("payslips");
	}

	internal int add_payslip(Payslip payslip)
	{
		return Adapter.add(payslip, payslip.getTableName());
	}

	internal int edit_payslip(Payslip payslip)
	{
		return Adapter.edit(payslip, payslip.getTableName());
	}

	internal Payslip getPayslipObject(int id)
	{
		DataTable tableByID = Adapter.getTableByID("payslips", "id", id);
		if (tableByID.Rows.Count > 0)
		{
			return AController.DataTableToList<Payslip>(tableByID).First();
		}
		return null;
	}

	internal DataTable getneratePayslipdetails(int id)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("vidpayslip", id);
		return Adapter.getTablebyProcedure("proc_generate_payslipdetails", dictionary);
	}

	internal DataTable getPayrollTable()
	{
		return Adapter.getcustomTable("payrolls");
	}

	internal int addPayroll(Payroll payroll)
	{
		return Adapter.add(payroll, "payrolls");
	}

	internal int editPayroll(Payroll payroll)
	{
		return Adapter.edit(payroll, "payrolls");
	}

	internal Payroll getPayrollTableByID(int id)
	{
		DataTable tableByID = Adapter.getTableByID("payrolls", "id", id);
		if (tableByID.Rows.Count > 0)
		{
			return AController.DataTableToList<Payroll>(tableByID).First();
		}
		return null;
	}

	internal DataTable generate_payrolldetails(int id)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("vidpayroll", id);
		return Adapter.getTablebyProcedure("proc_generate_payrolldetails", dictionary);
	}

	internal int editPayrollDetails(Payrolldetails payrolldetails)
	{
		return Adapter.edit(payrolldetails, "payrolldetails");
	}

	internal int deletePayslipDetails(int id)
	{
		return Adapter.deletePayslipDetails(id);
	}

	internal DataTable getDIcerTableView()
	{
		return Adapter.getTable("view_agents");
	}

	internal DataTable get_view_paidbyclient_dice(int iddicer)
	{
		return Adapter.getTableByID("view_paidbyclient_dice", "idagent", iddicer);
	}

	internal DataTable get_view_releasedclaims(int iddicer)
	{
		return Adapter.getTableByID("view_releasedclaims", "idagent", iddicer);
	}

	internal Payrolldetails getPayrollTableByEmployeeID(Payrolldetails payrolldetails)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("idemployee", payrolldetails.idemployee);
		dictionary.Add("idpayroll", payrolldetails.idpayroll);
		DataTable table = Adapter.getTable("payrolldetails", dictionary, 0, 0, "where idemployee=?idemployee and idpayroll=?idpayroll");
		if (table.Rows.Count > 0)
		{
			return AController.DataTableToList<Payrolldetails>(table).First();
		}
		return null;
	}

	internal DataTable get_view_PayrollValidLoansTable(dynamic idemployee)
	{
		return Adapter.getTableByID("view_validloans", "idemployee", idemployee);
	}

	internal int add_payrolldeduction(int idpayroll, decimal balance, int idloan)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("vidpayroll", idpayroll);
		dictionary.Add("vbalance", balance);
		dictionary.Add("vidloan", idloan);
		return Adapter.executeProcedure("proc_add_payrolldeduction", dictionary);
	}

	internal DataTable get_view_payrolldeduction_withbalance(int idemployee)
	{
		return Adapter.getTableByID("view_payrolldeduction_withbalance", "idemployee", idemployee);
	}

	internal Payrolldeduction getPayrollDeducObject(int idpayrolldeduc)
	{
		DataTable tableByID = Adapter.getTableByID("payrolldeduction", "id", idpayrolldeduc);
		if (tableByID.Rows.Count > 0)
		{
			return AController.DataTableToList<Payrolldeduction>(tableByID).First();
		}
		return null;
	}

	internal int edit_payrolldeduction(Payrolldeduction payrolldeduc)
	{
		return Adapter.edit(payrolldeduc, "payrolldeduction");
	}
}
