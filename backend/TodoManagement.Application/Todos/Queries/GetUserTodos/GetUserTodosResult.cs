namespace TodoManagement.Application.Todos.Queries.GetUserTodos;

public sealed class UserTodoItem
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Status { get; init; } = default!;
}