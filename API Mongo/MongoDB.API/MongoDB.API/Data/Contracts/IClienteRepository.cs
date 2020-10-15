using MongoDB.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.API.Data.Contracts
{
    public interface IClienteRepository
    {
        Task Add(Cliente cliente);
        Task Update(Cliente cliente);
        Task Delete(string id);
        Task<Cliente> GetCliente(string id);
        Task<List<Cliente>> GetClientes();
    }
}
