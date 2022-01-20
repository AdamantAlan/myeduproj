using Moq;
using NUnit.Framework;
using System.Linq;
using testMoq;

namespace TestMoqTeest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        #region stub
        [Test]
        public void Test1()
        {
            ILoggerDependency loggerDependency =
            Mock.Of<ILoggerDependency>(d => d.GetCurrentDirectory() == "D:\\Temp");
            var currentDirectory = loggerDependency.GetCurrentDirectory();

            Assert.That(currentDirectory, Is.EqualTo("D:\\Temp"));
        }
        [Test]
        public void Test2()
        {
            ILoggerDependency loggerDependency = Mock.Of<ILoggerDependency>(
      ld => ld.GetDirectoryByLoggerName(It.IsAny<string>()) == "C:\\Foo");

            string directory = loggerDependency.GetDirectoryByLoggerName("anything");

            Assert.That(directory, Is.EqualTo("C:\\Foo"));
        }
        [Test]
        public void Test3()
        {
            Mock<ILoggerDependency> stub = new Mock<ILoggerDependency>();
            stub.Setup(ld => ld.GetDirectoryByLoggerName(It.IsAny<string>()))
                                                        .Returns<string>(name => "C:\\" + name);

            string loggerName = "SomeLogger";
            ILoggerDependency logger = stub.Object;
            string directory = logger.GetDirectoryByLoggerName(loggerName);

            Assert.That(directory, Is.EqualTo("C:\\" + loggerName));
        }
        [Test]
        public void Test4()
        {
            ILoggerDependency logger =
     Mock.Of<ILoggerDependency>(
         d => d.GetCurrentDirectory() == "D:\\Temp" &&
                 d.DefaultLogger == "DefaultLogger" &&
                 d.GetDirectoryByLoggerName(It.IsAny<string>()) == "C:\\Temp");

            Assert.That(logger.GetCurrentDirectory(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.DefaultLogger, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.GetDirectoryByLoggerName("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }
        //old version
        [Test]
        public void Test5()
        {
            var stub = new Mock<ILoggerDependency>();
            stub.Setup(ld => ld.GetCurrentDirectory()).Returns("D:\\Temp");
            stub.Setup(ld => ld.GetDirectoryByLoggerName(It.IsAny<string>())).Returns("C:\\Temp");
            stub.SetupGet(ld => ld.DefaultLogger).Returns("DefaultLogger");

            ILoggerDependency logger = stub.Object;

            Assert.That(logger.GetCurrentDirectory(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.DefaultLogger, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.GetDirectoryByLoggerName("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }
        #endregion

        #region mock
        [Test]
        public void Test6()
        {
            var mock = new Mock<ILogWriter>();
            var logger = new Logger(mock.Object);

            logger.WriteLine("Hello, logger!");

            // ѕровер€ем, что вызвалс€ метод Write нашего мока с любым аргументом
            mock.Verify(lw => lw.Write(It.IsAny<string>()));

            mock.Verify(lw => lw.Write(It.IsAny<string>()),
    Times.Once());
        }

        [Test]
        public void Test7()
        {
            var mock = new Mock<ILogWriter>();
            mock.Setup(lw => lw.Write(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.WriteLine("Hello, logger!");

            // ћы не передаем методу Verify никаких дополнительных параметров.
            // Ёто значит, что будут использоватьс€ ожидани€ установленные
            // с помощью mock.Setup
            mock.Verify();
        }
        //ѕри использовании MockBehavior.Strict метод Verify завершитс€ неудачно,
        //если мы не укажем €вно, какие точно методы зависимости будут вызваны
       [Test]
        public void Test8()
        {
            var mock = new Mock<ILogWriter>(MockBehavior.Strict);
            // ≈сли закомментировать одну из следующих строк, то
            // метод mock.Verify() завершитс€ с исключением
            mock.Setup(lw => lw.Write(It.IsAny<string>()));
            mock.Setup(lw => lw.SetLogger(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.WriteLine("Hello, logger!");

            mock.Verify();
        }
        #endregion
        #region MockRepo
        [Test]
        public void Test9()
        {
            var repository = new MockRepository(MockBehavior.Default);
            ILoggerDependency logger = repository.Of<ILoggerDependency>()
                .Where(ld => ld.DefaultLogger == "DefaultLogger")
                .Where(ld => ld.GetCurrentDirectory() == "D:\\Temp")
                .Where(ld => ld.GetDirectoryByLoggerName(It.IsAny<string>()) == "C:\\Temp")
                .First();

            Assert.That(logger.GetCurrentDirectory(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.DefaultLogger, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.GetDirectoryByLoggerName("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }
        [Test]
        public void Test10()
        {
            var repo = new MockRepository(MockBehavior.Default);
            var logWriterMock = repo.Create<ILogWriter>();
            logWriterMock.Setup(lw => lw.Write(It.IsAny<string>()));


            var smartLogger = new Logger(logWriterMock.Object);

            smartLogger.WriteLine("Hello, Logger");

            repo.Verify();
        }
        //через Mock.Get
        [Test]
        public void Test11()
        {
            ILoggerDependency logger = Mock.Of<ILoggerDependency>(
            ld => ld.GetCurrentDirectory() == "D:\\Temp"
            && ld.DefaultLogger == "DefaultLogger");

            // «адаем более сложное поведение метода GetDirectoryByLoggerName
            // дл€ возвращени€ разных результатов, в зависимости от аргумента
            Mock.Get(logger)
                .Setup(ld => ld.GetDirectoryByLoggerName(It.IsAny<string>()))
                .Returns<string>(loggerName => "C:\\" + loggerName);
           
            Assert.That(logger.GetCurrentDirectory(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.DefaultLogger, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.GetDirectoryByLoggerName("Foo"), Is.EqualTo("C:\\Foo"));
            Assert.That(logger.GetDirectoryByLoggerName("Boo"), Is.EqualTo("C:\\Boo"));
        }
        #endregion
    }
}