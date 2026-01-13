using Microsoft.Extensions.DependencyInjection;
using TodoManagement.Application.Todos.Commands.CreateTodo;
using TodoManagement.Application.Todos.Queries.GetTodoDetail;
using TodoManagement.Application.Todos.Queries.GetUserTodos;
using TodoManagement.Application.Todos.Commands.CompleteTodo;
using TodoManagement.Application.Todos.Commands.CancelTodo;


namespace TodoManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 🔹 Commands
        services.AddScoped<CreateTodoCommandHandler>();
        services.AddScoped<CompleteTodoCommandHandler>();
        services.AddScoped<CancelTodoCommandHandler>();

        // 🔹 Queries
        services.AddScoped<GetTodoDetailQueryHandler>();
        services.AddScoped<GetUserTodosQueryHandler>();

        return services;
    }
}
