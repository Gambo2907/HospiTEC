using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamaController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public CamaController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("crear_cama")]
        public async Task<IActionResult> InsertCama(Cama modelo)
        {
            var result = await _postgresql.InsertCamaAsync(modelo);
            return Ok(result);
        }
        [HttpGet]
        [Route("camas")]
        public async Task<IActionResult> GetCamas()
        {
            var data = await _postgresql.GetCamasAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("cama/({numcama})")]
        public async Task<IActionResult> GetCamaPorNumCama(int numcama)
        {
            var data = await _postgresql.GetCamaPorNumCamaAsync(numcama);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("camas/({num_salon})")]
        public async Task<IActionResult> GetCamasPorNumSalon(int num_salon)
        {
            var data = await _postgresql.GetCamasPorNumSalonAsync(num_salon);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("camas_disponibles/({num_salon})")]
        public async Task<IActionResult> GetCamasDisponiblesPorNumSalon(int num_salon)
        {
            var data = await _postgresql.GetCamasDisponiblesPorNumSalonAsync(num_salon);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("camas_disponibles")]
        public async Task<IActionResult> GetCamasDisponibles()
        {
            var data = await _postgresql.GetCamasDisponiblesAsync();
            return Ok(data);
        }
        [HttpPut]
        [Route("actualizar_cama/{numcama}")]

        public async Task<IActionResult> UpdateCama([FromBody] Cama modelo, int numcama)
        {
            modelo.numcama = numcama;
            var result = await _postgresql.UpdateCamaAsync(modelo);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }
    }
}
