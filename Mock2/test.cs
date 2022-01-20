using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Mock2
{
    [TestFixture]
    class test:AssertionHelper
    {
        [Test]
        public void test1()
        {
            //A
            Biblioteca b = new Biblioteca();
            StubBook stub = new StubBook();
            b.SetBook(stub);
            //A
            List<string> nameBook = b.GetBooks();
            //A
            Expect("Гоголь", Contains(nameBook));

        }
    }
}
