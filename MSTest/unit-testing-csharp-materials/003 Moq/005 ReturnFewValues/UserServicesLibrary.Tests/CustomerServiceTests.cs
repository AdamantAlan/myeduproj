using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CustomerServiceLibrary;
using Moq;
using UserServiceLibrary.Interfaces;
using UserServiceLibrary.Classes;

namespace UserServicesLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void Create_IdWasCreated()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            Mock<ICustomerRepository> customerRepositoyMock = new Mock<ICustomerRepository>();
            Mock<IIdFactory> factoryMock = new Mock<IIdFactory>();
            CustomerService service = new CustomerService(customerRepositoyMock.Object, factoryMock.Object);

            int id = 1;
            factoryMock
                .Setup(x => x.Create()) // При вызове метода Create
                .Returns(() => id)      // возвращается значение переменной id
                .Callback(() => id++);  // после завершения метода Create вызывается callback, который увеличивает значение переменной Id

            // act
            service.Create(list);

            // assert
            customerRepositoyMock.Verify(x => x.Save(It.IsAny<Customer>()));
            factoryMock.Verify(x => x.Create(), Times.AtLeastOnce);
        }
    }
}
