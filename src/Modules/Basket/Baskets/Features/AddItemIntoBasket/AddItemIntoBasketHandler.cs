 

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

public class AddItemIntoBasketHandler(IBasketRepository repository) : ICommandHandler<AddItemIntoBasketCommand, GenericResult<Guid>>
{

    public async Task<GenericResult<Guid>> Handle(AddItemIntoBasketCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        if (request.item == null || request.item.ProductId == null || request.item.Quantity == null|| request.item.ShoppingCartId == null)
        {
            throw new ArgumentException("Invalid item details provided.");
        }
        ShoppingCart cart = await repository.GetCart((Guid)request.item.ShoppingCartId, false, cancellationToken);
        ShoppingCartItem? item = cart.items.Where(i => i.ProductId == (Guid)request.item.ProductId).SingleOrDefault();
        if (item == null)
        {
            item = ShoppingCartItem.Create(cart, (Guid)request.item.ProductId, (int)request.item.Quantity);
            await repository.AddItem(item);
        }
        else
        {
            item.Quantity += (int)request.item.Quantity;
        }
        await repository.SaveChangesAsync(cart.Id,cancellationToken);
        await repository.ReloadItems(cart, cancellationToken);
        
        Console.WriteLine($"cart {cart.Id} have {cart.items.Count()} items");
        return new GenericResult<Guid>(item.Id);


    }



}
