using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceLibrary.Classes;
using UserServiceLibrary.Interfaces;

namespace CustomerServiceLibrary
{
    public class Address
    {
    }

    public interface IEmailBuilder
    {
        Address From(CustomerDTO customer);
    }

    public class CustomerService
    {
        private ICustomerRepository _repositry;
        private IEmailBuilder _emailBuilder;

        public CustomerService(ICustomerRepository repository, IEmailBuilder emailBuilder)
        {
            _repositry = repository;
            _emailBuilder = emailBuilder;
        }

        public void Create(CustomerDTO customerDTO)
        {
            Customer customer = new Customer(customerDTO.FirstName, customerDTO.LastName);

            // При тестирование необходимо определить возвращаемое значение для _emailBuilder.From
            customer.Email = _emailBuilder.From(customerDTO);

            if (customer.Email == null)
            {
                throw new ApplicationException("Email не может быть пустым.");
            }

            _repositry.Save(customer);

        }
    }
}
