using System.Diagnostics;

namespace Service.B.Telemetry
{
    public class WorkerActivitySource
    {
        public static readonly string Name = nameof(WorkerActivitySource);

        public static readonly ActivitySource ActivitySource = new ActivitySource(Name);
    }
}
