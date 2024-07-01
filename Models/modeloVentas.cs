using System.ComponentModel.DataAnnotations;
namespace TiendaBDWeb.Models
{
    public class modeloVentas
    {
        public Int32 IdVta { get; set; }
        public DateTime FecVta { get; set; }
        public string HorVta { get; set; }
        public Int32 ClienteVta { get; set; }
        public Int32 EmpleadoVta { get; set; }
        public Int32 NumProDet { get; set; }
        public Int32 CantidadProd { get; set; }
        public string NombreProd { get; set; }
    }
}
 