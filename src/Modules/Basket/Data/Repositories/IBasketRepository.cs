 

namespace Basket.Data.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetCart(Guid cartId, bool AsNoTracking = true, CancellationToken cancellationToken = default);
    Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(Guid ShopingCartId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(Guid cartId,CancellationToken cancellationToken = default);
    Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default);
    Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default);

    Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default);
    

}
