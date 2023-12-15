using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class InventarioModel
    {
        //Método para agregar productos al sistema
        public int AgregarProductos(InventarioObj producto, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_AGREGAR_INVENTARIO",
                new { producto.CATEGORIA, producto.NOMBRE, producto.MARCA, producto.TALLA, producto.PRECIO, producto.DESCRIPCION, producto.CANTDISP, producto.URL },
                commandType: CommandType.StoredProcedure);

                //Se compara el resultado que devuelve el procedimiento y se compara para retornar el un valor numerico
                if (resultado == 1)
                {
                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
        }

        //Método para editar productos del sistema
        public int EditarProducto(InventarioObj producto, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_INVENTARIO",
                new { producto.ID, producto.CATEGORIA, producto.NOMBRE, producto.MARCA, producto.TALLA, producto.PRECIO, producto.DESCRIPCION, producto.CANTDISP, producto.URL },
                commandType: CommandType.StoredProcedure);

                //Se compara el resultado que devuelve el procedimiento y se compara para retornar el un valor numerico
                if (resultado == 1)
                {
                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
        }

        //Método para eliminar productos del sistema
        public int EliminarProducto(InventarioObj producto, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_INVENTARIO",
                new { producto.ID },
                commandType: CommandType.StoredProcedure);

                //Se compara el resultado que devuelve el procedimiento y se compara para retornar el un valor numerico
                if (resultado == 1)
                {
                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
        }

        //Método para Mostrar todos los productos del sistema
        public List<InventarioObj>? VerProductos(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO",
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

        //Método para Mostrar un producto en especifico del sistema
        public InventarioObj? VerProductoUnico(IConfiguration stringConnection, InventarioObj producto)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<InventarioObj>("SP_MOSTAR_INVENTARIO_UNICO",
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
    }
}

