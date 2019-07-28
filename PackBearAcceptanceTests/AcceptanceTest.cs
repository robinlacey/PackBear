using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using PackBear;

namespace PackBearAcceptanceTests
{

    public class Setup
    {
        public class APIWebApplicationFactory : WebApplicationFactory<Startup>
        {
        }

        [TestFixture]
        public class SampleControllerTests
        {
            private APIWebApplicationFactory _factory;
            private HttpClient _client;

            [OneTimeSetUp]
            public void GivenARequestToTheController()
            {
                _factory = new APIWebApplicationFactory();
                _client = _factory.CreateClient();
            }

            [Test]
            public async Task WhenSomeTextIsPosted_ThenTheResultIsOk()
            {
                
                var textContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Backpack for his applesauce"));
                textContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

                var result = await _client.PostAsync("/sample", textContent);
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }

            [Test]
            public async Task WhenNoTextIsPosted_ThenTheResultIsBadRequest()
            {
                var result = await _client.PostAsync("/sample", new StringContent(string.Empty));
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            }

            [OneTimeTearDown]
            public void TearDown()
            {
                _client.Dispose();
                _factory.Dispose();
            }
        }
    }
}