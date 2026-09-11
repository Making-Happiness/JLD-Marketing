# Workbook processor

This folder keeps the original workbook processing capability inside the same
repository as the dashboard.

- `Sample.xlsx` is the source workbook.
- `real_estate_printables.py` produces clean relational CSV data and
  `real_estate_tracker.db`.
- `requirements.txt` lists the Python packages needed by the processor.

From `jld-dashboard`, run `npm run refresh:data` to rebuild the normalized data
in `jld-dashboard/data/normalized_output` and copy the dashboard inputs into
`jld-dashboard/public/data`.
