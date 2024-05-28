﻿
using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Paciente
    {
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string nombre { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap1 { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string ap2 { get; set; }
        [Key] public required int cedula { get; set; }
        public required DateOnly nacimiento { get; set; }
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string direccion { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string correo { get; set; }
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string password { get; set; }
    }
}

