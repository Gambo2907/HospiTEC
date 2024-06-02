using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public SalonController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }

        [HttpPost]
        [Route("registrar_salon")]

        public async Task<IActionResult> InsertSalon(Salon modelo)
        {
            var result = await _postgresql.InsertSalonAsync(modelo.numsalon, modelo.nombre, modelo.piso, modelo.id_tipo_medicina);
            return Ok(result);

        }
        [HttpGet]
        [Route("salones")]
        public async Task<IActionResult> GetSalones()
        {
            var data = await _postgresql.GetSalonesAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("salones/{numsalon}")]
        public async Task<IActionResult> GetSalon(int numsalon)
        {
            var data = await _postgresql.GetSalonPorNumSalonAsync(numsalon);
            return Ok(data);
        }
    }
}
