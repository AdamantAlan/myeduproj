using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    class BibliotecaMock:IBiblioteca
    {
        List<string> outData;
        public BibliotecaMock()
        {
            outData = new List<string>();
        }
       
        public void SetBook(IBook book)
        {
            outData.Add(book.name);
        }
        public List<string> GetBooks()
        {
            return outData;
        }
    }
}
