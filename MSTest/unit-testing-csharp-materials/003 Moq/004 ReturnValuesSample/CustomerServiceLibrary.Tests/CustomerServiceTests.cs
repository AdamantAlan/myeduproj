using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserServiceLibrary.Classes;
using UserServiceLibrary.Interfaces;
using Moq;

namespace CustomerServiceLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Create_AddressNotCreated_ExceptionThrown()
        {
            CustomerDTO customer = new CustomerDTO() { FirstName = "Ivan", LastName = "Ivanov", Email = "ivanov@test.com" };

            Mock<ICustomerRepository> repositoryMock = new Mock<ICustomerRepository>();
            Mock<IEmailBuilder> emailBuilderMock = new Mock<IEmailBuilder>();

            CustomerService service = new CustomerService(repositoryMock.Object, emailBuilderMock.Object);

            emailBuilderMock
                .Setup(x => x.From(It.IsAny<CustomerDTO>())) // при вызове метода From с любым параметром типа CustomerDTO
                .Returns<Address>(null);                        // должно возвращаться значение null

            // тест закончиться успешно только в том случае если будет выброшено исключение типа ApplicationException

            service.Create(customer);
        }

        [TestMethod]
        public void Create_AddressCreated_CustomerShouldBeSaved()
        {
            CustomerDTO customer = new CustomerDTO() { FirstName = "Ivan", LastName = "Ivanov", Email = "ivanov@test.com" };

            Mock<ICustomerRepository> repositoryMock = new Mock<ICustomerRepository>();
            Mock<IEmailBuilder> emailBuilderMock = new Mock<IEmailBuilder>();

            CustomerService service = new CustomerService(repositoryMock.Object, emailBuilderMock.Object);

            emailBuilderMock
                .Setup(x => x.From(It.IsAny<CustomerDTO>())) // при вызове метода From с любым параметром типа CustomerDTO
                .Returns(new Address());               // должен возвращаться новый объект типа Address
            
            service.Create(customer);

            repositoryMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }
    }
}
