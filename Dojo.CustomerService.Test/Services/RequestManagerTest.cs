using Dojo.CustomerService.CX.Services;
using MongoDB.Driver.Core.Connections;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConnection = Dojo.CustomerService.CX.Services.IConnection;

namespace Dojo.CustomerService.CX.UnitTests.Services
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
        public void TestValidaGuid_ValidaGuid()
        {
            var requestManager = new RequestManager();
            var valida = requestManager.ValidaGuid("7e179018-5c07-42bd-9e0d-371c554e7c9b");
            Assert.IsTrue(valida);
        }

        [TestMethod]
        public void TestAtualizarComment_AtualizaComment()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.Atualizar(It.IsAny<Csat>())).Returns(Task.FromResult(csat));
            dbMock.Setup(x => x.BuscaPorId(It.IsAny<Guid>())).Returns(Task.FromResult(csat));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);

            var reqManager = requestManager.ConsultarCsat("7e179018-5c07-42bd-9e0d-371c554e7c9b");

            var retorno = requestManager.AtualizarComment(reqManager, "comentario");
            Assert.IsTrue(retorno);
        }

        [TestMethod]
        public void TestAtualizarProblemSolved_AtualizaProblemSolved()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.Atualizar(It.IsAny<Csat>())).Returns(Task.FromResult(csat));
            dbMock.Setup(x => x.BuscaPorId(It.IsAny<Guid>())).Returns(Task.FromResult(csat));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);

            var reqManager = requestManager.ConsultarCsat("7e179018-5c07-42bd-9e0d-371c554e7c9b");

            var retorno = requestManager.AtualizarProblemSolved(reqManager, true);
            Assert.IsTrue(retorno);
        }

        [TestMethod]
        public void TestRetornaLista()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            IList<Csat> lst = new List<Csat>();
            lst.Add(csat);

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.Todos()).Returns(Task.FromResult(lst));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);

            var reqManager = requestManager.Lista(5, true, "maria@gmail.com");

            Assert.AreEqual(lst.Count, reqManager.Count);
        }

        [TestMethod]
        public void TestRelatorio()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";

            IList<Csat> lst = new List<Csat>();
            lst.Add(csat);

            var mocker = new AutoMocker();
            var dbMock = mocker.GetMock<IConnection>();
            dbMock.Setup(x => x.Todos()).Returns(Task.FromResult(lst));
            var mongoDb = dbMock.Object;

            var requestManager = new RequestManager();
            requestManager.SetCSATMongodbInstance(mongoDb);

            var reqManager = requestManager.Relatorio("maria@gmail.com", null);
            Assert.AreEqual(reqManager.Score, 1);
        }
    }
}
