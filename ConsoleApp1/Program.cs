using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDBEntities TestDBEF = new TestDBEntities();
           var query = TestDBEF.MyTableTests.ToList();
            foreach(var item in query)
            {
                Console.WriteLine($"{item.Id} {item.User} {item.Age}");
            }
            Console.ReadKey();
        }
       
    }
}
