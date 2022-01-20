using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TestSeach.Models;

namespace TestSeach.Controllers
{
    /// <summary>
    /// АПИ доступ к внешним поисковым системам
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("search")]
    public class SeachController : ControllerBase
    {
        /// <summary>
        /// Метод подключения к поисковым системам
        /// </summary>
        /// <returns>Статус код</returns>
        [HttpGet]
        public async Task<ActionResult> Search()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await MoreSearch();
            sw.Stop();
            var res = Telemetry.GetResponseMetrics.ToArray();
            
            Telemetry.DeleteTelemetry();
            return Ok(res);
        }
        [NonAction]
        private async Task MoreSearch()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

           // var logic= new Task(async () => {
                var t = cts.Token;

            var a =  (HttpContext.RequestServices.GetService(typeof(ExternalA)) as AbsExternal).RequestAsync();
                if (t.IsCancellationRequested)
                    t.ThrowIfCancellationRequested();

            var b =  (HttpContext.RequestServices.GetService(typeof(ExternalB)) as AbsExternal).RequestAsync();
                if (t.IsCancellationRequested)
                    t.ThrowIfCancellationRequested();

            var c = (HttpContext.RequestServices.GetService(typeof(ExternalC)) as AbsExternal).RequestAsync();
                if (t.IsCancellationRequested)
                    t.ThrowIfCancellationRequested();
          await Task.WhenAll(a, b, c);
                if (c.Status== TaskStatus.RanToCompletion && c.Result.StatusCode == 200)
                await (HttpContext.RequestServices.GetService(typeof(ExternalD)) as AbsExternal).RequestAsync();
        }
            //});

          //var timer= new Task(async () => {
          //     await Task.Delay(5000);
          //      if (logic.Status != TaskStatus.RanToCompletion)
          //          cts.Cancel();
          //  });
          //  await Task.WhenAny(logic,timer);
        }
    }

