using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent notification);
}
