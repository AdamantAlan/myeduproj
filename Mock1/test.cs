using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;

namespace Mock1
{
    [TestFixture]
    class test : AssertionHelper
    {
        //Mock
        [Test]
        public void test1()
        {
            //A
            IBiblioteca b = new BibliotecaMock();
            IBook stub = new StubBook();
            b.SetBook(stub);
            //A

            List<string> nameBook = b.GetBooks();
            //A
            Expect(nameBook, Contains("Гоголь"));
        }
        [Test]
        public void testRhino()
        {
            MockRepository mock = new MockRepository();
            IBiblioteca b = mock.DynamicMock<IBiblioteca>();
            IBook book = new StubBook();
            using(mock.Record())
            {
                b.SetBook(book);
            }
            b.SetBook(book);
            mock.Verify(b);

        }
        [Test]
        public void test2()
        {
            Expect(true, Is.True);
        }
    }
}
