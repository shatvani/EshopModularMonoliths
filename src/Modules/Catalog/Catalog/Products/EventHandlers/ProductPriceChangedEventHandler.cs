namespace Catalog.Products.Events.EventHandlers;

public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger) 
    : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        // TODO: publish product price changed integration event for update basket price
        logger.LogInformation($"Domain Event handled: {domainEvent.GetType().Name}");
        await Task.CompletedTask;
    }
}