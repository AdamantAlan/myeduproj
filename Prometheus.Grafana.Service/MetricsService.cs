namespace Prometheus.Grafana.Edu
{
    public interface IMetricsService
    {
        void IncrementRequests();
        void SetActiveUsers(int value);
        void ObserveRequestDuration(double seconds);
    }

    public class MetricsService : IMetricsService
    {
        private static readonly Counter RequestCounter = Metrics
            .CreateCounter("app_requests_total", "Количество запросов к сервису");

        private static readonly Gauge ActiveUsersGauge = Metrics
            .CreateGauge("app_active_users", "Текущее количество активных пользователей");

        private static readonly Histogram RequestDurationHistogram = Metrics
            .CreateHistogram("app_request_duration_seconds", "Длительность выполнения запросов",
                new HistogramConfiguration
                {
                    Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.1, count: 10)
                });

        public void IncrementRequests()
        {
            RequestCounter.Inc();
        }

        public void SetActiveUsers(int value)
        {
            ActiveUsersGauge.Set(value);
        }

        public void ObserveRequestDuration(double seconds)
        {
            RequestDurationHistogram.Observe(seconds);
        }
    }
}
