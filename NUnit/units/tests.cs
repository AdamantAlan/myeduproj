using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace units
{   [TestFixture]
    class tests:AssertionHelper
    {
        int a;
        int b;
        int aa;
        int bb;
        ArrayList array;
        [SetUp]
        public void init()
        {
            a = 4; aa = 8;
            b = 4; bb = 10;
            a = b;
            array = new ArrayList() { 1, 2, 3, 4, 5 };
        }
        [Test]
        public void test1()
        {
            Assert.Contains(a,array);
        }
        [Test]
        public void test2()
        {
            Assert.IsTrue(a == b);
        }
        [Test]
        public void test3()
        {
            Assert.Greater(a, b);
        }
        [Test]
        public void test4()
        {
            Assert.Less(a, b);
        }
        [Test]
        public void test5()
        {
            Assert.IsFalse(a > b);
        }
        [Test]
        public void test6()
        {
            int t = 12;
            Assert.IsInstanceOf(typeof(Int32), t);
        }
        [Test]
        public void test7()
        {
            double o = 0;
            double t = 0/o;
            Assert.IsNaN(t);
        }
        [Test]
        public void test8()
        {
            string s = "";
            Assert.IsEmpty(s);
        }
        [Test]
        [Ignore("не будет результата")]
        public void test9()
        {
            string s = "";
            Assert.IsEmpty(s);
            Assert.Fail("sukaa");
        }

        [Test]
        public void test10()
        {
            string s = "";
            ArrayList l = new ArrayList() { 1, 2, 3, 5, 6, 7, 8 };
            Assert.That(s, Is.Empty);
            Assert.That(l, Has.Count.EqualTo(7));
            Expect(s,Empty);
            Expect(s, Not.NaN);
            Expect(s, TypeOf(typeof(string)));
            Expect(l, Not.Contains(4));

        }
        [TearDown]
        public void end()
        {
            array = null;
        }

    }
}
