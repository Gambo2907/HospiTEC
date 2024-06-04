namespace APIHospiTEC.Models
{
    public class PatologiaPorPaciente
    {
        public required int id_patologia {  get; set; }
        public required int cedulapaciente { get; set; }
        public required string tratamiento { get; set; }
    }
}
