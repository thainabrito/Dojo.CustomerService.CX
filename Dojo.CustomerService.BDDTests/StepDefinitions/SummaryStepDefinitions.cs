using Dojo.CustomerService.CX.Controllers;
using Dojo.CustomerService.CX.Models;
using System;
using TechTalk.SpecFlow;

namespace Dojo.CustomerService.BDDTests.StepDefinitions
{
    [Binding]
    public class SummaryStepDefinitions
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
        public async Task WhenSolicitadoOSumario()
        {
            await CsatController.Relatorio(Csat);
        }

        [Then(@"deve retornar a lista de csat")]
        public async Task<object> ThenDeveRetornarAListaDeCsat()
        {
            return CsatController.Index;
        }

        [Then(@"deve retornar o score")]
        public async Task<int> ThenDeveRetornarOScore()
        {
            return Csat.Score;
        }

        [Then(@"deve retornar o FCR")]
        public async Task<bool> ThenDeveRetornarOFCR()
        {
            return Csat.ProblemSolved;
        }
    }
}
