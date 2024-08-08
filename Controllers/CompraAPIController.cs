using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraAPIController : ControllerBase
    {
        [HttpPost("insertCompra")]
        public async Task<ActionResult<bool>> insertCompra(Compra reg)
        {
            var mensaje = await Task.Run(() => new CompraDAO().Registrar(reg));
            return Ok(mensaje);

        }

        [HttpGet("getCompra/{id}")]
        public async Task<ActionResult<bool>> getCompra(int id)
        {
            var mensaje = await Task.Run(() => new CompraDAO().ObtenerCompra(id));
            return Ok(mensaje);

        }
    }
}
