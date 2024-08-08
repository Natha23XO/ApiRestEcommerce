using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiRestProyecto.Repositorio.DAO
{
    public class MarcaDAO : IMarca
    {
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Marca where idMarca = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;
        }

        public List<Marca> Listar()
        {
            List<Marca> rptListaMarca = new List<Marca>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerMarca", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaMarca.Add(new Marca()
                        {
                            IdMarca = Convert.ToInt32(dr["IdMarca"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaMarca;

                }
                catch (Exception ex)
                {
                    rptListaMarca = null;
                    return rptListaMarca;
                }
            }
        }

        public bool Modificar(Marca oMarca)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarMarca", oConexion);
                    cmd.Parameters.AddWithValue("IdMarca", oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("Descripcion", oMarca.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", oMarca.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;
        }

        public bool Registrar(Marca oMarca)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oMarca.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
