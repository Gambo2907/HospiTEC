
using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Paciente
    {
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Nombre { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Ap1 { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Ap2 { get; set; }
        [MaxLength(9, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Key] public required string Cedula { get; set; }
        public required DateOnly Direccion { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Correo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Password { get; set; }
    }
}

