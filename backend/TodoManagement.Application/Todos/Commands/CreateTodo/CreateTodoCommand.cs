namespace TodoManagement.Application.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand(Guid UserId, string Title);