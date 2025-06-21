dotnet clean
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
dotnet build
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
dotnet run
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

# Stop all running dotnet processes
get-process dotnet -ErrorAction SilentlyContinue | Stop-Process -Force
Write-Host "API has been stopped" -ForegroundColor Green
