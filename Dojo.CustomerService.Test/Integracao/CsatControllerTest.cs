using Dojo.CustomerService.CX;
using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.CustomerService.Test.Integracao
{
    [TestClass]
    public class CsatControllerTest
    {
        public CsatControllerTest() 
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
        public async Task TestandoGetCsatController()
        {
            var client = _http.CreateClient();
            var response = await client.GetAsync("/csat");

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public async Task TestandoPostCsatController()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            var jsonContent = JsonConvert.SerializeObject(csat);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = _http.CreateClient();
            var response = await client.PostAsync("/csat", contentString);

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(201, statusCode);
        }

        public async Task InicializaMongo()
        {
            CSATMongodb.DataBase = "CSATMongoDBTest";
            var csatMongo = new CSATMongodb();
            await csatMongo.ApagarTudo();
        }

        [TestMethod]
        public async Task TestandoPutCsatController()
        {

            var csatMongo = new CSATMongodb();
            var csat = new Csat();
            csat.Score = 5; // se existem as propriedades
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            await csatMongo.Inserir(csat);


            var csatUpdate = new Csat();
            csat.Score = 7; // se existem as propriedades
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            var jsonContent = JsonConvert.SerializeObject(csatUpdate);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = _http.CreateClient();
            var response = await client.PutAsync($"/csat/{csat.Id}", contentString);

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, statusCode);
        }
    }
}
