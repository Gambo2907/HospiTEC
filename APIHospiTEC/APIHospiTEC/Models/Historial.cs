using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Models
{
    public class Historial
    {
        [Key] public required int id_historial { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required DateOnly fecha { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Tratamiento { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required int cedula { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required int id_procedimiento { get; set; }
    }
}