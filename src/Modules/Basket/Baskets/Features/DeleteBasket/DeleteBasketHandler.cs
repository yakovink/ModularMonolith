using System;

namespace Basket.Baskets.Features.DeleteBasket;


public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success) : GenericResult<bool>(Success);

internal class DeleteBasketHandler(BasketDbContext dbContext) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // Simulate deletion logic
        ShoppingCart? basket = await dbContext.getCartByUserName(request.UserName, cancellationToken, RequestType.Command);
        if (basket == null)
        {
            throw new BasketNotFoundException(request.UserName);
        }
        dbContext.ShoppingCarts.Remove(basket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteBasketResult(true);
    }
}
