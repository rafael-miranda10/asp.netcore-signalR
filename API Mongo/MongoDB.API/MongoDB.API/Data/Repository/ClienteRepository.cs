using MongoDB.API.Data.Context;
using MongoDB.API.Data.Contracts;
using MongoDB.API.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private ClienteContext _clienteContext;
        public ClienteRepository(ClienteContext clienteContext)
        {
            _clienteContext = clienteContext;
        }

        public async Task Add(Cliente cliente)
        {
            await _clienteContext.Clientes.InsertOneAsync(cliente);
        }

        public async Task Delete(string id)
        {
            FilterDefinition<Cliente> filtro = Builders<Cliente>.Filter.Eq("Id", id);
            await _clienteContext.Clientes.DeleteOneAsync(filtro);
        }

        public async Task<Cliente> GetCliente(string id)
        {
            FilterDefinition<Cliente> filtro = Builders<Cliente>.Filter.Eq("Id", id);
            return await _clienteContext.Clientes.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _clienteContext.Clientes.Find(_ => true).ToListAsync();
        }

        public async Task Update(Cliente cliente)
        {
            await _clienteContext.Clientes.ReplaceOneAsync(x=>x.Id == cliente.Id, cliente);
        }
    }
}
