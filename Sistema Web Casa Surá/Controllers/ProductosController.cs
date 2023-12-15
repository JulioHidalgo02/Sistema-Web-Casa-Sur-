using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal_WebApp.Helper;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using System.Text.Json;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class ProductosController : Controller
    {

        private readonly IConfiguration _configuration;
        private ProductosModel model = new ProductosModel();
        private InventarioModel modelInventario = new InventarioModel();
        private VerUsuarioModel modelUsuario = new VerUsuarioModel();


        public ProductosController(HelperUploadFiles helperUpload, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult DetalleProducto(InventarioObj producto)
        {
            var datos = model.VerProductoUnico(_configuration, producto);
            return View(datos);
        }

        public IActionResult Carrito()
        {
            try
            {
                var carritoActual = HttpContext.Session.GetString("DatosCarrito");
                var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);

                return View(contenido);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(InventarioObj producto)
        {
            try
            {

                FacturaObj obj = new FacturaObj();
                obj.Producto = producto.NOMBRE;
                obj.Precio = producto.PRECIO.Value;
                obj.CANTCOMPRADA = producto.CANTDISP.Value;
                obj.IDPRODUCTO = producto.ID;
                obj.TOTAL_LINEA = (producto.PRECIO.Value * producto.CANTDISP.Value);
                producto.CANTDISP = 0;
                modelInventario.EditarProducto(producto, _configuration);
                var carritoActual = HttpContext.Session.GetString("DatosCarrito");
                var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
                obj.IDUSUARIO = HttpContext.Session.GetString("Cedula");
                if (contenido.Count < 1)
                {
                    contenido.Add(new FacturaObj { CANTCOMPRADA = obj.CANTCOMPRADA, IDPRODUCTO = obj.IDPRODUCTO, IDUSUARIO = obj.IDUSUARIO, Producto = obj.Producto, Precio = obj.Precio, TOTAL_LINEA = obj.TOTAL_LINEA });
                }
                else if (contenido.Count >= 1)
                {
                    contenido.Add(new FacturaObj { CANTCOMPRADA = obj.CANTCOMPRADA, IDPRODUCTO = obj.IDPRODUCTO, IDUSUARIO = obj.IDUSUARIO, Producto = obj.Producto, Precio = obj.Precio, TOTAL_LINEA = obj.TOTAL_LINEA });

                }


                var contenidoNuevo = JsonSerializer.Serialize(contenido);
                HttpContext.Session.SetString("DatosCarrito", contenidoNuevo);
                carritoActual = HttpContext.Session.GetString("DatosCarrito");
                contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public IActionResult EliminarCarrito(int id)
        {
            InventarioObj inventario = new InventarioObj();
            inventario.ID = id;
            var producto = modelInventario.VerProductoUnico(_configuration, inventario);

            FacturaObj obj = new FacturaObj();
            obj.IDPRODUCTO = id;
            try
            {
                var carritoActual = HttpContext.Session.GetString("DatosCarrito");
                var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
                foreach (var item in contenido)
                {
                    if (item.IDPRODUCTO == obj.IDPRODUCTO)
                    {
                        contenido.Remove(item);
                        break;
                    }
                }
                producto.CANTDISP = 1;
                modelInventario.EditarProducto(producto, _configuration);
                var contenidoNuevo = JsonSerializer.Serialize(contenido);
                HttpContext.Session.SetString("DatosCarrito", contenidoNuevo);
                carritoActual = HttpContext.Session.GetString("DatosCarrito");
                contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
                return RedirectToAction("Carrito", "Productos");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public IActionResult VaciarCarrito()
        {
            try
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
                HttpContext.Session.Remove("DatosCarrito");
                var carrito = new List<FacturaObj>();
                var contenido2 = JsonSerializer.Serialize(carrito);

                HttpContext.Session.SetString("DatosCarrito", contenido2);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Checkout()
        {
            VerUsuarioObj2 obj = new VerUsuarioObj2();
            var Ced = HttpContext.Session.GetString("Cedula");
            var resultado = modelUsuario.VerUsuario(_configuration, Ced);
            var carritoActual = HttpContext.Session.GetString("DatosCarrito");
            var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
            obj = resultado;
            obj.objetos = contenido;
            return View(obj);
        }

        [HttpPost]
        public IActionResult CrearFactura(string obj)
        {
            try
            {
                FacturaObj factura = new FacturaObj();
                var Cedula = HttpContext.Session.GetString("Cedula");
                factura.IDUSUARIO = Cedula;
                factura.Detalle = obj;
                model.RegistrarFactura(factura, _configuration);
                var carritoActual = HttpContext.Session.GetString("DatosCarrito");
                var contenido = JsonSerializer.Deserialize<List<FacturaObj>>(carritoActual);
                foreach (var item in contenido)
                {
                    model.RegistrarDetalleFactura(item, _configuration);
                }
                HttpContext.Session.Remove("DatosCarrito");
                var carrito = new List<FacturaObj>();
                var NuevoCarrito = JsonSerializer.Serialize(carrito);
                HttpContext.Session.SetString("DatosCarrito", NuevoCarrito);
                return RedirectToAction("ConfirmacionCompra", "Paypal");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult VerVentas()
        {
            
            var datos = model.VerVentas(_configuration);
            return View(datos);
        }

    }
}
