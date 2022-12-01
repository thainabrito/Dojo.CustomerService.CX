using Dojo.CustomerService.CX.Controllers;

namespace DojoCustomerServiceTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // var project = new Dojo.CustomerService.CX.Controllers.CsatController();
            var project = new CsatController();
            Assert.Pass();
        }
    }
}