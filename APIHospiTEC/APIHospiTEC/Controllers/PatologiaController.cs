using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatologiaController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public PatologiaController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("registrar_patologia")]

        public async Task<IActionResult> InsertPatologia(Patologia modelo)
        {
            var result = await _postgresql.InsertPatologiaAsync(modelo);
            return Ok(result);

        }
        [HttpGet]
        [Route("patologias")]
        public async Task<IActionResult> GetPatologias()
        {
            var data = await _postgresql.GetPatologiasAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("patologia/{id}")]
        public async Task<IActionResult> GetPatologiaPorId(int id)
        {
            var data = await _postgresql.GetPatologiaPorIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
