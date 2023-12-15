using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class PerfilController : Controller
    {
        public readonly IConfiguration _configuration;
        VerUsuarioModel model = new VerUsuarioModel();

        public PerfilController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult VerPerfil()
        {
            var Ced = HttpContext.Session.GetString("Cedula");
            var respuesta = model.VerUsuario(_configuration, Ced);
            return View(respuesta);
        }

        [HttpGet]
        public IActionResult EditarUsuario()
        {
            var Ced = HttpContext.Session.GetString("Cedula");
            var respuesta = model.VerUsuario(_configuration, Ced);
            ViewBag.Ced = Ced;
            return View(respuesta);
        }

        [HttpPost]
        public IActionResult EditarUsuario(VerUsuarioObj2 usuario)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var Cedula = HttpContext.Session.GetString("Cedula");
                    usuario.Ced = Cedula;
               
                    var respuesta = model.EditarUsuario(_configuration, usuario);
                    if (respuesta != 0)
                    {
                        return RedirectToAction("VerPerfil", "Perfil");
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
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult CambiarContrasenia(VerUsuarioObj usuario)
        {
            try
            {
                var Cedula = HttpContext.Session.GetString("Cedula");
                usuario.loginObj.CED = Cedula;
                var respuesta = model.CambiarContrasenia(_configuration, usuario);
                if (respuesta != 0)
                {
                    HttpContext.Session.Remove("RolUsuario");
                    HttpContext.Session.Remove("Cedula");
                    HttpContext.Session.Remove("NombreUsuario");
                    return RedirectToAction("ValidarCrendeciales", "Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        


    }
}
