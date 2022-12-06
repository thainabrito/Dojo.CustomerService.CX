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
        [HttpGet("csat")]
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
                return StatusCode(400, new { mensagem = "Csat não pode ser menor que 1 ou maior que 5" });
            }
            var csatMongo = new CSATMongodb();
            csat.CreatedAt = DateTime.Now;
            csatMongo.Inserir(csat);
            return StatusCode(201, csat);
        }

        [HttpPut("csat/{id}")]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] Csat csat)
        {
            // fazer ação do código para o update
            return StatusCode(200);
        }
    }
}