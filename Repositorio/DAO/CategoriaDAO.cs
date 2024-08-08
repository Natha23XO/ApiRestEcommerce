using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiRestProyecto.Repositorio.DAO
{
    public class CategoriaDAO : ICategoria
    {
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from CATEGORIA where idcategoria = @id", oConexion);
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

        public List<Categoria> Listar()
        {
            List<Categoria> rptListaCategoria = new List<Categoria>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerCategoria", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaCategoria.Add(new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaCategoria;

                }
                catch (Exception ex)
                {
                    rptListaCategoria = null;
                    return rptListaCategoria;
                }
            }
        }

        public bool Modificar(Categoria oCategoria)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", oCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", oCategoria.Activo);
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

        public bool Registrar(Categoria oCategoria)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oCategoria.Descripcion);
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
