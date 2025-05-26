using System;

namespace Basket.Baskets.Features.DeleteBasket;


public record DeleteBasketCommand(Guid BasketId) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success) : GenericResult<bool>(Success);

public class DeleteBasketHandler
{

}
