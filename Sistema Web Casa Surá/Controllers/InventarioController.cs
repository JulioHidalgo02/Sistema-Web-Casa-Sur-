using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using ProyectoFinal_WebApp.Helper;
using ProyectoFinal_WebApp.Providers;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class InventarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private InventarioModel model = new InventarioModel();
        private CategoriaModel catModel = new CategoriaModel();
        private HelperUploadFiles helperUpload;
        private BitacoraModel Bmode = new BitacoraModel();

        public InventarioController(HelperUploadFiles helperUpload, IConfiguration configuration)
        {
            _configuration = configuration;
            this.helperUpload = helperUpload;
        }
        [HttpGet]
        public IActionResult VerInventario()
        {
            var datos = model.VerProductos(_configuration);
            return View(datos);
        }

        [HttpGet]
        public IActionResult EditarInventario(int id)
        {
            InventarioObj producto = new InventarioObj();
            producto.ID = id;
            var productoUnico = model.VerProductoUnico(_configuration, producto);

            var categorias = catModel.VerCategorias(_configuration);
            var datos = new List<SelectListItem>();

            foreach (var item in categorias)
                datos.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.DESCRIPCION });

            ViewBag.ComboTipos = datos;

            return View(productoUnico);
        }

        [HttpPost]
        public IActionResult EditarInventario(InventarioObj producto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = model.EditarProducto(producto, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerInventario", "Inventario");
                    }
                    else
                    {
                        throw new Exception($"Id No encontrado");
                        return RedirectToAction("VerInventario", "Inventario");
                    }
                }
                else
                {
                    var categorias = catModel.VerCategorias(_configuration);
                    var datos = new List<SelectListItem>();

                    foreach (var item in categorias)
                        datos.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.DESCRIPCION });

                    ViewBag.ComboTipos = datos;
                    return View();
                }
                
            }
            catch (Exception e)
            {
                string? cedula = HttpContext.Session.GetString("Cedula");
                Bmode.RegistrarErrores(cedula, e, "EditarInventario", _configuration);
                return BadRequest(e.Message);
            }

        }
        [HttpGet]
        public IActionResult CrearInventario()
        {
            var categorias = catModel.VerCategorias(_configuration);
            var datos = new List<SelectListItem>();

            foreach (var item in categorias)
                datos.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.DESCRIPCION });

            ViewBag.ComboTipos = datos;
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearInventario(IFormFile imagen, InventarioObj producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string nombreImagen = imagen.FileName;
                    string path = await this.helperUpload.UploadFilesAsync(imagen, nombreImagen, Folders.Images);
                    path = "/images/" + nombreImagen;
                    producto.URL = path;

                    var resultado = model.AgregarProductos(producto, _configuration);

                    if (resultado == 1)
                    {
                        return RedirectToAction("VerInventario", "Inventario");
                    }
                    else
                    {
                        return RedirectToAction("CrearInventario", "Inventario");
                    }
                }
                else
                {
                    var categorias = catModel.VerCategorias(_configuration);
                    var datos = new List<SelectListItem>();

                    foreach (var item in categorias)
                        datos.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.DESCRIPCION });

                    ViewBag.ComboTipos = datos;
                    return View();
                }
                
            }
            catch (Exception e)
            {
                string? cedula = HttpContext.Session.GetString("Cedula");
                Bmode.RegistrarErrores(cedula, e, "EditarInventario", _configuration);
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult EliminarProducto(int id)
        {
            InventarioObj producto = new InventarioObj();
            producto.ID = id;
            try
            {
                var resultado = model.EliminarProducto
                    
                    (producto, _configuration);

                if (resultado == 1)
                {
                    return RedirectToAction("VerInventario", "Inventario");
                }
                else
                {
                    throw new Exception($"Error al Eliminar, Id No encontrado");
                    return RedirectToAction("VerInventario", "Inventario");
                }
            }
            catch (Exception e)
            {
                string? cedula = HttpContext.Session.GetString("Cedula");
                Bmode.RegistrarErrores(cedula, e, "EliminarProducto", _configuration);
                return BadRequest(e.Message);
            }

        }

    }
}
