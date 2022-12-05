using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoCustomerServiceTest.Servicos // testa o serviço de conexão(inserir, buscar, etc)
{
    internal class CsatMongoDBTest
    {
        [SetUp]
        public async Task Setup() // antes de rodar
        {
            CSATMongodb.DataBase = "CSATMongoDBTest";
            var csatMongo = new CSATMongodb();
            await csatMongo.ApagarTudo();
        }

        [Test]
        public async Task InserindoDadoNoMongoDB()
        {
            var csatMongo = new CSATMongodb();
            var csat = new Csat();

            csat.Score = 5; // se existem as propriedades
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;


            await csatMongo.Inserir(csat);
            var quantidade = (await csatMongo.Todos()).Count;
            Assert.AreEqual(1, quantidade);
        }
    }
}
