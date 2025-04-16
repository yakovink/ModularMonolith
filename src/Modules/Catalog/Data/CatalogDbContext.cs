
namespace Catalog.Data;

public class CatalogDbContext:DbContext
{

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
    {
        Console.WriteLine(options.ToString());

        
    }

    public DbSet<Product> Products { get; set; }=default!;
    //public DbSet<ProductCategory> ProductCategories { get; set; }=default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
