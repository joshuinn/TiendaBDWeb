namespace TiendaBDWeb.Models
{
    public class modeloVentaDetalles
    {
        public modeloVentas Venta { get; set; }
        public List<modeloProductos> Productos { get; set; }

        public List<int> ProductosId { get; set; }
    }
}
