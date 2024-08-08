using ApiRestProyecto.Models;

namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface IProducto
    {
        List<Producto> Listar();
        int Registrar(Producto oProducto);
        bool Modificar(Producto oProducto);
        bool ActualizarRutaImagen(Producto oProducto);
        bool Eliminar(int id);
    }
}
