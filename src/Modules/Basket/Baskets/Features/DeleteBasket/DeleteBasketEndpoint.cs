using System;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Baskets.Features.DeleteBasket;


public record DeleteBasketResponse(bool Success) : GenericResult<bool>(Success);
internal class DeleteBasketEndpoint : GenericDeleteEndpoint<string, bool>
{
    public DeleteBasketEndpoint() : base("/basket", "Delete Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommand<string, bool> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }

        // Create the command
        DeleteBasketCommand command = new DeleteBasketCommand(request.input);

        // Send the command and get the result
        DeleteBasketResult result = await sender.Send(command);

        // Create the response
        DeleteBasketResponse response = new DeleteBasketResponse(result.Success);

        // Return the result
        return Results.Ok(response);
    }
}
