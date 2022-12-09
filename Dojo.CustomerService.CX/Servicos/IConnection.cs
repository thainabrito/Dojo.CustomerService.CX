using Dojo.CustomerService.CX.Models;

namespace Dojo.CustomerService.CX.Servicos
{
    public interface IConnection
    {
        Task ApagarTudo();
        Task Atualizar(Csat materialApoio);
        Task<Csat> BuscaPorComment(string comment);
        Task<Csat> BuscaPorId(Guid id);
        Task Inserir(Csat csat);
        Task RemovePorId(Guid id);
        Task<IList<Csat>> Todos();
    }
}