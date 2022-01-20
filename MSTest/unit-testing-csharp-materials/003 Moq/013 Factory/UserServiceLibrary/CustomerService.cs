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
    }

    public interface IEmailFormatter
    {
        void CreateMessage(string email);
    }

    public class Customer
    {
        public Customer(string fn, string ln)
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerService
    {
        private ICustomerRepository _repositry;
        private IEmailFormatter _emailFormatter;

        public CustomerService(ICustomerRepository repository, IEmailFormatter emailFormatter)
        {
            _repositry = repository;
            _emailFormatter = emailFormatter;
        }

        public void Create(IEnumerable<CustomerDTO> customers)
        {
            foreach (var customer in customers)
            {
                _repositry.Save(new Customer(customer.FirstName, customer.LastName));
                _emailFormatter.CreateMessage(customer.Email);
            }
        }
    }
}
