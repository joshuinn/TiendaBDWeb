using TiendaBDWeb.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace TiendaBDWeb.data

{
    public class ProductosDatos
    {
        public List<modeloProductos> Listar(Int32 IDProd, Int32 numOper)
        {
            var ListaProd = new List<modeloProductos>();
            var conn = new ConexionBD();

            using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_ConsultaProductos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("idProd", IDProd);
                comando.Parameters.AddWithValue("tipCon", numOper);

                using (var LectorDatos = comando.ExecuteReader())
                {
                    while (LectorDatos.Read())
                    {
                        ListaProd.Add(new modeloProductos()
                        {
                            id_prod_mod = Convert.ToInt32(LectorDatos["id_prod"]),
                            nom_prod_mod = Convert.ToString(LectorDatos["nom_prod"]),
                            precio_compra_prod_mod = Convert.ToDecimal(LectorDatos["precio_compra_prod"]),
                            cantidad_prod_mod = Convert.ToInt32(LectorDatos["cantidad_prod"]),
                            fecha_caduca_prod_mod = Convert.ToDateTime(LectorDatos["fec_cad_prod"]),
                            id_proveedor_prod_mod = Convert.ToInt32(LectorDatos["id_prove_prod"]),
                            id_categoria_prod_mod = Convert.ToInt32(LectorDatos["id_cate_prod"]),
                        });

                    }
                }
            }
            return ListaProd;
        }
        public bool EliminaProducto(Int32 IDProducto)
        {
            bool exito;

            try
            {
                var con = new ConexionBD();
                using (var conexion = new SqlConnection(con.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_EliminaProd", conexion);
                    comando.Parameters.AddWithValue("idProd", IDProducto);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                }
                exito = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                exito = false;
            }
            return exito;
        }


        public (bool Exito, string MensajeError) GuardaModificaProducto(modeloProductos Producto, Int32 tipOper)
        {
            bool exito;
            string nombreSP = "";
            string mensajeError = null;
            try
            {
                var con = new ConexionBD();
                using (var conexion = new SqlConnection(con.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_AdministraProductos", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("idProd", Convert.ToInt32
                    (Producto.id_prod_mod));
                    comando.Parameters.AddWithValue("nomProd", Producto.nom_prod_mod).ToString();
                    comando.Parameters.AddWithValue("precioComProd", Convert.ToDecimal
                    (Producto.precio_compra_prod_mod));
                    comando.Parameters.AddWithValue("precioVenProd", Convert.ToDecimal
                    (Producto.precio_venta_prod_mod));
                    comando.Parameters.AddWithValue("cantiProd", Convert.ToInt32
                        (Producto.precio_venta_prod_mod));
                    comando.Parameters.AddWithValue("fecCadProd", Convert.ToDateTime
                    (Producto.fecha_caduca_prod_mod));
                    comando.Parameters.AddWithValue("idProvProd", Convert.ToInt32
                    (Producto.id_proveedor_prod_mod));
                    comando.Parameters.AddWithValue("idCateProd", Convert.ToInt32
                    (Producto.id_categoria_prod_mod));
                    comando.Parameters.AddWithValue("idMarcProd", Convert.ToInt32
                    (Producto.id_marca_prod_mod));
                    comando.Parameters.AddWithValue("tipOper", tipOper);
                    comando.ExecuteNonQuery();
                }
                exito = true;
            }

            catch (Exception ex)
            {
                mensajeError = ex.Message;
                exito = false;
            }
            return (exito, mensajeError);
        }
        public modeloProductos RegresaProducto(Int32 IdProducto, Int32 tipOper)
        {
            var Producto = new modeloProductos();
            var con = new ConexionBD();
            using (var conexion = new SqlConnection(con.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_ConsultaProductos", conexion);
                comando.Parameters.AddWithValue("idProd", IdProducto);
                comando.Parameters.AddWithValue("tipCon", tipOper);
                comando.CommandType = CommandType.StoredProcedure;
                using (var LectorDatos = comando.ExecuteReader())
                {
                    {
                        while (LectorDatos.Read())
                        {
                            Producto.id_prod_mod = Convert.ToInt32(LectorDatos["id_prod"]);
                            Producto.nom_prod_mod = LectorDatos["nom_prod"].ToString();
                            Producto.precio_compra_prod_mod = Convert.ToDecimal(LectorDatos["precio_compra_prod"]);
                            Producto.precio_venta_prod_mod = Convert.ToDecimal(LectorDatos["precio_venta_prod"]);
                            Producto.cantidad_prod_mod = Convert.ToInt32(LectorDatos
                            ["cantidad_prod"]);
                            Producto.fecha_caduca_prod_mod = Convert.ToDateTime(LectorDatos["fec_cad_prod"]);
                            Producto.id_proveedor_prod_mod = Convert.ToInt32(LectorDatos
                            ["id_prove_prod"]);
                            Producto.id_categoria_prod_mod = Convert.ToInt32(LectorDatos["id_cate_prod"]);
                            Producto.id_marca_prod_mod = Convert.ToInt32(LectorDatos["id_marca_prod"]);
                            Producto.tipOper_mod = tipOper;
                        }
                    }

                }
                return Producto;
            }

        }

    }
}
