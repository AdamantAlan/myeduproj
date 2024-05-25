using Microsoft.AspNetCore.Mvc;
using OpenSearch.Client;
using OpenSearch.Net;
using OpenSearchLearn.Domain;

namespace OpenSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DocumentController : ControllerBase
{
    private readonly OpenSearchClient _searchClient;

    public DocumentController(OpenSearchClient searchClient)
    {
        _searchClient = searchClient;
    }

    [HttpPost]
    public async Task<ActionResult> Index(Document doc, string index)
    {
        try
        {
            var result = await _searchClient.IndexAsync(doc, i => i.Index(index));
            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Get(string index, int id)
    {
        try
        {
            var result = await _searchClient.GetAsync<Document>(id, d => d.Index(index));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Source);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(string index, int id)
    {
        try
        {
            var result = await _searchClient.DeleteAsync<Brand>(id, s => s.Index(index));
            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Update(string index, int id)
    {
        try
        {
            var result = await _searchClient.UpdateAsync<Document>(id, u =>
                u.Index(index).Doc(new Document { Department = "test", Description = "test" }).Refresh(Refresh.True));

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");

            return Ok(result.Result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}