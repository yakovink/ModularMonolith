using System;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Baskets.Features.GetBasket;



public class GetBasketEndpoint : GenericGetEndpoint<string, ShoppingCartDto>
{
    public GetBasketEndpoint() : base("/baskets/{name}", "Get Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }


    protected async override Task<IResult> NewEndpoint(string input, ISender sender)
    {
        return await SendResults(new GetBasketQuery(input), sender);
    }
}
