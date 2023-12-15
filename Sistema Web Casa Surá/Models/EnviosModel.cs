using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class EnviosModel
    {

        //Método para agregar categorías de productos del sistema
        public int CrearEnvio(EnviosObj envios, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                    //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                    var resultado = connection.Execute("SP_AGREGAR_ENVIO",
                    new { envios.ID,envios.Cedula,envios.Rastreo,envios.Estado},
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

        //Método para editar categorías de productos del sistema
        public int EditarEnvio(EnviosObj envios, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_ENVIO",
                new { envios.ID,envios.Cedula,envios.Rastreo,envios.Estado },
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

        //Método para eliminar categorías de productos del sistema
        public int EliminarEnvio(EnviosObj envios, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_ENVIO",
                new { envios.ID},
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

        //Método para mostrar todas las categorias del sistema
        public List<EnviosObj>? VerEnvios(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<EnviosObj>("SP_MOSTRAR_ENVIOS",
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

        //Metodo para mostrar una categoría en específico
        public EnviosObj? VerEnvioUnico(IConfiguration stringConnection, EnviosObj envios)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<EnviosObj>("SP_MOSTRAR_ENVIO_UNICO",
                        new { envios.ID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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
