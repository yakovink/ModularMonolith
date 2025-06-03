using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;



public class RemoveItemFromBasketEndpoint : GenericDeleteEndpoint<ItemInCartDto, Guid>
{
    public RemoveItemFromBasketEndpoint() : base("/baskets/{name}", "Remove Item From Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommand<ItemInCartDto, Guid> request, ISender sender)
    {

        if (request.input == null || string.IsNullOrEmpty(request.input.UserName) || request.input.ItemDto == null || request.input.ItemDto.ProductId == null)
        {
            throw new ArgumentException("Invalid input: UserName and ItemDto must be provided.");
        }

        // Create the command
        return await SendResults(new RemoveItemFromBasketCommand(request.input.UserName, (Guid)request.input.ItemDto.ProductId), sender);
    }
}
