using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CustomerServiceLibrary;
using Moq;

namespace UserServicesLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void CreateMethod_Strict()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            // MockBehavior.Strict - при Stirct поведении mock объект будет выбрасывать исключение
            // при вызове методов для которых явно не устанавливались настройки через setup
            Mock<ICustomerRepository> repositoryMock = new Mock<ICustomerRepository>(MockBehavior.Strict);
            Mock<IEmailFormatter> emailFormatterMock = new Mock<IEmailFormatter>(MockBehavior.Strict);

            repositoryMock.Setup(x => x.Save(It.IsAny<Customer>()));
            emailFormatterMock.Setup(x => x.CreateMessage(It.IsAny<string>()));

            CustomerService service = new CustomerService(repositoryMock.Object, emailFormatterMock.Object);

            // act
            service.Create(list);

            // assert
            repositoryMock.Verify();
            emailFormatterMock.Verify();
        }

        [TestMethod]
        public void CreateMethod_Loose()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            // MockBehavior.Loose - при вызове метода для, которого не проводилась настройка будет возвращаться значение по умолчанию.
            Mock<ICustomerRepository> repsoitoryMock = new Mock<ICustomerRepository>(MockBehavior.Loose);
            Mock<IEmailFormatter> emailFormatterMock = new Mock<IEmailFormatter>(MockBehavior.Loose);
            CustomerService service = new CustomerService(repsoitoryMock.Object, emailFormatterMock.Object);

            // act
            service.Create(list);

            // assert
            repsoitoryMock.Verify(x => x.Save(It.IsAny<Customer>()), Times.Exactly(3));
        }

        [TestMethod]
        public void CreateMethod_Factory()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            // Фабрика для создания mock объектов
            MockRepository mockRepo = new MockRepository(MockBehavior.Loose);

            // создание mock объектов через фабрику
            Mock<ICustomerRepository> repsoitoryMock = mockRepo.Create<ICustomerRepository>();
            Mock<IEmailFormatter> emailFormatterMock = mockRepo.Create<IEmailFormatter>();

            repsoitoryMock.Setup(x => x.Save(It.IsAny<Customer>()));
            emailFormatterMock.Setup(x => x.CreateMessage(It.IsAny<string>()));

            CustomerService service = new CustomerService(repsoitoryMock.Object, emailFormatterMock.Object);

            // act
            service.Create(list);

            // assert
            // проверка всех mock объектов
            mockRepo.Verify();
        }
    }
}
