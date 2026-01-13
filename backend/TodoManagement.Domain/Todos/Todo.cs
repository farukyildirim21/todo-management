using TodoManagement.Domain.Abstractions;
using TodoManagement.Domain.Exceptions;
using TodoManagement.Domain.Todos.Events;

namespace TodoManagement.Domain.Todos;
// sealed ile Todo extend edilemez.
// ama obje üretilebilir
public sealed class Todo : AggregateRoot
{
    public TodoId Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = default!;
    public TodoStatus Status { get; private set; }

   private Todo(
    TodoId id,
    Guid userId,
    string title,
    TodoStatus status) {
        Id = id;
    UserId = userId;
    Title = title;
    Status = status;


     } 

    private Todo(TodoId id, Guid userId, string title)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Status = TodoStatus.Created;

        AddDomainEvent(new TodoCreatedDomainEvent(Id, UserId, Title));
    }
    // ToDo objecesi create üzerinden oluşturulur. 
    public static Todo Create(Guid userId, string title)
    {
        if (userId == Guid.Empty)
            throw new DomainException("UserId cannot be empty.");

        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty.");

        var id = TodoId.New();
        return new Todo(id, userId, title.Trim());
    }

    public void Complete()
    {
        if (Status == TodoStatus.Completed)
            return;

        if (Status == TodoStatus.Cancelled)
            throw new DomainException("Cancelled todo cannot be completed.");

        Status = TodoStatus.Completed;
        AddDomainEvent(new TodoCompletedDomainEvent(Id, UserId));
    }

    public void Cancel()
    {
        if (Status == TodoStatus.Cancelled)
            return;

        if (Status == TodoStatus.Completed)
            throw new DomainException("Completed todo cannot be cancelled.");

        Status = TodoStatus.Cancelled;
        AddDomainEvent(new TodoCancelledDomainEvent(Id, UserId));
    }
    public static Todo Rehydrate(
    TodoId id,
    Guid userId,
    string title,
    TodoStatus status)
{
    return new Todo(id, userId, title, status);
}

}