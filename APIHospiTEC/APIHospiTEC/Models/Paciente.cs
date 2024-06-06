using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Paciente
    {
        
        public required string nombre { get; set; }
        
        public required string ap1 { get; set; }
       
        public required string ap2 { get; set; }
        [Key] public required int cedula { get; set; }
        public required DateOnly nacimiento { get; set; }
       
        public string? direccion { get; set; }
       
        public required string correo { get; set; }
      
        public required string password { get; set; }
    }
}
