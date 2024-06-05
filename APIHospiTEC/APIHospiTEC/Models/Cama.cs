using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Cama
    {
        [Key] public required int numcama {  get; set; }
        public required bool uci { get; set; }
        public required int id_estadocama { get; set; }
        public required int num_salon {  get; set; }
    }
    public class CamaParaPut
    {
        public required bool uci { get; set; }
        public required int num_salon { get; set; }
    }
}
