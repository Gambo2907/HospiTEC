using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Equipo
    {
        [Key] public required int id { get; set; }
        public required string nombre { get; set; }
        public required string proveedor { get; set; }
        public required int cantdisponible { get; set; }
    }
    public class EquipoParaPut
    {
        public required string nombre { get; set; }
        public required string proveedor { get; set; }
        public required int cantdisponible { get; set; }
    }
}