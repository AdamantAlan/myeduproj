using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomerServiceLibrary.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void Create_CheckFullName_FromFirstNameAndLastName()
        {
            CustomerDTO customer = new CustomerDTO()
            {
                FirstName = "Ivan",
                LastName = "Ivanov"
            };

            Mock<ICustomerRepository> repositoryMock = new Mock<ICustomerRepository>();
            Mock<INameBuilder> nameBuilderMock = new Mock<INameBuilder>();

            CustomerService service = new CustomerService(repositoryMock.Object, nameBuilderMock.Object);

            service.Create(customer);

            nameBuilderMock.Verify(x => x.From(
                It.Is<string>(val => val.Equals(customer.FirstName)),
                It.Is<string>(val => val.Equals(customer.LastName))));
        }
    }
}
