using Elastic.Clients.Elasticsearch;
using ElasticSearchLearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly string _index;

    public UserController(ElasticsearchClient client)
    {
        _client = client;
        _index = "usertest";
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateIndexStore()
    {
        try
        {
            var result = await _client.Indices.CreateAsync(_index, c =>
                c.Mappings(m => m
                        .Properties<User>(p => p
                            .Keyword(k => k.Name)
                            .IntegerNumber(n => n.Age)
                        ))
                    .Settings(s => s.NumberOfShards(1).NumberOfReplicas(2)));
            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Index);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(User user)
    {
        try
        {
            var result = await _client.IndexAsync(user, s => s.Index(_index));
            if (!result.IsValidResponse)
                throw new Exception("error");
            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _client.SearchAsync<User>(_index);
            if (!result.IsValidResponse)
                throw new Exception("error");
            return Ok(result.Hits.Select(h => new {id = h.Id, doc = h.Source}).FirstOrDefault());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _client.DeleteAsync<User>(id, s => s.Index(_index));
            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> Update(string id)
    {
        try
        {
            var result = await _client.UpdateAsync<User, User>(id, u =>
                u.Index(_index).Doc(new User() { Name = "test", Age = 18 }).Refresh(Refresh.True));

            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}