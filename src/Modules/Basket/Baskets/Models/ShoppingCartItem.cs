
namespace Basket.Baskets.Models;

public class ShoppingCartItem : Entity<Guid>
{
    public Guid ShoppingCartId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public int Quantity { get; internal set; } = default!;
    public string Color { get; private set; } = default!;

    public string ProductName { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    internal ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, string color, string productName, decimal price)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Color = color;
        ProductName = productName;
        Price = price;
    }
    
}
