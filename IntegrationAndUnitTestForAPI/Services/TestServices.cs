using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAndUnitTestForAPI.Services
{
    public interface ITestServices
    {
        bool CheckId(string id);
    }

    public class TestServices : ITestServices
    {
        public bool CheckId(string id) => long.TryParse(id, out long trueId);
    }
}
