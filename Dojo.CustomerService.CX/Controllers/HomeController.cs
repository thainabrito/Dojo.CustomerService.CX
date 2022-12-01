using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dojo.CustomerService.CX.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "Home")]
        public ActionResult Index()
        {
            new MaterialApoioRepositorio().Database.add
            var lista = new MaterialApoioRepositorio().MateriaisApoio();
            // return StatusCode(200, new { Mensagem = "Bem vindo à api" });
            return StatusCode(200, lista);
        }


    }
}