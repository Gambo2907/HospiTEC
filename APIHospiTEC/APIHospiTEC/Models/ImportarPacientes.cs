using System.ComponentModel.DataAnnotations;

namespace APIHospiTEC.Models
{
    public class ImportarPacientes
    {
        [Required]
        public IFormFile Archivo { get; set; }
    }
}
