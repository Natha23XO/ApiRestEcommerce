using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbigeoAPIController : ControllerBase
    {
        [HttpGet("getDepartamento")]
        public async Task<ActionResult<List<Departamento>>> getDepartamento()
        {
            var lista = await Task.Run(() => new UbigeoDAO().ObtenerDepartamento());
            return Ok(lista);
        }

        [HttpGet("getProvincia/{iddepartamento}")]
        public async Task<ActionResult<List<Provincia>>> getProvincia(string iddepartamento)
        {
            var lista = await Task.Run(() => new UbigeoDAO().ObtenerProvincia(iddepartamento));
            return Ok(lista);
        }

        [HttpGet("getDistrito/{idprovincia}/{iddepartamento}")]
        public async Task<ActionResult<List<Distrito>>> getDistrito(string idprovincia, string iddepartamento)
        {
            var lista = await Task.Run(() => new UbigeoDAO().ObtenerDistrito(idprovincia,iddepartamento));
            return Ok(lista);
        }
    }
}
