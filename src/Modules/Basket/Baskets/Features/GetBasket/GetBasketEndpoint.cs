using System;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Baskets.Features.GetBasket;


public record GetBasketResponse(ShoppingCartDto ShoppingCart) : GenericResult<ShoppingCartDto>(ShoppingCart);
public class GetBasketEndpoint : GenericGetEndpoint<string, ShoppingCartDto>
{
    public GetBasketEndpoint() : base("/basket/{name:string}", "Get Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }


    protected async override Task<IResult> NewEndpoint(string input, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }

        // Create the query
        GetBasketQuery query = new GetBasketQuery(input);

        // Send the query and get the result
        GetBasketResult result = await sender.Send(query);

        // Create the response
        GetBasketResponse response = new GetBasketResponse(result.ShoppingCart);

        // Return the result
        return Results.Ok(response);
    }
}
