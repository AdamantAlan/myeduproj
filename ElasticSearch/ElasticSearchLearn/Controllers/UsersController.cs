using Elastic.Clients.Elasticsearch;
using ElasticSearchLearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly string _index;

    public UsersController(ElasticsearchClient client)
    {
        _client = client;
        _index = "usertest";
    }

    [HttpPost]
    public async Task<ActionResult> ManyIndex(User[] docs)
    {
        try
        {
            var result = await _client.IndexManyAsync(docs, _index);
            if (!result.IsValidResponse)
                throw new Exception("error");

            return Ok(result.Items.Count);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _client.SearchAsync<User>(s => s.Index(_index));
            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Hits.Select(h => new { id = h.Id, doc = h.Source }));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMany(string[] docs)
    {
        try
        {
            var result = await _client.DeleteByQueryAsync<User>(s => s.Indices(_index)
                .Query(q => q
                    .Ids(i => i.Values(docs))));
            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Deleted);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateMany(string field, string value, string[] ids)
    {
        try
        {
            var result = await _client.UpdateByQueryAsync<User>(u => u
                .Indices(_index)
                .Query(q => q
                    .Ids(i => i.Values(ids)))
                .Script(new Script(new InlineScript($"ctx._source.{field} = params.value") 
                    { Params = new Dictionary<string, object>() { ["value"] = value }})));

            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Updated);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Aggregation()
    {
        try
        {
            var result = await _client.SearchAsync<User>(s => s
                .Index(_index)
                .Size(0)
                .Aggregations(a => a
                    .Add("avg", s => s.Avg(f => f.Field(ff => ff.Age)))));

            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Aggregations.GetValueOrDefault("avg"));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> BulkRequest(User[] docs)
    {
        try
        {
            var result = await _client.BulkAsync(s => s.Index(_index).IndexMany(docs));
            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");

            return Ok(result.Items.Count);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}