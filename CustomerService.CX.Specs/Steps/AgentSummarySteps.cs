using Dojo.CustomerService.CX.Controllers;
using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Services;
using System;
using TechTalk.SpecFlow;

namespace Dojo.CustomerService.CX.Specs.Steps
{
    [Binding]
    public class AgentSummarySteps
    {
        public Csat Csat { get; set; }
        public CsatController CsatController { get; set; }
        public Guid Result { get; set; }

        [Given(@"o AttendantEmail '([^']*)'")]
        public void GivenOAttendantEmail(string AttendantEmail)
        {
            Csat.AttendantEmail = AttendantEmail;
        }

        [Given(@"o InitialDate '([^']*)'")]
        public void GivenOInitialDate(DateTime initialDate)
        {
            Csat.InitialDate = initialDate;
        }

        [Given(@"o EndDate '([^']*)'")]
        public void GivenOEndDate(DateTime endDate)
        {
            Csat.EndDate = endDate;
        }

        [When(@"solicitado o sumario")]
        public async Task WhenSolicitadoOSumario(string attendantEmail)
        {
            await CsatController.Relatorio(attendantEmail, null);
        }

        [Then(@"deve retornar a lista de csat")]
        public void ThenDeveRetornarAListaDeCsat()
        {
            var resultado = CsatController.Index();
            resultado.Should().NotBeNull();
        }

        [Then(@"deve retornar o score")]
        public void ThenDeveRetornarOScore()
        {
            var score = Csat.Score;
            score.Should().BeGreaterThan(0);
            score.Should().BeLessThan(6);
        }

        [Then(@"deve retornar o FCR")]
        public void ThenDeveRetornarOFCR()
        {
            Csat.Should().NotBeNull();
            var problemSolved = Csat.ProblemSolved;
            problemSolved.Should().Be(true);
        }
    }
}
