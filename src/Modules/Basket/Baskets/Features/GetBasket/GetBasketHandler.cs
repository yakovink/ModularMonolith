using System;

namespace Basket.Baskets.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCartDto ShoppingCart) : GenericResult<ShoppingCartDto>(ShoppingCart);



internal class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // Simulate retrieval logic
        ShoppingCart? basket = await dbContext.getCartByUserName(request.UserName, cancellationToken, RequestType.Query);
        if (basket == null)
        {
            throw new BasketNotFoundException(request.UserName);
        }
        
        var shoppingCartDto = basket.Adapt<ShoppingCartDto>();
        
        return new GetBasketResult(shoppingCartDto);
    }
}
