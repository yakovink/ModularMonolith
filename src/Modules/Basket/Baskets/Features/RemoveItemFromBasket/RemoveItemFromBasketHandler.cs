using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<GenericResult<Guid>>;

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
internal class RemoveItemFromBasketHandler(BasketDbContext dbContext) : ICommandHandler<RemoveItemFromBasketCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
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
        return new GenericResult<Guid>(shoppingCart.Id);
    }


}

