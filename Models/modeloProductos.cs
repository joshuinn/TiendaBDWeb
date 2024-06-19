using System.ComponentModel.DataAnnotations;
namespace TiendaBDWeb.Models
{
    public class modeloProductos
    {
        public Int32 id_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor del ID del producto es oligatorio")]
        public string nom_prod_mod { get; set; }
        [Required(ErrorMessage = "EÑ valor del Nombre del producto e obligatorio")]
        public decimal precio_compra_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor de precio de compra del producto es obligatorio")]
        public decimal precio_venta_prod_mod { get; set; }
        [Required(ErrorMessage = "EL valor del precio de venta del producto es obligatorio")]
        public Int32 cantidad_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor de cantdad del proucto es obligatorio")]
        public DateTime fecha_caduca_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor de la fecha de caducicidad del producto es obligatorio")]
        public Int32 id_proveedor_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor del ID del proveeodr del producto es obligatorio")]
        public Int32 id_categoria_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor del ID de la categoria es obligatoria")]
        public Int32 id_marca_prod_mod { get; set; }
        [Required(ErrorMessage = "El valor del ID de la marca del producto es obligatorio")]
        public Int32 tipOper_mod { get; set; }

    }
}
