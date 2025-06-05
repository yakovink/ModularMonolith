
using System.Text.Json;

namespace Basket.Baskets.Models;

public class ShoppingCartItem : Entity<Guid>
{
    public Guid ShoppingCartId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public int Quantity { get; internal set; } = default!;

    internal ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public static async Task<JsonElement> GetProduct(Guid productId)
    {
        HttpController controller = Constants.BasketController;
        JsonDocument doc = await controller.Get($"products/get?input={productId}");
        return doc.RootElement;
    }


}
