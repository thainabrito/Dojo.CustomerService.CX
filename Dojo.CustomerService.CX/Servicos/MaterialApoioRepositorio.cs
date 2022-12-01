using Dojo.CustomerService.CX.Models;
using MongoDB.Driver;
using System;

namespace Dojo.CustomerService.CX.Servicos
{
    public class MaterialApoioRepositorio
    {
        private static List<MaterialApoio> materiaisDeApoio = new List<MaterialApoio>();
        public static void Adicionar(MaterialApoio materialApoio)
        {
            materiaisDeApoio.Add(materialApoio);
        }

        public static List<MaterialApoio> Todos()
        {
            return materiaisDeApoio;
        }

        public MongoDatabase Database;

        public MaterialApoioRepositorio()
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017");

            var server = dbClient.GetServer();
            this.Database = server.GetDatabase("DojoCustomerService");
        }

        public MongoCollection<MaterialApoio> MateriaisApoio()
        {
            var materiaisApoio = Database.GetCollection<MaterialApoio>("MaterialApoio");
            return materiaisApoio;
        }

    }
}
