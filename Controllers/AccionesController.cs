﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TiendaBDWeb.data;
using TiendaBDWeb.Models;


namespace TiendaBDWeb.Controllers
{
    public class AccionesController : Controller
    {
        ProductosDatos proDat = new ProductosDatos();
        public IActionResult ListarProd()
        {
            var ListaPro = proDat.Listar(1, 1);
            return View(ListaPro);
        }


        public IActionResult GuardarProd()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GuardarProd(modeloProductos recProductos)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error: Datos del producto no válidos.";
                return View();
            }
            var (Exito, MensajeError) = proDat.GuardaModificaProducto(recProductos, 1);

            if (Exito)
            {
                return RedirectToAction("ListarProd");
            }
            else
            {
                TempData["ErrorMessage"] = $"Error: {MensajeError}";
                return View();
            }
        }

        public IActionResult EditarProd(Int32 IdProd)
        {
            var Producto = proDat.RegresaProducto(IdProd, 2);
            return View(Producto);
        }
        [HttpPost]

        public IActionResult EditarProd(modeloProductos numProd)
        {
            if (!ModelState.IsValid) return View();
            var (Exito, MensajeError) = proDat.GuardaModificaProducto(numProd, 2);

            if (Exito) return RedirectToAction("ListarProd");

            else
            {
                TempData["ErrorMessage"] = $"Error: {MensajeError}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarProd(Int32 datProd)
        {
            var Producto = proDat.RegresaProducto(datProd, 2);
            return View(Producto);
        }
        [HttpPost]
        public IActionResult EliminarProd(modeloProductos datProd)
        {
            var Respuesta = proDat.EliminaProducto(datProd.id_prod_mod);
            if (Respuesta)
                return RedirectToAction("ListarProd");
            else
                return View();
        }

    }
}
