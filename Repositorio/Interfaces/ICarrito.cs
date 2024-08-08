using ApiRestProyecto.Models;


namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface ICarrito
    {
        int Registrar(Carrito oCarrito);
        int Cantidad(int idusuario);
        List<Carrito> Obtener(int _idusuario);
        bool Eliminar(string IdCarrito, string IdProducto);
        List<Compra> ObtenerCompra(int IdUsuario);
    }
}
