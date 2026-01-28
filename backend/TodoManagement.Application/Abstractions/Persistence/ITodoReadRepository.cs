using TodoManagement.Application.Todos.Queries.GetTodoDetail;
using TodoManagement.Application.Todos.Queries.GetUserTodos;

namespace TodoManagement.Application.Abstractions.Persistence;

public interface ITodoReadRepository
{
    Task<GetTodoDetailResult?> GetTodoDetailAsync(Guid todoId);
    Task<IReadOnlyList<GetUserTodosResult>> GetUserTodosAsync(Guid userId);
}