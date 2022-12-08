using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dojo.CustomerService.CX.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Dojo.CustomerService.CX.Servicos
{
    public class CSATMongodb
    {
        public static string DataBase = "DojoCustomerService";
        private IMongoDatabase mongoDatabase;
        public CSATMongodb()
        {
            var cnn = "mongodb://localhost:27017";
            this.mongoDatabase = new MongoClient(cnn).GetDatabase(CSATMongodb.DataBase);

        }

        private IMongoCollection<Csat> mongoCollection()
        {
            return this.mongoDatabase.GetCollection<Csat>("Csat");
        }

        public async Task Inserir(Csat csat)
        {
            await this.mongoCollection().InsertOneAsync(csat);
        }

        public async Task Atualizar(Csat materialApoio)
        {
            await this.mongoCollection().ReplaceOneAsync(i => i.Id == materialApoio.Id, materialApoio);
        }

        public async Task RemovePorId(Guid id)
        {
            await this.mongoCollection().DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IList<Csat>> Todos()
        {
            return await this.mongoCollection().AsQueryable().ToListAsync();
        }

        public async Task<Csat> BuscaPorId(Guid id)
        {
            foreach (var item in mongoCollection().AsQueryable().ToList())
            {
                if (item.Id.ToString() == id.ToString())
                {
                    return item;
                }
            }
            return null;
        }

        public async Task<Csat> BuscaPorComment(string comment)
        {
            return await this.mongoCollection().AsQueryable().Where(p => p.Comment == comment).FirstAsync();
        }

        public async Task ApagarTudo()
        {
            var itens = await Todos();
            foreach(var item in itens)
            {
                await this.mongoCollection().DeleteOneAsync(p => p.Id == item.Id);
            }
        }
    }
}
