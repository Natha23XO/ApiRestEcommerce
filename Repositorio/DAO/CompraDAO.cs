using ApiRestProyecto.Models;
using ApiRestProyecto.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace ApiRestProyecto.Repositorio.DAO
{
    public class CompraDAO : ICompra
    {
        public bool Registrar(Compra oCompra)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                    {
                        query.AppendLine("insert into detalle_compra(IdCompra,IdProducto,Cantidad,Total) values (¡idcompra!," + dc.IdProducto + "," + dc.Cantidad + "," + dc.Total + ")");
                    }

                    SqlCommand cmd = new SqlCommand("sp_registrarCompra", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oCompra.IdUsuario);
                    cmd.Parameters.AddWithValue("TotalProducto", oCompra.TotalProducto);
                    cmd.Parameters.AddWithValue("Total", oCompra.Total);
                    cmd.Parameters.AddWithValue("Contacto", oCompra.Contacto);
                    cmd.Parameters.AddWithValue("Telefono", oCompra.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", oCompra.Direccion);
                    cmd.Parameters.AddWithValue("IdDistrito", oCompra.IdDistrito);
                    cmd.Parameters.AddWithValue("QueryDetalleCompra", query.ToString());
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


        public List<Compra> ObtenerCompra(int IdUsuario)
        {
            List<Compra> rptDetalleCompra = new List<Compra>();
            using (SqlConnection oConexion = new SqlConnection(ConexionDAO.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerCompra", oConexion);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    oConexion.Open();
                    using (XmlReader dr = cmd.ExecuteXmlReader())
                    {
                        while (dr.Read())
                        {
                            XDocument doc = XDocument.Load(dr);
                            if (doc.Element("DATA") != null)
                            {
                                rptDetalleCompra = (from c in doc.Element("DATA").Elements("COMPRA")
                                                    select new Compra()
                                                    {
                                                        Total = Convert.ToDecimal(c.Element("Total").Value, new CultureInfo("es-PE")),
                                                        FechaTexto = c.Element("Fecha").Value,
                                                        oDetalleCompra = (from d in c.Element("DETALLE_PRODUCTO").Elements("PRODUCTO")
                                                                          select new DetalleCompra()
                                                                          {
                                                                              oProducto = new Producto()
                                                                              {
                                                                                  oMarca = new Marca() { Descripcion = d.Element("Descripcion").Value },
                                                                                  Nombre = d.Element("Nombre").Value,
                                                                                  RutaImagen = d.Element("RutaImagen").Value
                                                                              },
                                                                              Total = Convert.ToDecimal(d.Element("Total").Value, new CultureInfo("es-PE")),
                                                                              Cantidad = Convert.ToInt32(d.Element("Cantidad").Value)
                                                                          }).ToList()
                                                    }).ToList();
                            }
                            else
                            {
                                rptDetalleCompra = new List<Compra>();
                            }
                        }

                        dr.Close();

                    }

                    return rptDetalleCompra;
                }
                catch (Exception ex)
                {
                    rptDetalleCompra = null;
                    return rptDetalleCompra;
                }
            }
        }

    }
}
