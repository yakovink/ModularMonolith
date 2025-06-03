

namespace Basket.Baskets.Features.CreateBasket;




internal class CreateBasketEndpoint : GenericPostEndpoint<ShoppingCartDto, Guid>
{
    public CreateBasketEndpoint() : base("/baskets/create", "Create Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<ShoppingCartDto, Guid> request, ISender sender)
    {
        return await SendResults(new CreateBasketCommand(request.input), sender);
    }
}
