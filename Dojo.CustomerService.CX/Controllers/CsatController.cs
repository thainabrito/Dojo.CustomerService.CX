using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dojo.CustomerService.CX.Controllers
{
    [ApiController]
    [Route("/")]
    public class CsatController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CsatController> _logger;

        public CsatController(ILogger<CsatController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Csat")]
        public async Task<ActionResult> Index()
        {
            var csatMongo = new CSATMongodb();
            //var csat = new MaterialApoio();
            //csat.Score = 5;
            //csat.ProblemSolved = true;
            //csat.Comment = "teste para salvar";
            //csat.AttendantEmail = "maria@gmail.com";
            //csat.CreatedAt = DateTime.Now;
            // csat.Inserir(csat);
            var lista = await csatMongo.Todos();
            return StatusCode(200, lista);
        }

        [HttpPost("csat")]
        public async Task<ActionResult> Create()
        {
            var csatMongo = new CSATMongodb();
            var csat = new Csat();
            csat.Score = 5;
            csat.ProblemSolved = true;
            csat.Comment = "teste para salvar";
            csat.AttendantEmail = "maria@gmail.com";
            csat.CreatedAt = DateTime.Now;
            csatMongo.Inserir(csat);
            return StatusCode(200, csat);
        }

        [HttpPost]
        public ReturnScore Post(Guid id, int score, string comment, bool problemSolved, string attendantEmail, DateTime createdAt)
        {
            var validateScore = HttpStatusCode.NoContent;
            if (score < 1 || score > 5) validateScore = HttpStatusCode.InternalServerError;
            else validateScore = HttpStatusCode.OK;
            return new ReturnScore() { validateScore = validateScore, idRegistration = 2 };
        }
    }
}