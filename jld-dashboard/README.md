# JLD accounting workspace

An accountant-focused React interface for JLD property operations. Run `npm run dev`, `npm run build`, and `node --test tests/workflows.test.cjs` from this folder.

## Connected workflows

- Overview → properties → buyers → contracts → collections → agents → cash reporting.
- Employees → loans and benefits → draft payroll → draft payslips.
- Recording a collection uses its contract's agent percentage to accrue commission. Releasing a commission updates the agent balance and creates a single expense voucher.
- Cash reporting includes all recorded receipts and expense vouchers, including archived entries. Archiving is not a financial reversal. Pending payroll and outstanding commission balances are not cash disbursements.
- CSV reports support date-period filtering and text search. Statements of account open for the selected buyer contract.
- New contracts reject occupied lots and inactive parent records. Receipt numbers are unique across active and archived entries.

## Current operating limits

This is a demo using bundled sample records and React session state. Reloading resets changes. It has no shared database, authenticated accounting identity, server-enforced permissions, backups, or bank reconciliation. Do not enter live accounting data.

Payroll preparation treats employee salary as a daily rate and uses the entered working days. Benefits dated within the cutoff are included; eligible loan amortizations are capped at their remaining balance. Payroll and payslip generation create drafts; approval, actual payroll release, and loan settlement are not implemented. Statutory deductions and tax calculations require business rules and validation before production use.

Commission accrual currently uses collections multiplied by the contract percentage. Validate the business's eligibility, caps, reversals, and collection basis before live use. Seed balances are retained as provided and are not historical reconciliation results.

Statements retain the existing price-plus-fees balance model; financed interest and installment schedules need a unified production accounting model. Date-based attention items are review prompts, not certified arrears balances.

## Verification

Production build and four payroll workflow tests pass. Lint has no blocking errors; existing unused-variable and React-effect warnings remain. Browser interaction and visual regression tests were not performed.
