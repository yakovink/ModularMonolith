using System;
using Shared.Mechanism;

namespace Basket;

public class BasketModuleStructre : ModuleMechanism<ShoppingCart>
{
    // SubModel configuration
    public interface CartItem : IMModelConfiguration.Property;

    // Model configuration
    public interface ICartConfigurations : IMModelConfiguration;


    // SubModel configuration
    public interface ICartItemConfigurations : IMModelConfiguration.IMPropertyConfiguration<ShoppingCartItem>;



    // DbContext
    public abstract class MBasketDbContext(DbContextOptions<BasketDbContext> options) : IMModelConfiguration.MContext<BasketDbContext>(options, new[] { typeof(ShoppingCart), typeof(ShoppingCartItem) });


    //repositories
    public interface IMBasketRepository : IMModelConfiguration.IMRepository;

    public abstract class MBasketRepository(BasketDbContext dbContext) : IMModelConfiguration.MRepository<BasketDbContext>(dbContext);

    public abstract class MBasketCachedRepository(MBasketRepository repository, IDistributedCache cache) : IMModelConfiguration.MCachedRepository<BasketDbContext>(repository, cache);


    //commands
    public interface CreateBasket : MPost<bool, Guid>;
    public interface DeleteBasket : MDelete<Guid, bool>;
    public interface AddItemIntoBasket : MPost<ShoppingCartItemDto, Guid>;
    public interface RemoveItemFromBasket : MDelete<ShoppingCartItemDto, bool>;

    //queries
    public interface GetBasket : MGet<Guid, HashSet<ShoppingCartItemDto>>;
    

}
