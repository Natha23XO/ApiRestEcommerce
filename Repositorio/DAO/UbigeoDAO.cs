using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiRestProyecto.Repositorio.DAO
{
    public class UbigeoDAO : IUbigeo
    {
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lst = new List<Departamento>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from departamento", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Departamento()
                            {
                                IdDepartamento = dr["IdDepartamento"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Departamento>();
                }
            }
            return lst;
        }

        public List<Distrito> ObtenerDistrito(string _idprovincia, string _iddepartamento)
        {
            List<Distrito> lst = new List<Distrito>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from DISTRITO where IdProvincia = @idprovincia and IdDepartamento = @iddepartamento", oConexion);
                    cmd.Parameters.AddWithValue("@idprovincia", _idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", _iddepartamento);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Distrito()
                            {
                                IdDistrito = dr["IdDistrito"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdProvincia = dr["IdProvincia"].ToString(),
                                IdDepartamento = dr["IdDepartamento"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Distrito>();
                }
            }
            return lst;
        }

        public List<Provincia> ObtenerProvincia(string _iddepartamento)
        {
            List<Provincia> lst = new List<Provincia>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from provincia where IdDepartamento = @iddepartamento", oConexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", _iddepartamento);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new Provincia()
                            {
                                IdProvincia = dr["IdProvincia"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdDepartamento = dr["IdDepartamento"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lst = new List<Provincia>();
                }
            }
            return lst;
        }
    }
}
