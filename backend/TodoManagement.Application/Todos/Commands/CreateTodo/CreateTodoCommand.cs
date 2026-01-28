namespace TodoManagement.Application.Todos.Commands.CreateTodo;
//adece veri taşımak için kullanılan tip
//sealed extend edilmeyen.
//structure aslında şuna eşitttir:
/*
public sealed class CreateTodoCommand
{
    public Guid UserId { get; init; }
    public string Title { get; init; }

    public CreateTodoCommand(Guid userId, string title)
    {
        UserId = userId;
        Title = title;
    }
}*/

public sealed record CreateTodoCommand(Guid UserId, string Title);