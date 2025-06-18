 

namespace Basket.Baskets.Features.DeleteBasket;


internal class DeleteBasketHandler(IBasketRepository repository) : BasketModuleStructre.DeleteBasket.IMEndpointDeleteHandler
{
    public async Task<GenericResult<bool>> Handle(BasketModuleStructre.DeleteBasket.Command request, CancellationToken cancellationToken)
    {
        // Simulate deletion logic
        bool output = await repository.DeleteBasket(request.input, cancellationToken);
        return new GenericResult<bool>(output);
    }
}
