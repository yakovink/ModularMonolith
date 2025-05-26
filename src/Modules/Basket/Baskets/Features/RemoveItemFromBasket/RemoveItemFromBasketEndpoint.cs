using System;

namespace Basket.Baskets.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketResponse(Guid CartId) : GenericResult<Guid>(CartId);

public class RemoveItemFromBasketEndpoint : GenericDeleteEndpoint<ItemInCartDto, Guid>
{
    public RemoveItemFromBasketEndpoint() : base("/basket/{name:string}", "Remove Item From Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommand<ItemInCartDto, Guid> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        if (request.input == null || string.IsNullOrEmpty(request.input.UserName) || request.input.ItemDto == null || request.input.ItemDto.ProductId == null)
        {
            throw new ArgumentException("Invalid input: UserName and ItemDto must be provided.");
        }

        // Create the command
        RemoveItemFromBasketCommand command = new RemoveItemFromBasketCommand(request.input.UserName, (Guid)request.input.ItemDto.ProductId);

        // Send the command and get the result
        RemoveItemFromBasketResult result = await sender.Send(command);

        // Create the response
        RemoveItemFromBasketResponse response = new RemoveItemFromBasketResponse(result.cartId);

        // Return the result
        return Results.Ok(response);
    }
}
