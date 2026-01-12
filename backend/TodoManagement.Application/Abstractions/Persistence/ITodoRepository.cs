using TodoManagement.Domain.Todos;

namespace TodoManagement.Application.Abstractions.Persistence;

public interface ITodoRepository
{
    Task<Todo?> GetByIdAsync(TodoId id);
    Task AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
}