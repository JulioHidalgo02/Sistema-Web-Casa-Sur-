using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using System.Text.Json;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private LoginModel model = new LoginModel();
        private InventarioModel modelInventario = new InventarioModel();

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult ValidarCrendeciales()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarCrendeciales(LoginObj usuario)
        {
            try
            {
                var respuesta = model.ValidarCrendeciales(usuario, _configuration);
                if (respuesta !=  null)
                {
                    var carrito = new List<FacturaObj>();
                    var contenido = JsonSerializer.Serialize(carrito);
                    HttpContext.Session.SetString("DatosCarrito", contenido);

                    HttpContext.Session.SetString("RolUsuario", respuesta.IdRol.ToString());
                    HttpContext.Session.SetString("Cedula", respuesta.Cedula);
                    HttpContext.Session.SetString("NombreUsuario", respuesta.Nombre + " " + respuesta.Papellido  + " " + respuesta.Sapellido);
                    if(respuesta.IdRol == 2)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index2", "Home");
                    }
                    
                }
                else
                {
                    ViewBag.Msj = "Correo Electrónico o Contraseña Incorrectos";
                    return View();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public IActionResult RegistroClientes()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegistroClientes(LoginObj usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respuesta = model.RegistroUsuarioCliente(usuario, _configuration);
                    if (respuesta == 1)
                    {
                        ViewBag.Msj = "Usuario Creado Con Éxito";
                        return View();
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
        public IActionResult RecuperarContrasenia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarContrasenia(LoginObj2 usuario)
        {
            try
            {
                var respuesta = model.OlvidoContrasenia(usuario, _configuration);
                if (respuesta == 1)
                {
                    ViewBag.Msj = "Revise su bandeja de spam";
                    return View();
                }
                else
                {
                    ViewBag.Msj = "Error, El correo no se encuentra asociado al sistema";
                    return View();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            var carritoActual = HttpContext.Session.GetString("DatosCarrito");
            var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
            foreach (var item in contenido)
            {
                InventarioObj inventario = new InventarioObj();
                inventario.ID = item.IDPRODUCTO;
                var producto = modelInventario.VerProductoUnico(_configuration, inventario);
                producto.CANTDISP = 1;
                modelInventario.EditarProducto(producto, _configuration);

            }
            HttpContext.Session.Remove("RolUsuario");
            HttpContext.Session.Remove("Cedula");
            HttpContext.Session.Remove("NombreUsuario");
            return RedirectToAction("ValidarCrendeciales", "Login");
        }
    }
}