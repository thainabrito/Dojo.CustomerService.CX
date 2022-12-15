using Dojo.CustomerService.CX.Controllers;
using Dojo.CustomerService.CX.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using TechTalk.SpecFlow;

namespace Dojo.CustomerService.BDDTests.StepDefinitions
{
    [Binding]
    public class UpdateCSATStepDefinitions
    {
        public Csat Csat { get; set; }
        public CsatController CsatController { get; set; }
        public Guid Result { get; set; }

        [Given(@"o Id '([^']*)'")]
        public void GivenOId(Guid id)
        {
            Csat.Id = id;
        }

        [Given(@"o Score ‘(.*)'")]
        public void GivenOScore(int score)
        {
            Csat.Score = score;
        }

        [When(@"atualizada a avaliacao")]
        public async Task WhenAtualizadaAAvaliacao(string id)
        {
            await CsatController.Atualizar(id, Csat);

        }

        [Then(@"retornar o csatId")]
        public Guid ThenRetornarOCsatId()
        {
            return Csat.Id;
        }

        [Given(@"o Score '([^']*)'")]
        public void GivenOScore2(int score)
        {
            Csat.Score = score;
        }

        [When(@"nota não for válida")]
        public async Task  WhenNotaNaoForValida(Guid id)
        {
            await CsatController.Create(Csat);
        }

        [Then(@"retornar mensagem de nota inválida")]
        public async Task ThenRetornarMensagemDeNotaInvalida(string id)
        {
            await CsatController.Atualizar(id, Csat);
        }

        [Given(@"o Comment '([^']*)'")]
        public void GivenOComment(string comment)
        {
            Csat.Comment = comment;
        }


        [When(@"o comentario for nulo ou vazio")]
        public void WhenOComentarioForNuloOuVazio()
        {
            throw new PendingStepException();
        }

        [Then(@"retornar a mensagem de comentario deve ser preenchido")]
        public async Task ThenRetornarAMensagemDeComentarioDeveSerPreenchido(string id)
        {
            await CsatController.Atualizar(id, Csat);
        }

        [Given(@"o ProblemSolved 'true’")]
        public void GivenOProblemSolvedTrue(bool problemSolved)
        {
            Csat.ProblemSolved = problemSolved;
        }
    }
}
