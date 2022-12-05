using Dojo.CustomerService.CX.Controllers;
using Dojo.CustomerService.CX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoCustomerServiceTest.Models
{
    internal class CsatTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestandoModelo()
        {
            var csat = new Csat();
            csat.Score = 5; // 22 a 27 se existem as propriedades
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
