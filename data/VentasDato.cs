using TiendaBDWeb.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace TiendaBDWeb.data
{
    public class VentasDato
    {
        public List<modeloVentas> Listar()
        {
            var ListarVentas = new List<modeloVentas>();
            var conn = new ConexionBD();

            using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_VerTodasLasVentas", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                using (var LectorDatos = comando.ExecuteReader())
                {
                    while (LectorDatos.Read())
                    {
                        ListarVentas.Add(new modeloVentas()
                        {
                            IdVta = Convert.ToInt32(LectorDatos["id_vta"]),
                            FecVta = Convert.ToDateTime(LectorDatos["fec_vta"]).Date,
                            HorVta = LectorDatos["hor_vta"].ToString(),
                            ClienteVta = Convert.ToInt32(LectorDatos["cliente_vta"]),
                            EmpleadoVta = Convert.ToInt32(LectorDatos["empleado_vta"]),
                            NumProDet = Convert.ToInt32(LectorDatos["num_pro_det"]),
                            CantidadProd = Convert.ToInt32(LectorDatos["cantidad_prod"]),
                            NombreProd = LectorDatos["nom_prod"].ToString()
                        });

                    }
                }
            }
            return ListarVentas;
        }

        public (bool, string) RegistrarVenta(modeloVentas venta, List<int> ProductosId)
        {
            try
            {
                var conteoProductos = NormalizarProductos(ProductosId);
                var conn = new ConexionBD();
                using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
                {
                    conexion.Open();
                    foreach (var conteoProducto in conteoProductos)
                    {

                        SqlCommand comando = new SqlCommand("sp_realizarVenta", conexion);
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("id_vta", Convert.ToInt32(venta.IdVta));
                        comando.Parameters.AddWithValue("fec_vta", Convert.ToDateTime(venta.FecVta));
                        comando.Parameters.AddWithValue("hor_vta", Convert.ToString(venta.HorVta));
                        comando.Parameters.AddWithValue("cli_vta", Convert.ToInt32(venta.ClienteVta));
                        comando.Parameters.AddWithValue("Empleado_vta", Convert.ToInt32(venta.EmpleadoVta));
                        comando.Parameters.AddWithValue("id_prod", Convert.ToInt32(conteoProducto.IdProducto));
                        comando.Parameters.AddWithValue("CantidadProd", Convert.ToInt32(conteoProducto.Cantidad));
                        comando.ExecuteNonQuery();
                    }
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public List<ConteoProducto> NormalizarProductos(List<int> productosId)
        {
            Dictionary<int, int> conteo = new Dictionary<int, int>();

            // Contar la cantidad de cada producto en el arreglo
            foreach (int idProducto in productosId)
            {
                if (conteo.ContainsKey(idProducto))
                {
                    conteo[idProducto]++;
                }
                else
                {
                    conteo[idProducto] = 1;
                }
            }

            // Crear una lista de ConteoProducto a partir del diccionario
            List<ConteoProducto> conteoProductos = new List<ConteoProducto>();
            foreach (var kvp in conteo)
            {
                conteoProductos.Add(new ConteoProducto { IdProducto = kvp.Key, Cantidad = kvp.Value });
            }

            return conteoProductos;
        }
        public modeloVentaDetalles RegresaDetalleVenta(int idVenta, int idProd)
        {
            var venta = new modeloVentaDetalles();
            venta.Venta = new modeloVentas();
            var conn = new ConexionBD();
            using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_VerDetalleVenta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("idVta", idVenta);
                comando.Parameters.AddWithValue("numProd", idProd);
                using (var LectorDatos = comando.ExecuteReader())
                {
                    while (LectorDatos.Read())
                    {
                        venta.Venta.IdVta = Convert.ToInt32(LectorDatos["id_vta"]);
                        venta.Venta.NumProDet = Convert.ToInt32(LectorDatos["num_pro_det"]);
                        venta.Venta.CantidadProd = Convert.ToInt32(LectorDatos["cantidad_prod"]);
                        venta.Venta.NombreProd = LectorDatos["nom_prod"].ToString();
                        venta.Venta.FecVta = Convert.ToDateTime(LectorDatos["fec_vta"]);
                        venta.Venta.HorVta = LectorDatos["hor_vta"].ToString();
                        venta.Venta.ClienteVta = Convert.ToInt32(LectorDatos["cliente_vta"]);
                        venta.Venta.EmpleadoVta = Convert.ToInt32(LectorDatos["empleado_vta"]);

                    }
                }
            }
            return venta;
        }

        public (bool, string) EliminarVenta(Int32 idVenta, Int32 idProd)
        {
            try
            {
                var con = new ConexionBD();
                using (var conexion = new SqlConnection(con.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_EliminarVenta", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("IdVta", idVenta);
                    comando.Parameters.AddWithValue("numProd", idProd);
                    comando.ExecuteNonQuery();
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public (bool, string) EditarVenta(modeloVentas venta)
        {
            try
            {
                var conn = new ConexionBD();
                using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("EditarDetalleVenta", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("idVta", Convert.ToInt32
                        (venta.IdVta));
                    comando.Parameters.AddWithValue("numProd", Convert.ToInt32(venta.NumProDet));
                    comando.Parameters.AddWithValue("nuevaFecha", Convert.ToDateTime(venta.FecVta));
                    comando.Parameters.AddWithValue("nuevaHora", Convert.ToString(venta.HorVta));
                    comando.Parameters.AddWithValue("nuevoCliente", Convert.ToInt32(venta.ClienteVta));
                    comando.Parameters.AddWithValue("nuevoEmpleado", Convert.ToInt32(venta.EmpleadoVta));
                    comando.Parameters.AddWithValue("nuevaCantidad", Convert.ToInt32(venta.CantidadProd));
                    comando.ExecuteNonQuery();
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
