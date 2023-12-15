using Microsoft.AspNetCore.Mvc;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using static NuGet.Packaging.PackagingConstants;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly IConfiguration _configuration;
        private CategoriaModel model = new CategoriaModel();

        public CategoriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult VerCategorias()
        {
            try
            {
                var datos = model.VerCategorias(_configuration);
                return View(datos);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult EditarCategorias(int id)
        {
            try
            {
                
                    CategoriaObj categoria = new CategoriaObj();
                categoria.ID = id;
                var dato = model.VerCategoriaUnica(_configuration, categoria);
                return View(dato);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult EditarCategorias(CategoriaObj categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.EditarCategoria(categoria, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerCategorias", "Categoria");
                    }
                    else
                    {
                        return RedirectToAction("EditarCategorias", "Categoria");
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
        public IActionResult CrearCategorias()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCategorias(CategoriaObj categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.AgregarCategoria(categoria, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerCategorias", "Categoria");
                    }
                    else
                    {
                        return RedirectToAction("CrearCategorias", "Categoria");
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
        public IActionResult EliminarCategoria(int id)
        {
            CategoriaObj categoria = new CategoriaObj();
            categoria.ID = id;
            try
            {
                var resultado = model.EliminarCategoria(categoria, _configuration);

                if (resultado == 1)
                {
                    return RedirectToAction("VerCategorias", "Categoria");
                }
                else
                {
                    return RedirectToAction("EditarCategorias", "Categoria");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
