

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

/*
    public async Task<ShoppingCart> getCartById(Guid id, CancellationToken cancellationToken, RequestType type)
    {

        ShoppingCart? shoppingCart = null;

        //get the product entity ID
        if (type == RequestType.Query)
        {
            shoppingCart = await ShoppingCarts.AsNoTracking().Include(c=>c.items).SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        }
        else if (type == RequestType.Command)
        {

            shoppingCart = await ShoppingCarts.Include(c=>c.items).SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        if (shoppingCart == null)
        {
            throw new Exception($"Product not found {id}");
        }
        Console.WriteLine($"found cart {id} as {shoppingCart}");
        return shoppingCart;
    }
    public async Task<ShoppingCartItem> getCartItemById(Guid id, CancellationToken cancellationToken, RequestType type)
    {

        ShoppingCartItem? shoppingCartItem = null;
        //get the product entity ID
        if (type == RequestType.Query)
        {
            shoppingCartItem = await ShoppingCartItems.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        else if (type == RequestType.Command)
        {

            shoppingCartItem = await ShoppingCartItems.FindAsync(id, cancellationToken);
        }

        if (shoppingCartItem == null)
        {
            throw new Exception($"Product not found {id}");
        }
        return shoppingCartItem;
    }

    public async Task<ShoppingCartItem?> getCartItemByParams(Guid CartId, Guid ProductId, CancellationToken cancellationToken, RequestType type)
    {


        List<ShoppingCartItem> items = await getCartItems(CartId, cancellationToken,type);
        return items.SingleOrDefault(i => i.ProductId == ProductId);

    }


    public async Task<List<ShoppingCartItem>> getCartItems(Guid CartId, CancellationToken cancellationToken, RequestType type)
    {

        ShoppingCart cart = await getCartById(CartId, cancellationToken, type);
        Console.WriteLine($"cart {cart.Id} have {cart.items.Count()} items");
        return cart.items;
    }*/
}

