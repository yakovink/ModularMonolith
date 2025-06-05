
namespace Basket.Data;

public class BasketDbContext : DbContext
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
    {
    }

    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {


        builder.HasDefaultSchema("basket");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public async Task<ShoppingCart> getCartById(Guid id, CancellationToken cancellationToken,RequestType type)
    {

        ShoppingCart? shoppingCart = null;
        //get the product entity ID
        if (type == RequestType.Query)
        {
            shoppingCart= await ShoppingCarts.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        else if (type == RequestType.Command){
            
            shoppingCart = await ShoppingCarts.FindAsync([id], cancellationToken);
        }
        
        if (shoppingCart == null)
        {
            throw new Exception($"Product not found {id}");
        }
        return shoppingCart;
    }
}

