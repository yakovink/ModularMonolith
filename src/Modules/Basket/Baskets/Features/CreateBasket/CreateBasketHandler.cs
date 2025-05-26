using System;

namespace Basket.Baskets.Features.CreateBasket;


public record CreateBasketCommand(ShoppingCartDto input) : ICommand<CreateBasketResult>;
public record CreateBasketResult(Guid Id): GenericResult<Guid>(Id);
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



internal class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart? ShoppingCart = CreateNewBasket(request.input);
        dbContext.ShoppingCarts.Add(ShoppingCart);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateBasketResult(ShoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto input)
    {
        if (input == null || input.UserName == null || input.Items == null || input.Items.Count == 0)
        {
            throw new ArgumentNullException(nameof(input), "ShoppingCartDto or UserName cannot be null");
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
