using Microsoft.AspNetCore.Mvc;

namespace Prometheus.Grafana.Edu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsDemoController : ControllerBase
    {
        private readonly IMetricsService _metrics;

        public MetricsDemoController(IMetricsService metrics)
        {
            _metrics = metrics;
        }

        // Counter: инкремент
        [HttpPost("counter")]
        public IActionResult IncrementCounter()
        {
            _metrics.IncrementRequests();
            return Ok("Counter incremented");
        }

        // Gauge: установка значения
        [HttpPost("gauge/{value:int}")]
        public IActionResult SetGauge(int value)
        {
            _metrics.SetActiveUsers(value);
            return Ok($"Gauge set to {value}");
        }

        // Histogram: наблюдение значения
        [HttpPost("histogram/{seconds:double}")]
        public IActionResult ObserveHistogram(double seconds)
        {
            _metrics.ObserveRequestDuration(seconds);
            return Ok($"Histogram observed: {seconds}s");
        }
    }
}
