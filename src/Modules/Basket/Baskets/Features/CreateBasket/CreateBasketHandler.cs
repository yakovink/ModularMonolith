

namespace Basket.Baskets.Features.CreateBasket;


public record CreateBasketCommand(ShoppingCartDto input) : ICommand<GenericResult<Guid>>;
public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        
        RuleFor(x => x.input.Id)
            .NotNull()
            .WithMessage("Id is required");
    }
}



internal class CreateBasketHandler(IBasketRepository repository) : ICommandHandler<CreateBasketCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart ShoppingCart = CreateNewBasket(request.input);
        ShoppingCart=await repository.CreateBasket(ShoppingCart);
        return new GenericResult<Guid>(ShoppingCart.Id);
    }
    


    private ShoppingCart CreateNewBasket(ShoppingCartDto input)
    {
        if (input == null )
        {
            throw new ArgumentNullException(nameof(input), "ShoppingCartDto or UserName cannot be null");
        }
        var newBasket = ShoppingCart.Create();
        return newBasket;
    }
}
