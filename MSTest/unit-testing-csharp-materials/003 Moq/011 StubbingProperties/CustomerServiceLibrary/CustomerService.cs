using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
        string LocalTimeZone { get; set; }
    }

    public interface IWorkstationSettings
    {
        int? WorkstationId { get; set; }
        string WorkspaceName { get; set; }
    }

    public class Address
    {

    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address MailingAddress { get; set; }
        public string WorkspaceName { get; internal set; }
        public int? WorkstationId { get; internal set; }
    }

    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

    }

    public class CustomerService
    {
        ICustomerRepository _customerRepository;
        IWorkstationSettings _workstationSettings;

        public CustomerService(ICustomerRepository customerRepository, IWorkstationSettings workstationSettings)
        {
            _customerRepository = customerRepository;
            _workstationSettings = workstationSettings;
        }

        public void Create(CustomerDTO customer)
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = customer.FirstName;
            newCustomer.LastName = customer.LastName;

            newCustomer.WorkstationId = _workstationSettings.WorkstationId;
            newCustomer.WorkspaceName = _workstationSettings.WorkspaceName;

            if (!newCustomer.WorkstationId.HasValue)
            {
                throw new ApplicationException();
            }

            if (string.IsNullOrEmpty(newCustomer.WorkspaceName))
            {
                throw new ApplicationException();
            }

            _customerRepository.Save(newCustomer);
        }
    }
}
