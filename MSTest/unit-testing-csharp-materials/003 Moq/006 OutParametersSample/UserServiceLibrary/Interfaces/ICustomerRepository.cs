using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceLibrary.Classes;

namespace UserServiceLibrary.Interfaces
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
    }
}
