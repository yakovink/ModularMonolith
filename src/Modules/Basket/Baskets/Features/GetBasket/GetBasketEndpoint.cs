using System;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Baskets.Features.GetBasket;



public class GetBasketEndpoint : GenericGetEndpoint<Guid, ShoppingCartDto>
{
    public GetBasketEndpoint() : base("/baskets/get/", "Get Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }


    protected async override Task<IResult> NewEndpoint(Guid input, ISender sender)
    {
        return await SendResults(new GetBasketQuery(input), sender);
    }
}
