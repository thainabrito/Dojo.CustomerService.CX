using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

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
            csat.Id = Guid.NewGuid();
            csatMongo.Inserir(csat);
            return StatusCode(201, csat);
        }

        //[HttpPut("csat/{id}")]
        //public async Task<ActionResult> Update([FromRoute] string id, [FromBody] Csat csat)
        //{
        //    var buscaId = new CSATMongodb();

        //    var idReturn = buscaId.BuscaPorId(csat.Id);
        //    if (idReturn == null)
        //    {
        //        return StatusCode(400, new { mensagem = "id não pode ser nulo" });
        //    }
        //    return StatusCode(200);
        //}

        [HttpGet("csat/{id}")]
        public async Task<ActionResult> Consultar([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return StatusCode(400, new { mensagem = "Id não pode estar vazio" });
            }
            var guidOutput = new Guid();
            bool isValid = Guid.TryParse(id, out guidOutput);
            if (!isValid)
            {
                return StatusCode(400, new { mensagem = "id inválido" });
            }

            var buscaId = new CSATMongodb();

            var idReturn = buscaId.BuscaPorId(guidOutput);
            if (idReturn.Result == null)
            {
                return StatusCode(400, new { mensagem = "Não foi encontrado o csat" });
            }
            return StatusCode(200, idReturn.Result);
        }
    }
}