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
    }
}
