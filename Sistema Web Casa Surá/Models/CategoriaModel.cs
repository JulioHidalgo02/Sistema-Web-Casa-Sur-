using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class CategoriaModel
    {

        //Método para agregar categorías de productos del sistema
        public int AgregarCategoria(CategoriaObj categoria, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                    //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                    var resultado = connection.Execute("SP_AGREGAR_CATEGORIA",
                    new { categoria.DESCRIPCION},
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
        public int EditarCategoria(CategoriaObj categoria, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_CATEGORIA",
                new { categoria.ID,categoria.DESCRIPCION },
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
        public int EliminarCategoria(CategoriaObj categoria, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_CATEGORIA",
                new { categoria.ID},
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
        public List<CategoriaObj>? VerCategorias(IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<CategoriaObj>("SP_MOSTRAR_CATEGORIAS",
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
        public CategoriaObj? VerCategoriaUnica(IConfiguration stringConnection, CategoriaObj categoria)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todas las categorias creadas en la bd.
                var datos = connection.Query<CategoriaObj>("SP_MOSTRAR_CATEGORIA_UNICA",
                        new { categoria.ID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

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
