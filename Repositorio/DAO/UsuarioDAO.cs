using ApiRestProyecto.Models;
using Microsoft.Data.SqlClient;
using ApiRestProyecto.Repositorio.Interfaces;
using System.Data;

namespace ApiRestProyecto.Repositorio.DAO
{
    public class UsuarioDAO : IUsuario
    {
        public Usuario Obtener(string _correo, string _contrasena)
        {
            Usuario objeto = null;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_obtenerUsuario", oConexion);
                    cmd.Parameters.AddWithValue("@Correo", _correo);
                    cmd.Parameters.AddWithValue("@Contrasena", _contrasena);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                EsAdministrador = Convert.ToBoolean(dr["EsAdministrador"].ToString())
                            };

                        }
                    }

                }
                catch (Exception ex)
                {
                    objeto = null;
                }
            }
            return objeto;
        }

        public int Registrar(Usuario oUsuario)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("Nombres", oUsuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oUsuario.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oUsuario.Contrasena);
                    cmd.Parameters.AddWithValue("EsAdministrador", oUsuario.EsAdministrador);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }
    }
}
