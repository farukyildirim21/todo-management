using MongoDB.Driver;
using TodoManagement.Application.Abstractions.Caching;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Application.Todos.Queries.GetTodoDetail;
using TodoManagement.Application.Todos.Queries.GetUserTodos;
using TodoManagement.Infrastructure.Caching;
using TodoManagement.Infrastructure.Persistence.Documents;

namespace TodoManagement.Infrastructure.Persistence;


public sealed class MongoTodoReadRepository : ITodoReadRepository
{
    private readonly IMongoCollection<TodoDocument> _collection;
    private readonly ICacheService _cache;

    public MongoTodoReadRepository(
        IMongoDatabase database,
        ICacheService cache)
    {
        _collection = database.GetCollection<TodoDocument>("Todos");
        _cache = cache;
    }

    // Tekil Todo detayı
    public async Task<GetTodoDetailResult?> GetTodoDetailAsync(Guid todoId)
    {
        var cacheKey = CacheKeys.TodoById(todoId);

        // 1Cache
        var cached = await _cache.GetAsync<GetTodoDetailResult>(cacheKey);
        if (cached != null)
            return cached;

        // Mongo
        var doc = await _collection
            .Find(x => x.Id == todoId)
            .FirstOrDefaultAsync();

        if (doc is null)
            return null;

        //3Mapping → Application Read Model
        var result = new GetTodoDetailResult
        {
            Id = doc.Id,
            Title = doc.Title,
            Status = doc.Status
        };

        // Cache write
        await _cache.SetAsync(
            cacheKey,
            result,
            TimeSpan.FromMinutes(5)
        );

        return result;
    }

    // Kullanıcıya ait todo listesi
    public async Task<IReadOnlyList<GetUserTodosResult>> GetUserTodosAsync(Guid userId)
    {
        var cacheKey = CacheKeys.TodosByUser(userId);

        // Cache
        var cached = await _cache.GetAsync<IReadOnlyList<GetUserTodosResult>>(cacheKey);
        if (cached != null){

Console.WriteLine("🟢 TODOS FROM REDIS");
            return cached;
}

Console.WriteLine("🟡 TODOS FROM MONGO");

        // Mongo
        var docs = await _collection
            .Find(x => x.UserId == userId)
            .ToListAsync();

        // Mapping → Application Read Model
        var items = docs
            .Select(x => new GetUserTodosResult
            {
                Id = x.Id,
                Title = x.Title,
                Status = x.Status
            })
            .ToList();

        // Cache write
        await _cache.SetAsync(
            cacheKey,
            items,
            TimeSpan.FromMinutes(5)
        );

        return items;
    }
}
