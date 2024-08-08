using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAPIController : ControllerBase
    {
        [HttpGet("getUsuario/{correo}/{contraseña}")]
        public async Task<ActionResult<List<Usuario>>> getUsuario(string correo, string contraseña)
        {
            var lista = await Task.Run(() => new UsuarioDAO().Obtener(correo, contraseña));
            return Ok(lista);
        }

        [HttpPost("insertUsuario")]
        public async Task<ActionResult<int>> insertUsuario(Usuario reg)
        {
            var mensaje = await Task.Run(() => new UsuarioDAO().Registrar(reg));
            return Ok(mensaje);
        }

    }
}
