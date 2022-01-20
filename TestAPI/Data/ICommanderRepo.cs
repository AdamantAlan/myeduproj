using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;
namespace TestAPI.Data
{
   public interface ICommanderRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAppComands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}
