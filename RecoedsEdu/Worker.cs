using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecoedsEdu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecoedsEdu
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                User2 u = new() {OldYears=123, Role = new() {Name="asd" } };
                User2 uu = u;

                uu.OldYears = 12;
                uu.Role.Name = "tyu";


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                User p1 = new User("Jon", 1) { Role = new Role { Name = "hjkg" } };
                User p2 = p1;

                _logger.LogInformation($"p1 == p2 ? {p1 == p2}");
                _logger.LogInformation($"p1 ea p2 ? {p1.Equals(p2)}");

                User p3 = p1 with { Role = new() { Name = p1.Role.Name } };
                p3.Role.Name = "tuy";

                _logger.LogInformation($"p1 to string ? {p1.ToString()}");
                p1.Deconstruct(out string Name, out int Id);
                _logger.LogInformation($"p1 Deconstr ? {Name}, {Id}");
                await Task.Delay(1000, stoppingToken);
                break;
            }
        }
    }
}
