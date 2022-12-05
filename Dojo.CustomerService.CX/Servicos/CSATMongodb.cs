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
        private IMongoDatabase mongoDatabase;
        public CSATMongodb()
        {
            var cnn = "mongodb://localhost:27017";
            this.mongoDatabase = new MongoClient(cnn).GetDatabase("DojoCustomerService");
        }

        private IMongoCollection<Csat> mongoCollection()
        {
            return this.mongoDatabase.GetCollection<Csat>("Csat");
        }

        public async void Inserir(Csat csat)
        {
            await this.mongoCollection().InsertOneAsync(csat);
        }

        public async void Atualizar(Csat materialApoio)
        {
            await this.mongoCollection().ReplaceOneAsync(i => i.Id == materialApoio.Id, materialApoio);
        }

        public async void RemovePorId(ObjectId id)
        {
            await this.mongoCollection().DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IList<Csat>> Todos()
        {
            return await this.mongoCollection().AsQueryable().ToListAsync();
        }

        public async Task<Csat> BuscaPorId(ObjectId id)
        {
            return await this.mongoCollection().AsQueryable().Where(p => p.Id == id).FirstAsync();
        }
    }
}
