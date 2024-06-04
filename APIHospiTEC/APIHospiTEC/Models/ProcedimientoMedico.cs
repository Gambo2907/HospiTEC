using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class ProcedimientoMedico
    {
        public required int ID {  get; set; }
        public required string nombre { get; set; }
        public required int cantdias {  get; set; }
    }
}
