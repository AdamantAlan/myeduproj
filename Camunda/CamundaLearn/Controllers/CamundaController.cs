using Microsoft.AspNetCore.Mvc;

namespace CamundaLearn.Controllers;

[Route("[controller]/[action]")]
public class CamundaController : ControllerBase
{
    private readonly CamundaClient _client;

    public CamundaController(CamundaClient client)
    {
        _client = client;
    }
    [HttpGet]
    public async Task<ActionResult> GetDeployment()
    {
        var processDefinitions = await _client.GetProcessDefinitionsAsync();
        return Ok(processDefinitions);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateDeployment(string pathToBpmn, string nameDeployment)
    {
        var deploymentResponse = await _client.DeployProcessAsync(pathToBpmn, nameDeployment);
        return Ok(deploymentResponse);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteProcess(string definitionKey)
    {
        if (definitionKey is not null)
        {
            await _client.DeleteDeploymentCascadeAsync(definitionKey);
            return Ok();
        }
        var depId = await _client.GetDeploymentIdAsync();
        await _client.DeleteDeploymentCascadeAsync(depId.Id);
        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult> StartProcess(string processDefinitionKey, string businessKey, string key, string value)
    {
        var deploymentResponse = await _client.StartProcessInstanceAsync(processDefinitionKey, businessKey,
            new Dictionary<string, VariableValue>() {[key] = new VariableValue(){Type = "string", Value = value}});
        return Ok(deploymentResponse);
    }
}