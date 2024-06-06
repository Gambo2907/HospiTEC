using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class ProcedimientoMedico
    {
        public required int id {  get; set; }
        public required string nombre { get; set; }
        public required int idpatologia {  get; set; }
        public required int cantdias {  get; set; }
    }
    public class ProcedimientoMedicoParaPut
    {
        public required string nombre { get; set; }
        public required int idpatologia { get; set; }
        public required int cantdias { get; set; }
    }
}
