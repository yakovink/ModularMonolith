 

namespace Basket.Baskets.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(ShoppingCartItemDto input) : ICommand<GenericResult<bool>>;

public class RemoveItemFromBasketValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {   
        RuleFor(x => x.input).NotNull().WithMessage("input is required.");
        RuleFor(x => x.input.ShoppingCartId).NotNull().WithMessage("User name is required.");
        RuleFor(x => x.input.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
internal class RemoveItemFromBasketHandler(IBasketRepository repository) : ICommandHandler<RemoveItemFromBasketCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        if (request.input.ShoppingCartId == null || request.input.ProductId == null)
        {
            throw new BasketNotFoundException("Shopping cart id and product id are both required");
        }
        ShoppingCart cart = await repository.GetCart((Guid)request.input.ShoppingCartId,false,cancellationToken);
        ShoppingCartItem? item = cart.items.Where(i => i.ProductId == (Guid)request.input.ProductId).SingleOrDefault();
        if (item == null)
        {
            throw new NotFoundException($"item {(Guid)request.input.ProductId} was not found in cart {(Guid)request.input.ShoppingCartId}");
        }
        var output= await repository.RemoveItem(item);
        return new GenericResult<bool>(output);
    }


}

