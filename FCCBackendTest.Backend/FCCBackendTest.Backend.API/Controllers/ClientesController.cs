using FCCBackendTest.Backend.Entities;
using FCCBackendTest.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCCBackendTest.Backend.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_clientesService.Listar());
        }

        [HttpGet]
        [Route("ListarPorId/{id}")]
        public IActionResult ListarPorId(int id) 
        {
            Clientes cliente = _clientesService.ListarPorId(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpGet]
        [Route("ListarPorCPF/{CPF}")]
        public IActionResult ListarPorCPF(string CPF)
        {
            Clientes cliente = _clientesService.ListarPorCPF(CPF);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Clientes cliente)
        {
            _clientesService.Criar(cliente);

            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar([FromBody] Clientes cliente)
        {
            _clientesService.Alterar(cliente);

            return Ok();    
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            _clientesService.Excluir(id);

            return Ok();
        }
    }
}
