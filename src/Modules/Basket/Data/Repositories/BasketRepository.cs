 

namespace Basket.Data.Repositories;

public class BasketRepository(BasketDbContext dbContext) : IBasketRepository
{
    public async Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        await dbContext.ShoppingCartItems.AddAsync(item, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await dbContext.ShoppingCarts.AddAsync(cart, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteBasket(Guid ShopingCartId, CancellationToken cancellationToken = default)
    {
        var basket = await GetCart(ShopingCartId, false, cancellationToken);
        dbContext.ShoppingCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetCart(Guid cartId, bool AsNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = dbContext.ShoppingCarts.Include(c => c.items).Where(c => c.Id == cartId);

        if (AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        var cart = await query.SingleOrDefaultAsync(cancellationToken);
        return cart ?? throw new NotFoundException($"cart {cartId} was not found");
    }

    public async Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await dbContext.Entry(cart).Collection(c => c.items).LoadAsync();
        return true;
    }

    public async Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        dbContext.ShoppingCartItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<int> SaveChangesAsync(Guid cartId,CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    
}
