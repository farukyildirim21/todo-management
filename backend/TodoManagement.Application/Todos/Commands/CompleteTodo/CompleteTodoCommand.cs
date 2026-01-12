using TodoManagement.Domain.Todos;

namespace TodoManagement.Application.Todos.Commands.CompleteTodo;

public sealed record CompleteTodoCommand(TodoId TodoId);