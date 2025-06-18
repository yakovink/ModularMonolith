

namespace Basket.Baskets.Features.AddItemIntoBasket;


public class AddItemIntoBasketEndpoint : BasketModuleStructre.AddItemIntoBasket.MPostEndpoint
{
    public AddItemIntoBasketEndpoint() : base("/baskets/add", "Add Item Into Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<ShoppingCartItemDto, Guid> request, [FromServices] ISender sender)
    {
        // Return the result
        return await SendResults(new BasketModuleStructre.AddItemIntoBasket.Command(request.input),sender);
    }
}
