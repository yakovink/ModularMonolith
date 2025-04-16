using System;

namespace Catalog.Data.Seed;

public class CatalogDataSeed(CatalogDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (!await dbContext.Products.AnyAsync()){
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }
}
