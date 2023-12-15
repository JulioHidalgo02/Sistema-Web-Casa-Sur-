using System.ComponentModel.DataAnnotations;
namespace Sistema_Web_Casa_Surá.Entities
{
    public class InventarioObj: ValidationAttribute
    {
 
        public int ID { get; set; }


        public int  CATEGORIA { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        public string NOMBRE { get; set; } = string.Empty;



        public string NOMBRECAT { get; set; } = string.Empty;


        [Required(ErrorMessage = "Este campo es requerido")]
        public string MARCA { get; set; } = string.Empty;


        [Required(ErrorMessage = "Este campo es requerido")]
        public string TALLA { get; set; } = string.Empty;


        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([1-9]\\d*(\\.\\d*[1-9][0-9])?)|(0\\.\\d*[1-9][0-9])|(0\\.\\d*[1-9])$", ErrorMessage = "Solo se admite números mayores que 0")]
        public decimal? PRECIO { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        public string DESCRIPCION { get; set; } = string.Empty;


        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([1-9]\\d*(\\.\\d*[1-9][0-9])?)|(0\\.\\d*[1-9][0-9])|(0\\.\\d*[1-9])$", ErrorMessage = "Solo se admite números mayores que 0")]
        public int? CANTDISP { get; set; }

        public string URL { get; set; } = String.Empty;

        public string FACTOR { get; set; } = String.Empty;
    }

    public class ErroresObj 
    {
        public int Id { get; set; }
       public string Cedula { get; set; } = String.Empty;

       public DateTime Fecha { get; set; }
       public string Accion { get; set; } = String.Empty;
       public string Codigo { get; set; } = String.Empty;
        public string Mensaje { get; set; } = String.Empty;

    }
}
