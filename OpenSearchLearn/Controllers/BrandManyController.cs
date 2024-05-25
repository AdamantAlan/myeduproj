using Microsoft.AspNetCore.Mvc;
using OpenSearch.Client;
using OpenSearchLearn.Domain;

namespace OpenSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BrandManyController : ControllerBase
{
    private readonly OpenSearchClient _searchClient;
    private readonly string _index;

    public BrandManyController(OpenSearchClient searchClient)
    {
        _searchClient = searchClient;
        _index = "brands";
    }

    [HttpPost]
    public async Task<ActionResult> ManyIndex(Brand[] docs)
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
            var result = await _searchClient.SearchAsync<Brand>(s => s.Index(_index));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMany(List<BaseDomain> docs)
    {
        try
        {
            var result = await _searchClient.DeleteManyAsync(docs, _index);
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Items.Count);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteByQuery(string query)
    {
        try
        {
            var result = await _searchClient.DeleteByQueryAsync<Brand>(s => s.Index(_index)
                .Query(q => q
                    .QueryString(qs => qs.Query(query))));
            
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Deleted);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateMany(string field, string value, string query)
    {
        try
        {
            var result = await _searchClient.UpdateByQueryAsync<Brand>(u => u
                .Index(_index)
                .Query(q => q
                    .QueryString(qs => qs.Query(query)))
                .Script(s =>
                    s.Source($"ctx._source.{field} = params.value")
                        .Params(p => p.Add("value", value))));
            
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Updated);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}