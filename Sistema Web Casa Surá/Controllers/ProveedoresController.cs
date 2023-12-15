using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
namespace Sistema_Web_Casa_Surá.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IConfiguration _configuration;
        private ProveedoresModel model = new ProveedoresModel();
        public ProveedoresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult VerProveedores()
        {
            try
            {
                var datos = model.VerProveedores(_configuration);
                return View(datos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult EditarProveedores(int id)
        {
            try
            {

                ProveedoresObj proveedores = new ProveedoresObj();
                proveedores.ID = id;
                var dato = model.VerProveedorUnico(_configuration, proveedores);
                return View(dato);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult EditarProveedores(ProveedoresObj proveedores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.EditarProveedores(proveedores, _configuration); if (resultado == 1)
                    {
                        return RedirectToAction("VerProveedores", "Proveedores");
                    }
                    else
                    {
                        return RedirectToAction("EditarProveedores", "Proveedores");
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
        [HttpGet]
        public IActionResult CrearProveedores()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearProveedores(ProveedoresObj proveedores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.AgregarProveedores(proveedores, _configuration); if (resultado == 1)
                    {
                        return RedirectToAction("VerProveedores", "Proveedores");
                    }
                    else
                    {
                        return RedirectToAction("Proveedores", "Proveedores");
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
        public IActionResult EliminarProveedores(int id)
        {
            ProveedoresObj proveedores = new ProveedoresObj();
            proveedores.ID = id;
            try
            {
                var resultado = model.EliminarProveedores(proveedores, _configuration); 
                if (resultado == 1)
                {
                    return RedirectToAction("VerProveedores", "Proveedores");
                }
                else
                {
                    return RedirectToAction("EditarProveedores", "Proveedores");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
