
using System.ComponentModel.DataAnnotations;

namespace Sistema_Web_Casa_Surá.Entities
{
    public class VerUsuarioObj
    {
        public string Cedula { get; set; }
        public long IdRol { get; set; }
        public string Nombre { get; set; }
        public string Papellido { get; set; }
        public string Sapellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
        public LoginObj loginObj { get; set; }
    }

    public class VerUsuarioObj2 : ValidationAttribute
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[1-9]-?\\d{4}-?\\d{4}$", ErrorMessage = "Digíte una cédula válida")]
        public string Ced { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un nombre válido")]
        public string Nombre { get; set; }

        public long IdRol { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un apellido válido")]
        public string Papellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un apellido válido")]
        public string Sapellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("[\\w]+@{1}[\\w]+\\.[a-z]{2,3}", ErrorMessage = "Digíte un correo electrónico válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[5-9]\\d{3}-?\\d{4}$", ErrorMessage = "Digíte un número de télefono válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Canton { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string DireccionExacta { get; set; }

        public LoginObj loginObj { get; set; }
        public List<FacturaObj> objetos { get; set; }
    }


}
