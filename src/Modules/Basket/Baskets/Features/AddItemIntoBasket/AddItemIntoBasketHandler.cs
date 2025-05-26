using System;

namespace Basket.Baskets.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto item) : ICommand<AddItemIntoBasketResult>;
public record AddItemIntoBasketResult(Guid id) : GenericResult<Guid>(id);


public class AddItemIntoBasketValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.item.ProductId).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}

public class AddItemIntoBasketHandler(BasketDbContext dbContext) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{

    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        if (request.item == null || request.item.ProductId == null || request.item.Quantity == null || request.item.color == null || request.item.ProductName == null)
        {
            throw new ArgumentException("Invalid item details provided.");
        }

        ShoppingCart? shoppingCart = await dbContext.getCartByUserName(request.UserName, cancellationToken, RequestType.Command);
        if (shoppingCart == null)
        {
            throw new BasketNotFoundException(request.UserName);
        }
        shoppingCart.AddItem(
            (Guid)request.item.ProductId,
            (int)request.item.Quantity,
            request.item.color,
            request.item.ProductName,
            request.item.Price);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddItemIntoBasketResult(shoppingCart.Id);
    }



}
