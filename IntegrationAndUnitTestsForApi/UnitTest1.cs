using IntegrationAndUnitTestForAPI;
using IntegrationAndUnitTestForAPI.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationAndUnitTestsForApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient client = webHost.CreateClient();

            var isId = await client.GetAsync("/user/123");
            Assert.AreEqual(HttpStatusCode.OK, isId.StatusCode);
            var resultApi = await isId.Content.ReadAsStringAsync();
            var boolid = bool.Parse(resultApi);
            Assert.IsTrue(boolid);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(b =>
            {
                b.ConfigureTestServices(s =>
                {
                    s.AddTransient<ITestServices, TestServices>();
                    //s.AddScoped<ITestServices, TestServices>();
                });
            });

            ITestServices service = webHost.Services.GetService<ITestServices>();
            //ITestServices service = webHost.Services.CreateScope().ServiceProvider.GetService<ITestServices>();
            Assert.IsTrue(service.CheckId("123"));

        }
    }
}
