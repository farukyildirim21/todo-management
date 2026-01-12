using TodoManagement.Domain.Abstractions;

namespace TodoManagement.Domain.Todos.Events;

public sealed class TodoCreated : IDomainEvent
{
    public TodoId TodoId { get; }
    public Guid UserId { get; }
    public string Title { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public TodoCreated(TodoId todoId, Guid userId, string title)
    {
        TodoId = todoId;
        UserId = userId;
        Title = title;
    }
}