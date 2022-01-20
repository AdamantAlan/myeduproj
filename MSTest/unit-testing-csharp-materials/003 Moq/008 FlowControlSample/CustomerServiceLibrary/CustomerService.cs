using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
        void SaveExtended(Customer customer);
    }

    public interface ICustomerStatusFactory
    {
        StatusLevel CreateFrom(CustomerDTO customer);
    }

    public enum StatusLevel
    {
        Bronze, Silver, Gold
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public StatusLevel StatusLevel { get; set; }
    }

    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public StatusLevel DesiredLevel { get; set; }
        
    }

    public class CustomerService
    {
        ICustomerRepository _customerRepository;
        ICustomerStatusFactory _statusFactory;

        public CustomerService(ICustomerRepository customerRepository, ICustomerStatusFactory statusFactory)
        {
            _customerRepository = customerRepository;
            _statusFactory = statusFactory;
        }

        public void Create(CustomerDTO customer)
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = customer.FirstName;
            newCustomer.LastName = customer.LastName;

            newCustomer.StatusLevel = _statusFactory.CreateFrom(customer);

            if (newCustomer.StatusLevel == StatusLevel.Gold)
            {
                _customerRepository.SaveExtended(newCustomer);
            }
            else
            {
                _customerRepository.Save(newCustomer);
            }
        }
    }
}
