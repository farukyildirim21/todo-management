using MongoDB.Driver;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Domain.Todos;
using TodoManagement.Infrastructure.Persistence.Documents;

namespace TodoManagement.Infrastructure.Persistence;

public sealed class MongoTodoRepository : ITodoRepository
{
    private readonly IMongoCollection<TodoDocument> _collection;

    public MongoTodoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TodoDocument>("Todos");
    }
    //MongoDB’den Todo verisini alır, domain aggregate root’a çevirir ve Application Layer’a döner.
    public async Task<Todo?> GetByIdAsync(TodoId id)
    {
        var doc = await _collection
            .Find(x => x.Id == id.Value)
            .FirstOrDefaultAsync();

        if (doc is null)
            return null;

        return MapToDomain(doc);
    }
    //Yeni oluşturulmuş Todo aggregate’ını MongoDB’ye ilk kez yazar.
    public async Task AddAsync(Todo todo)
    {
        await _collection.InsertOneAsync(MapToDocument(todo));
    }
    //Rehydrate edilmiş ve değiştirilmiş Todo aggregate’ının yeni state’ini MongoDB’ye yazar.
    public async Task UpdateAsync(Todo todo)
    {
        var filter = Builders<TodoDocument>.Filter.Eq(x => x.Id, todo.Id.Value);

        var update = Builders<TodoDocument>.Update
            .Set(x => x.Status, todo.Status.ToString())
            .Set(x => x.Title, todo.Title);

        await _collection.UpdateOneAsync(filter, update);
    }
    //verileri documente ceviriyor
    private static TodoDocument MapToDocument(Todo todo)
        => new()
        {
            Id = todo.Id.Value,
            UserId = todo.UserId,
            Title = todo.Title,
            Status = todo.Status.ToString()
        };
    //mongodb verilerini domain ceviriyor. 
    private static Todo MapToDomain(TodoDocument doc)
        => Todo.Rehydrate(
            new TodoId(doc.Id),
            doc.UserId,
            doc.Title,
            Enum.Parse<TodoStatus>(doc.Status)
        );
}
