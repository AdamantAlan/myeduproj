using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoLearn.Entity;

public class Person
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("Login")]
    public string? Name { get; set; }
    [BsonIgnoreIfDefault]
    public int Age { get; set; }
    public Company? Company { get; set; }
    public List<string>? Languages { get; set; } = new List<string>();
}