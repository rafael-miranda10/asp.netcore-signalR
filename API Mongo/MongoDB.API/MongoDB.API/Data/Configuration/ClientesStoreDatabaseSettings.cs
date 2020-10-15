using MongoDB.API.Data.Contracts;

namespace MongoDB.API.Data.Configuration
{
    public class ClientesStoreDatabaseSettings : IClientesStoreDatabaseSettings
    {
        public string ClientesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
