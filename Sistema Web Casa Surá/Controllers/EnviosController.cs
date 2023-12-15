using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using static NuGet.Packaging.PackagingConstants;


namespace Sistema_Web_Casa_Surá.Controllers
{
    public class EnviosController : Controller
    {

        private readonly IConfiguration _configuration;
        private EnviosModel model = new EnviosModel();

        public EnviosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult VerEnvios()
        {
            try
            {
                var datos = model.VerEnvios(_configuration);
                return View(datos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult EditarEnvios(int id)
        {
            try
            {

                EnviosObj envios = new EnviosObj();
                envios.ID = id;
                var dato = model.VerEnvioUnico(_configuration, envios);
                return View(dato);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult EditarEnvios(EnviosObj envios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.EditarEnvio(envios, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerEnvios", "Envios");
                    }
                    else
                    {
                        return RedirectToAction("EditarEnvios", "Envios");
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
        public IActionResult CrearEnvios()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearEnvios(EnviosObj envios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.CrearEnvio(envios, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerEnvios", "Envios");
                    }
                    else
                    {
                        return RedirectToAction("CrearEnvios", "Envios");
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
        public IActionResult EliminarEnvio(int id)
        {
           EnviosObj envios = new EnviosObj();  
            envios.ID = id;
            try
            {
                var resultado = model.EliminarEnvio(envios, _configuration);

                if (resultado == 1)
                {
                    return RedirectToAction("VerEnvios", "Envios");
                }
                else
                {
                    return RedirectToAction("VerEnvios", "Envios");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
