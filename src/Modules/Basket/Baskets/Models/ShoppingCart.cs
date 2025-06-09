





namespace Basket.Baskets.Models;

public class ShoppingCart : Aggregate<Guid>
{
    [JsonInclude]
    public List<ShoppingCartItem> items { get; private set; } = new();


    public static ShoppingCart Create()
    {

        var shoppingCart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            _createdBy = Environment.UserName,
            _createdDate = DateTime.UtcNow,
            _lastModifiedBy = Environment.UserName,
            _lastModifiedDate = DateTime.UtcNow
        };
        return shoppingCart;
    }

    public ShoppingCartItem AddItem(ShoppingCartItemDto itemDto)
    {
        if (itemDto.ProductId == null || itemDto.Quantity == null)
        {
            throw new ArgumentException("invalid parameters");
        }
        ShoppingCartItem item = ShoppingCartItem.Create(this, (Guid)itemDto.ProductId, (int)itemDto.Quantity);
        item.setCart(this);
        items.Add(item);
        return item;
    }

    public void checkEmpty()
    {
        if (items.Count() == 0)
        {
            throw new Exception("the cart is empty");
        }
    }
    

}
