using System.ComponentModel.DataAnnotations;
namespace TiendaBDWeb.Models
{
    public class modeloClientes
    {
        public Int32 id_cli { get; set; }
        [Required(ErrorMessage = "El valor del ID del cliente es oligatorio")]
        public string nom_cli { get; set; }
        public string tel_cli { get; set; }
        public DateTime fna_cli{ get; set; }
        public String sex_cli { get; set; }
    }
}
