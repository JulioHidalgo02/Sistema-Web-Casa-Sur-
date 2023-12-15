using System.ComponentModel.DataAnnotations;

namespace Sistema_Web_Casa_Surá.Entities
{
    public class ApartadosObj : ValidationAttribute
    {
        public long IdApartados { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[1-9]-?\\d{4}-?\\d{4}$", ErrorMessage = "Digíte una cédula válida")]
        public string CedulaUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        
        public DateTime FechaLimite { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([1-9]\\d*(\\.\\d*[1-9][0-9])?)|(0\\.\\d*[1-9][0-9])|(0\\.\\d*[1-9])$", ErrorMessage = "Solo se admite números mayores que 0")]
        public decimal? Saldo { get; set; }


        public int Abonos { get; set; }

        public string Estado { get; set; }

        public string Nombre { get; set; }

        public string Papellido { get; set; }
        public string Sapellido { get; set; }
        public string Telefono { get; set; }

        public decimal? Abonar { get; set; }

    }
}
