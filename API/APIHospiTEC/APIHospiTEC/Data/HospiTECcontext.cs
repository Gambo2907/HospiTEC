using APIHospiTEC.Models;
using Microsoft.EntityFrameworkCore;

namespace APIHospiTEC.Data
{
    public class HospiTECcontext : DbContext
    {
        public HospiTECcontext(DbContextOptions<HospiTECcontext> options) : base(options)
        {
        }
        public DbSet<Paciente> paciente { get; set; }
    }
}
