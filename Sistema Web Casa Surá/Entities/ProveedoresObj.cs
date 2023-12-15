using System.ComponentModel.DataAnnotations;

namespace Sistema_Web_Casa_Surá.Entities
{
    public class ProveedoresObj: ValidationAttribute
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un nombre válido")]
        public string NOMBRE { get; set; } = String.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string EMPRESA { get; set; } = String.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[1-9]\\d{3}-?\\d{4}$", ErrorMessage = "Digíte un número de télefono válido")]
        public string TEL { get; set; } = String.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("[\\w]+@{1}[\\w]+\\.[a-z]{2,3}", ErrorMessage = "Digíte un correo electrónico válido")]
        public string CORREO { get; set; } = String.Empty;
    }
}
