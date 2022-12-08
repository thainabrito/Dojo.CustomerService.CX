using Dojo.CustomerService.CX;
using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
            var response = await client.GetAsync("csat/listar");

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
        public async Task AoChamarAlterarComentario_AlteraComentario_RetornaStatus200()
        {

            var csatMongo = new CSATMongodb();
            var csat = new Csat();
            csat.Id = Guid.NewGuid();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            await csatMongo.Inserir(csat);

            csat.Comment = "novo comentário";
            var jsonContent = JsonConvert.SerializeObject(csat);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = _http.CreateClient();
            var response = await client.PutAsync($"/csat/comment/{csat.Id}", contentString);

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(204, statusCode);
        }

        [TestMethod]
        public async Task AoChamarAlterarProblemSolved_AlteraProblemSolved_RetornaStatus200()
        {

            var csatMongo = new CSATMongodb();
            var csat = new Csat();
            csat.Id = Guid.NewGuid();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            await csatMongo.Inserir(csat);

            csat.ProblemSolved = false;
            var jsonContent = JsonConvert.SerializeObject(csat);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = _http.CreateClient();
            var response = await client.PutAsync($"csat/problemSolved/{csat.Id}", contentString);

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(204, statusCode);
        }

        [TestMethod]
        public async Task AoChamarListar_Lista_RetornaStatus200()
        {
            var client = _http.CreateClient();
            var response = await client.GetAsync("csat/listar?score=2&problemSolved=true&attendantEmail=teste@teste");

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public async Task AoChamarListarAttendantEmail_ListaAttendantEmail_RetornaStatus200()
        {
            var csatMongo = new CSATMongodb();
            var csat = new Csat();
            csat.Id = Guid.NewGuid();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            await csatMongo.Inserir(csat);

            var client = _http.CreateClient();
            var response = await client.GetAsync("csat/listar/maria@gmail.com");

            Assert.IsNotNull(response);
            int statusCode = 0;
            if (response != null)
                statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, statusCode);
        }
    }
}
