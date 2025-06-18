 

namespace Basket.Baskets.Features.GetBasket;


internal class GetBasketHandler(IBasketRepository repository) : BasketModuleStructre.GetBasket.IMEndpointGetHandler
{
    public async Task<GenericResult<HashSet<ShoppingCartItemDto>>> Handle(BasketModuleStructre.GetBasket.Query request, CancellationToken cancellationToken)
    {
        // Simulate retrieval logic
        List<ShoppingCartItem> items = (await repository.GetCartById(request.input, true,cancellationToken)).items.ToList()??
            throw new BasketNotFoundException(request.input.ToString());

        HashSet<ShoppingCartItemDto> dtos = items.Select(i => i.ToDto()).ToHashSet();
        return new GenericResult<HashSet<ShoppingCartItemDto>>(dtos);
    }
}
