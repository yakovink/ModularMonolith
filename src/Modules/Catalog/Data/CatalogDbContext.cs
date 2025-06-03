
namespace Catalog.Data;

public class CatalogDbContext:DbContext
{

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
    {
    }

    public DbSet<Product> Products { get; set; }=default!;
    //public DbSet<ProductCategory> ProductCategories { get; set; }=default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<Product> getProductById(Guid id, CancellationToken cancellationToken,RequestType type)
    {

        Product? product = null;
        //get the product entity ID
        if (type == RequestType.Query)
        {
            product= await Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        else if (type == RequestType.Command){
            
            product = await Products.FindAsync([id], cancellationToken);
        }
        
        if (product == null)
        {
            throw new Exception($"Product not found {id}");
        }
        return product;
    }

}
