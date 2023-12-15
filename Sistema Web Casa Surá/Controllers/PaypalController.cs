using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoFinal_WebApp.Helper;
using Sistema_Web_Casa_Surá.Entities;
using Sistema_Web_Casa_Surá.Models;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using static Sistema_Web_Casa_Surá.Entities.TipoCambioObj;

namespace Sistema_Web_Casa_Surá.Controllers
{

    public class PaypalController : Controller
    {
         

        private readonly IConfiguration _configuration;
        private ProductosModel model = new ProductosModel();

        public PaypalController(HelperUploadFiles helperUpload, IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                var contenido = JsonConvert.DeserializeObject<List<FacturaObj>>(carritoActual);
                foreach (var item in contenido)
                {
                    model.RegistrarDetalleFactura(item, _configuration);
                }
                HttpContext.Session.Remove("DatosCarrito");
                var carrito = new List<FacturaObj>();
                var NuevoCarrito = JsonConvert.SerializeObject(carrito);
                HttpContext.Session.SetString("DatosCarrito", NuevoCarrito);
                return RedirectToAction("ConfirmacionCompra", "Paypal");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult ConfirmacionCompra()
        {
            return View();
        }



        [HttpGet]
        public async Task<ActionResult> Orden()
        {

            //id de la autorizacion para obtener el dinero
            string token = Request.Query["token"];

            bool status = false;


            using (var client = new HttpClient())
            {

                //INGRESA TUS CREDENCIALES AQUI -> CLIENT ID - SECRET
                var userName = _configuration.GetValue<string>("Paypal:Key");
                var passwd = _configuration.GetValue<string>("Paypal:Secret");

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);


                status = response.IsSuccessStatusCode;

                ViewData["Status"] = status;
                if (status)
                {
                    var jsonRespuesta = response.Content.ReadAsStringAsync().Result;

                    Entities.PaypalTransaccion.PaypalTransaccionObjcs objeto = JsonConvert.DeserializeObject<Entities.PaypalTransaccion.PaypalTransaccionObjcs>(jsonRespuesta);

                    ViewData["IdTransaccion"] = objeto.purchase_units[0].payments.captures[0].id;

                    string obj = "Paypal " + objeto.purchase_units[0].payments.captures[0].id;

                    CrearFactura(obj);
                }

            }
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Paypal()
        {
            Rootobject respuesta4 = new Rootobject();
            var carritoActual = HttpContext.Session.GetString("DatosCarrito");
            var contenido = JsonConvert.DeserializeObject<List<FacturaObj>>(carritoActual);
            decimal total = 0;
            foreach(var item in contenido)
            {
                total = total + item.TOTAL_LINEA;
            }


            total = total + 2500;
            bool status = false;
            string respuesta = string.Empty;
            var producto = "Camisa";

            using (var client = new HttpClient())
            {
                string ruta = "https://tipodecambio.paginasweb.cr/api";
                HttpResponseMessage respuesta2 = client.GetAsync(ruta).Result;
                if (respuesta2.IsSuccessStatusCode)
                {
                    var respuesta3 = respuesta2.Content.ReadAsStringAsync().Result;
                    respuesta4 = JsonConvert.DeserializeObject<Rootobject>(respuesta3);
                   
                }
               
            }
            total = total / (decimal)respuesta4.compra;
            string redondeo = Math.Round(Convert.ToDecimal(total), 0).ToString();


            using (var client = new HttpClient())
            {

                //INGRESA TUS CREDENCIALES AQUI -> CLIENT ID - SECRET
                var userName = _configuration.GetValue<string>("Paypal:Key");
                var passwd = _configuration.GetValue<string>("Paypal:Secret");

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));


                var orden = new PaypalObj()
                {
                    intent = "CAPTURE",
                    purchase_units = new List<Entities.PurchaseUnit>() {

                        new Entities.PurchaseUnit() {

                            amount = new Entities.Amount() {
                                currency_code = "USD",
                                value = redondeo
                            },
                            description = producto
                        }
                    },
                    application_context = new ApplicationContext()
                    {
                        brand_name = "Casa Sura",
                        landing_page = "NO_PREFERENCE",
                        user_action = "PAY_NOW", //Accion para que paypal muestre el monto de pago
                        return_url = this.Request.Scheme + "://" + this.Request.Host + "/Paypal/Orden",// cuando se aprovo la solicitud del cobro
                        cancel_url = this.Request.Scheme + "://" + this.Request.Host + "/Home/Index"// cuando cancela la operacion
                    }
                };


                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);


                status = response.IsSuccessStatusCode;


                if (status)
                {
                    respuesta = response.Content.ReadAsStringAsync().Result;
                }



            }

            return Json(new { status = status, respuesta = respuesta });

        }

    }
}
