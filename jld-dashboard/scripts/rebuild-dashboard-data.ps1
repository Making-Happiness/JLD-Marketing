<#!
.SYNOPSIS
Rebuilds the in-repository normalized data from the in-repository workbook.

.DESCRIPTION
This is the single-repository refresh command. It runs the Python workbook
processor stored at ../data-tools, writes the normalized CSV and SQLite output
to data/normalized_output, then copies the three browser-required CSV files to
public/data.
#>

$dashboardRoot = Split-Path -Parent $PSScriptRoot
$repositoryRoot = Split-Path -Parent $dashboardRoot
$toolDirectory = Join-Path $repositoryRoot "data-tools"
$processor = Join-Path $toolDirectory "real_estate_printables.py"
$workbook = Join-Path $toolDirectory "Sample.xlsx"
$outputDirectory = Join-Path $dashboardRoot "data\normalized_output"

foreach ($requiredPath in @($processor, $workbook)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        throw "Required repository data tool was not found: $requiredPath"
    }
}

& python $processor --source $workbook --output $outputDirectory --build
if ($LASTEXITCODE -ne 0) {
    throw "The workbook processor failed with exit code $LASTEXITCODE. Install Python dependencies with: python -m pip install -r ..\data-tools\requirements.txt"
}

& (Join-Path $PSScriptRoot "sync-normalized-data.ps1") -SourceDirectory $outputDirectory
if ($LASTEXITCODE -ne 0) {
    throw "The public CSV sync failed with exit code $LASTEXITCODE."
}

Write-Host "Repository data refreshed successfully."
