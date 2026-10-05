using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;

namespace QA.AllureExample
{
    [AllureNUnit]
    [AllureEpic("Orders")]
    [AllureFeature("Order API")]
    public class OrderTests
    {
        [Test]
        [AllureStory("Validate order")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Dmitriy")]
        public void ValidateOrder_ShouldBeSuccessful()
        {
            var orderId = 123;

            Assert.That(orderId, Is.GreaterThan(0));
        }

        [Test]
        [AllureStory("Create order")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("Dmitriy")]
        public void CreateOrder_ShouldBeSuccessful()
        {
            var request = PrepareRequest();

            AllureApi.AddAttachment("Request", "application/json", 
                System.Text.Encoding.UTF8.GetBytes(request));

            var orderId = SendRequest(request);

            CheckResponse(orderId);
        }

        [AllureStep("Prepare order request")]
        private string PrepareRequest()
        {
            return """
    {
        "productId": 10,
        "quantity": 2
    }
    """;
        }

        [AllureStep("POST /orders")]
        private int SendRequest(string request)
        {
            return 123;
        }

        [AllureStep("Check response")]
        private void CheckResponse(int orderId)
        {
            Assert.That(orderId, Is.GreaterThan(0));
        }
    }
}
