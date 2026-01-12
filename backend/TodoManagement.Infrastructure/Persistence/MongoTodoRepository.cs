using MongoDB.Driver;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Domain.Todos;

namespace TodoManagement.Infrastructure.Persistence.Mongo;

public class MongoTodoRepository : ITodoRepository
{
    private readonly IMongoCollection<TodoDocument> _collection;

    public MongoTodoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TodoDocument>("Todos");
    }

    public async Task<Todo?> GetByIdAsync(TodoId id)
    {
        var doc = await _collection
            .Find(x => x.Id == id.Value)
            .FirstOrDefaultAsync();

        return doc == null ? null : MapToDomain(doc);
    }

    public async Task AddAsync(Todo todo)
    {
        await _collection.InsertOneAsync(MapToDocument(todo));
    }

    public async Task UpdateAsync(Todo todo)
    {
        var filter = Builders<TodoDocument>.Filter.Eq(x => x.Id, todo.Id.Value);

        var update = Builders<TodoDocument>.Update
            .Set(x => x.Status, todo.Status.ToString());

        await _collection.UpdateOneAsync(filter, update);
    }

    private static TodoDocument MapToDocument(Todo todo)
        => new()
        {
            Id = todo.Id.Value,
            UserId = todo.UserId,
            Title = todo.Title.Value,
            Status = todo.Status.ToString(),
            CreatedAt = todo.CreatedAt
        };
//🎯 REHYDRATE’İN AMACI TAM OLARAK Rehydrate = “Bu aggregate zaten vardı, ben sadece belleğe geri alıyorum.”
    private static Todo MapToDomain(TodoDocument doc)
        => Todo.Rehydrate(
            new TodoId(doc.Id),
            doc.UserId,
            doc.Title,
            doc.Status,
            doc.CreatedAt
        );
}
