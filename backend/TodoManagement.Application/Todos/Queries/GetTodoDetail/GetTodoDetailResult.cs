namespace TodoManagement.Application.Todos.Queries.GetTodoDetail;

public sealed class GetTodoDetailResult
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Status { get; init; } = default!;
}