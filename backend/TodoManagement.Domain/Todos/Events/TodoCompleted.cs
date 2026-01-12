using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Domain.Todos.Events;

public sealed class TodoCompleted : IDomainEvent
{
    public TodoId TodoId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public TodoCompleted(TodoId todoId)
    {
        TodoId = todoId;
    }
}