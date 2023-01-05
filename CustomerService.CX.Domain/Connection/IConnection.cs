using Dojo.CustomerService.CX.Models;
using Dojo.CustomerService.CX.Services;

namespace Dojo.CustomerService.CX.Services
{
    public interface IConnection
    {
        Task ApagarTudo();
        Task Atualizar(Csat csat);
        Task<Csat> BuscaPorComment(string comment);
        Task<Csat> BuscaPorId(Guid id);
        Task Inserir(Csat csat);
        Task RemovePorId(Guid id);
        Task<IList<Csat>> Todos();
    }
}