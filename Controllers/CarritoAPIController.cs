using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoAPIController : ControllerBase
    {
        [HttpGet("getCantidad/{id}")]
        public async Task<ActionResult<int>> getCantidad(int id)
        {
            var lista = await Task.Run(() => new CarritoDAO().Cantidad(id));
            return Ok(lista);
        }

        [HttpDelete("eliminarCarrito")]
        public async Task<ActionResult<bool>> eliminarCarrito(string IdCarrito, string IdProducto)
        {
            var mensaje = await Task.Run(() => new CarritoDAO().Eliminar(IdCarrito, IdProducto));
            return Ok(mensaje);

        }

        [HttpGet("getCarrito/{id}")]
        public async Task<ActionResult<List<Carrito>>> getCarrito(int id)
        {
            var lista = await Task.Run(() => new CarritoDAO().Obtener(id));
            return Ok(lista);

        }
        [HttpGet("getCompra/{id}")]
        public async Task<ActionResult<List<Compra>>> getCompra(int id)
        {
            var lista = await Task.Run(() => new CarritoDAO().ObtenerCompra(id));
            return Ok(lista);

        }
        [HttpPost("insertCarrito")]
        public async Task<ActionResult<int>> insertCarrito(Carrito carrito)
        {
            var mensaje = await Task.Run(() => new CarritoDAO().Registrar(carrito));
            return Ok(mensaje);

        }
    }
}
