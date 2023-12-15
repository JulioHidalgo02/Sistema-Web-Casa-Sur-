using System.ComponentModel.DataAnnotations;
namespace Sistema_Web_Casa_Surá.Entities
{
    public class EnviosObj: ValidationAttribute
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[1-9]-?\\d{4}-?\\d{4}$", ErrorMessage = "Digíte una cédula válida")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Rastreo { get; set; } = string.Empty;


        public string Estado { get; set; } = String.Empty;

        public string Nombre { get; set; } = String.Empty;
    }
}
