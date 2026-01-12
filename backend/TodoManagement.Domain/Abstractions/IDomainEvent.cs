namespace TodoManagement.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}