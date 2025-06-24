


namespace Basket.Data.Repositories;

public class BasketLocalRepository(IGenericRepository<ShoppingCart> repository, IHttpController controller) : BasketModuleStructre.BasketRepository(repository), IBasketRepository

{
    private IHttpController c=controller;
    public async Task<ShoppingCartItem> AddItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        item = await repository.AddElementToCollection(item,
         c => c.ShoppingCartId,
         c => c.ShoppingCartId == item.ShoppingCartId && c.ProductId == item.ProductId,
         c => { c.Quantity += item.Quantity; return c; }
          , cancellationToken);
        await ReloadItems(item.ShoppingCart, cancellationToken);
        return item;
    }

    public async Task<ShoppingCart> CreateBasket(CancellationToken cancellationToken)

    {
        return await repository.CreateElement(ShoppingCart.Create(), cancellationToken);
    }

    public async Task<bool> DeleteBasket(Guid cartId, CancellationToken cancellationToken)
    {
        return await repository.DeleteElement(cartId, cancellationToken);
    }

    public async Task<ShoppingCart> GetCartById(Guid cartId, bool AsNoTracking, CancellationToken cancellationToken)
    {
        return await repository.GetElementById(cartId, AsNoTracking, cancellationToken, c => c.items)??throw new Exception($"Shopping cart with ID {cartId} not found.");
  
    }

    public async Task<bool> ReloadItems(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        return await repository.ReloadElementCollection(cart, c=>c.items, cancellationToken);
    }

    public async Task<bool> RemoveItem(ShoppingCartItem item, CancellationToken cancellationToken = default)
    {
        return await repository.RemoveElementFromCollection(item, i => i.ShoppingCartId, cancellationToken);
    }



    
}
