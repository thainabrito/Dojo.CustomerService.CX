using Dojo.CustomerService.CX.Services;

namespace Dojo.CustomerService.CX.Business
{
    public static class Validation
    {
        public static Result ValidateId(string id, RequestManager requestManager)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Result(400, new { mensagem = "Id não pode estar vazio" }, false);
            }

            if (!requestManager.ValidaGuid(id))
            {
                return new Result(400, new { mensagem = "id inválido" }, false);
            }
            var idReturn = requestManager.ConsultarCsat(id);
            if (idReturn.Result == null)
            {
                return new Result(404, new { mensagem = "Não foi encontrado o id" }, false);
            }
            return new Result(200, idReturn, true);
        }
    }

    public class Result
    {
        public Result(int code, bool status)
        {
            Code = code;
            Status = status;
        }
        public Result(int code, object message, bool status)
        {
            Code = code;
            Message = message;
            Status = status;
        }

        public int Code { get; set; }
        public object Message { get; set; }
        public bool Status { get; set; }
    }
}
