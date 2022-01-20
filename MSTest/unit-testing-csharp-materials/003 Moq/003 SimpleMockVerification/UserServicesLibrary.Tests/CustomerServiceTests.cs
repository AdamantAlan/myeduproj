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
        public void CreateMethod_Save_WasCalled()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
            ICustomerRepository repository = mock.Object;
            CustomerService service = new CustomerService(repository);

            // act
            service.Create(list);

            // assert
            mock.Verify(x => x.Save(It.IsAny<Customer>()));
        }

        [TestMethod]
        public void CreateMethod_Save_WassCalled_ThreeTimes()
        {
            // arrange
            List<CustomerDTO> list = new List<CustomerDTO>() {
                new CustomerDTO() { FirstName ="Ivan", LastName="Ivanov" },
                new CustomerDTO() { FirstName ="Petr", LastName="Petrov" },
                new CustomerDTO() { FirstName ="Fedor", LastName="Fedorov" }
            };

            Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
            ICustomerRepository repository = mock.Object;
            CustomerService service = new CustomerService(repository);

            // act
            service.Create(list);

            // assert
            mock.Verify(x => x.Save(It.IsAny<Customer>()), Times.Exactly(3));
        }
    }
}
