

using Shared.Exceptions;

namespace Basket;

public class ShoppingCart : Aggregate<Guid>
{
    public string UserName { get; private set; } = default!;
    private readonly List<ShopingCartItem> _items = new();
    public IReadOnlyCollection<ShopingCartItem> Items => _items.AsReadOnly();
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
        ShopingCartItem? existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            ShopingCartItem newItem = new(
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
        ShopingCartItem? itemToRemove = _items.FirstOrDefault(item => item.ProductId == productId);
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
