using MongoDB.Bson;

namespace MongoLearn.Entity;

public class Student
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public Course? Course { get; set; }
}
