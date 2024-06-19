using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TiendaBDWeb.data
{
    public class ConexionBD
    {
        private string cadSQL = string.Empty;

        public ConexionBD()
        {
            var ObtenConexionBD = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            cadSQL = ObtenConexionBD.GetSection("ConnectionStrings:ParametroSQL").Value;
        }
        public string RegresaCadSQL()
        {
            return cadSQL;
        }
    }
}
