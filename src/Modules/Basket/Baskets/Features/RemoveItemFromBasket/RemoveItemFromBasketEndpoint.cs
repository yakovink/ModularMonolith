using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;



public class RemoveItemFromBasketEndpoint : GenericDeleteEndpoint<ShoppingCartItemDto, Guid>
{
    public RemoveItemFromBasketEndpoint() : base("/baskets/{name}", "Remove Item From Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<ShoppingCartItemDto, Guid> request, ISender sender)
    {

        // Create the command
        return await SendResults(new RemoveItemFromBasketCommand(request.input), sender);
    }
}
