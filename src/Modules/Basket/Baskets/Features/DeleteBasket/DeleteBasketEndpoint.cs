using System;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Baskets.Features.DeleteBasket;



internal class DeleteBasketEndpoint : GenericDeleteEndpoint<string, bool>
{
    public DeleteBasketEndpoint() : base("/baskets/delete", "Delete Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400",
            "status404"
        };
    }


    protected async override Task<IResult> NewEndpoint(GenericCommand<string, bool> request, ISender sender)
    {
        return await SendResults(new DeleteBasketCommand(request.input), sender);
    }
}
