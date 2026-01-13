using TodoManagement.Application.Abstractions.Caching;
using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Domain.Todos.Events;
using TodoManagement.Infrastructure.Caching;

namespace TodoManagement.Infrastructure.Events.Handlers;

public sealed class TodoCompletedCacheInvalidationHandler
    : IDomainEventHandler<TodoCompletedDomainEvent>
{
    private readonly ICacheService _cache;

    public TodoCompletedCacheInvalidationHandler(ICacheService cache)
    {
        _cache = cache;
    }

    public async Task Handle(TodoCompletedDomainEvent notification)
    {
        await _cache.RemoveAsync(
            CacheKeys.TodoById(notification.TodoId.Value));

        await _cache.RemoveAsync(
            CacheKeys.TodosByUser(notification.UserId));
    }
}
