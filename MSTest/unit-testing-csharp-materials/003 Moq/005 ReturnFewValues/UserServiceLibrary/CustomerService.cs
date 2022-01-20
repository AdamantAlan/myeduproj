using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceLibrary.Classes;
using UserServiceLibrary.Interfaces;

namespace CustomerServiceLibrary
{
    public class CustomerService
    {
        private IIdFactory _idFactory;
        private ICustomerRepository _repositry;

        public CustomerService(ICustomerRepository repository, IIdFactory idFactory)
        {
            _repositry = repository;
            _idFactory = idFactory;
        }

        public void Create(IEnumerable<CustomerDTO> customers)
        {
            foreach (var currentCustomer in customers)
            {
                Customer newCustomer = new Customer(currentCustomer.FirstName, currentCustomer.LastName);
                newCustomer.Id = _idFactory.Create();
                _repositry.Save(newCustomer);
            }
        }
    }


}
