using Microsoft.AspNetCore.Mvc;
using OpenSearch.Client;
using OpenSearchLearn.Domain;

namespace OpenSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SearchController : ControllerBase
{
    private readonly OpenSearchClient _searchClient;
    private readonly string _index;

    public SearchController(OpenSearchClient searchClient)
    {
        _searchClient = searchClient;
        _index = "stories";
    }

    [HttpGet]
    public async Task<IActionResult> ForString(string name)
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s
                .Index(_index)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(name)
                        .Analyzer("standard")))
            );

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> ForStringFuzzing(string desc)
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s
                .Index(_index)
                .Query(q => q
                    .Fuzzy(f => f
                        .Field(fl => fl.Description)
                        .Value(desc)
                        .Fuzziness(Fuzziness.Auto)
                        .MaxExpansions(3)))
            );

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GreaterOrEqualNumber(double count)
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s
                .Index(_index)
                .Query(q => q
                    .Range(r => r
                        .Field(f => f.Count)
                        .GreaterThanOrEquals(count)))
            );

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GreaterOrEqualDate(DateTime date)
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s
                .Index(_index)
                .Query(q => q
                    .DateRange(d => d
                        .Field(f => f.StartDate)
                        .GreaterThanOrEquals(date)))
            );

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GeneralSearchRequest(string name, double count, DateTime date)
    {
        try
        {
            var result = await _searchClient.SearchAsync<Store>(s => s
                    .Index(_index)
                    .Query(q => q
                        .Bool(b => b
                            .Filter(f => f
                                    .Match(m => m
                                        .Field(f => f.Name)
                                        .Query(name)
                                        .Analyzer("standard")),
                                f => f.Range(r => r
                                    .Field(f => f.Count)
                                    .GreaterThanOrEquals(count)),
                                f => f
                                    .DateRange(dr => dr
                                        .Field(f => f.StartDate)
                                        .GreaterThanOrEquals(date)))))
                );

            if (!result.IsValid)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}