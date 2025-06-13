 

namespace Basket.Baskets.Features.GetBasket;

public record GetBasketQuery(Guid input) : IQuery<GenericResult<HashSet<ShoppingCartItemDto>>>;



internal class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GenericResult<HashSet<ShoppingCartItemDto>>>
{
    public async Task<GenericResult<HashSet<ShoppingCartItemDto>>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // Simulate retrieval logic
        List<ShoppingCartItem> items = (await repository.GetCart(request.input, true,cancellationToken)).ToList()??
            throw new BasketNotFoundException(request.input.ToString());

        HashSet<ShoppingCartItemDto> dtos = items.Select(i => i.ToDto()).ToHashSet();
        return new GenericResult<HashSet<ShoppingCartItemDto>>(dtos);
    }
}
