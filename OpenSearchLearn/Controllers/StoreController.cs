using Microsoft.AspNetCore.Mvc;
using OpenSearch.Client;
using OpenSearchLearn.Domain;

namespace OpenSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class StoreController : ControllerBase
{
    private readonly OpenSearchClient _searchClient;
    private readonly string _index;

    public StoreController(OpenSearchClient searchClient)
    {
        _searchClient = searchClient;
        _index = "stories";
    }

    [HttpPost]
    public async Task<ActionResult> CreateIndexStore()
    {
        try
        {
            var result = await _searchClient.Indices.CreateAsync(_index, c =>
                c.Map(m => m.AutoMap<Store>()
                        .Properties<Store>(p => p
                            .Keyword(k => k.Name(f => f.Name))
                            .Keyword(k => k.Name(f => f.Description))
                            .Number(n => n.Name(f => f.Count))
                        ))
                    .Settings(s => s.NumberOfShards(1).NumberOfReplicas(2)));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Index);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteIndexStore()
    {
        try
        {
            var result = await _searchClient.Indices.DeleteAsync(_index);
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.IsValid);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> ManyIndex(Store[] docs)
    {
        try
        {
            var result = await _searchClient.IndexManyAsync(docs, _index);
            return Ok(result.Items.Count);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s.Index(_index));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> BulkRequest(Store[] docs)
    {
        try
        {
            var result = await _searchClient.BulkAsync(s => s.Index(_index).IndexMany(docs));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Items.Count);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Aggregation()
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s.Index(_index)
                .Size(0)
                .Aggregations(a => a
                    .Average("avg", avg => avg.Field(f => f.Count))));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Aggregations.GetValueOrDefault("avg"));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}