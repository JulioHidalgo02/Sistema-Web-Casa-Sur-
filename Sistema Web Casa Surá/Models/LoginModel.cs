using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net.Mail;

namespace Sistema_Web_Casa_Surá.Models
{
    public class LoginModel
    {
        //Método para registro de usuarios clientes del sistema
        public int RegistroUsuarioCliente(LoginObj usuario, IConfiguration stringConnection)
        {
            var bandera = 1;
            var cedula = usuario.CED;
            //Se recupera la conexión a bases de datos del archivo appsettings
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todos los usuarios creados en la bd.
                var datos = connection.Query<LoginObj3>("SP_VER_USUARIOS",
                        new {}, commandType: CommandType.StoredProcedure).ToList();

                //Se recorre la lista de usuarios
                foreach(var item in datos)
                {
                    //Se compara con la cedula que se va a ingresar al sistema.
                    if(cedula == item.Cedula)
                    {
                        bandera = -1;
                        break;
                    }
                    else
                    {
                        bandera = 1;
                    }
                }
                if(bandera == 1)
                {

                    //Se realiza la ejecución del procedimiento almacenado de la base de datos por medio de Dapper
                    var resultado = connection.Execute("SP_REGISTRAR_CLIENTE",
                    new { usuario.CED, usuario.NOM, usuario.PAPE, usuario.SAPE, usuario.CORREO, usuario.TEL, usuario.PROVINCIA, usuario.CANTON, usuario.DISTRITO, usuario.DIRECCION, usuario.CONTRA },
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

        public LoginObj2? ValidarCrendeciales(LoginObj usuario, IConfiguration stringConnection)
        {
            //Se recupera la conexión a bases de datos del archivo appsettings.
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se utiliza el comando Query para utilizar el SP que se encuentra en la base de datos y se guardan los datos en la variable datos.
                var datos = connection.Query<LoginObj2>("SP_AUTENTICACION",
                        new { usuario.CORREO, usuario.CONTRA }, commandType: CommandType.StoredProcedure).FirstOrDefault();


                //Se realiza la validación de si el objeto es diferente de nulo para retornarlo posteriormente.
                if (datos != null)

                    return datos;
                else
                    return null;
            }        

        }

        public int OlvidoContrasenia(LoginObj2 usuario, IConfiguration stringConnection)
        {
            var bandera = 0;
            var correo = usuario.CORREO;
            using (var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                //Se realiza la ejecución del procedimiento almacenado para traer todos los usuarios creados en la bd.
                var listaUsuarios = connection.Query<LoginObj3>("SP_VER_USUARIOS",
                        new { }, commandType: CommandType.StoredProcedure).ToList();

                //Se recorre la lista de usuarios
                foreach (var item in listaUsuarios)
                {
                    //Se compara con la cedula que se va a ingresar al sistema.
                    if (correo == item.CorreoElectronico)
                    {
                        bandera = 1;
                        break;
                    }
                    else
                    {
                        bandera = -1;
                    }
                }
                //Si la bandera es 1 significa que si encontró un correo igual al que se ingresó
                if(bandera == 1)
                {
                    //Se procede a hacer uso de SP para recuperar la contraseña
                    var datos = connection.Query<LoginObj2>("SP_OLVIDO_CONTRASENIA",
                                        new { usuario.CORREO }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (datos != null)
                    {
                        //Si lo que devuele la ejecución no es nulo, se procede a guardar los datos en variables
                        usuario.Contrasenia = datos.Contrasenia;
                        datos.CORREO = usuario.CORREO;

                        //Se llama al metodo para enviar correos electronicos y se pasan los datos.
                        EnviarCorreo(datos);

                        return 1;
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

        public void EnviarCorreo(LoginObj2 correo)
        {
            //Este metodo se utiliza para enviar correos electronicos
            string servidor = "smtp.office365.com";
            int puerto = 587;
            string correoElectronico = "casasura7418@outlook.com";
            string contrasenia = "Acosta7418*";

            SmtpClient client = new SmtpClient(servidor, puerto);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(correoElectronico, contrasenia);

            MailAddress from = new MailAddress(correoElectronico, String.Empty, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(correo.CORREO);
            MailMessage message = new MailMessage(from, to);

            message.Body = "Su contraseña se actualizó exitosamente, su nueva contraseña es: " + correo.Contrasenia;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Restrablecimiento de contraseña";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(message);
        }

    }
}
