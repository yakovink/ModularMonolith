 

namespace Basket.Baskets.Features.RemoveItemFromBasket;


public class RemoveItemFromBasketValidator : BasketModuleStructre.RemoveItemFromBasket.MValidator
{
    public RemoveItemFromBasketValidator()
    {   
        RuleFor(x => x.input).NotNull().WithMessage("input is required.");
        RuleFor(x => x.input.ShoppingCartId).NotNull().WithMessage("User name is required.");
        RuleFor(x => x.input.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
internal class RemoveItemFromBasketHandler(IBasketRepository repository) : BasketModuleStructre.RemoveItemFromBasket.IMEndpointDeleteHandler
{
    public async Task<GenericResult<bool>> Handle(BasketModuleStructre.RemoveItemFromBasket.Command request, CancellationToken cancellationToken)
    {
        ShoppingCartItemDto dto = request.input ?? throw new ArgumentNullException(nameof(dto));
        Guid shoppingCartId = dto.ShoppingCartId ?? throw new ArgumentNullException(nameof(dto.ShoppingCartId));
        Guid productId = dto.ProductId ?? throw new ArgumentNullException(nameof(dto.ProductId));


        IEnumerable<ShoppingCartItem> items = (await repository.GetCartById(shoppingCartId,false,cancellationToken)).items;
        Console.WriteLine($"cart {shoppingCartId} have {items.Count()} items");
        ShoppingCartItem item = items.Where(i => i.ProductId == productId).SingleOrDefault()??
            throw new NotFoundException($"item {productId} was not found in cart {shoppingCartId}");

        bool output= await repository.RemoveItem(item);
        return new GenericResult<bool>(output);
    }


}

