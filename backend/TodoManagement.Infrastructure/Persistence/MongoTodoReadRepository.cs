using MongoDB.Driver;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Application.Contracts.Todos;
using TodoManagement.Infrastructure.Caching;

namespace TodoManagement.Infrastructure.Persistence.Mongo;

public class MongoTodoReadRepository : ITodoReadRepository
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

    public async Task<TodoDetailDto?> GetTodoDetailAsync(Guid todoId)
    {
        var cacheKey = CacheKeys.TodoById(todoId);
        var cached = await _cache.GetAsync<TodoDetailDto>(cacheKey);
        if (cached != null) return cached;

        var doc = await _collection
            .Find(x => x.Id == todoId)
            .FirstOrDefaultAsync();

        if (doc == null) return null;
//dto: data transfer object: katmanlar veya sistemler arasında veri taşımak için kullanılan,davranış (business logic) içermeyen basit veri nesnesidir.
        var dto = new TodoDetailDto(
            doc.Id,
            doc.Title,
            doc.Status
        );

        await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(5));
        return dto;
    }
}
