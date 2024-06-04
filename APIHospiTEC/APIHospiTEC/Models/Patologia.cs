using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class Patologia
    {
        [Key] public required int id { get; set; }
        public required string nombre { get; set; }
    }
}
