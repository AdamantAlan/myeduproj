using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeFirst cf = new CodeFirst();
            cf.UserInfos.Add(new UserInfo() {Name="Alan", Age=23 });
            cf.UserInfos.Add(new UserInfo() { Name = "Adel", Age = 21 });
            cf.UserInfos.Add(new UserInfo() { Name = "Shark", Age = 1 });
            cf.SaveChanges();
            var q = cf.UserInfos.ToList();
            foreach (var i in q)
                Console.WriteLine($"{i.id} {i.Name} {i.Age}");
            Console.ReadKey();
        }
    }
}
