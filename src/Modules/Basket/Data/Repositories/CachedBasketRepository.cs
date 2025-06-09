 



namespace Basket.Data.Repositories;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        ShoppingCartItem i = await repository.AddItem(item, cancellationToken);
        
        await removeCache(i.ShoppingCartId.ToString(), cancellationToken);
        ShoppingCart cart = await GetCart(i.ShoppingCartId, true, cancellationToken);
        await saveCache(i.ShoppingCartId.ToString(), cart, cancellationToken);
        return i;

    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        cart = await repository.CreateBasket(cart, cancellationToken);
        await saveCache(cart.Id.ToString(), cart, cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteBasket(Guid ShopingCartId, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(ShopingCartId, cancellationToken);
        await removeCache(ShopingCartId.ToString(), cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetCart(Guid cartId, bool AsNoTracking = true, CancellationToken cancellationToken = default)
    {
        ShoppingCart cart;
        if (!AsNoTracking)
        {
            cart = await repository.GetCart(cartId, AsNoTracking, cancellationToken);
        }
        else
        {
            try
            {
                cart = await loadCache<ShoppingCart>(cartId.ToString(), cancellationToken);
                Console.WriteLine(cart.items.FirstOrDefault());
            }
            catch (NotFoundException)
            {
                cart = await repository.GetCart(cartId, AsNoTracking, cancellationToken);
                await saveCache(cartId.ToString(), cart, cancellationToken);
                Console.WriteLine($"{cart.Id} was not found in cache, writing");
            }
        }
        return cart;

    }

    public async Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await repository.ReloadItems(cart, cancellationToken);
        await saveCache(cart.Id.ToString(), cart, cancellationToken);
        return true;
    }

    public async Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        bool i= await repository.RemoveItem(item, cancellationToken);
        
        await removeCache(item.ShoppingCartId.ToString(), cancellationToken);
        ShoppingCart cart = await GetCart(item.ShoppingCartId, true, cancellationToken);
        await saveCache(item.ShoppingCartId.ToString(), cart, cancellationToken);
        return i;

    }

    public async Task<int> SaveChangesAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var result = await repository.SaveChangesAsync(cartId,cancellationToken);
        await removeCache(cartId.ToString(), cancellationToken);
        return result;
    }

    private async Task saveCache<T>(string key, T value, CancellationToken cancellationToken)
    where T : class
    {
        await cache.SetStringAsync(key.ToString(), JsonSerializer.Serialize(value,new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = false
        }), cancellationToken);
    }

    private async Task<T> loadCache<T>(string key, CancellationToken cancellationToken)
    where T : class
    {
        string obj = await cache.GetStringAsync(key, cancellationToken) ?? throw new NotFoundException($"cache for {key} was not found");
        Console.WriteLine(obj);
        T result = JsonSerializer.Deserialize<T>(obj) ?? throw new NotFoundException($"cache value is not valid for {typeof(T)}");
        return result;
        
    }

    private async Task removeCache(string key, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }
    
}
