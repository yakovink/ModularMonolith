



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
    public abstract class BasketRepository(IGenericRepository<ShoppingCart> repository) : IMModelConfiguration.LocalRepository<BasketDbContext>(repository);




    //commands
    public interface CreateBasket : MPost<object, Guid>;
    public interface DeleteBasket : MDelete<Guid, bool>;
    public interface AddItemIntoBasket : MPost<ShoppingCartItemDto, Guid>;
    public interface RemoveItemFromBasket : MDelete<ShoppingCartItemDto, bool>;

    //queries
    public interface GetBasket : MGet<Guid, HashSet<ShoppingCartItemDto>>;
    
    //controllers

    public class BasketHttpController() : HttpController("http://localhost", 5000);

}
