 

namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
{
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        // Log the price change event
        logger.LogInformation("Domain Event handeled: {DomainEvent}", notification.GetType().Name);
        // Here you can add additional logic, such as updating a cache or notifying other services
        return Task.CompletedTask;
    }
}
