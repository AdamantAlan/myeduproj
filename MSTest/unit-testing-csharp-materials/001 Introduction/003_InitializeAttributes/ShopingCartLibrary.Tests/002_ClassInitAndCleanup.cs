using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartLibrary;
using System.Diagnostics;

namespace ShopingCartLibrary.Tests
{
    [TestClass]
    public class ClassInitAndCleanup
    {
        private static ShoppingCart cart;
        // Запускается перед стартом первого тестирующего метода (один раз для класса)
        // Метод должен быть открытым, статическим и принимать параметр типа TestContext
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Debug.WriteLine("Class Initialize");
            Item item = new Item();
            item.Name = "Unit Test Video Lessons";
            item.Quantity = 10;

            cart = new ShoppingCart();
            cart.Add(item);
        }

        // Запускается после завершения последнего тестирующего метода (один раз для класса)
        // Метод должен быть открытым статическим и возвращать void
        [ClassCleanup]
        public static void TestCleanUp()
        {
            Debug.WriteLine("Class CleanUp");
            cart.Dispose();
        }

        [TestMethod]
        public void ShopingCart_AddToCart()
        {
            int expected = cart.Items.Count + 1;

            cart.Add(new Item() { Name = "Test", Quantity = 1 });

            Assert.AreEqual(expected, cart.Count);
        }

        [TestMethod]
        public void ShopingCart_RemoveFromCart()
        {
            int expected = cart.Items.Count - 1;

            cart.Remove(0);

            Assert.AreEqual(expected, cart.Count);
        }
    }
}
