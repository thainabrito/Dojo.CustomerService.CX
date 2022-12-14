using Dojo.CustomerService.CX.Controllers;
using Dojo.CustomerService.CX.Models;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Dojo.CustomerService.CX.Specs.Steps
{
    [Binding]
    public class CreatecsatSteps
    {
        public Csat Csat { get; set; }
        public CsatController CsatController { get; set; }
        public Guid Result { get; set; }

        [Given(@"o Id '([^']*)'")]
        public void GivenOId(Guid id)
        {
            Csat.Id = id;
        }

        [Given(@"o Comment '([^']*)'")]
        public void GivenOComment(string comment)
        {
            Csat.Comment = comment;
        }

        [Given(@"o Score '([^']*)'")]
        public void GivenOScore(int score)
        {
            Csat.Score = score;
        }

        [Given(@"o ProblemSolved '([^']*)'")]
        public void GivenOProblemSolved(bool problemSolved)
        {
            Csat.ProblemSolved = problemSolved;
        }

        [When(@"criada a avaliacao")]
        public async Task WhenCriadaAAvaliacaoAsync()
        {
            var result = await CsatController.Create(Csat);
            result.Should().NotBeNull();
        }

        [Then(@"retornar o csatId")]
        public void ThenRetornarOCsatId()
        {
            Csat.Id.Should().NotBeEmpty();
        }
    }
}
