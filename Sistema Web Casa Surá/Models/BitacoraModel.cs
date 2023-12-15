using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Web_Casa_Surá.Entities;
using System.Data;

namespace Sistema_Web_Casa_Surá.Models
{
    public class BitacoraModel
    {
        public void RegistrarErrores(string CEDULA, Exception ex, string ACCION, IConfiguration stringConnection)
        {
            using var connection = new MySqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value);
            string CODIGO = ex.HResult.ToString();
            if(CODIGO == null)
            {
                CODIGO = "N/A";
            }
            string MENSAJE = ex.Message;
            if (MENSAJE == null)
            {
                MENSAJE = "N/A";
            }
            var FECHA = DateTime.Now;

            connection.Execute("SP_CREAR_ERRORES", new { CEDULA, FECHA, CODIGO, MENSAJE, ACCION }, commandType: CommandType.StoredProcedure);
            
        }

       
    }
}
