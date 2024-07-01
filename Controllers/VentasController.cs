using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TiendaBDWeb.data;
using TiendaBDWeb.Models;

namespace TiendaBDWeb.Controllers
{
    public class VentasController : Controller
    {
        VentasDato ventasData = new VentasDato();
        ProductosDatos productosData = new ProductosDatos();
        public IActionResult ListarVentas()
        {
            try
            {
                var ListaVentas = ventasData.Listar();
                return View(ListaVentas);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult RealizarVenta()
        {
            var productos = productosData.Listar(1, 1);
            var viewModel = new modeloVentaDetalles
            {
                Productos = productos,
                Venta = new modeloVentas {
                    FecVta = DateTime.Now.Date,
                    HorVta = DateTime.Now.ToString("HH:mm")
                }
            };
            return View(viewModel);

        }
        [HttpPost]
        public IActionResult RealizarVenta([FromBody] modeloVentaDetalles jsonData)
        {
            var venta = jsonData.Venta;
            var productosId = jsonData.ProductosId;

            var (exito, error) = ventasData.RegistrarVenta(venta, productosId);
            return Json(jsonData);
            if (exito)
            {
                return Json("Venta realizada con éxito");
            }
            return Json(error);
        }

        [HttpGet]
        public IActionResult EliminarVenta(Int32 idVenta, Int32 idProd)
        {
            var venta = ventasData.RegresaDetalleVenta(idVenta, idProd);
            TempData["ErrorMessage"] = $"{idVenta}   ° {idProd}";
            return View(venta);
        }
        [HttpPost]
        public IActionResult EliminarVenta(modeloVentas venta)
        {
            var (exito, error) = ventasData.EliminarVenta(venta.IdVta, venta.NumProDet);
            if (exito)
            {
                return RedirectToAction("ListarVentas");
            }
            TempData["ErrorMessage"] = $"Error: {error}";
            return View();
        }


        public IActionResult EditarVenta(Int32 idVenta, Int32 idProd) { 
            var venta = ventasData.RegresaDetalleVenta(idVenta, idProd);
            return View(venta.Venta);
        }

        [HttpPost]
        public IActionResult EditarVenta(modeloVentas venta) { 
            var (exito, error)= ventasData.EditarVenta(venta);
            if(exito){
                return RedirectToAction("ListarVentas");
            }
            TempData["ErrorMessage"] = $"Error: {error}";
            return View();
        }

    }
}
