using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceLibrary.Classes;
using UserServiceLibrary.Interfaces;

namespace CustomerServiceLibrary
{
    public interface IMailingAddressFactory
    {
        bool TryParse(string mail, out Address address);
    }

    public class CustomerService
    {
        private ICustomerRepository _repositry;
        private IMailingAddressFactory _mailingAddressFactory;

        public CustomerService(ICustomerRepository repository, IMailingAddressFactory mailingAddressFactory)
        {
            _repositry = repository;
            _mailingAddressFactory = mailingAddressFactory;
        }

        public void Create(CustomerDTO customerDTO)
        {
            Customer customer = new Customer(customerDTO.FirstName, customerDTO.LastName);

            Address mailingAddress;

            // Для того чтобы проверить корректность работы этого метода, mock объект должен подставить значение в качестве out параметра
            _mailingAddressFactory.TryParse(customerDTO.Email, out mailingAddress);

            if (mailingAddress == null)
            {
                throw new ApplicationException();
            }

            customer.Email = mailingAddress;
            _repositry.Save(customer);

        }
    }
}
