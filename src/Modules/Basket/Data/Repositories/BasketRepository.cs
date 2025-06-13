

using Shared.Data;

namespace Basket.Data.Repositories;

public class BasketRepository(BasketDbContext dbContext) : GenericRepository<ShoppingCart,BasketDbContext>(dbContext),IBasketRepository
{
    public async Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        item = await AddElementToCollection(item,
         c => c.ShoppingCartId,
         c => c.ShoppingCartId == item.ShoppingCartId && c.ProductId == item.ProductId,
         c => { c.Quantity += item.Quantity; return c; }
          , cancellationToken);
        await ReloadItems(item.ShoppingCart, cancellationToken);
        return item;
    }

    public async Task<IEnumerable<ShoppingCartItem>> GetCart(Guid cartId, bool AsNoTracking, CancellationToken cancellationToken)
    {
        ShoppingCart cart = await GetElementById(cartId, AsNoTracking, cancellationToken, c => c.items)??throw new Exception($"Shopping cart with ID {cartId} not found.");
        return cart.items;
    }

    public async Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        return await ReloadElementCollection(cart, c=>c.items, cancellationToken);
    }

    public async Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        return await RemoveElementFromCollection(item, i => i.ShoppingCartId, cancellationToken);
    }



    
}
