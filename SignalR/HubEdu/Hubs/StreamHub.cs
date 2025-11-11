using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace HubEdu.Hubs
{
    public class StreamHub : Hub
    {
        public async IAsyncEnumerable<int> Counter(int count, int delay, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}
