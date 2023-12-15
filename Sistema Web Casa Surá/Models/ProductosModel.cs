using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class ProductosModel
    {
        //Método para agregar productos al sistema

        //Método para Mostrar un producto en especifico del sistema
        public InventarioObj? VerProductoUnico(IConfiguration stringConnection, InventarioObj producto)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<InventarioObj>("SP_MOSTRAR_PRODUCTO_UNICO",
                        new { producto.ID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                //Se compara si lo que devuelve la ejecución del SP, es diferente de nulo
                if (datos != null)
                {
                    return datos;
                }
                else
                {
                    return null;
                }
            }

        }

        public int RegistrarFactura(FacturaObj factura, IConfiguration stringConnection)
        {
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("SP_AGREGARFACTURA",
                     new { factura.IDUSUARIO, factura.Detalle },
                     commandType: CommandType.StoredProcedure);


            }
        }

        public int RegistrarDetalleFactura(FacturaObj factura, IConfiguration stringConnection)
        {
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("SP_AGREGARDETALLEFACTURA",
                     new { factura.CANTCOMPRADA, factura.TOTAL_LINEA, factura.IDPRODUCTO },
                     commandType: CommandType.StoredProcedure);


            }
        }

        public List<VentasObj>? VerVentas(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<VentasObj>("SP_MOSTRAR_VENTAS",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

                //Se compara si la longitud de la lista es mayor a 0
                if (datos != null)
                {
                    return datos;
                }
                else
                {
                    return null;
                }
            }

        }



    }
}
