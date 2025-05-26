

namespace Basket.Baskets.Features.CreateBasket;

public record CreateBasketResponse(Guid ShoppingCartId) : GenericResponse<Guid>(ShoppingCartId);


internal class CreateBasketEndpoint : GenericPostEndpoint<ShoppingCartDto, Guid>
{
    public CreateBasketEndpoint() : base("/basket", "Create Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint(GenericCommand<ShoppingCartDto, Guid> request, ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }

        // Create the command
        CreateBasketCommand command = new CreateBasketCommand(request.input);

        // Send the command and get the result
        CreateBasketResult result = await sender.Send(command);

        // Create the response
        CreateBasketResponse response = new CreateBasketResponse(result.Id);

        // Return the result
        return Results.Ok(response);
    }
}
