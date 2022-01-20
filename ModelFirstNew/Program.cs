using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFirstNew
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelContainer mc = new ModelContainer();
            
                mc.UsersSet.Add(new Users {Name="Alan", Age=23});
                mc.UsersSet.Add(new Users { Name = "Adel", Age = 21 });
                mc.SaveChanges();
                var q = mc.UsersSet.ToList();
            foreach (var item in q)
                Console.WriteLine($"{item.Id} {item.Name} {item.Age}");
            Console.ReadKey();
            
        }
    }
}
