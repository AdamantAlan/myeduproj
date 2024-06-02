using Elastic.Clients.Elasticsearch;
using ElasticSearchLearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchLearn.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SearchController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly string _index;

    public SearchController(ElasticsearchClient client)
    {
        _client = client;
        _index = "usertest";
    }

    [HttpGet]
    public async Task<IActionResult> ForStringFuzzing(string desc)
    {
        try
        {
            var result = await _client.SearchAsync<User>(s => s
                .Index(_index)
                .Query(q => q
                    .Fuzzy(f => f
                        .Field(fl => fl.Name)
                        .Value(desc)
                        .Fuzziness(new Fuzziness(2))
                        .MaxExpansions(3)))
            );

            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GeneralSearchRequest(string name, double age)
    {
        try
        {
            var result = await _client.SearchAsync<User>(s => s
                .Index(_index)
                .Query(q => q
                    .Bool(b => b
                        .Filter(f => f.Fuzzy(f => f
                            .Field(fl => fl.Name)
                            .Value(name)
                            .Fuzziness(new Fuzziness(5))
                            .MaxExpansions(1)),
                            f => f.Range(r => r
                                .NumberRange(f => f.Field(f => f.Age).Gte(age))
                            ))))
            );

            if (!result.IsValidResponse)
                throw new Exception("Getting documents has errors");
            return Ok(result.Documents);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}