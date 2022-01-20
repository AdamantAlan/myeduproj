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
        public void Create_ShouldCallSaveMethod_Sample1()
        {
            Mock<ICustomerRepository> customerRespositoryMock = new Mock<ICustomerRepository>();
            Mock<IWorkstationSettings> workstationSettingsMock = new Mock<IWorkstationSettings>();

            // 1 вариант
            workstationSettingsMock.SetupProperty(x => x.WorkspaceName, "TestWorkspace");
            workstationSettingsMock.SetupProperty(x => x.WorkstationId, 111);
            //workstationSettingsMock.Object.WorkstationId = 123; // после вызова метода SetupProperty можно использовать метод 

            CustomerService service = new CustomerService(customerRespositoryMock.Object, workstationSettingsMock.Object);
            service.Create(new CustomerDTO());

            customerRespositoryMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }

        [TestMethod]
        public void Create_ShouldCallSaveMethod_Sample2()
        {
            Mock<ICustomerRepository> customerRespositoryMock = new Mock<ICustomerRepository>();
            Mock<IWorkstationSettings> workstationSettingsMock = new Mock<IWorkstationSettings>();

            // 2 вариант
            workstationSettingsMock.SetupAllProperties();
            workstationSettingsMock.Object.WorkspaceName = "TestWorkspace";
            workstationSettingsMock.Object.WorkstationId = 111;

            CustomerService service = new CustomerService(customerRespositoryMock.Object, workstationSettingsMock.Object);
            service.Create(new CustomerDTO());

            customerRespositoryMock.Verify(x => x.Save(It.IsAny<Customer>()));
        }
    }
}
