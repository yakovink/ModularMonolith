

namespace Basket.Baskets.Features.AddItemIntoBasket;


public class AddItemIntoBasketEndpoint : GenericPostEndpoint<ItemInCartDto, Guid>
{
    public AddItemIntoBasketEndpoint() : base("/baskets/add", "Add Item Into Basket")
    {
        this.serviceNames = new List<string>
        {
            "status400"
        };
    }

    protected async override Task<IResult> NewEndpoint([AsParameters] GenericCommand<ItemInCartDto, Guid> request, [FromServices] ISender sender)
    {

        if (request.input == null || request.input.ItemDto == null || string.IsNullOrEmpty(request.input.UserName))
        {
            throw new ArgumentException("Invalid input: UserName and ItemDto must be provided.");
        }

        

        // Return the result
        return await SendResults(new AddItemIntoBasketCommand(request.input.UserName,request.input.ItemDto),sender);
    }
}
