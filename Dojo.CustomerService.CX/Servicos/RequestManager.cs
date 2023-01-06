using Dojo.CustomerService.CX.Models;

namespace Dojo.CustomerService.CX.Servicos
{
    public class RequestManager
    {
        private IConnection _csatMongodbInstance;
        public IConnection GetCSATMongodbInstance()
        {
            if (_csatMongodbInstance is null)
            {
                _csatMongodbInstance = new CSATMongodb();
            }
            return _csatMongodbInstance;
        }

        public void SetCSATMongodbInstance(IConnection instance)
        {
            _csatMongodbInstance = instance;
        }
        public Csat CreateCsat(Csat csat)
        {
            csat.CreatedAt = DateTime.Now;
            csat.Id = Guid.NewGuid();
            var task = GetCSATMongodbInstance().Inserir(csat);
            if (task.IsCompletedSuccessfully)
            {
                return csat;
            } 
            return null;
        }

        public Task<Csat> ConsultarCsat(string id)
        {
            var idReturn = GetCSATMongodbInstance().BuscaPorId(new Guid(id));
            return idReturn;
        }

        public bool ValidaGuid(string palavra)
        {
            var guidOutput = new Guid();
            bool isValid = Guid.TryParse(palavra, out guidOutput);
            return isValid;
        }

        public bool AtualizarComment(Task<Csat> idReturn, string comment)
        {
            var csatReturn = idReturn.Result;
            csatReturn.Comment = comment;
            var atualizaComment = GetCSATMongodbInstance().Atualizar(csatReturn);
            return true;
        }

        public bool AtualizarProblemSolved(Task<Csat> idReturn, bool problemSolved)
        {
            var csatReturn = idReturn.Result;
            csatReturn.ProblemSolved = problemSolved;
            var atualizaProblemSolved = GetCSATMongodbInstance().Atualizar(csatReturn);
            return true;
        }

        public IList<Csat> Lista(int? score, bool? problemSolved, string? attendantEmail)
        {

            var lista = GetCSATMongodbInstance().Todos().Result;

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

            return lista;
        }

        public Summary Relatorio(string attendantEmail, DateTime? createdAt)
        {
            var lista = GetCSATMongodbInstance().Todos().Result;

            if (createdAt != null)
            {
                lista = lista.Where(s => s.CreatedAt == createdAt).ToList();
            }

            lista = lista.Where(a => a.AttendantEmail == attendantEmail).ToList();

            if (lista.Count == 0)
            {
                return null;
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

            return summary;
        }

    }
}
