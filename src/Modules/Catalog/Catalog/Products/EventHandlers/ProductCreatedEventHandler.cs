namespace Catalog.Products.Events.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) 
    : INotificationHandler<ProductCreatedEvent>
{
    public async Task Handle(ProductCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation($"PDomain Event handled: {domainEvent.GetType().Name}");
        await Task.CompletedTask; 
    }
}
