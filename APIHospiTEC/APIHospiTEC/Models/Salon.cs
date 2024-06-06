using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Salon
    {
        [Key] public required int numsalon {  get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string nombre { get; set; }
        public int? piso { get; set; }
        public required int id_tipo_medicina { get; set; }
    }
    public class SalonParaPut
    {

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string nombre { get; set; }
        public int? piso { get; set; }
        public required int id_tipo_medicina { get; set; }
    }
}
