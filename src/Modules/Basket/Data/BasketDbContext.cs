

using Shared.Data;

namespace Basket.Data;

public class BasketDbContext : GenericDbContext<BasketDbContext>
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options) :
    base(options, new[] { typeof(ShoppingCart), typeof(ShoppingCartItem) })
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {


        builder.HasDefaultSchema("basket");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

