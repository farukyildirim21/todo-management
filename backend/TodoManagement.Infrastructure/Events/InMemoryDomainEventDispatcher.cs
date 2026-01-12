

namespace TodoManagement.Infrastructure.Events;

public class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent)
    {
        var handlers = _serviceProvider.GetServices(
            typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType())
        );

        foreach (dynamic handler in handlers)
        {
            await handler.HandleAsync((dynamic)domainEvent);
        }
    }
}
