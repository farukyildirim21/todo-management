using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Domain.Todos;

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
        var todoId = new TodoId(command.todoId); // ✅ kritik satır

        var todo = await _repository.GetByIdAsync(todoId);
        if (todo is null)
            throw new Exception("Todo not found.");

        todo.Complete();

        await _repository.UpdateAsync(todo);

        await _eventDispatcher.DispatchAsync(todo.DomainEvents);
        todo.ClearDomainEvents();
    }
}
