using TiendaBDWeb.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
namespace TiendaBDWeb.data
{
    public class ClientesDatos
    {
        public List<modeloClientes> Listar()
        {
            var clientes = new List<modeloClientes>();
            var conn = new ConexionBD();
            using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_VerClientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                using (var LectorDatos = comando.ExecuteReader())
                {
                    while (LectorDatos.Read())
                    {
                        clientes.Add(new modeloClientes()
                        {
                            id_cli = Convert.ToInt32(LectorDatos["id_cli"]),
                            nom_cli = LectorDatos["nom_cli"].ToString(),
                            tel_cli = LectorDatos["tel_cli"].ToString(),
                            fna_cli = Convert.ToDateTime(LectorDatos["fna_cli"]),
                            sex_cli = LectorDatos["sex_cli"].ToString()
                        });
                    }
                }
            }
            return clientes;

        }

        public (bool Exito, string MensajeError) RegistrarCiente(modeloClientes cliente)
        {
            bool exito = false;
            string mensajeError = null;

            try
            {
                var con = new ConexionBD();
                using (var conexion = new SqlConnection(con.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_InsertarCliente", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("id_cli", Convert.ToInt32
                    (cliente.id_cli));
                    comando.Parameters.AddWithValue("nom_cli", cliente.nom_cli.ToString());
                    comando.Parameters.AddWithValue("tel_cli", cliente.tel_cli.ToString());
                    comando.Parameters.AddWithValue("fna_cli", Convert.ToDateTime
                    (cliente.fna_cli));
                    comando.Parameters.AddWithValue("sex_cli", cliente.sex_cli.ToString());
                    comando.ExecuteNonQuery();
                }
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                mensajeError = ex.Message;
            }
            return (exito, mensajeError);
        }

        public (bool, string) EliminarCliente(Int32 idCliente)
        {
            try
            {
                var con = new ConexionBD();
                using (var conexion = new SqlConnection(con.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_EliminarCliente", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("id_cli", idCliente);
                    comando.ExecuteNonQuery();
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public modeloClientes RegresaCliente(int idCliente)
        {
            var cliente = new modeloClientes();
            var con = new ConexionBD();
            using (var conexion = new SqlConnection(con.RegresaCadSQL()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("sp_VerCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("id_cli", idCliente);
                using (var LectorDatos = comando.ExecuteReader())
                {
                    while (LectorDatos.Read())
                    {
                        cliente.id_cli = Convert.ToInt32(LectorDatos["id_cli"]);
                        cliente.nom_cli = LectorDatos["nom_cli"].ToString();
                        cliente.tel_cli = LectorDatos["tel_cli"].ToString();
                        cliente.fna_cli = Convert.ToDateTime(LectorDatos["fna_cli"]);
                        cliente.sex_cli = LectorDatos["sex_cli"].ToString();
                    }
                }
            }
            return cliente;
        }

        public (bool exito, string mensajeError) EditarCliente(modeloClientes cliente)
        {
            try
            {
                var conn = new ConexionBD();
                using (var conexion = new SqlConnection(conn.RegresaCadSQL()))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_EditarCliente", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("id_cli", Convert.ToInt32
                        (cliente.id_cli));
                    comando.Parameters.AddWithValue("nom_cli", cliente.nom_cli.ToString());
                    comando.Parameters.AddWithValue("tel_cli", cliente.tel_cli.ToString());
                    comando.Parameters.AddWithValue("fna_cli", Convert.ToDateTime
                    (cliente.fna_cli));
                    comando.Parameters.AddWithValue("sex_cli", cliente.sex_cli.ToString());
                    comando.ExecuteNonQuery();
                    return (true, null);

                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
