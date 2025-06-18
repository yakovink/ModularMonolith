

namespace Basket.Baskets.Features.CreateBasket;




internal class CreateBasketEndpoint : BasketModuleStructre.CreateBasket.MPostEndpoint
{
    public CreateBasketEndpoint() : base("/baskets/create", "Create Basket")
    {
        serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommandRequest<bool, Guid> request, ISender sender)
    {
        return await SendResults(new BasketModuleStructre.CreateBasket.Command(request.input), sender);
    }
}
