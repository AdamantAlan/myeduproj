
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace TestSeach.Models
{
    /// <summary>
    /// Класс сбора информации
    /// </summary>
    static class Telemetry
    {
        #region Ping
        /// <summary>
        /// model ping
        /// </summary>
        public class PingM
        {
            public string Name { get; set; }
            public int Ping { get; set; }
        }
        /// <summary>
        /// Метрики времени ответа
        /// </summary>
        private static List<PingM> MetricsPing = new List<PingM>();

        /// <summary>
        /// Сохраняет информацию о задержке
        /// </summary>
        /// <param name="p">ping</param>
        internal static void SetPingTelemetry(PingM p) => MetricsPing.Add(p);
        /// <summary>
        /// Получаем метрики
        /// </summary>
        internal static IEnumerable<PingM> GetPingMetrics() {
            var q = MetricsPing.OrderBy(x => x.Ping).ThenBy(x => x.Name);
            List<int> countA = new List<int>();
            List<int> countB = new List<int>();
            List<int> countC = new List<int>();
            List<int> countD = new List<int>();
           // Dictionary<int, IEnumerable<PingM>> res = new Dictionary<int, IEnumerable<PingM>>();
            List<(int count, IEnumerable<PingM>)> res = new List<(int count, IEnumerable<PingM>)>();
            for (int i = 1; i < 5; i++)
            {
                res.Add((q.Where(x => x.Ping == i && x.Name == nameof(ExternalA)).Count(), q.Where(x => x.Ping == i && x.Name == nameof(ExternalA))));
            }
            for (int i = 1; i < 5; i++)
            {
                res.Add((q.Where(x => x.Ping == i && x.Name == nameof(ExternalB)).Count(),q.Where(x => x.Ping == i && x.Name == nameof(ExternalB))));
            }

            for (int i = 1; i < 5; i++)
            {
                res.Add((q.Where(x => x.Ping == i && x.Name == nameof(ExternalC)).Count(), q.Where(x => x.Ping == i && x.Name == nameof(ExternalC))));
            }
          
            for (int i = 1; i < 5; i++)
            {
                res.Add((q.Where(x => x.Ping == i && x.Name == nameof(ExternalD)).Count(), q.Where(x => x.Ping == i && x.Name == nameof(ExternalD))));
            }
            foreach (var item in res )
            {

            }
            StringBuilder sb = new StringBuilder();

            return q;
        }
        #endregion

        #region Response
        /// <summary>
        /// метрики значения ответов
        /// </summary>
        private static Dictionary<string, int> MetricsResponse = new Dictionary<string, int>();
        /// <summary>
        /// Сохраняет информацию об ответе
        /// </summary>
        /// <param name="SystemName">Название класса поисковой системы</param>
        /// <param name="Response">Время выдачи ответа</param>
        internal static void SetResponseTelemetry(string SystemName, int Response) => MetricsResponse.TryAdd(SystemName, Response);
        /// <summary>
        /// Получаем метрики для одного запроса
        /// </summary>
        internal static Dictionary<string, int> GetResponseMetrics { get => MetricsResponse; }
        #endregion

        #region clear
        /// <summary>
        /// Удаляет все записи телеметрии response
        /// </summary>
        internal static void DeleteTeResponseTelemetry() => MetricsResponse.Clear();
        /// <summary>
        /// Удаляет все записи телеметрии ping
        /// </summary>
        internal static void DeleteTePingTelemetry() => MetricsPing.Clear();
        #endregion
    }

}