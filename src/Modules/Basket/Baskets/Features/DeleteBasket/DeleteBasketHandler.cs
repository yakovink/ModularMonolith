 

namespace Basket.Baskets.Features.DeleteBasket;


public record DeleteBasketCommand(Guid input) : ICommand<GenericResult<bool>>;


internal class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // Simulate deletion logic
        bool output = await repository.DeleteBasket(request.input);
        return new GenericResult<bool>(output);
    }
}
