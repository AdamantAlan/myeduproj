using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock1
{
    class Book:IBook
    {
        public string Name;
        string Price;
        public string name { get { return Name; } set { Name = value; } }

        public string price { get { return Price; } set { Price = value; } }

    }
}
