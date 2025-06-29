 

namespace Basket.Baskets.Dtos;

public record ShoppingCartItemDto : BasketModuleStructre.ICartItemConfigurations.MDto
{
    public Guid? Id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ShoppingCartId { get; set; }
    public int? Quantity { get; set; }
}
