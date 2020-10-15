using Microsoft.AspNetCore.SignalR;
using MongoDB.API.Data.Contracts;
using MongoDB.API.Domain.Entities;
using System.Threading.Tasks;

namespace MongoDB.API.NotificationHub
{
    public class ClienteHub : Hub
    {
        private const string groupId = "TheosGroup";
        private readonly IClienteRepository _clienteRepository;

        public ClienteHub(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Mensagem", "Conectado com sucesso!");
        }

        public async Task SubscribeToGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            await Clients.Caller.SendAsync("Mensagem", "Adicionado ao grupo com sucesso!");
        }

        public async Task AdicionarNovoCliente(Cliente cliente)
        {
            await _clienteRepository.Add(cliente);
            await Clients.Group(groupId)
               .SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
        }
    }
}
