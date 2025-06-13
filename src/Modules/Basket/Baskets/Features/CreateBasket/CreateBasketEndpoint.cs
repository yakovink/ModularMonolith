

namespace Basket.Baskets.Features.CreateBasket;




internal class CreateBasketEndpoint : GenericPostEndpoint<object, Guid>
{
    public CreateBasketEndpoint() : base("/baskets/create", "Create Basket")
    {
        serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<object, Guid> request, ISender sender)
    {
        return await SendResults(new CreateBasketCommand(), sender);
    }
}
