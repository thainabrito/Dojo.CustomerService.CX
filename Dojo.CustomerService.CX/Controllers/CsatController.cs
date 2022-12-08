using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Dojo.CustomerService.CX.Controllers
{
    [ApiController]
    [Route("/")]
    public class CsatController : ControllerBase
    {
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

            var csatMongoDB = new CSATMongodb();

            var idReturn = csatMongoDB.BuscaPorId(guidOutput);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o csat" });
            }
            return StatusCode(200, idReturn.Result);
        }

        [HttpPut("csat/comment/{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] string id, [FromBody] Csat csat)
        {
            var comment = csat.Comment;
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

            var csatMongoDB = new CSATMongodb();

            var idReturn = csatMongoDB.BuscaPorId(guidOutput);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o csat" });
            }
            var csatReturn = idReturn.Result;
            csatReturn.Comment = comment;
            csatMongoDB.Atualizar(csatReturn);
            return StatusCode(204);
        }

        [HttpPut("csat/problemSolved/{id}")]
        public async Task<ActionResult> AtualizarProblemSolved([FromRoute] string id, [FromBody] Csat csat)
        {
            var problemSolved = csat.ProblemSolved;
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

            var csatMongoDB = new CSATMongodb();

            var idReturn = csatMongoDB.BuscaPorId(guidOutput);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o csat" });
            }
            var csatReturn = idReturn.Result;
            csatReturn.ProblemSolved = problemSolved;
            csatMongoDB.Atualizar(csatReturn);
            return StatusCode(204);
        }

        [HttpGet("csat/listar")]
        public async Task<ActionResult> Index(int? score = null, bool? problemSolved = null, string? attendantEmail = "")
        {
            var csatMongo = new CSATMongodb();
            var lista = await csatMongo.Todos();

            if (score >= 1 && score <= 5)
            {
                lista = lista.Where(s => s.Score == score).ToList();
            }

            if (problemSolved != null)
            {
                lista = lista.Where(p => p.ProblemSolved == problemSolved).ToList();
            }

            if (attendantEmail != "")
            {
                lista = lista.Where(a => a.AttendantEmail == attendantEmail).ToList();
            }

            return StatusCode(200, lista);
        }

        [HttpGet("csat/listar/{attendantEmail}")]
        public async Task<ActionResult> Index([FromRoute] string attendantEmail, DateTime? createdAt)
        {
            var csatMongo = new CSATMongodb();
            var lista = await csatMongo.Todos();

            if (createdAt != null)
            {
                lista = lista.Where(s => s.CreatedAt == createdAt).ToList();
            }

            lista = lista.Where(a => a.AttendantEmail == attendantEmail).ToList();

            if (lista.Count == 0)
            {
                return StatusCode(204);
            }
            int contaPromotores = lista.Where(a => a.Score == 5).Count();
            int contaTotal = lista.Count();
            decimal calculoScore = (decimal)contaPromotores / contaTotal;

            int contaPositivos = lista.Where(a => a.ProblemSolved == true).Count();
            int contaNegativos = contaTotal - contaPositivos;

            var problemSolved = new ProblemSolved()
            {
                Total = contaTotal,
                Negatives = contaNegativos,
                Positives = contaPositivos
            };

            var summary = new Summary()
            {
                Score = calculoScore,
                Fcr = problemSolved
            };
            return StatusCode(200, summary);
        }
    }
}