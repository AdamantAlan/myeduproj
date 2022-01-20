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
        public void Create_NotifyEventRised()
        {
            Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
            Mock<IMailingRepository> mailingRepsoitoryMock = new Mock<IMailingRepository>();

            CustomerService service = new CustomerService(customerRepositoryMock.Object, mailingRepsoitoryMock.Object);

            // Raise - инициирует событие Notify и передает в качестве параметра значение new NotifyEventArgs("Ivan")
            customerRepositoryMock.Raise(x => x.Notify += null, new NotifyEventArgs("Ivan"));

            mailingRepsoitoryMock.Verify(x => x.CreatenewMessage(It.IsAny<string>()));
        }
        
    }
}
