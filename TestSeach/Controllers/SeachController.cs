using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        Stopwatch sw = new Stopwatch();

        #region Search
        /// <summary>
        ///  Получение ответа от поисковых систем
        /// </summary>
        /// <remarks>
        /// Имитация работы поисковых систем
        /// </remarks>
        /// <returns>Json "название_системы:ответ"</returns>
        /// <response code="200">ответ ОК</response>
        /// <response code="400">ответ ERROR</response>
        [HttpGet]
        public async Task<ActionResult> Search()
        {
            sw.Start();
            await MoreSearchAsync();
            var res = Telemetry.GetResponseMetrics.ToArray(); 
            Telemetry.DeleteTeResponseTelemetry();
            sw.Stop();
            return Ok(res);
        }

        /// <summary>
        /// Получение ответа 
        /// </summary>
        [NonAction]
        private async Task MoreSearchAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var t = cts.Token;

            cts.CancelAfter(TimeSpan.FromMilliseconds(4800));
            var a = (HttpContext.RequestServices.GetService(typeof(ExternalA)) as AbsExternal).RequestAsync(t);
            var b = (HttpContext.RequestServices.GetService(typeof(ExternalB)) as AbsExternal).RequestAsync(t);
            var c = (HttpContext.RequestServices.GetService(typeof(ExternalC)) as AbsExternal).RequestAsync(t);

            await Task.WhenAll(a, b, c);

            if (c.Result?.StatusCode == 200)
            {
               var d = await (HttpContext.RequestServices.GetService(typeof(ExternalD)) as AbsExternal).RequestAsync(t);
                if (d != null)
                    Telemetry.SetResponseTelemetry(nameof(ExternalD), d.StatusCode);
            }
            if (a.Status == TaskStatus.RanToCompletion && a.Result != null)
                Telemetry.SetResponseTelemetry(nameof(ExternalA), a.Result.StatusCode);

            if (b.Status == TaskStatus.RanToCompletion && b.Result != null)
                Telemetry.SetResponseTelemetry(nameof(ExternalB), b.Result.StatusCode);

            if (c.Status == TaskStatus.RanToCompletion && c.Result != null)
                Telemetry.SetResponseTelemetry(nameof(ExternalC), c.Result.StatusCode);
        }
        #endregion

        #region Metrics
        /// <summary>
        ///  Получение репорта с пингом
        /// </summary>
        /// <remarks>
        /// Имитация работы поисковых систем
        /// </remarks>
        /// <returns>json "название_системы:пинг"</returns>
        /// <response code="200">ответ - список систем и их пинг</response>
        [HttpGet("Metrics")]
        public ActionResult Metrics() {
            return Ok(Telemetry.GetPingMetrics());
        }
        #endregion

    }
}

