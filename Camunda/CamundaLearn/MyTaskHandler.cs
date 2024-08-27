using Camunda.Worker;

namespace CamundaLearn;

[HandlerTopics("myTopic")]
public class MyTaskHandler :  IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10000; i++)
        {
            Console.WriteLine("Camunda!");
        }
        return await Task.FromResult<IExecutionResult>(new CompleteResult());
    }
}