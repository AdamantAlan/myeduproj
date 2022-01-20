using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ModelFirstContainer MF = new ModelFirstContainer())
            {
                MF.PersonalSet.Add(new Personal() { Name = "Alan" });
                MF.PersonalSet.Add(new Personal() { Name = "Adel" });
                MF.PersonalInfoSet.Add(new PersonalInfo() { Location = "Rus", Phone = "+79213412", Role = "Adm" });
                MF.PersonalInfoSet.Add(new PersonalInfo() { Location = "Rus", Phone = "+7657453", Role = "User" });
                var query = MF.PersonalSet.ToList();
                foreach (var item in query)
                    Console.WriteLine($"{item.Id} {item.Name}");
                Console.ReadKey();
            }
        }

    }
}
