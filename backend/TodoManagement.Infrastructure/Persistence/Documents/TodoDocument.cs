using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoManagement.Infrastructure.Persistence.Documents;

public sealed class TodoDocument
{
    //  [BsonId] = Bu property MongoDB document’inin _id alanıdır 
    // [BsonRepresentation(BsonType.String)]= Bu Guid’i MongoDB’de string olarak sakla.
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = default!;

    [BsonElement("status")]
    public string Status { get; set; } = default!;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
