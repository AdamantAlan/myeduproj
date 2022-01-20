
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Leaf.xNet.Services.Cloudflare;
using System.Net.Http.Headers;

namespace AdelTest.SyncData
{
    public interface IMyHttpClient
    {
        Task<(int code, string content)> Test();
    }
    public class MyHttpClient : IMyHttpClient
    {
        private readonly HttpClient _httpClient;

        public MyHttpClient()
        {

        }
        public MyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(int code, string content)> Test()


        {
            _httpClient.Timeout = TimeSpan.FromSeconds(60);

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer qpgn0qt-8FDDjG1IAf00SQBwhwKlC1qIMRqDNyD8cHn3Koe3Oeez2OX0e-NlOcFDfbMZvZL7vXBNRaRN_O1jpry0Avj2qd41zyMwM05JWxwIqKrye0dIGxVz0xeliLGvNjPeADaIulY3ElR5bKgKRrU5gQdSovyzcHkIlWVXdeAPAICt59bzN7tgXul26dCXUwWe__tHOVrPa0gtUGzgIm-QyswZ-zV_1adl-k4xg_c61yKld2SCpdFZxX5MQVW20FkM9aqnlrn8KhK52cB0paEXls-hlUX4eYCPi3Yl5C-aWy3RZ0u2HEcPuLUj2bz8EMuso_xNYiDbEzdgsKHxfilz-QYBs45GNjBOKFnPL1AExrYdk1r5yW017J21Jj-nD3RL85H0uWAv55RuwmVA8YXYdJ0TDu1KmCkuktysrk5vydXdr4jVAg3HnZVP_kI6Ul-ncwEki6rXZ-RhB0A7qDOAQT6KhUiNOOf_X1E0BNr2fZSscuMZiCnZKhQeuaVJZgy3qlVRlKVvZ_G6D0RXSdx3IMS_JwLMEsXvVnmqb7EC90CiPpiKVclzShgirQkMh0A0a4gY0f6oZ_p2SeM7OXn0REWjZYQFQmqE9cas-vLCI-lT");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36");
            var response = await _httpClient.GetAsync("https://api.author.today/v1/work/134663/meta-info");
            var strings = await response.Content.ReadAsStringAsync();
            if (strings.Contains("Please allow up to 5 seconds"))
            {
                strings = strings.Substring(5778, strings.Length - 5778);
                var i = strings.IndexOf("' method='POST'", 0);
                var __cf_chl_jschl_tk__ = strings.Substring(0, i);
                response = await _httpClient.GetAsync("https://api.author.today/v1/work/134663/meta-info?" + __cf_chl_jschl_tk__);
                return ((int)response.StatusCode, strings);
            }
            var headers = _httpClient.DefaultRequestHeaders;
            (int code, string content) result = ((int)response.StatusCode, strings);

            return result;
        }


    }
}
