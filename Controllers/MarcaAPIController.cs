using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaAPIController : ControllerBase
    {
        [HttpGet("getMarcas")]
        public async Task<ActionResult<List<Marca>>> getMarcas()
        {
            var lista = await Task.Run(() => new MarcaDAO().Listar());
            return Ok(lista);
        }

        [HttpPost("insertMarca")]
        public async Task<ActionResult<bool>> insertMarca(Marca reg)
        {
            var mensaje = await Task.Run(() => new MarcaDAO().Registrar(reg));
            return Ok(mensaje);

        }

        [HttpPut("updateMarca")]
        public async Task<ActionResult<bool>> updateMarca(Marca reg)
        {
            var mensaje = await Task.Run(() => new MarcaDAO().Modificar(reg));
            return Ok(mensaje);

        }

        [HttpDelete("deleteMarca")]
        public async Task<ActionResult<bool>> deleteMarca(int id)
        {
            var mensaje = await Task.Run(() => new MarcaDAO().Eliminar(id));
            return Ok(mensaje);

        }
    }
}
