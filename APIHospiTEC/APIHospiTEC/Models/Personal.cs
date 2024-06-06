using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Personal
    {
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string nombre { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap1 { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap2 { get; set; }
        [Key] public required int cedula { get; set; }
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string? direccion { get; set; }
        public required DateOnly nacimiento { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string correo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string password { get; set; }
        public required DateOnly fechaingreso { get; set; }
        public required int idtipopersonal { get; set; }
    }
    public class PersonalParaPut
    {
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string nombre { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap1 { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap2 { get; set; }
        public string? direccion { get; set; }
        public required DateOnly nacimiento { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string correo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string password { get; set; }
        public required DateOnly fechaingreso { get; set; }
        public required int idtipopersonal { get; set; }
    }
}
