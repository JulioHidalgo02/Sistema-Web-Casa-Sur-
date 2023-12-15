using System.ComponentModel.DataAnnotations;

namespace Sistema_Web_Casa_Surá.Entities
{
    public class LoginObj: ValidationAttribute
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[1-9]-?\\d{4}-?\\d{4}$",ErrorMessage="Digíte una cédula válida")]
        public string CED { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public int ID { get; set; } = 0;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un nombre válido")]
        public string NOM { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un apellido válido")]
        public string PAPE { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^([A-Z]{1}[a-zñáéíóú]+[\\s]*)+$", ErrorMessage = "Digíte un apellido válido")]
        public string  SAPE { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("[\\w]+@{1}[\\w]+\\.[a-z]{2,3}", ErrorMessage = "Digíte un correo electrónico válido")]
        public string CORREO { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^[5-9]\\d{3}-?\\d{4}$", ErrorMessage = "Digíte un número de télefono válido")]
        public string TEL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string PROVINCIA { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string CANTON { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string DISTRITO { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string DIRECCION { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])([A-Za-z\\d$@$!%*?&]|[^ ]){8,15}$", ErrorMessage = "Digíte una contraseña válida")]
        public string CONTRA { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        [Compare("CONTRA", ErrorMessage = "Las contraseñas no coinciden.")]
        public string CONTRA2 { get; set; } = string.Empty;
    }

    public class LoginObj2
    {
        public string Cedula { get; set; } = string.Empty;

        public string CORREO { get; set; } = string.Empty;

        public int IdRol { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Papellido { get; set; } = string.Empty;
        public string Sapellido { get; set; } = string.Empty;

        public string Contrasenia { get; set; } = string.Empty;
    }

    public class LoginObj3
    {
        public string Cedula { get; set; } = string.Empty;
        public int IdRol { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Papellido { get; set; } = string.Empty;
        public string Sapellido { get; set; } = string.Empty;

        public string CorreoElectronico { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Canton { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public string DireccionExacta { get; set; } = string.Empty;


    }

}
