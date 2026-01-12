using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;

namespace TodoManagement.Application.Todos.Commands.CompleteTodo;

public sealed class CompleteTodoCommandHandler
{
    private readonly ITodoRepository _repository;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public CompleteTodoCommandHandler(
        ITodoRepository repository,
        IDomainEventDispatcher eventDispatcher)
    {
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(CompleteTodoCommand command)
    {
        var todo = await _repository.GetByIdAsync(command.TodoId);

        if (todo is null)
            throw new Exception("Todo not found."); // sonra ApplicationException yaparız

        todo.Complete();

        await _repository.UpdateAsync(todo);

        await _eventDispatcher.DispatchAsync(todo.DomainEvents);
        todo.ClearDomainEvents();
    }
}