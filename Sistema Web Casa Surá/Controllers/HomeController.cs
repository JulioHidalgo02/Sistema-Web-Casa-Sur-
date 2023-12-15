using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using System.Diagnostics;

namespace Sistema_Web_Casa_Surá.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;
        private InventarioModel model = new InventarioModel();
        private CategoriaModel catModel = new CategoriaModel();
        private MantenimientoModel manModel = new MantenimientoModel();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
                var datos = model.VerProductos(_configuration);
                return View(datos);
        }

        public IActionResult Index2()
        {
            var TotalHoy = manModel.TotalVentasHoy(_configuration);
            var Total = manModel.TotalVentas(_configuration);
            var Cantidad = manModel.CantidadVentasHoy(_configuration);
            FacturaObj2 obj = new FacturaObj2();
            obj.Total = Total.Total;
            obj.TotalDia = TotalHoy.TotalDia;
            obj.Cantidad = Cantidad.Cantidad;

            return View(obj);
        }


    }
}