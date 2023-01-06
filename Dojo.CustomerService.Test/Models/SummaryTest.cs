using Dojo.CustomerService.CX.Models;

namespace Dojo.CustomerService.Test.Models
{
    [TestClass]
    public class SummaryTest
    {
        [TestMethod]
        public void TestandoClasseSummary()
        {
            var problemSolved = new ProblemSolved();
            problemSolved.Total = 10;
            problemSolved.Positives = 5;
            problemSolved.Negatives = 5;

            var summary = new Summary();
            summary.Score = 10;
            summary.Fcr = problemSolved;

            Assert.AreEqual(10, summary.Fcr.Total);
            Assert.AreEqual(5, summary.Fcr.Positives);
            Assert.AreEqual(5, summary.Fcr.Negatives);
            Assert.AreEqual(10, summary.Score);

        }
    }
}
