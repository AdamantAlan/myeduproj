using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Collection;


namespace SetUpTearDownAttributes
{
    [TestFixture]
    class TestClass
    {
        static UserCollection<string> myCollection = new UserCollection<string>();

        // Метод помеченый атрибутом SetUp будет выполняться при запуске каждого нового теста
        [SetUp]
        public static void SetUpTestsMethod()
        {
            myCollection = new UserCollection<string>();
        }

        [Test]
        public static void TestAddingNewElementToUserCollection()
        {
            myCollection.Add("First");
            myCollection.Add("Second");
            myCollection.Add("Theerd");

            Assert.AreEqual(3, myCollection.Count);
        }

        [Test]
        public static void TestRemoveElementFromUserCollection()
        {
            myCollection.Add("First");
            myCollection.Add("Second");
            myCollection.Add("Third");

            myCollection.Remove("First");

            Assert.AreEqual(2, myCollection.Count);
        }

        // Метод помеченый атрибутом SetUp будет выполняться при окончании каждого теста 
        [TearDown]
        public static void TearDownTestMethod()
        {
            myCollection = null;
        }
    }
}
