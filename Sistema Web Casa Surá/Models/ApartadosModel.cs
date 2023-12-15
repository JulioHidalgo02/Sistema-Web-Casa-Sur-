using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class ApartadosModel
    {
        public List<ApartadosObj>? VerApartados(IConfiguration stringConnection)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query<ApartadosObj>("SP_VER_APARTADOS", new { }, commandType: CommandType.StoredProcedure).ToList();
            if (resultado != null)
            {
                return resultado;
            }
            else
            {
                return null;
            }
        }

        public ApartadosObj? VerApartadoUnico(IConfiguration stringConnection, long IdApartados)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            var resultado = connection.Query("SP_VER_DATOS_APARTADOS", new { IdApartados }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            ApartadosObj apartado = new ApartadosObj();
            if (resultado != null)
            {
                apartado.CedulaUsuario = resultado.CedulaUsuario;
                apartado.IdApartados = resultado.IdApartados;
                apartado.Nombre = resultado.Nombre;
                apartado.Papellido = resultado.Papellido;
                apartado.Sapellido = resultado.Sapellido;
                apartado.Telefono = resultado.Telefono;
                apartado.FechaLimite = resultado.FechaLimite;
                apartado.Saldo = resultado.Saldo;
                apartado.Abonos = resultado.Abonos;
                apartado.Estado = resultado.Estado;
            }
            return apartado;
        }

        public int EditarApartado(IConfiguration stringConnection, ApartadosObj apartado)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_EDITAR_APARTADO",
                     new
                     { apartado.IdApartados, apartado.FechaLimite, apartado.Saldo }, commandType: CommandType.StoredProcedure);

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

        public int EliminarApartado(long idApartados, IConfiguration stringConnection)
        {

            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                var resultado = connection.Execute("SP_ELIMINAR_APARTADO",
                new { idApartados },
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

        public int RegistrarApartado(ApartadosObj apartado, IConfiguration stringConnection)
        {
            var bandera = 1;
            var cedula = apartado.CedulaUsuario;
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todos los usuarios creados en la bd.
                var datos = connection.Query<ApartadosObj>("SP_VER_USUARIOS",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

                //Se recorre la lista de usuarios
                foreach (var item in datos)
                {
                    //Se compara con la cedula que se va a ingresar al sistema.
                    if (cedula == item.CedulaUsuario)
                    {
                        bandera = -1;
                        break;
                    }
                    else
                    {
                        bandera = 1;
                    }
                }
                if (bandera == 1)
                {

                    //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                    var resultado = connection.Execute("SP_AGREGAR_APARTADO",
                    new { apartado.CedulaUsuario, apartado.FechaLimite, apartado.Saldo},
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
                else
                {
                    return -1;
                }

            }
        }


        public int ActualizarApartado(IConfiguration stringConnection, long idApartados, decimal Abonar, decimal Saldo)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var ID = idApartados;
                if(Saldo < Abonar)
                {
                    return -2;
                }
                else if(Saldo >= Abonar)
                {
                    var Plata = Saldo - Abonar;
                    var realizarAbono = connection.Execute("SP_ACTUALIZAR_SALDO", new { ID, Plata }, commandType: CommandType.StoredProcedure);
                    var resultado = connection.Execute("SP_AGREGAR_ABONO", new{ ID}, commandType: CommandType.StoredProcedure);

                    return resultado;

                }
                else
                {
                    return -1;
                }
                return -1;
            }
        }

    }
}