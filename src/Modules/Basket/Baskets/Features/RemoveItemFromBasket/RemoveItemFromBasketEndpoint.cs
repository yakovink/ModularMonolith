 

namespace Basket.Baskets.Features.RemoveItemFromBasket;



public class RemoveItemFromBasketEndpoint : BasketModuleStructre.RemoveItemFromBasket.MDeleteEndpoint
{
    public RemoveItemFromBasketEndpoint() : base("/baskets/remove", "Remove Item From Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<ShoppingCartItemDto, bool> request, ISender sender)
    {

        // Create the command
        return await SendResults(new BasketModuleStructre.RemoveItemFromBasket.Command(request.input), sender);
    }
}
