using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;

namespace TodoManagement.Application.Todos.Commands.CancelTodo;

public sealed class CancelTodoCommandHandler
{
    private readonly ITodoRepository _repository;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public CancelTodoCommandHandler(
        ITodoRepository repository,
        IDomainEventDispatcher eventDispatcher)
    {
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(CancelTodoCommand command)
    {
        var todo = await _repository.GetByIdAsync(command.TodoId);

        if (todo is null)
            throw new Exception("Todo not found.");

        todo.Cancel();

        await _repository.UpdateAsync(todo);

        await _eventDispatcher.DispatchAsync(todo.DomainEvents);
        todo.ClearDomainEvents();
    }
}