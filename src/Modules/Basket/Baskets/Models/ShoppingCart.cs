


using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Basket.Baskets.Models;

public class ShoppingCart : Aggregate<Guid>
{
    public string UserName { get; private set; } = default!;
    private readonly List<ShoppingCartItem> _items = new();
    public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();
 

    public async Task<decimal> GetTotalPriceAsync()
    {
        decimal total = 0;

        foreach (var item in _items)
        {
            JsonElement product = await ShoppingCartItem.GetProduct(item.ProductId);
            decimal price = product.GetProperty("price").GetDecimal();
            total += price * item.Quantity;
        }
        return total;
    }




    public static ShoppingCart Create(Guid id, string userName)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        var shoppingCart = new ShoppingCart
        {
            Id = id,
            UserName = userName,
            _createdBy=Environment.UserName,
            _createdDate=DateTime.UtcNow,
            _lastModifiedBy=Environment.UserName,
            _lastModifiedDate=DateTime.UtcNow
        };
        return shoppingCart;
    }



    public void AddItem(Guid productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        ShoppingCartItem? existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            ShoppingCartItem newItem = new ShoppingCartItem(
                shoppingCartId: Id,
                productId: productId,
                quantity: quantity
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
