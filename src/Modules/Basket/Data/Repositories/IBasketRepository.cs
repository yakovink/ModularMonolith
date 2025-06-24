

using Shared.Data;

namespace Basket.Data.Repositories;

public interface IBasketRepository 
{


    public Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default);
    public Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default);

    public Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default);

    public Task<ShoppingCart> GetCartById(Guid cartId, bool AsNoTracking, CancellationToken cancellationToken);

    public Task<ShoppingCart> CreateBasket(CancellationToken cancellationToken);
    public Task<bool> DeleteBasket(Guid cartId, CancellationToken cancellationToken);


}
