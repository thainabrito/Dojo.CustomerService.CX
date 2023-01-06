using Dojo.CustomerService.CX.Models;

namespace Dojo.CustomerService.CX.UnitTests.UseCases
{
    [TestClass]
    public class ProblemSolvedTest
    {
        [TestMethod]
        public void TestandoClasseProblemSolved()
        {
            var problemSolved = new ProblemSolved();
            problemSolved.Total = 10;
            problemSolved.Positives = 5;
            problemSolved.Negatives = 5;

            Assert.AreEqual(10, problemSolved.Total);
            Assert.AreEqual(5, problemSolved.Positives);
            Assert.AreEqual(5, problemSolved.Negatives);
        }
    }
}
