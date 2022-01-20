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
    }

    public interface ICustomerAddressFactory
    {
        Address CreateFrom(CustomerDTO customer);
    }

    public class Address
    {

    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address MailingAddress { get; set; }
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
        ICustomerAddressFactory _addressFactory;

        public CustomerService(ICustomerRepository customerRepository, ICustomerAddressFactory addressFactory)
        {
            _customerRepository = customerRepository;
            _addressFactory = addressFactory;
        }

        public void Create(CustomerDTO customer)
        {
            try
            {
                Customer newCustomer = new Customer();
                newCustomer.FirstName = customer.FirstName;
                newCustomer.LastName = customer.LastName;
                newCustomer.MailingAddress = _addressFactory.CreateFrom(customer);

                _customerRepository.Save(newCustomer);
            }
            catch (InvalidCustomerAddressException)
            {
                throw new CustomerCreateException();
            }
        }
    }

    #region Exceptions
    [Serializable]
    public class InvalidCustomerAddressException : Exception
    {
        public InvalidCustomerAddressException()
        {
        }

        public InvalidCustomerAddressException(string message) : base(message)
        {
        }

        public InvalidCustomerAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCustomerAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class CustomerCreateException : Exception
    {
        public CustomerCreateException()
        {
        }

        public CustomerCreateException(string message) : base(message)
        {
        }

        public CustomerCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    #endregion
}
