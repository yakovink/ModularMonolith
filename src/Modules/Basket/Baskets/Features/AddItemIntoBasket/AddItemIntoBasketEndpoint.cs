

namespace Basket.Baskets.Features.AddItemIntoBasket;

public record AddItemIntoBasketResponse(Guid id) : GenericResult<Guid>(id);
public class AddItemIntoBasketEndpoint : GenericPostEndpoint<ItemInCartDto, Guid>
{
    public AddItemIntoBasketEndpoint() : base("/basket/add", "Add Item Into Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint([AsParameters] GenericCommand<ItemInCartDto, Guid> request, [FromServices] ISender sender)
    {
        if (sender == null)
        {
            throw new InvalidOperationException("Sender is not set.");
        }
        if (request.input == null || request.input.ItemDto == null || string.IsNullOrEmpty(request.input.UserName))
        {
            throw new ArgumentException("Invalid input: UserName and ItemDto must be provided.");
        }

        // Create the command
        AddItemIntoBasketCommand command = new AddItemIntoBasketCommand(request.input.UserName, request.input.ItemDto);

        // Send the command and get the result
        AddItemIntoBasketResult result = await sender.Send(command);

        // Create the response
        AddItemIntoBasketResponse response = new AddItemIntoBasketResponse(result.id);

        // Return the result
        return Results.Ok(response);
    }
}
