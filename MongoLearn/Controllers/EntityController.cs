using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoLearn.Entity;

namespace MongoLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EntityController : ControllerBase
{
    private readonly IMongoCollection<Person> _collection;
    private readonly FilterDefinitionBuilder<Person> _filter;

    public EntityController(MongoClient client)
    {
        var db = client.GetDatabase("entity");
        _collection = db.GetCollection<Person>("withattribute");
        _filter = Builders<Person>.Filter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            using var cursor = await _collection.FindAsync("{}");
            return Ok(cursor.ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCount()
    {
        try
        {
            return Ok(await _collection.CountDocumentsAsync("{}"));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetForAge()
    {
        try
        {
            var filter = _filter.Gt("Age", 18);
            using var coursor = await _collection.FindAsync(filter);
            var result = coursor.ToList();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetForAgeAndName(string name, int age)
    {
        try
        {
            var filter = _filter.And(_filter.Gt("Age",age), _filter.Eq("Name", name));
            using var coursor = await _collection.FindAsync(filter);
            var result = coursor.ToList();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertOne(Person person)
    {
        person.Id = ObjectId.GenerateNewId();
        try
        {
            await _collection.InsertOneAsync(person);
        }
        catch (Exception e)
        {
            return BadRequest((person.Id, e.Message));
        }

        return Ok(person.Id);
    }

    [HttpPost]
    public async Task<IActionResult> InsertMany(IEnumerable<Person> persons)
    {
        foreach (var p in persons)
            p.Id = ObjectId.GenerateNewId();

        try
        {
            await _collection.InsertManyAsync(persons);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(persons.Select(p => p.Id));
    }
}