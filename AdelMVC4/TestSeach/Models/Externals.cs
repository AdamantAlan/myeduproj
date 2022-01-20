using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestSeach.Models
{
    /// <summary>
    /// Интерфейс для внешних поисковых систем
    /// </summary>
    abstract class AbsExternal
    {
        protected Random random=new Random();
        /// <summary>
        /// Запрос во внешнюю поисковую систему
        /// </summary>
        /// <returns>Статус код выполнился или не выполнился запрос(имитация)</returns>
        internal virtual async Task<StatusCodeResult> RequestAsync()
        {
            int workTime = random.Next(1, 10);
            await Task.Delay(workTime);
            var response = random.Next(2);
            Telemetry.SetPingTelemetry(this.GetType().Name,workTime);
            Telemetry.SetResponseTelemetry(this.GetType().Name, response);
            return response == 1 ? new StatusCodeResult(200) : response == 0 ? new StatusCodeResult(503) : null;
        }
    }
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
}
