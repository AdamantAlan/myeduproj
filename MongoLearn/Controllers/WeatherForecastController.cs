using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MongoClient _client;
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<BsonDocument> _collection;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, MongoClient client)
    {
        _logger = logger;
        _client = client;
        _db = _client.GetDatabase("mytestdb");
        _collection = _db.GetCollection<BsonDocument>("tdata");
        ;
    }

    [HttpPost]
    public async Task<IActionResult> Insert()
    {
        for (int i = 0; i < 10000; i++)
        {
            await _collection.InsertManyAsync(new List<BsonDocument>()
            {
                new() { { "Name", "Dima" }, { "Age", 26 }, { "occupation", "dev" } },
                new() { { "Name", "Delya" }, { "Age", 24 }, { "occupation", "writter" } },
                new() { { "Name", "Dima" }, { "Age", 26 }, { "occupation", "dev" } },
                new() { { "Name", "Delya" }, { "Age", 24 }, { "occupation", "writter" } },
                new() { { "Name", "Dima" }, { "Age", 26 }, { "occupation", "dev" } },
                new() { { "Name", "Delya" }, { "Age", 24 }, { "occupation", "writter" } },
                new() { { "Name", "Dima" }, { "Age", 26 }, { "occupation", "dev" } },
                new() { { "Name", "Delya" }, { "Age", 24 }, { "occupation", "writter" } },
                new() { { "Name", "Dima" }, { "Age", 26 }, { "occupation", "dev" } },
                new() { { "Name", "Delya" }, { "Age", 24 }, { "occupation", "writter" } }
            });
        }

        //var couple = await (await _collection.FindAsync("{}")).ToListAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<string> Get()
    {
        var tdatas = await _collection.Find("{}").ToListAsync();
        return tdatas.ToJson();
    }

    [HttpGet(Name = "{id}")]
    public async Task<string> GetFor(string id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
        var tdatas = await _collection.Find(filter).ToListAsync();
        return tdatas.ToJson();
    }

    [HttpPut]
    public async Task<string> UpdateFor(string id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
        var updateCommand = Builders<BsonDocument>.Update
            .Set("Name", "Sardelka");

        var result = await _collection.UpdateOneAsync(filter, updateCommand);
        return result.MatchedCount.ToJson();
    }

    [HttpPost]
    public async Task<string> ReplaceFor([FromBody] string id, string name, int age, string occupation)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
        var result = await _collection.ReplaceOneAsync(filter, new BsonDocument()
        {
            { "Name", name }, { "Age", age }//, { "occupation", occupation } 
        });
        
        return result.MatchedCount.ToJson();
    }
    
    [HttpDelete]
    public async Task<string> DeleteFor([FromBody] string id)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
        var result = await _collection.DeleteOneAsync(filter);
        
        return result.DeletedCount.ToJson();
    }
}