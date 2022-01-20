using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggingLibrary;
using Moq;

namespace LogginLibrary.Tests
{
    // moq - mocking framework для .NET Используется для юнит тестирования для изоляции класса, который тестируется
    // от его зависимостей и необходим, для того, чтобы убедиться, что на зависимостях были вызваны нужные методы.

    // https://en.wikipedia.org/wiki/Mock_object

    // Для того чтобы установить библиотеку moq воспользуйтесь командой Install-Package moq

    [TestClass]
    public class LoggignTests
    {
        Logger _logger;

        Mock<ILoggingConfiguration> _mockLoggingConfig;
        Mock<IMessageBodyGenerator> _mockBodyGenerator;
        Mock<IMessageHeaderGenerator> _mockHeaderGenerator;
        Mock<ISensitiveDataScruber> _mockSensitiveDataScruber;

        [TestInitialize]
        public void Initialize()
        {
            // создание mock объектов
            _mockLoggingConfig = new Mock<ILoggingConfiguration>();
            _mockBodyGenerator = new Mock<IMessageBodyGenerator>();
            _mockHeaderGenerator = new Mock<IMessageHeaderGenerator>();
            _mockSensitiveDataScruber = new Mock<ISensitiveDataScruber>();

            // _mockLoggingConfig.Object - объект заглушка с интерфейсом зависимости
            _logger = new Logger(_mockLoggingConfig.Object,
                _mockHeaderGenerator.Object,
                _mockBodyGenerator.Object,
                _mockSensitiveDataScruber.Object);
        }

        [TestMethod]
        public void Logger_CreateEntry_SensitiveDataShouldBeScrubed()
        {
            // arrange
            // Setup - устанавливаем параметры, определяющие корректность работы тестируемого кода.
            // .Setup(x => x.ClearSensitive(It.IsAny<string>())) - ожидаем вызов метода ClearSensitive с любым строковым значением в качестве параметра.
            _mockSensitiveDataScruber.Setup(x => x.ClearSensitive(It.IsAny<string>()));

            // act
            _logger.CreateEntry("Test log", LogLevel.Error);

            // assert
            // VerifyAll - проверяем, правильно ли взаимодействовал тестируемый метод с mock объектом
            _mockSensitiveDataScruber.VerifyAll();

        }

        [TestMethod]
        public void Logger_CreateEntry_HeaderCreated()
        {
            _mockHeaderGenerator.Setup(x => x.CreateHeader(It.IsAny<LogLevel>()));

            _logger.CreateEntry("Test log", LogLevel.Error);

            _mockHeaderGenerator.VerifyAll();

        }

        [TestMethod]
        public void Logger_CreateEntry_BodyGeneratedCreated()
        {
            _mockBodyGenerator.Setup(x => x.CreateBody(It.IsAny<string>()));

            _logger.CreateEntry("Test log", LogLevel.Error);

            _mockBodyGenerator.VerifyAll();
        }
    }
}

