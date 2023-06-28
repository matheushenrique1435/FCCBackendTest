using FCCBackendTest.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FCCBackendTest.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        AutenticacaoUsuarioService _autenticacaoUsuarioService;

        public AutenticacaoController(AutenticacaoUsuarioService autenticacaoUsuarioService)
        {
            _autenticacaoUsuarioService = autenticacaoUsuarioService;
        }

        // POST api/<AutenticacaoController>
        [HttpPost]        
        public async Task<IActionResult> Post([FromBody] string CPF)
        {
            return Ok(await _autenticacaoUsuarioService.Login(CPF));
        }

        
    }
}
