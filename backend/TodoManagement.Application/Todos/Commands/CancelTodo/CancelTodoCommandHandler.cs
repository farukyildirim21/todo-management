using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Domain.Todos;

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
        var todoId = new TodoId(command.todoId); // doğru yer

        var todo = await _repository.GetByIdAsync(todoId);
        if (todo is null)
            throw new Exception("Todo not found.");

        todo.Cancel();

        await _repository.UpdateAsync(todo);

        await _eventDispatcher.DispatchAsync(todo.DomainEvents);
        todo.ClearDomainEvents();
    }
}
