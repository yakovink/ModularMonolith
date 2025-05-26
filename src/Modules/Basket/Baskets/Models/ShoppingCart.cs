

using Shared.Exceptions;

namespace Basket;

public class ShoppingCart : Aggregate<Guid>
{
    public string UserName { get; private set; } = default!;
    private readonly List<ShoppingCartItem> _items = new();
    public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();
    public decimal TotalPrice => _items.Sum(item => item.Price * item.Quantity);




    public static ShoppingCart Create(Guid id, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        var shoppingCart = new ShoppingCart
        {
            Id = id,
            UserName = userName
        };
        return shoppingCart;
    }

    public void AddItem(Guid productId, int quantity, string color, string productName, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        ShoppingCartItem? existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            ShoppingCartItem newItem = new(
                shoppingCartId: Id,
                productId: productId,
                quantity: quantity,
                color: color,
                productName: productName,
                price: price
            )
            {
                Id = Guid.NewGuid()
            };
            _items.Add(newItem);
        }
    }

    public void RemoveItem(Guid productId)
    {
        ShoppingCartItem? itemToRemove = _items.FirstOrDefault(item => item.ProductId == productId);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
        }
        else
        {
            throw new ProductNotFoundException(productId);
        }
    }
}
