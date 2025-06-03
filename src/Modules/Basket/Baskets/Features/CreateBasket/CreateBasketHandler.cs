
namespace Basket.Baskets.Features.CreateBasket;


public record CreateBasketCommand(ShoppingCartDto input) : ICommand<GenericResult<Guid>>;
public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        
        RuleFor(x => x.input.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("UserName is required");
    }
}



internal class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart? ShoppingCart = CreateNewBasket(request.input);
        dbContext.ShoppingCarts.Add(ShoppingCart);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new GenericResult<Guid>(ShoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto input)
    {
        if (input == null || input.UserName == null )
        {
            throw new ArgumentNullException(nameof(input), "ShoppingCartDto or UserName cannot be null");
        }
        
        if (input.Items == null)
        {
            input.Items = new List<ShoppingCartItemDto>();
        }


        var newBasket = ShoppingCart.Create(
            Guid.NewGuid(),
            input.UserName
            );
        input.Items.ForEach(item =>
        {
            if (item == null || item.ProductId == null || item.Quantity ==null || item.Price <= 0 || item.ProductName == null || item.color == null)
            {
                throw new ArgumentNullException(nameof(item), "ShoppingCartItemDto cannot be null or have invalid values");
            }
            newBasket.AddItem(
                (Guid)item.ProductId,
                (int)item.Quantity,
                item.color,
                item.ProductName,
                item.Price
            );
        });
        return newBasket;
    }
}
