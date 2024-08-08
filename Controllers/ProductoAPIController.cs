using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoAPIController : ControllerBase
    {
        [HttpGet("getProductos")]
        public async Task<ActionResult<List<Producto>>> getProductos()
        {
            var lista = await Task.Run(() => new ProductoDAO().Listar());
            return Ok(lista);
        }

        [HttpPost("insertProducto")]
        public async Task<ActionResult<int>> insertProducto(Producto reg)
        {
            var mensaje = await Task.Run(() => new ProductoDAO().Registrar(reg));
            return Ok(mensaje);

        }

        [HttpPut("updateProducto")]
        public async Task<ActionResult<bool>> updateProducto(Producto reg)
        {
            var mensaje = await Task.Run(() => new ProductoDAO().Modificar(reg));
            return Ok(mensaje);

        }
        [HttpPut("updateRutaProducto")]
        public async Task<ActionResult<bool>> updateRutaProducto(Producto reg)
        {
            var mensaje = await Task.Run(() => new ProductoDAO().ActualizarRutaImagen(reg));
            return Ok(mensaje);

        }

        [HttpDelete("deleteProducto/{id}")]
        public async Task<ActionResult<bool>> deleteProducto(int id)
        {
            var mensaje = await Task.Run(() => new ProductoDAO().Eliminar(id));
            return Ok(mensaje);

        }
    }
}
