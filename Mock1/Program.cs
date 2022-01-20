using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblia = new Biblioteca();
            while(true)
            {
                Book book = new Book();
                string nameBook=Console.ReadLine();
                if (nameBook == "q")
                    break;
                book.Name = nameBook;
                biblia.SetBook(book);
            }
            List<string> names = biblia.GetBooks();
            foreach (var i in names)
                Console.WriteLine($"name - {i}\n");
            Console.ReadKey();

        }

    }
}
