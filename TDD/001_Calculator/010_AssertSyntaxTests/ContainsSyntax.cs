using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace _010_AssertSyntaxTests
{
    [TestFixture]
    class ContainsSyntax : AssertionHelper
    {
        [Test]
        public void ClassicContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Classic syntax
            Assert.Contains(3, iarray);
            Assert.Contains("b", sarray);
        }

        [Test]
        public void HelperContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Helper syntax
            Assert.That(iarray, Has.Member(3));

            Assert.That(sarray, Has.No.Member("x"));
        }

        [Test]
        public void InheritedContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Inherited syntax
            Expect(iarray, Contains(3));
            Expect(sarray, Contains("b"));
            Expect(sarray, Not.Contains("x"));
        }
    }
}