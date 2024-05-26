
namespace APIHospiTEC.Models
{
    public class Paciente
    {
        public required string Nombre { get; set; }
        public required string Ap1 { get; set; }
        public required string Ap2 { get; set; }
        public required int Cedula { get; set; }
        public DateOnly Direccion { get; set; }
        public int? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
    }
}

