using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecoedsEdu.Models
{
    public class Role
    {
        public string Name { get; set; }
    }
    public abstract record Person(int id);

    public record User(string name, int id) : Person(id)
    {
        public byte OldYears { get; set; } = 18;
        public Role Role { get; set; }
    }

    public class User2
    {
        public byte OldYears { get; set; } = 18;
        public Role Role { get; set; }
    }
}
