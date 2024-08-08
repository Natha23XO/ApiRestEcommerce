using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaAPIController : ControllerBase
    {
        [HttpGet("getCategorias")]
        public async Task<ActionResult<List<Categoria>>> getCategorias()
        {
            var lista = await Task.Run(() => new CategoriaDAO().Listar());
            return Ok(lista);
        }

        [HttpPost("insertCategoria")]
        public async Task<ActionResult<bool>> insertCategoria(Categoria reg)
        {
            var mensaje = await Task.Run(() => new CategoriaDAO().Registrar(reg));
            return Ok(mensaje);

        }

        [HttpPut("updateCategoria")]
        public async Task<ActionResult<bool>> updateCategoria(Categoria reg)
        {
            var mensaje = await Task.Run(() => new CategoriaDAO().Modificar(reg));
            return Ok(mensaje);

        }

        [HttpDelete("deleteCategoria")]
        public async Task<ActionResult<bool>> deleteCategoria(int id)
        {
            var mensaje = await Task.Run(() => new CategoriaDAO().Eliminar(id));
            return Ok(mensaje);

        }
    }
}
