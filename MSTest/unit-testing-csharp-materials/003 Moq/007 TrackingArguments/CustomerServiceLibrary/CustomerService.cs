using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    public class Customer
    {
        public Customer(string fullName)
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface ICustomerRepository
    {
        void Save(Customer customer);
    }

    public interface INameBuilder
    {
        string From(string firstName, string lastName);
    }

    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INameBuilder _nameBuilder;

        public CustomerService(ICustomerRepository customerRepository, INameBuilder nameBuilder)
        {
            _customerRepository = customerRepository;
            _nameBuilder = nameBuilder;
        }

        public void Create(CustomerDTO customer)
        {
            string fullName = _nameBuilder.From(customer.FirstName, customer.LastName);

            Customer newCustomer = new Customer(fullName);

            _customerRepository.Save(newCustomer);
        }
    }
}
