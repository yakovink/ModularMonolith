

using Shared.Data;

namespace Basket.Data.Repositories;

public interface IBasketRepository : IGenericRepository<ShoppingCart>
{


    public Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default);
    public Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default);

    public Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default);

    public Task<IEnumerable<ShoppingCartItem>> GetCart(Guid cartId, bool AsNoTracking, CancellationToken cancellationToken);

}
