using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace _010_AssertSyntaxTests
{
    // AssertionHelper - класс для использования унаследованного синтаксиса
    [TestFixture]
    public class AreEqualSyntax: AssertionHelper
    {
        [Test]
        public void ClassicAreEqual()
        {
            // Classic syntax workarounds
            Assert.AreEqual(typeof(string), "Hello".GetType());
            Assert.AreEqual("System.String", "Hello".GetType().FullName);

            Assert.AreNotEqual(typeof(int), "Hello".GetType());
            Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName);
        }

        [Test]
        public void HelperAreEqual()
        {
            // Helper syntax
            Assert.That("Hello", Is.TypeOf(typeof(string)));
            Assert.That("Hello", Is.Not.TypeOf(typeof(int)));
        }

        [Test]
        public void InheritedAreEqual()
        {
            // Inherited syntax
            Expect("Hello", TypeOf(typeof(string)));
            Expect("Hello", Not.TypeOf(typeof(int)));
        }
    }
}
