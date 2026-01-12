using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Domain.Todos.Events;

public sealed class TodoCancelled : IDomainEvent
{
    public TodoId TodoId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public TodoCancelled(TodoId todoId)
    {
        TodoId = todoId;
    }
}