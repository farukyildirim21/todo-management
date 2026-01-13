using TodoManagement.Application.Abstractions.Persistence;

namespace TodoManagement.Application.Todos.Queries.GetTodoDetail;

public sealed class GetTodoDetailQueryHandler
{
    private readonly ITodoReadRepository _repository;

    public GetTodoDetailQueryHandler(ITodoReadRepository repository)
    {
        _repository = repository;
    }

    public Task<GetTodoDetailResult?> Handle(GetTodoDetailQuery query)
    {
        return _repository.GetTodoDetailAsync(query.TodoId);
    }
}
