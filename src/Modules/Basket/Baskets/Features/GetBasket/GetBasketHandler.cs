using System;

namespace Basket.Baskets.Features.GetBasket;

public record GetBasketQuery(Guid input) : IQuery<GenericResult<ShoppingCartDto>>;



internal class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetBasketQuery, GenericResult<ShoppingCartDto>>
{
    public async Task<GenericResult<ShoppingCartDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // Simulate retrieval logic
        ShoppingCart? basket = await dbContext.getCartById(request.input, cancellationToken, RequestType.Query);
        if (basket == null)
        {
            throw new BasketNotFoundException(request.input.ToString());
        }
        
        var shoppingCartDto = basket.Adapt<ShoppingCartDto>();
        
        return new GenericResult<ShoppingCartDto>(shoppingCartDto);
    }
}
