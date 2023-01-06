using Dojo.CustomerService.CX.Models;

namespace Dojo.CustomerService.Test.Models
{
    [TestClass]
    public class CsatTest
    {
        [TestMethod]
        public void TestandoClasseCsat()
        {
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;

            Assert.AreEqual(5, csat.Score);
            Assert.IsTrue(csat.ProblemSolved);
            Assert.AreEqual("teste para salvar", csat.Comment);
            Assert.AreEqual("maria@gmail.com", csat.AttendantEmail);
            Assert.IsNotNull(csat.CreatedAt);
        }
    }
}