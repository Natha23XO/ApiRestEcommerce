using ApiRestProyecto.Models;

namespace ApiRestProyecto.Repositorio.Interfaces
{
    public interface IUsuario
    {
        Usuario Obtener(string _correo, string _contrasena);
        int Registrar(Usuario oUsuario);


    }
}
