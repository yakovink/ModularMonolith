using System;

namespace Basket.Baskets.Features.DeleteBasket;


public record DeleteBasketCommand(string input) : ICommand<GenericResult<bool>>;


internal class DeleteBasketHandler(BasketDbContext dbContext) : ICommandHandler<DeleteBasketCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // Simulate deletion logic
        ShoppingCart? basket = await dbContext.getCartByUserName(request.input, cancellationToken, RequestType.Command);
        if (basket == null)
        {
            throw new BasketNotFoundException(request.input);
        }
        dbContext.ShoppingCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new GenericResult<bool>(true);
    }
}
