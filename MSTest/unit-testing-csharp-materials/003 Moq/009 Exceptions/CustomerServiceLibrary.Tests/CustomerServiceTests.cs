using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CustomerServiceLibrary;

namespace CustomerServiceLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(CustomerCreateException))]
        public void Create_ExceptionShouldBeThrown()
        {
            Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
            Mock<ICustomerAddressFactory> customerAddressFactoryMock = new Mock<ICustomerAddressFactory>();

            customerAddressFactoryMock
                .Setup(x => x.CreateFrom(It.IsAny<CustomerDTO>()))  // при вызове метода CreateFrom
                .Throws<InvalidCustomerAddressException>();         // выбрасывается исключение типа InvalidCustomerAddressException

            CustomerService service = new CustomerService(customerRepositoryMock.Object, customerAddressFactoryMock.Object);

            service.Create(new CustomerDTO());
        }
    }
}
