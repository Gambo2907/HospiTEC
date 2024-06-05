using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Models
{
    public class Historial
    {
        public required DateOnly fecha { get; set; }

        public required string tratamiento { get; set; }
        public required int cedulapaciente { get; set; }
        public required int id_procedimiento { get; set; }
    }
    public class HistorialParaPut
    {
        public required DateOnly fecha { get; set; }
        public required string tratamiento { get; set; }
        public required int id_procedimiento { get; set; }
    }
}