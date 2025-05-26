using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;
public record RemoveItemFromBasketResult(Guid cartId) : GenericResult<Guid>(cartId);

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
internal class RemoveItemFromBasketHandler(BasketDbContext dbContext) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the shopping cart by user name
        ShoppingCart? shoppingCart = await dbContext.getCartByUserName(request.UserName, cancellationToken, RequestType.Command);
        if (shoppingCart == null)
        {
            throw new BasketNotFoundException(request.UserName);
        }

        // Remove the item from the shopping cart
        shoppingCart.RemoveItem(request.ProductId);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new RemoveItemFromBasketResult(shoppingCart.Id);
    }


}

