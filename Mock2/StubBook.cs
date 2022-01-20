using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock2
{
    class StubBook:IBook
    {
        public string name { get { return name; } set { value = "Гоголь"; } }
    }
}
