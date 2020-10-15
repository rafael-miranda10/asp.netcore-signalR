using MongoDB.API.Data.Contracts;
using MongoDB.API.Domain.Entities;
using MongoDB.Driver;

namespace MongoDB.API.Data.Context
{
    public class ClienteContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public ClienteContext(IClientesStoreDatabaseSettings settings)
        {
            var mdbClient = new MongoClient("mongodb://localhost:27017");//settings.ConnectionString);
            _mongoDatabase = mdbClient.GetDatabase("ClientesStoreDB");//settings.DatabaseName);
            //var database = mdbClient.GetDatabase(settings.DatabaseName);
            // _clientesCollection = database.GetCollection<Cliente>(settings.ClientesCollectionName);
        }

        //private readonly IMongoCollection<Cliente> _clientesCollection;

        public IMongoCollection<Cliente> Clientes
        {
            get
            {
                return _mongoDatabase.GetCollection<Cliente>("Clientes");
            }
        }
    }
}
