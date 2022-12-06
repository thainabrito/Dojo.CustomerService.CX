using Dojo.CustomerService.CX;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.CustomerService.Test.Integracao
{
    [TestClass]
    public class HomeControllerTest
    {
        public HomeControllerTest() 
        {
            this.Setup();
        }

        private static WebApplicationFactory<Startup> _http;

        public void Setup()
        {
            _http = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.UseSetting("https_port", "5001").UseEnvironment("Testing")
                );
        }

        [TestMethod]
        public async Task TestandoGetHomeController()
        {
            var client = _http.CreateClient();
            var response = await client.GetAsync("/");

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, statusCode);
        }
    }
}
