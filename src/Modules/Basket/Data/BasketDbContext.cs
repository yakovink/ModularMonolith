

using Shared.Data;

namespace Basket.Data;

public class BasketDbContext(DbContextOptions<BasketDbContext> options) : BasketModuleStructre.MBasketDbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {


        builder.HasDefaultSchema("basket");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

