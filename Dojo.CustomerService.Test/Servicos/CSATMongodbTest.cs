using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dojo.CustomerService.Test.Servico
{
    [TestClass]
    public class CSATMongodbTest
    {
        public async Task Inicializa()
        {
            CSATMongodb.DataBase = "CSATMongoDBTest";
            var csatMongo = new CSATMongodb();
            await csatMongo.ApagarTudo();
        }

     
        [TestMethod]
        public async Task InserindoDadoNoMongoDB()
        {
            await Inicializa();

            var csatMongo = new CSATMongodb();
            var csat = new Csat();

            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;


            await csatMongo.Inserir(csat);
            var quantidade = (await csatMongo.Todos()).Count;
            Assert.AreEqual(1, quantidade);
        }

        [TestMethod]
        public async Task AlterandoDadoNoMongoDB()
        {
            await Inicializa();

            var csatMongo = new CSATMongodb();
            var csat = new Csat();

            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            await csatMongo.Inserir(csat);

            csat.Comment = "texto alterado";
            await csatMongo.Atualizar(csat);

            var csatBancoDeDados = await csatMongo.BuscaPorComment("texto alterado");
            Assert.IsNotNull(csatBancoDeDados);
        }
    }
}