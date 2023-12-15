using System.ComponentModel.DataAnnotations;
namespace Sistema_Web_Casa_Surá.Entities
{
    public class CategoriaObj:ValidationAttribute
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string DESCRIPCION { get; set; } = string.Empty;
    }
}
