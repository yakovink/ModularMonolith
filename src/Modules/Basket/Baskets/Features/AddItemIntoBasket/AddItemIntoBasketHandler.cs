 

namespace Basket.Baskets.Features.AddItemIntoBasket;


public class AddItemIntoBasketValidator : BasketModuleStructre.AddItemIntoBasket.MValidator
{
    public AddItemIntoBasketValidator()
    {
        RuleFor(x => x.input.ShoppingCartId).NotNull().WithMessage("User name is required.");
        RuleFor(x => x.input.ProductId).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.input.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}

public class AddItemIntoBasketHandler(IBasketRepository repository) : BasketModuleStructre.AddItemIntoBasket.IMEndpointPostHandler
{

    public async Task<GenericResult<Guid>> Handle(BasketModuleStructre.AddItemIntoBasket.Command request, CancellationToken cancellationToken)
    {
        // Validate the command
        ShoppingCartItemDto itemDto = request.input?? throw new ArgumentNullException(nameof(request.input), "Item cannot be null");
        Guid shoppingCartId = itemDto.ShoppingCartId ?? throw new ArgumentNullException(nameof(itemDto.ShoppingCartId), "Shopping Cart ID cannot be null");
        Guid productId = itemDto.ProductId ?? throw new ArgumentNullException(nameof(itemDto.ProductId), "Product ID cannot be null");
        int quantity = itemDto.Quantity ?? throw new ArgumentNullException(nameof(itemDto.Quantity), "Quantity cannot be null");


        ShoppingCart cart = await repository.GetCartById(shoppingCartId, false, cancellationToken) ?? throw new Exception($"Shopping cart with ID {shoppingCartId} not found.");
        ShoppingCartItem demoItem = ShoppingCartItem.Create(cart, productId, quantity);
        demoItem = await repository.AddItem(demoItem, cancellationToken);
        
        
        Console.WriteLine($"cart {cart.Id} have {cart.items.Count()} items");
        return new GenericResult<Guid>(demoItem.Id);


    }



}
