using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    class Client
    {
        IBiblioteca b;
        List<string> listOrder;
        string name;
        public Client(string name)
        {
            this.name = name;
            b = new Biblioteca();
            listOrder = b.GetBooks();
        }

    }
}
