using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    interface IBiblioteca
    {
         void SetBook(IBook book);
         List<string> GetBooks();
    }
}
