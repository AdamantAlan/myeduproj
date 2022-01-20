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
        event EventHandler<NotifyEventArgs> Notify;
    }

    public interface IMailingRepository
    {
        void CreatenewMessage(string name);
    }

    public class NotifyEventArgs : EventArgs
    {
        public NotifyEventArgs(string customerName)
        {
            CustomerName = customerName;
        }

        public string CustomerName { get; set; }

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
        IMailingRepository _mailingRepository;

        public CustomerService(ICustomerRepository customerRepository, IMailingRepository mailingRepository)
        {
            _customerRepository = customerRepository;
            _mailingRepository = mailingRepository;

            // установка обработчика события
            _customerRepository.Notify += CustomerRepository_Notify;
        }

        // обработчик события
        private void CustomerRepository_Notify(object sender, NotifyEventArgs e)
        {
            _mailingRepository.CreatenewMessage(e.CustomerName);
        }

        public void Create(CustomerDTO customer)
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = customer.FirstName;
            newCustomer.LastName = customer.LastName;

            _customerRepository.Save(newCustomer);
        }
    }
}
