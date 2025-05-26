
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

    public async Task<ShoppingCart> getCartByUserName(string userName, CancellationToken cancellationToken,RequestType type)
    {

        ShoppingCart? shoppingCart = null;
        //get the product entity ID
        if (type == RequestType.Query)
        {
            shoppingCart= await ShoppingCarts.AsNoTracking().SingleOrDefaultAsync(p => p.UserName == userName, cancellationToken);
        }
        else if (type == RequestType.Command){
            
            shoppingCart = await ShoppingCarts.FindAsync([userName], cancellationToken);
        }
        
        if (shoppingCart == null)
        {
            throw new Exception($"Product not found {userName}");
        }
        return shoppingCart;
    }
}

