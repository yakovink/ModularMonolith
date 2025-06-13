

namespace Basket.Baskets.Features.CreateBasket;


public record CreateBasketCommand() : ICommand<GenericResult<Guid>>;
public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        

    }
}



internal class CreateBasketHandler(IBasketRepository repository) : ICommandHandler<CreateBasketCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart ShoppingCart = ShoppingCart.Create();
        ShoppingCart=await repository.CreateElement(ShoppingCart);
        return new GenericResult<Guid>(ShoppingCart.Id);
    }
}
