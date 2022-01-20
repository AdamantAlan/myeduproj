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
    }

    public class Address
    {

    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address MailingAddress { get; set; }
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

            // Для того чтобы метод выполнился без исключений, свойство WorkstationId должно вернуть значение 
            int? id = _workstationSettings.WorkstationId;

            if (!id.HasValue)
            {
                throw new ApplicationException();
            }

            newCustomer.WorkstationId = id;
            // тест должен проверить что свойство было установлено
            _customerRepository.LocalTimeZone = TimeZone.CurrentTimeZone.StandardName;

            _customerRepository.Save(newCustomer);
        }
    }
}
