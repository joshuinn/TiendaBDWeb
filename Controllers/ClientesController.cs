using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TiendaBDWeb.data;
using TiendaBDWeb.Models;

namespace TiendaBDWeb.Controllers
{
    public class ClientesController : Controller
    {
        ClientesDatos clientesDatos = new ClientesDatos();
        public IActionResult listarClientes()
        {
            try
            {
                var clientes = clientesDatos.Listar();
                return View(clientes);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult RegistrarCliente()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegistrarCliente(modeloClientes dataClientes)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error: Datos del cliente no válidos.";
                return View();
            }
            var (Exito, MensajeError) = clientesDatos.RegistrarCiente(dataClientes);

            if (Exito)
            {
                return RedirectToAction("ListarClientes");
            }
            else
            {
                TempData["ErrorMessage"] = $"Error: {MensajeError}";
                return View();
            }
        }

        public IActionResult EditarCliente(Int32 idCliente) { 
            var cliente = clientesDatos.RegresaCliente(idCliente);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult EditarCliente(modeloClientes cliente)
        {
            if (!ModelState.IsValid) return View();
            var (Exito, Error) = clientesDatos.EditarCliente(cliente);

            if (Exito) return RedirectToAction("ListarClientes");
            else
            {
                TempData["ErrorMessage"] = $"Error: {Error} cliente: {cliente.id_cli}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarCliente(Int32 idCliente)
        {
            var cliente = clientesDatos.RegresaCliente(idCliente);
            return View(cliente);
        }
        [HttpPost]
        public IActionResult EliminarCliente(modeloClientes cliente)
        {
            var (Exito, Error) = clientesDatos.EliminarCliente(cliente.id_cli);

            if (Exito) return RedirectToAction("ListarClientes");
            else
            {
                TempData["ErrorMessage"] = $"Error: {Error} No se pudo eliminar el cliente: {cliente.id_cli}";
                return View();
            }
        }



    }
}
