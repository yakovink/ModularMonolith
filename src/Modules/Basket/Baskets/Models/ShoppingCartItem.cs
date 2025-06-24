



namespace Basket.Baskets.Models;

public class ShoppingCartItem : Entity<Guid>, BasketModuleStructre.CartItem
{

    public Guid ShoppingCartId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public int Quantity { get; set; } = default!;
    [JsonIgnore]
    public ShoppingCart ShoppingCart { get; private set; } = default!;

    [JsonConstructor]
    public ShoppingCartItem(Guid id, Guid ShoppingCartId, Guid ProductId, int Quantity)
    {
        Id = id;
        this.ShoppingCartId = ShoppingCartId;
        this.ProductId = ProductId;
        this.Quantity = Quantity;
        _createdBy = Environment.UserName;
        _createdDate = DateTime.UtcNow;
        _lastModifiedBy = Environment.UserName;
        _lastModifiedDate = DateTime.UtcNow;       
    }

    private ShoppingCartItem(ShoppingCart shoppingCart, Guid ProductId, int Quantity)
    {
        ShoppingCartId = shoppingCart.Id;
        this.ProductId = ProductId;
        this.Quantity = Quantity;
        _createdBy = Environment.UserName;
        _createdDate = DateTime.UtcNow;
        _lastModifiedBy = Environment.UserName;
        _lastModifiedDate = DateTime.UtcNow;
    }

    public static ShoppingCartItem Create(ShoppingCart shoppingCart, Guid productId, int quantity)
    {
        ShoppingCartItem item = new ShoppingCartItem(shoppingCart,productId,quantity){Id=Guid.NewGuid()};
        return item;
    }



    public ShoppingCartItemDto ToDto()
    {
        return new ShoppingCartItemDto
        {
            Id = Id,
            ShoppingCartId = ShoppingCartId,
            ProductId = ProductId,
            Quantity = Quantity
        };
    }


    public override string ToString()
    {
        return $"item: {Id}, cart: {ShoppingCartId}, product: {ProductId}, quantity: {Quantity}";
    }


}
