using ApiRestProyecto.Models;

namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface IMarca
    {
        List<Marca> Listar();
        bool Registrar(Marca oMarca);
        bool Modificar(Marca oMarca);
        bool Eliminar(int id);
    }
}
