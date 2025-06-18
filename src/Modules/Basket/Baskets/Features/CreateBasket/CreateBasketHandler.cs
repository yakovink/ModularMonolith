

namespace Basket.Baskets.Features.CreateBasket;



public class CreateBasketCommandValidator :  BasketModuleStructre.CreateBasket.MValidator
{
    public CreateBasketCommandValidator()
    {

    }
}



internal class CreateBasketHandler(IBasketRepository repository) : BasketModuleStructre.CreateBasket.IMEndpointPostHandler
{
    public async Task<GenericResult<Guid>> Handle(BasketModuleStructre.CreateBasket.Command request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = await repository.CreateBasket(cancellationToken);
        return new GenericResult<Guid>(cart.Id);
    }
}
