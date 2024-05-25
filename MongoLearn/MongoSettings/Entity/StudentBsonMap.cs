using MongoDB.Bson.Serialization;
using MongoLearn.Entity;

namespace MongoLearn.MongoSettings.Entity;

public class StudentBsonMap
{
    public static void RegisterBson()
    {
        BsonClassMap.RegisterClassMap<Student>(p =>
            {
                p.AutoMap();
                p.SetIdMember(p.GetMemberMap(pp => pp.Id));
                p.MapMember(p => p.Name)
                    .SetElementName("StudentName")
                    .SetIsRequired(true);
                p.MapMember(p => p.Course).SetIgnoreIfNull(true);
            });
    }
}