using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Infrastructure.Events;

public sealed class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        return Task.CompletedTask;
    }
}
