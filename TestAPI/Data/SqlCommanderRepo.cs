using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        readonly private CommanderContext _context;
       public SqlCommanderRepo(CommanderContext Context) {
            this._context = Context;
        }

       

        public IEnumerable<Command> GetAppComands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id );
        }
        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));
            _context.Commands.Add(cmd);
        }
        public bool SaveChanges()
        {
         return  (_context.SaveChanges() >=0);
        }

        public void UpdateCommand(Command cmd)
        {
          //  throw new NotImplementedException();

        }

        public void DeleteCommand(Command cmd)
        {
            _context.Commands.Remove(cmd);
        }
    }
}
