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

        //  BURAYA EKLE
        var handlers = _serviceProvider
            .GetServices(handlerType)
            .ToList();

        Console.WriteLine(
            $"[DomainEventDispatcher] Event: {domainEvent.GetType().Name}, HandlerCount: {handlers.Count}"
        );

        foreach (var handler in handlers)
        {
            Console.WriteLine(
                $"[DomainEventDispatcher] → Calling handler: {handler.GetType().Name}"
            );

            await ((dynamic)handler).Handle((dynamic)domainEvent);
        }
    }
}

}
