using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserServiceLibrary.Interfaces;
using UserServiceLibrary.Classes;

namespace CustomerServiceLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void Create_ShouldSaveUser()
        {
            Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
            Mock<IMailingAddressFactory> factoryMock = new Mock<IMailingAddressFactory>();

            CustomerService service = new CustomerService(customerRepositoryMock.Object, factoryMock.Object);

            Address stubAddress = new Address();

            factoryMock
                .Setup(x => x.TryParse(It.IsAny<string>(), out stubAddress)) // определение значения, которое возвращает out параметр
                .Returns(() => true);

            service.Create(new CustomerDTO());

            customerRepositoryMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }
    }
}
