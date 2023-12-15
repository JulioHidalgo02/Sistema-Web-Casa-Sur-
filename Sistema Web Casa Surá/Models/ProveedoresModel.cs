using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;
namespace Sistema_Web_Casa_Surá.Models
{
    public class ProveedoresModel
    {
        //Método para agregar los proveedores de productos al sistema
        public int AgregarProveedores(ProveedoresObj proveedores, IConfiguration stringConnection)
        {   //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_AGREGAR_PROVEEDOR",
                new { proveedores.NOMBRE, proveedores.EMPRESA, proveedores.TEL, proveedores.CORREO },
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

        //Método para editar la información de los proveedores
        public int EditarProveedores(ProveedoresObj proveedores, IConfiguration stringConnection)
        {   
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_PROVEEDOR",
                new { proveedores.ID, proveedores.NOMBRE, proveedores.EMPRESA, proveedores.TEL, proveedores.CORREO },
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

        //Método para eliminar proveedores del sistema
        public int EliminarProveedores(ProveedoresObj proveedores, IConfiguration stringConnection)
        {   //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_PROVEEDORES",
                new { proveedores.ID },
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

        //Método para mostrar la lista de proveedores en el sistema
        public List<ProveedoresObj>? VerProveedores(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas los proveedores creadas en la bd.
                var datos = connection.Query<ProveedoresObj>("SP_MOSTRAR_PROVEEDORES",
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

        //Metodo para mostrar un proveedor en específico
        public ProveedoresObj? VerProveedorUnico(IConfiguration stringConnection, ProveedoresObj proveedor)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer el proveedor que se necesite
                var datos = connection.Query<ProveedoresObj>("SP_MOSTRAR_PROVEEDOR_UNICO",
                        new { proveedor.ID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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