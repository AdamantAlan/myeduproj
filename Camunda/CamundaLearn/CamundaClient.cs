using System.Net.Http.Headers;
using CamundaLearn.Controllers;

namespace CamundaLearn;

public class CamundaClient
{
    private readonly HttpClient _httpClient;

    public CamundaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<DeploymentResponse> DeployProcessAsync(string bpmnFilePath, string deploymentName)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(bpmnFilePath));
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
        content.Add(fileContent, "data", System.IO.Path.GetFileName(bpmnFilePath));
        content.Add(new StringContent(deploymentName), "deployment-name");

        var response = await _httpClient.PostAsync("/engine-rest/deployment/create", content);
        response.EnsureSuccessStatusCode();

        var deploymentResponse = await response.Content.ReadFromJsonAsync<DeploymentResponse>();
        return deploymentResponse;
    }

    public async Task<Deployment> GetDeploymentIdAsync()
    {
        var response = await _httpClient.GetAsync("/engine-rest/deployment");
        response.EnsureSuccessStatusCode();
        var content1 = await response.Content.ReadAsStringAsync();
        //var content = await response.Content.ReadFromJsonAsync<Deployment>();
        return new Deployment();
    }
    
    public async Task<string> DeleteDeploymentCascadeAsync(string deploymentId)
    {
        var response = await _httpClient.DeleteAsync($"/engine-rest/deployment/{deploymentId}?cascade=true");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
    
    public async Task<string> GetProcessDefinitionsAsync()
    {
        var response = await _httpClient.GetAsync("/engine-rest/process-definition");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
    
    public async Task<List<ProcessInstance>> GetProcessInstancesAsync(string processDefinitionKey)
    {
        var response = await _httpClient.GetAsync($"/engine-rest/process-instance?processDefinitionKey={processDefinitionKey}");
        response.EnsureSuccessStatusCode();

        var processInstances = await response.Content.ReadFromJsonAsync<List<ProcessInstance>>();
        return processInstances;
    }
    
    public async Task DeleteProcessInstanceAsync(string processInstanceId)
    {
        var response = await _httpClient.DeleteAsync($"/engine-rest/process-instance/{processInstanceId}");
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<List<ProcessInstance>> StartProcessInstanceAsync(string processDefinitionKey, string businessKey, Dictionary<string, VariableValue> variables)
    {
        var request = new StartProcessInstanceRequest
        {
            BusinessKey = businessKey,
            Variables = variables
        };

        var response = await _httpClient.PostAsJsonAsync($"/engine-rest/process-definition/key/{processDefinitionKey}/start", request);
        response.EnsureSuccessStatusCode();

        var processInstanceResponse = await response.Content.ReadFromJsonAsync<ProcessInstanceResponse>();
        var info = await GetProcessInstancesAsync(processDefinitionKey);
        return info;
    }
}
