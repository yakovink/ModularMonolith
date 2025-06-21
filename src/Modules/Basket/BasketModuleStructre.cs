using Shared.Data;
using Shared.Mechanism;
using System;

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
    public abstract class BasketRepository<R>(R repository) : IMModelConfiguration.LocalRepository<R, BasketDbContext>(repository) where R : class, IGenericRepository<ShoppingCart>;


    public class BasketSQLRepository(GenericDbContext<BasketDbContext> dbContext) :
        BasketLocalRepository<GenericRepository<ShoppingCart, BasketDbContext>>(
            new GenericRepository<ShoppingCart, BasketDbContext>(dbContext));


    public class CachedBasketRepository(BasketSQLRepository repository, IDistributedCache cache) :
        BasketLocalRepository<GenericCachedRepository<ShoppingCart, BasketDbContext>>(
            new GenericCachedRepository<ShoppingCart, BasketDbContext>(repository.getMasterRepository(), cache));



    //commands
    public interface CreateBasket : MPost<bool, Guid>;
    public interface DeleteBasket : MDelete<Guid, bool>;
    public interface AddItemIntoBasket : MPost<ShoppingCartItemDto, Guid>;
    public interface RemoveItemFromBasket : MDelete<ShoppingCartItemDto, bool>;

    //queries
    public interface GetBasket : MGet<Guid, HashSet<ShoppingCartItemDto>>;
    

}
