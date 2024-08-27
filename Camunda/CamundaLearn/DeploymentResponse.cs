namespace CamundaLearn;

public class DeploymentResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public DateTime DeploymentTime { get; set; }
    public string TenantId { get; set; }
}