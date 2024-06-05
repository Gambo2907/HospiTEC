using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Reservacion
    {
        [Key] public required int id {  get; set; }
        public DateOnly fechaingreso { get; set; }
        public required int cedpaciente { get; set; }
        public required int idprocmed { get; set; }
        public required int numcama { get; set; }
    }
}
