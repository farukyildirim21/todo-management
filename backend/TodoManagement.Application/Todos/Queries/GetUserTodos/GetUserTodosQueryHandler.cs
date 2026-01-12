using TodoManagement.Application.Abstractions.Persistence;

namespace TodoManagement.Application.Todos.Queries.GetUserTodos;

public sealed class GetUserTodosQueryHandler
{
    private readonly ITodoReadRepository _repository;

    public GetUserTodosQueryHandler(ITodoReadRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<UserTodoItem>> Handle(GetUserTodosQuery query)
    {
        return _repository.GetUserTodosAsync(query.UserId);
    }
}