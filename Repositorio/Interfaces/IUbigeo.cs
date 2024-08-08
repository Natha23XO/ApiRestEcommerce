using ApiRestProyecto.Models;

namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface IUbigeo
    {
        List<Departamento> ObtenerDepartamento();
        List<Provincia> ObtenerProvincia(string _iddepartamento);
        List<Distrito> ObtenerDistrito(string _idprovincia, string _iddepartamento);
    }
}
