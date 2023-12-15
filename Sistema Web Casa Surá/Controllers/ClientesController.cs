using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IConfiguration _configuration;
        private LoginModel model = new LoginModel();
        VerUsuarioModel verUsuario = new VerUsuarioModel();

        public ClientesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCliente(LoginObj usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respuesta = model.RegistroUsuarioCliente(usuario, _configuration);
                    if (respuesta == 1)
                    {
                        ViewBag.Msj = "Usuario Creado Con Éxito";
                        return RedirectToAction("VerClientes", "Clientes");
                    }
                    else
                    {
                        ViewBag.Msj = "Error, por favor, valide los datos";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult EditarCliente(string Ced)
        {
            var resultado = verUsuario.VerUsuarioObj2(_configuration, Ced);
            return View(resultado);
        }

        [HttpPost]
        public IActionResult EditarCliente(VerUsuarioObj2 usuario)
        {
            try
            {
                var resultado = verUsuario.EditarUsuario(_configuration, usuario);

                if (resultado == 1)
                {
                    return RedirectToAction("VerClientes", "Clientes");
                }
                else
                {
                    return View();
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult VerClientes()
        {
            var resultado = verUsuario.VerClientes(_configuration);
            return View(resultado);
        }


        [HttpPost]
        public IActionResult EliminarCliente(string cedula)
        {
            try
            {
                var resultado = verUsuario.EliminarCliente(cedula, _configuration);

                if (resultado == 1)
                {
                    return RedirectToAction("VerClientes", "Clientes");
                }
                else
                {
                    return RedirectToAction("VerClientes", "Clientes");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
