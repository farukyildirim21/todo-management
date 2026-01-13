using TodoManagement.Domain.Todos;

namespace TodoManagement.Application.Todos.Commands.CancelTodo;

public sealed record CancelTodoCommand(Guid todoId);