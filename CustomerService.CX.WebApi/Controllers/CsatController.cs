using Dojo.CustomerService.CX.Business;
using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Services;
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
        private RequestManager _requestManager;
        public CsatController(): base() {
            _requestManager = new RequestManager();
        }

        [HttpPost("csat")]
        public async Task<ActionResult> Create([FromBody] Csat csat)
        {
            if (csat.Score < 1 || csat.Score > 5)
            {
                return StatusCode(400, new { mensagem = "Score não pode ser menor que 1 ou maior que 5" });
            }
            var newCsat = _requestManager.CreateCsat(csat);
            return StatusCode(201, newCsat);
        }

        [HttpGet("csat/{id}")]
        public async Task<ActionResult> Consultar([FromRoute] string id)
        {
            var result = Validation.ValidateId(id, _requestManager);
            return StatusCode(result.Code, result.Message);
        }

        [HttpPut("csat/comment/{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] string id, [FromBody] Csat csat)
        {
            var comment = csat.Comment;
            if (string.IsNullOrEmpty(comment))
            {
                return StatusCode(400, new { mensagem = "O comentário deve ser preenchido" });
            }
            var result = Validation.ValidateId(id, _requestManager);

            if (result.Status == false)
            {
                return StatusCode(result.Code, result.Message);
            }

            _requestManager.AtualizarComment((Task <Csat>)result.Message, comment);
            return StatusCode(204);
        }

        [HttpPut("csat/problemSolved/{id}")]
        public async Task<ActionResult> AtualizarProblemSolved([FromRoute] string id, [FromBody] Csat csat)
        {
            var problemSolved = csat.ProblemSolved;

            var result = Validation.ValidateId(id, _requestManager);

            if (result.Status == false)
            {
                return StatusCode(result.Code, result.Message);
            }
            _requestManager.AtualizarProblemSolved((Task<Csat>)result.Message, problemSolved);
            return StatusCode(204);
        }

        [HttpGet("csat/listar")]
        public async Task<ActionResult> Index(int? score = null, bool? problemSolved = null, string? attendantEmail = "")
        {
            var lista = _requestManager.Lista(score, problemSolved, attendantEmail);
            return StatusCode(200, lista);
        }

        [HttpGet("csat/listar/{attendantEmail}")]
        public async Task<ActionResult> Relatorio([FromRoute] string attendantEmail, DateTime? createdAt)
        {
            var summary = _requestManager.Relatorio(attendantEmail, createdAt);

            if (summary == null)
            {
                return StatusCode(204);
            }
            return StatusCode(200, summary);
        }
    }
}