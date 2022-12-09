using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.CustomerService.Test.Servicos
{
    [TestClass]
    public class RequestManagerTest
    {
        [TestMethod]
        public void TestaRequestManager_CreateCsat()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.Inserir(It.IsAny<Csat>())).Returns(Task.FromResult(csat));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);
            var reqManager = requestManager.CreateCsat(csat);
            Assert.IsNotNull(reqManager);
        }

        [TestMethod]
        public void TestConsultarCsat_ConsultarCsat()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.BuscaPorId(It.IsAny<Guid>())).Returns(Task.FromResult(csat));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);
            var reqManager = requestManager.ConsultarCsat("7e179018-5c07-42bd-9e0d-371c554e7c9b");
            Assert.IsNotNull(reqManager);
        }

        [TestMethod]
        public void TestValidaGui_ValidaGuid()
        {
            var requestManager = new RequestManager();
            var valida = requestManager.ValidaGuid("7e179018-5c07-42bd-9e0d-371c554e7c9b");
            Assert.IsTrue(valida);
        }
    }
}
