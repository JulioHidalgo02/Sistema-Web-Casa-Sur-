using System.ComponentModel.DataAnnotations;
namespace Sistema_Web_Casa_Surá.Entities
{
    public class VentasObj: ValidationAttribute
    {
 
        public int ID { get; set; }


        public int Cedula { get; set; }


        public string Nombre { get; set; } = string.Empty;
          public int Total { get; set; }

        public string DetallePago { get; set; } = string.Empty;
    }
}
