using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public EquipoController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("crear_equipo")]
        public async Task<IActionResult> InsertEquipo(Equipo modelo)
        {
            var result = await _postgresql.InsertEquipoAsync(modelo);
            return Ok(result);
        }
        [HttpGet]
        [Route("equipos")]
        public async Task<IActionResult> GetEquipos()
        {
            var data = await _postgresql.GetEquiposMedicosAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("equipo/{id}")]
        public async Task<IActionResult> GetEquiposPorID(int id)
        {
            var data = await _postgresql.GetEquipoPorIdAsync(id);
            return Ok(data);
        }
        [HttpPut]
        [Route("actualizar_equipo/{id}")]

        public async Task<IActionResult> UpdateEquipo([FromBody] EquipoParaPut modelo, int id)
        {
            var result = await _postgresql.UpdateEquipoAsync(modelo,id);
            if (result == 0)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }
    }
}
