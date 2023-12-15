using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class MantenimientoModel
    {
        //Metodo para mostrar una usuario en específico
        public LoginObj? VerUsuario(IConfiguration stringConnection, LoginObj usuario)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer un usuario creado en la bd.
                var datos = connection.Query<LoginObj>("SP_MOSTRAR_USUARIO",
                        new { usuario.CED }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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

        //Aplicar filtro de menor a mayor precio
        public List<InventarioObj>? FiltroMenor(IConfiguration stringConnection /*InventarioObj filtro*/)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para aplicar el filtro y traer los precios de menor a mayor
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO_ASC_PRECIO",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

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

        public List<InventarioObj>? FiltroMayor(IConfiguration stringConnection /*InventarioObj filtro*/)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para aplicar el filtro y traer los precios de menor a mayor
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO_DESC_PRECIO",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

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


        public List<InventarioObj>? FiltroAlfabeto(IConfiguration stringConnection /*InventarioObj filtro*/)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para aplicar el filtro y traer los precios de menor a mayor
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO_ASC_ALFABETO",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

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


        public List<InventarioObj>? FiltroNoAlfabeto(IConfiguration stringConnection /*InventarioObj filtro*/)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para aplicar el filtro y traer los precios de menor a mayor
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO_DESC_ALFABETO",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

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

        public List<InventarioObj>? Busqueda(IConfiguration stringConnection ,InventarioObj filtro)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para aplicar el filtro y traer los precios de menor a mayor
                var datos = connection.Query<InventarioObj>("SP_BUSQUEDA",
                        new {filtro.FACTOR }, commandType: CommandType.StoredProcedure).ToList();

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

        public List<ErroresObj>? VerErrores(IConfiguration stringConnection)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query<ErroresObj>("SP_MOSTRAR_ERRORES", new { }, commandType: CommandType.StoredProcedure).ToList();
            if (resultado != null)
            {
                return resultado;
            }
            else
            {
                return null;
            }
        }

        //Método para Mostrar el Total de las ventas
        public FacturaObj2? TotalVentas(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<FacturaObj2>("SP_TOTAL_VENTAS",
                        new {}, commandType: CommandType.StoredProcedure).FirstOrDefault();

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

        //Método para Mostrar el Total de las ventas
        public FacturaObj2? TotalVentasHoy(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<FacturaObj2>("SP_TOTAL_VENTAS_HOY",
                        new { }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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

        //Método para Mostrar el Total de las ventas
        public FacturaObj2? CantidadVentasHoy(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<FacturaObj2>("SP_TOTAL_CANTIDAD_VENTAS_HOY",
                        new { }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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
    }
}

