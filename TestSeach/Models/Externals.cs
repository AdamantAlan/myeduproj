using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static TestSeach.Models.Telemetry;

namespace TestSeach.Models
{
    /// <summary>
    /// Интерфейс для внешних поисковых систем
    /// </summary>
    abstract class AbsExternal
    {
        /// <summary>
        /// Запрос во внешнюю поисковую систему
        /// </summary>
        /// <returns>Статус код выполнился или не выполнился запрос(имитация)</returns>
        internal virtual async Task<StatusCodeResult> RequestAsync(CancellationToken t)
        {
            double ping = 0;
            int workTime = new Random().Next(1, 100);
            for (int i = 0; i < workTime; i++)
            {
                await Task.Delay(100);
                ping += 100;
                if (t.IsCancellationRequested)
                    return null;
            }
            var response = new Random().Next(2);
            if (ping >= 4.5)
                ping = 4;
            Telemetry.SetPingTelemetry(new PingM { Name = this.GetType().Name, Ping = (int)Math.Round(ping/1000)}) ;
            return response == 1 ? new StatusCodeResult(200) : response == 0 ? new StatusCodeResult(503) : null;
        }
    }
    #region search systems
    /// <summary>
    /// Поисковая система A
    /// </summary>
    class ExternalA: AbsExternal
    {

    }
    /// <summary>
    /// Поисковая система B
    /// </summary>
    class ExternalB : AbsExternal
    {

    }
    /// <summary>
    /// Поисковая система C
    /// </summary>
    class ExternalC : AbsExternal
    {

    }
    /// <summary>
    /// Поисковая система D
    /// </summary>
    class ExternalD : AbsExternal
    {
       
    }
    #endregion
}
