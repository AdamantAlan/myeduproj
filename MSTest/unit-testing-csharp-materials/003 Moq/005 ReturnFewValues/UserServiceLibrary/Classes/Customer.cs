using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceLibrary.Classes
{
    public class Customer
    {
        public Customer(string fn, string ln)
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
