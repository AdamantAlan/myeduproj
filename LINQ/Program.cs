using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
   public abstract class absClass
    {
      
        protected virtual int GetID()
        {
            return 0;
        }
        protected int id;
        protected int SetId()
        {
            return this.id;
        }
     
        public absClass(int id)
        {
            this.id = id;
        }
    }
  public  class a : absClass
    { 
       public  a(int id):base(id)
        {

        }
    }

    class User
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public User()
        {
            Languages = new List<string>();
        }
    }
    class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
    }
    class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>
               {
            new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
            new User {Name="Боб", Age=37, Languages = new List<string> {"английский", "французский" }},
            new User {Name="Бип", Age=29, Languages = new List<string> {"английский", "испанский" }},
            new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            var q = from u in users
                    where u.Age > 18 & u.Name.StartsWith("Б")
                    orderby u.Age
                    select new { NameMan = u.Name, AgeMan= u.Age };
            var qq = from u in users
                     from l in u.Languages
                     where u.Age > 18 & l.StartsWith("Англ")
                     orderby u.Name
                     select (u.Name, u.Age, l);

            var selectedUsers = users.SelectMany(u => u.Languages,
                            (u, l) => new { User = u, Lang = l })
                          .Where(u => u.Lang == "английский" && u.User.Age < 28)
                          .Select(u => u.User);
            var orderArray = users.OrderByDescending(u => u.Age).Select(user=>user);
            foreach (var item in q)
                Console.WriteLine($"Name-{item.NameMan}, Age-{item.AgeMan}\n");
            foreach (var item in orderArray)
                Console.WriteLine($"Name-{item.Name}, Age-{item.Age}");
            {
                string[] soft = { "Microsoft", "Google", "Apple" };
                string[] hard = { "Apple", "IBM", "Samsung" };

                // разность последовательностей
                var result = soft.Except(hard).OrderByDescending(s => s);
                foreach (string s in result)
                    Console.WriteLine(s + '\n');
            }

            {
                string[] soft = { "Microsoft", "Google", "Apple" };
                string[] hard = { "Apple", "IBM", "Samsung" };

                // пересечение последовательностей
                var result = soft.Intersect(hard);

                foreach (string s in result)
                    Console.WriteLine(s + '\n');
            }

            {
                string[] soft = { "Microsoft", "Google", "Moogle", "Apple", "Moogle" };
                string[] hard = { "Apple", "IBM", "Samsung ", "Moogle" };

                // пересечение последовательностей
                var result = soft.Concat(hard).Where(r =>r.Contains("oo"));

                foreach (string s in result)
                    Console.WriteLine("Dstinct"+s + '\n');
            }
            {

                List<Phone> phones = new List<Phone>
{
    new Phone {Name="Lumia 430", Company="Microsoft" },
    new Phone {Name="Mi 5", Company="Xiaomi" },
    new Phone {Name="LG G 3", Company="LG" },
    new Phone {Name="iPhone 5", Company="Apple" },
    new Phone {Name="Lumia 930", Company="Microsoft" },
    new Phone {Name="iPhone 6", Company="Apple" },
    new Phone {Name="Lumia 630", Company="Microsoft" },
    new Phone {Name="LG G 4", Company="LG" }
};
                var q1 = phones.GroupBy(ph => ph.Company);
                foreach (var k in q1)
                {
                    Console.WriteLine(k.Key);
                    foreach (var r in k)
                        Console.WriteLine(r.Name+ ' '+ r.Company);
                }
                Console.WriteLine('\n');
              var q2 = phones.GroupBy(ph => ph.Company).Select(ph => new { Name = ph.Key, Count = ph.Count() });
                foreach (var i in q2)
                    Console.WriteLine($"{i.Name} - {i.Count}");
                        }
{
                List<Team> teams = new List<Team>()
{
    new Team { Name = "Бавария", Country ="Германия" },
    new Team { Name = "Барселона", Country ="Испания" }
};
                List<Player> players = new List<Player>()
{
    new Player {Name="Месси", Team="Барселона"},
    new Player {Name="Неймар", Team="Барселона"},
    new Player {Name="Роббен", Team="Бавария"}
};


                var q1 = players.Join(teams,
                    f => f.Team,
                    m => m.Name,
                    (f, m) => new {NameTeam = f.Team, Country = m.Country }).Distinct();

                foreach (var i in q1)
                    Console.WriteLine($"{i.NameTeam} - {i.Country}\n");

                var q2 = teams.GroupJoin(
                             players, // второй набор
                             t => t.Name, // свойство-селектор объекта из первого набора
                             pl => pl.Team,
                             (t,pl) => new { 
                                NameTeaam = t.Name,
                                Country = t.Country,
                                Players = pl.Select(p => p)
                             });
                foreach (var i in q2)
                {
                    Console.WriteLine($"{i.NameTeaam} - {i.Country} ");
                    foreach (var p in i.Players)
                        Console.WriteLine($"{p.Name}");
                }
            }
            Console.ReadKey();
        }
    }
}
