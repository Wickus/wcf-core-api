# Use the directory where the script is located
# $baseDir = $PSScriptRoot

# Or use the working directory instead:
$baseDir = (Get-Location).Path

# Define publish output and project path relative to current directory
$publishDir = Join-Path $baseDir "publish"
$projectPath = Join-Path $baseDir "src/Example.WCF.Core.Api/Example.WCF.Core.Api.csproj"

# ✅ Check if directory exists, if not create it
if (-Not (Test-Path -Path $publishDir)) {
    Write-Host "Directory does not exist. Creating: $publishDir"
    New-Item -ItemType Directory -Path $publishDir -Force | Out-Null
} else {
    Write-Host "Directory already exists: $publishDir"
}

# ✅ Run dotnet publish
Write-Host "Running dotnet publish..."
dotnet publish $projectPath -c Release -o $publishDir

# ✅ Check for errors
if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Publish completed successfully."
} else {
    Write-Host "❌ Publish failed with exit code $LASTEXITCODE."
}

# ✅ Pause to allow checking the output before closing
Write-Host "Press any key to continue..."
[void][System.Console]::ReadKey($true)
