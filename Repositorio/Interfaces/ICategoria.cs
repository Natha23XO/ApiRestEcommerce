using ApiRestProyecto.Models;

namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface ICategoria
    {
        List<Categoria> Listar();
        bool Registrar(Categoria oCategoria);
        bool Modificar(Categoria oCategoria);
        bool Eliminar(int id);
    }
}
