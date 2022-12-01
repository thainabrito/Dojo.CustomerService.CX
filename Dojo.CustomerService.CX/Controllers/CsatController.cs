using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dojo.CustomerService.CX.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost]
        //public Tuple< HttpStatusCode, int > Post(int score)
        //{
        //    var validateScore = HttpStatusCode.NoContent;
        //    if (score < 1 || score > 5) validateScore = HttpStatusCode.InternalServerError;
        //    else validateScore = HttpStatusCode.OK;
        //    return new Tuple<HttpStatusCode, int> ( validateScore, 2 );
        //}

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