using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public HistorialController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("registrar_historial")]

        public async Task<IActionResult> InsertHistorial(Historial modelo)
        {
            var result = await _postgresql.InsertHistorialAsync(modelo.id, modelo.fecha, modelo.tratamiento, modelo.cedulapaciente, modelo.id_procedimiento);
            return Ok(result);

        }
        

        [HttpGet]
        [Route("historial_registrados")]
        public async Task<IActionResult> GetHistorial()
        {
            var data = await _postgresql.GetHistorialAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("historial/{cedula}")]
        public async Task<IActionResult> GetHistorial(int cedula)
        {
            var data = await _postgresql.GetHistorialPorCedulaAsync(cedula);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

    }
}
