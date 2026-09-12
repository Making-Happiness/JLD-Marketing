<#!
.SYNOPSIS
Copies the latest normalized exports into Vite's public data folder.

.DESCRIPTION
Run this after the Python workbook processor updates normalized_output.  The
dashboard intentionally reads only copies inside public/data so a browser does
not need permission to access folders outside the web application.
#>
param(
    [string]$SourceDirectory = (Join-Path $PSScriptRoot "..\data\normalized_output")
)

$requiredFiles = @(
    "dim_accounts.csv",
    "fact_payments.csv",
    "account_balance_verification.csv"
)
$targetDirectory = Join-Path $PSScriptRoot "..\public\data"

if (-not (Test-Path -LiteralPath $SourceDirectory -PathType Container)) {
    throw "Normalized output directory was not found: $SourceDirectory"
}

New-Item -ItemType Directory -Path $targetDirectory -Force | Out-Null
foreach ($file in $requiredFiles) {
    $sourcePath = Join-Path $SourceDirectory $file
    if (-not (Test-Path -LiteralPath $sourcePath -PathType Leaf)) {
        throw "Required normalized export is missing: $sourcePath"
    }
    Copy-Item -LiteralPath $sourcePath -Destination (Join-Path $targetDirectory $file) -Force
}

Write-Host "Copied normalized CSV data to $targetDirectory"
