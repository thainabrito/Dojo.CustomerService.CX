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
            var lista = await csatMongo.Todos();
            return StatusCode(200, lista);
        }

        [HttpPost("csat")]
        public async Task<ActionResult> Create([FromBody] Csat csat)
        {
            if (csat.Score < 1 || csat.Score > 5)
            {
                return StatusCode(400, new { mensagem = "Csat não pode ser menor que 1 e maior que 5" });
            }
            var csatMongo = new CSATMongodb();
            csat.CreatedAt = DateTime.Now;
            csatMongo.Inserir(csat);
            return StatusCode(200, csat);
        }
    }
}