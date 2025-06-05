using System;
using System.Text.Json;
using Shared.Communicate;

namespace Basket.Baskets.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(ShoppingCartItemDto item) : ICommand<GenericResult<Guid>>;



public class AddItemIntoBasketValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketValidator()
    {
        RuleFor(x => x.item.ShoppingCartId).NotNull().WithMessage("User name is required.");
        RuleFor(x => x.item.ProductId).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.item.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}

public class AddItemIntoBasketHandler(BasketDbContext dbContext) : ICommandHandler<AddItemIntoBasketCommand, GenericResult<Guid>>
{

    public async Task<GenericResult<Guid>> Handle(AddItemIntoBasketCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        if (request.item == null || request.item.ProductId == null || request.item.Quantity == null|| request.item.ShoppingCartId == null)
        {
            throw new ArgumentException("Invalid item details provided.");
        }
        Guid shopping_cart = (Guid)request.item.ShoppingCartId;
        ShoppingCart? shoppingCart = await dbContext.getCartById(shopping_cart, cancellationToken, RequestType.Command);
        JsonElement product = await ShoppingCartItem.GetProduct((Guid)request.item.ProductId);
        if (shoppingCart == null)
        {
            throw new BasketNotFoundException(shopping_cart.ToString());
        }
        if (product.ValueKind == JsonValueKind.Null || 
            product.ValueKind == JsonValueKind.Undefined ||
            (product.ValueKind == JsonValueKind.Object && product.GetRawText() == "{}"))
        {
            throw new ProductNotFoundException((Guid)request.item.ProductId);
        }
            
        shoppingCart.AddItem(
            (Guid)request.item.ProductId,
            (int)request.item.Quantity);
        Console.WriteLine(shoppingCart.Items.Count());
        await dbContext.SaveChangesAsync(cancellationToken);
        return new GenericResult<Guid>(shoppingCart.Id);


    }



}
