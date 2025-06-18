 

namespace Basket.Baskets.Dtos;

public record ShoppingCartDto : BasketModuleStructre.ICartConfigurations.MDto
{
    public Guid? Id { get; set; }
  
}
