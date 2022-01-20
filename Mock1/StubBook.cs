using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    class StubBook:IBook
    {
      public string Name;
        public string name { get { return "Гоголь"; } set { Name = value; } }

      
    }
}
