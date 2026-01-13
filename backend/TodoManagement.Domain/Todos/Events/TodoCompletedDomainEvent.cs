using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Domain.Todos.Events;

public sealed class TodoCompletedDomainEvent : IDomainEvent
{
    public TodoId TodoId { get; }
    public Guid UserId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public TodoCompletedDomainEvent(TodoId todoId, Guid userId)
    {
        TodoId = todoId;
        UserId = userId;
    }
}
