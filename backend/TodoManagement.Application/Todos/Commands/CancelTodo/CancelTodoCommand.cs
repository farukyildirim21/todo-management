using TodoManagement.Domain.Todos;

namespace TodoManagement.Application.Todos.Commands.CancelTodo;

public sealed record CancelTodoCommand(TodoId TodoId);