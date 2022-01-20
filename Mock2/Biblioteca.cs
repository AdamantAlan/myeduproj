using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock2
{
    class Biblioteca
    {
        List<IBook> books = new List<IBook>();

        public void SetBook(IBook book)
        {
            books.Add(book);
        }
        public List<string> GetBooks()
        {
            List<string> outData = new List<string>();
            foreach (var book in books)
            {
                outData.Add(book.name);
            }
            return outData;
        }

    }
}