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
        [HttpPost]
        [Route("crear_equipo_por_cama")]
        public async Task<IActionResult> InsertEquipoPorCama(EquipoPorCama modelo)
        {
            var result = await _postgresql.InsertEquipoPorCamaAsync(modelo);
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
        [Route("equipo_por_cama/{numcama}")]
        public async Task<IActionResult> GetEquipoPorCama(int numcama)
        {
            var data = await _postgresql.GetEquipoPorCamaAsync(numcama);
            if (data == null)
            {
                return NotFound();
            }
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
        [Route("camas_disponibles")]
        public async Task<IActionResult> GetCamasDisponibles(DateOnly fecha)
        {
            var data = await _postgresql.GetCamasDisponiblesPorFechaAsync(fecha);
            return Ok(data);
        }
        [HttpPut]
        [Route("actualizar_cama/{numcama}")]

        public async Task<IActionResult> UpdateCama([FromBody] CamaParaPut modelo, int numcama)
        {
            
            var result = await _postgresql.UpdateCamaAsync(modelo,numcama);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }
    }
}
