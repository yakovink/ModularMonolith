dotnet ef database update -c AccountDbContext
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
dotnet ef database update -c WerhouseDbContext
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
dotnet ef database update -c BasketDbContext
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
dotnet ef database update -c CatalogDbContext
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
