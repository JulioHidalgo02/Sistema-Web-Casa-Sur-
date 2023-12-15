using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class ApartadosController : Controller
    {
        private readonly IConfiguration _configuration;
        ApartadosModel model = new ApartadosModel();

        public ApartadosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult RealizarApartado()
        {
           return View();
        }



        [HttpPost]
        public IActionResult RealizarApartado(ApartadosObj apartado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var respuesta = model.RegistrarApartado(apartado, _configuration);

                    if(respuesta == 1)
                    {
                        return RedirectToAction("VerApartados", "Apartados");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }

                   
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult VerApartados()
        {
            var resultado = model.VerApartados(_configuration);
            return View(resultado);
        }

        [HttpGet]
        public IActionResult RealizarAbono(long IdApartados)
        {
            var respuesta = model.VerApartadoUnico(_configuration, IdApartados);
            return View(respuesta);
        }

        [HttpPost]
        public IActionResult RealizarAbono(long idApartados, decimal Abonar, decimal Saldo)
        {
            var respuesta = model.ActualizarApartado(_configuration, idApartados, Abonar, Saldo);
            if (respuesta == 1)
            {
                return RedirectToAction("VerApartados", "Apartados");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult EditarApartado(long IdApartados)
        {
            var respuesta = model.VerApartadoUnico(_configuration, IdApartados);
            return View(respuesta);
        }

        [HttpPost]
        public IActionResult EditarApartado(ApartadosObj apartados)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respuesta = model.EditarApartado(_configuration, apartados);
                    if (respuesta == 1)
                    {
                        return RedirectToAction("VerApartados", "Apartados");
                    }
                    else
                    {
                        return View();
                    }
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

        [HttpPost]
        public IActionResult EliminarApartado(long idApartados)
        {
            try
            {
                var resultado = model.EliminarApartado(idApartados, _configuration);

                if (resultado == 1)
                {
                    return RedirectToAction("VerApartados", "Apartados");
                }
                else
                {
                    return RedirectToAction("VerApartados", "Apartados");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
