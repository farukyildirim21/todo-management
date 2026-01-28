using TodoManagement.Application.Abstractions.Caching;
using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Domain.Todos.Events;
using TodoManagement.Infrastructure.Caching;

namespace TodoManagement.Infrastructure.Events.Handlers;

public sealed class TodoCreatedCacheInvalidationHandler
    : IDomainEventHandler<TodoCreatedDomainEvent>
{
    private readonly ICacheService _cache;

    public TodoCreatedCacheInvalidationHandler(ICacheService cache)
    {
        _cache = cache;
    }

    public async Task Handle(TodoCreatedDomainEvent domainEvent)
    {
        var userId = domainEvent.UserId;

        await _cache.RemoveAsync(
            CacheKeys.TodosByUser(userId)
        );
    }
}
