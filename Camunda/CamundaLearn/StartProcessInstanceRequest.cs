namespace CamundaLearn;

public class StartProcessInstanceRequest
{
    public string BusinessKey { get; set; }
    public Dictionary<string, VariableValue> Variables { get; set; }
}