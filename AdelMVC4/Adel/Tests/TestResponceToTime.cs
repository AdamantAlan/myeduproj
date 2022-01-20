using System;
using System.Reflection;
using NUnit.Framework;
using Adel.Areas.Adel.Data;
namespace Adel.Tests
{

    [TestFixture]
    public class TestResponceToTime
    {
        [Test]
        public void testResponce()
        {
            //A
            IResponceToTime Time = new StubResponceToTime();
            DateTime t = new DateTime(2021, 01, 01, 12, 0, 0);
            //A
            string AssertData = Time.Responce;
            string AssertTime = t.Hour.ToString();
            //A
            Assert.AreEqual(AssertTime, AssertData);
        }
        [Test]
        public void testResponce2()
        {
            //A
            IResponceToTime Time = new ResponceToTime();
            //A
            string AssertData = Time.Responce;
            //A
            Assert.AreEqual("Доброе утро, Адель!", AssertData);
        }
    }
    [TestFixture]
    public class AuthentificationAdel : AssertionHelper
    {
        [Test]
    public void Authentification()
    {
            //A
            IAuthentificationAdel authentification = new StubAuthentificationAdel();
            string responce;
            //A
            responce = authentification.Authentification("435235324234");
            //A

            Expect(responce, EqualTo("200OK"));
    }

}
}