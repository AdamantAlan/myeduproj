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
        public void Create_BronzCustomer_SaveShouldBeCalled()
        {
            Mock<ICustomerRepository> customerRepositoyMock = new Mock<ICustomerRepository>();
            Mock<ICustomerStatusFactory> statusFactoryMock = new Mock<ICustomerStatusFactory>();

            CustomerService service = new CustomerService(customerRepositoyMock.Object, statusFactoryMock.Object);

            CustomerDTO customer = new CustomerDTO()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                DesiredLevel = StatusLevel.Bronze
            };

            statusFactoryMock
                // Если объект переданый в параметры в свойстве DesiredLevel содержит значение Bronze
                .Setup(x => x.CreateFrom(It.Is<CustomerDTO>(c => c.DesiredLevel == StatusLevel.Bronze)))
                // Метод CreateFrom должен вернуть значение Bronze
                .Returns(StatusLevel.Bronze);

            service.Create(customer);

            customerRepositoyMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }

        [TestMethod]
        public void Create_GoldCustomer_SaveExtendedShouldBeCalled()
        {
            Mock<ICustomerRepository> customerRepositoyMock = new Mock<ICustomerRepository>();
            Mock<ICustomerStatusFactory> statusFactoryMock = new Mock<ICustomerStatusFactory>();

            CustomerService service = new CustomerService(customerRepositoyMock.Object, statusFactoryMock.Object);

            CustomerDTO customer = new CustomerDTO()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                DesiredLevel = StatusLevel.Gold
            };

            statusFactoryMock
                .Setup(x => x.CreateFrom(It.Is<CustomerDTO>(c => c.DesiredLevel == StatusLevel.Gold))) 
                .Returns(StatusLevel.Gold);

            service.Create(customer);

            customerRepositoyMock.Verify(x => x.SaveExtended(It.IsAny<Customer>()));
        }
    }
}
