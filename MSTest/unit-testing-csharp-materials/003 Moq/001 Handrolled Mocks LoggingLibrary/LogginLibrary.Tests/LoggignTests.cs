using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggingLibrary;
using LogginLibrary.Tests.Mocks;

namespace LogginLibrary.Tests
{
    [TestClass]
    public class LoggignTests
    {
        private Logger logger;

        private MockSensitiveDataScruber _mockScruber;
        private MockLoggingConfiguration _mockConfiguration;
        private MockMessageBodyGenerator _mockBodyGenerator;
        private MockMessageHeaderGenerator _mockHeaderGenerator;

        [TestInitialize]
        public void Initialize()
        {
            _mockBodyGenerator = new MockMessageBodyGenerator();
            _mockScruber = new MockSensitiveDataScruber();
            _mockConfiguration = new MockLoggingConfiguration();
            _mockHeaderGenerator = new MockMessageHeaderGenerator();


            logger = new Logger(_mockConfiguration, _mockHeaderGenerator, _mockBodyGenerator, _mockScruber);
        }

        [TestMethod]
        public void Logger_CreateEntry_SensitiveDataShouldBeScrubed()
        {
            logger.CreateEntry("Test log", LogLevel.Error);

            Assert.IsTrue(_mockScruber.ClearSensitiveWasCalled);
        }

        [TestMethod]
        public void Logger_CreateEntry_HeaderCreated()
        {
            logger.CreateEntry("Test log", LogLevel.Error);

            Assert.IsTrue(_mockHeaderGenerator.CreateHeaderWasCalled);
        }

        [TestMethod]
        public void Logger_CreateEntry_BodyGeneratedCreated()
        {
            logger.CreateEntry("Test log", LogLevel.Error);

            Assert.IsTrue(_mockBodyGenerator.CreateBodyWasCalled);
        }
    }
}
