namespace MongoDB.API.Data.Contracts
{
    public interface IClientesStoreDatabaseSettings
    {
        string ClientesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
