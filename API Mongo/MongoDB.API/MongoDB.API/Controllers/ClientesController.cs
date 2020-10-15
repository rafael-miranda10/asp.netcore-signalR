using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.API.Data.Contracts;
using MongoDB.API.Domain.Entities;
using MongoDB.API.NotificationHub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IHubContext<ClienteHub> _hub;
        private const string groupId = "TheosGroup";
        public  ClientesController(
            IClienteRepository clienteRepository,
            IHubContext<ClienteHub> hub
            )
        {
            _hub = hub;
            _clienteRepository = clienteRepository;
        }

        [HttpGet(("todos-clientes"))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GeClientes()
        {
            var lista = await _clienteRepository.GetClientes();
            if (lista == null) return NotFound();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GeCliente(string id)
        {
            var cliente = await _clienteRepository.GetCliente(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPut]
        public async Task<ActionResult> PutCliente(Cliente cliente)
        {
            await _clienteRepository.Update(cliente);
            await _hub.Clients.Group(groupId)
                .SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
            //await _hub.Clients.All.SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostCliente(Cliente cliente)
        {
            await _clienteRepository.Add(cliente);
            await _hub.Clients.Group(groupId)
                .SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
           // await _hub.Clients.All.SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(string id)
        {
            await _clienteRepository.Delete(id);
            await _hub.Clients.Group(groupId)
                .SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
            //await _hub.Clients.All.SendAsync("TheosSignalR", await _clienteRepository.GetClientes());
            return Ok();
        }



    }
}
