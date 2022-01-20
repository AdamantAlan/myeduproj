
using System;
using System.Collections.Generic;

namespace TestSeach.Models
{
    /// <summary>
    /// Класс сбора информации
    /// </summary>
    static class Telemetry
    {
        #region Ping
        /// <summary>
        /// Метрики времени ответа
        /// </summary>
        private static Dictionary<string, int> MetricsPing = new Dictionary<string, int>();
      
        /// <summary>
        /// Сохраняет информацию о задержке
        /// </summary>
        /// <param name="SystemName">Название класса поисковой системы</param>
        /// <param name="PingResponse">Время выдачи ответа</param>
        internal static void SetPingTelemetry(string SystemName, int PingResponse) => MetricsPing.TryAdd(SystemName, PingResponse);
        /// <summary>
        /// Получаем метрики
        /// </summary>
        internal static Dictionary<string, int>  GetPingMetrics {get=> MetricsPing;}
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
        /// Получаем метрики
        /// </summary>
        internal static Dictionary<string, int> GetResponseMetrics { get => MetricsResponse; }
        #endregion
        /// <summary>
        /// Удаляет все записи телеметрии
        /// </summary>
        internal static void DeleteTelemetry() {MetricsPing.Clear();MetricsResponse.Clear();}

    }

}
