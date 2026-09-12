# Printable Accounts module

This isolated dashboard page reads the normalized account, payment, and balance
CSV files copied into `public/data`. It does not change any existing JLD
workspace records, tables, navigation behavior, or modals. The workbook
processor, workbook, normalized exports, and dashboard are all included in this
same repository.

## Update the data

To rebuild the exports directly from the repository workbook, install the
Python requirements once and then run:

```powershell
npm run refresh:data
```

If this is the first data refresh on the machine:

```powershell
python -m pip install -r ..\data-tools\requirements.txt
npm run refresh:data
```

The command copies these browser-readable files into `public/data`:

- `dim_accounts.csv`
- `fact_payments.csv`
- `account_balance_verification.csv`

## Run in VS Code

Open the `jld-dashboard` folder in VS Code, then run in its integrated terminal:

```powershell
npm install
python -m pip install -r ..\data-tools\requirements.txt
npm run refresh:data
npm run dev
```

Open the local address shown by Vite, normally `http://localhost:5173`. Choose
**Printable accounts** in the left navigation. Search accepts either
`Firstname Lastname` or `Lastname, Firstname`; select a result to inspect the
preview and choose **Convert to PDF** to download the statement.

Create a production build with:

```powershell
npm run build
```
