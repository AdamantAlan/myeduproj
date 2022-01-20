using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<Command> GetAppComands()
        {
            var commands = new List<Command> {
                new Command() { Id=0, HowTo="Server0", Line="Command line0", Plaform="Unix0"},
                new Command() { Id=1, HowTo="Server1", Line="Command line1", Plaform="Unix1"},
                new Command() { Id=2, HowTo="Server2", Line="Command line2", Plaform="Unix2"}
        };
            return commands;
        }

       public Command GetCommandById(int id)
        {
            return new Command() { Id=0, HowTo="Server", Line="Command line", Plaform="Unix"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
