param(
    [Parameter(Mandatory = $true)]
    [string]$ModuleName,
    [string]$MigrationName = "InitialCreate"
)

# Build paths based on convention
$modulePath = "Modules/$ModuleName"
$csproj = "$modulePath/$ModuleName.csproj"
$dbContext = "${ModuleName}DbContext"

# Use the same migration output as Catalog
$migrationOutput = "Data/Migrations"

# Build the dotnet ef command
$cmd = "dotnet-ef migrations add $MigrationName -o $migrationOutput -p $csproj -s Bootstraper/API -c $dbContext"
Write-Host "Running: $cmd" -ForegroundColor Cyan
Invoke-Expression $cmd
