using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
}