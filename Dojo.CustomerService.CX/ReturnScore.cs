using System.Net;

namespace Dojo.CustomerService.CX
{
    public class ReturnScore
    {
        public HttpStatusCode validateScore { get; set; }
        public int idRegistration { get; set; }
    }
}
