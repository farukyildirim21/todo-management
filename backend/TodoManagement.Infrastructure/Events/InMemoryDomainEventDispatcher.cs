using Microsoft.Extensions.DependencyInjection;
using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Infrastructure.Events;

public sealed class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await ((dynamic)handler).Handle((dynamic)domainEvent);
            }
        }
    }
}
