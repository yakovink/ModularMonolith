





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

    

}
