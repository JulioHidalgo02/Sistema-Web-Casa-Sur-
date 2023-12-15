using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using System.Numerics;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly IConfiguration _configuration;
        MantenimientoModel model = new MantenimientoModel();
        InventarioModel Modelinventario = new InventarioModel();

        public MantenimientoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult ConsultarUsuario(string id)
        {
            try
            {
                LoginObj usuario = new LoginObj();
                usuario.CED = id;
                var respuesta = model.VerUsuario(_configuration, usuario);
                if(respuesta != null)
                {
                    var jsonString = JsonConvert.SerializeObject(respuesta.NOM);
                    return Json(jsonString);
                }
                else
                {
                    return Json(1);
                }
                
            }
            catch (Exception ex) {
                return Json(ex);
            }     
        }

                
        [HttpGet]
        public IActionResult FiltroMenor()
        {
            try
            {
                var respuesta = model.FiltroMenor(_configuration);
                if (respuesta != null)
                {
                   
                    return View(respuesta);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public IActionResult FiltroMayor()
        {
            try
            {
                var respuesta = model.FiltroMayor(_configuration);
                if (respuesta != null)
                {
                    return View(respuesta);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public IActionResult FiltroAlfabeto()
        {
            try
            {
                var respuesta = model.FiltroAlfabeto(_configuration);
                if (respuesta != null)
                {
                    return View(respuesta);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public IActionResult FiltroNoAlfabeto()
        {
            try
            {
                var respuesta = model.FiltroNoAlfabeto(_configuration);
                if (respuesta != null)
                {
                    return View(respuesta);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }



        [HttpGet]
        public IActionResult Busqueda(string Fac)
        {
            InventarioObj obj = new InventarioObj();
            obj.FACTOR = Fac;
            try
            {
                var respuesta = model.Busqueda(_configuration, obj);
                if (respuesta != null)
                {
                    return View(respuesta);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }


    [HttpPost]
    public IActionResult RealizarBusqueda(string Factor)
    {
       
        try
        {
                return RedirectToAction("Busqueda", "Mantenimiento", new { Fac = Factor });

        }
        catch (Exception ex)
        {
            return null;
        }

    }

        [HttpGet]
        public IActionResult VerErrores()
        {

            var datos = model.VerErrores(_configuration);
            return View(datos);
        }

    }

}

