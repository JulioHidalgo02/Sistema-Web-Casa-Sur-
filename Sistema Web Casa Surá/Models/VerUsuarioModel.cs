using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class VerUsuarioModel
    {
        public VerUsuarioObj2? VerUsuario(IConfiguration stringConnection, string Ced)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query("SP_VER_DATOS_USUARIO", new { Ced }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            VerUsuarioObj2 usuario = new VerUsuarioObj2();
            if (resultado != null)
            {
                usuario.Ced = resultado.Cedula;
                usuario.IdRol = resultado.IdRol;
                usuario.Nombre = resultado.Nombre;
                usuario.Papellido = resultado.Papellido;
                usuario.Sapellido = resultado.Sapellido;
                usuario.CorreoElectronico = resultado.CorreoElectronico;
                usuario.Telefono = resultado.Telefono;
                usuario.Provincia = resultado.Provincia;
                usuario.Canton = resultado.Canton;
                usuario.Distrito = resultado.Distrito;
                usuario.DireccionExacta = resultado.DireccionExacta;
            }
            return usuario;
        }

        public VerUsuarioObj2? VerUsuarioObj2(IConfiguration stringConnection, string Ced)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query("SP_VER_DATOS_USUARIO", new { Ced }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            VerUsuarioObj2 usuario = new VerUsuarioObj2();
            if (resultado != null)
            {
                usuario.Ced = resultado.Cedula;
                usuario.Nombre = resultado.Nombre;
                usuario.Papellido = resultado.Papellido;
                usuario.Sapellido = resultado.Sapellido;
                usuario.CorreoElectronico = resultado.CorreoElectronico;
                usuario.Telefono = resultado.Telefono;
                usuario.Provincia = resultado.Provincia;
                usuario.Canton = resultado.Canton;
                usuario.Distrito = resultado.Distrito;
                usuario.DireccionExacta = resultado.DireccionExacta;
            }
            return usuario;
        }


        public List<VerUsuarioObj>? VerClientes(IConfiguration stringConnection)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query<VerUsuarioObj>("SP_VER_USUARIOs", new { }, commandType: CommandType.StoredProcedure).ToList();
            if (resultado != null)
            {
               return resultado;
            }
            else 
            {
                return null;
            }
        }

        public int EditarUsuario(IConfiguration stringConnection, VerUsuarioObj2 usuario)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_USUARIO",
                     new { usuario.Ced,usuario.Nombre, usuario.Papellido, usuario.Sapellido, usuario.CorreoElectronico, usuario.Telefono,
                         usuario.Provincia, usuario.Canton, usuario.Distrito, usuario.DireccionExacta
                     },
                     commandType: CommandType.StoredProcedure);

                //Se compara el resultado que devuelve el procedimiento y se compara para retornar el un valor numerico
                 if (resultado != 0)
                {
                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int CambiarContrasenia(IConfiguration stringConnection, VerUsuarioObj usuario)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_CAMBIAR_CONTRASENIA",
                     new
                     {
                         usuario.loginObj.CED,
                         usuario.loginObj.CONTRA
                     },
                     commandType: CommandType.StoredProcedure);

                //Se compara el resultado que devuelve el procedimiento y se compara para retornar el un valor numerico
                if (resultado != 0)
                {
                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
        }


        public int EliminarCliente(string cedula, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_CLIENTE",
                new { cedula },
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


    }
}
