 

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
        ShoppingCartItemDto itemDto = request.item?? throw new ArgumentNullException(nameof(request.item), "Item cannot be null");
        Guid shoppingCartId = itemDto.ShoppingCartId ?? throw new ArgumentNullException(nameof(itemDto.ShoppingCartId), "Shopping Cart ID cannot be null");
        Guid productId = itemDto.ProductId ?? throw new ArgumentNullException(nameof(itemDto.ProductId), "Product ID cannot be null");
        int quantity = itemDto.Quantity ?? throw new ArgumentNullException(nameof(itemDto.Quantity), "Quantity cannot be null");
        ShoppingCart cart = await repository.GetElementById(shoppingCartId, false, cancellationToken) ?? throw new Exception($"Shopping cart with ID {shoppingCartId} not found.");
        ShoppingCartItem demoItem = ShoppingCartItem.Create(cart, productId, quantity);
        demoItem = await repository.AddItem(demoItem, cancellationToken);
        
        
        Console.WriteLine($"cart {cart.Id} have {cart.items.Count()} items");
        return new GenericResult<Guid>(demoItem.Id);


    }



}
