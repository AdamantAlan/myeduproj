using IntegrationAndUnitTestForAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace XUnitEdu
{
    [CollectionDefinition("Service")]
    public class InService : ICollectionFixture<TestServices> { }

    [Collection("Service")]
    public class UnitTest2 : IClassFixture<InService>
    {
        private readonly TestServices _ser;

        public UnitTest2(TestServices ser)
        {
            _ser = ser;
        }

        [Fact]
        public void Test()
        {
           Assert.True(_ser.CheckId("123"));
        }
    }

    public class UnitTest4 : IClassFixture<TestServices>
    {
        private readonly TestServices _ser;

        public UnitTest4(TestServices ser)
        {
            _ser = ser;
        }

        [Fact]
        public void Test()
        {
            Assert.True(_ser.CheckId("123"));
        }
    }
}
