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
            if (string.IsNullOrEmpty(id))
            {
                return StatusCode(400, new { mensagem = "Id não pode estar vazio" });
            }

            if (!_requestManager.ValidaGuid(id))
            {
                return StatusCode(400, new { mensagem = "id inválido" });
            }
            var idReturn = _requestManager.ConsultarCsat(id);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o id" });
            }
            return StatusCode(200, idReturn.Result);
        }

        [HttpPut("csat/comment/{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] string id, [FromBody] Csat csat)
        {
            var comment = csat.Comment;
            if (string.IsNullOrEmpty(id))
            {
                return StatusCode(400, new { mensagem = "O comentário deve ser preenchido" });
            }

            if (!_requestManager.ValidaGuid(id))
            {
                return StatusCode(400, new { mensagem = "id inválido" });
            }

            var idReturn = _requestManager.ConsultarCsat(id);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o id" });
            }
            _requestManager.AtualizarComment(idReturn, comment);
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

            if (!_requestManager.ValidaGuid(id))
            {
                return StatusCode(400, new { mensagem = "id inválido" });
            }

            var idReturn = _requestManager.ConsultarCsat(id);
            if (idReturn.Result == null)
            {
                return StatusCode(404, new { mensagem = "Não foi encontrado o id" });
            }
            _requestManager.AtualizarProblemSolved(idReturn, problemSolved);
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