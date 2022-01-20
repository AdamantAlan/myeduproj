using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CustomerServiceLibrary;

namespace CustomerServiceLibrary.Tests
{
    [TestClass]
    public class CustoemrServiceTests
    {
        [TestMethod]
        public void Create_ShouldSetTimeZone_AndCallSave()
        {
            Mock<ICustomerRepository> repositoryMock = new Mock<ICustomerRepository>();
            Mock<IWorkstationSettings> workstationSettingsMock = new Mock<IWorkstationSettings>();

            CustomerService service = new CustomerService(repositoryMock.Object, workstationSettingsMock.Object);
            // При обращении к свойству WorkstationId возвращаем значенеи 111
            workstationSettingsMock.Setup(x => x.WorkstationId).Returns(111);
            
            service.Create(new CustomerDTO());

            // проверка что свойству LocalTimeZone на мок объекте было установлено любое строковое значение
            repositoryMock.VerifySet(x => x.LocalTimeZone = It.IsAny<string>());
            repositoryMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }
    }
}
