using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(ShoppingCartItemDto input) : ICommand<GenericResult<Guid>>;

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {   
        RuleFor(x => x.input).NotNull().WithMessage("input is required.");
        RuleFor(x => x.input.ShoppingCartId).NotNull().WithMessage("User name is required.");
        RuleFor(x => x.input.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
internal class RemoveItemFromBasketHandler(BasketDbContext dbContext) : ICommandHandler<RemoveItemFromBasketCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        if (request.input.ShoppingCartId == null || request.input.ProductId == null)
        {
            throw new BasketNotFoundException("Shopping cart id and product id are both required");
        }
        Guid shopping_cart_id = (Guid)request.input.ShoppingCartId;
        // Retrieve the shopping cart by user name
        ShoppingCart? shoppingCart = await dbContext.getCartById(shopping_cart_id, cancellationToken, RequestType.Command);
        if (shoppingCart == null)
        {
            throw new BasketNotFoundException(shopping_cart_id.ToString());
        }

        // Remove the item from the shopping cart
        shoppingCart.RemoveItem((Guid)request.input.ProductId);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new GenericResult<Guid>(shopping_cart_id);
    }


}

