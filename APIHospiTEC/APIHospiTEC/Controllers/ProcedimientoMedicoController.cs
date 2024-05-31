using APIHospiTEC.Data;
using APIHospiTEC.Models;
using APIHospiTEC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIHospiTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimientoMedicoController : ControllerBase
    {
        private readonly PostgreSqlService _postgresql;

        public ProcedimientoMedicoController(PostgreSqlService postgresql)
        {
            _postgresql = postgresql;
        }
        [HttpPost]
        [Route("crear_procedimiento_medico")]
        public async Task<IActionResult> InsertProcedimientoMedico(ProcedimientoMedico modelo)
        {
            var result = await _postgresql.InsertProcedimientoMedicoAsync(modelo.nombre,modelo.cantdias);
            return Ok(result);

        }
        [HttpGet]
        [Route("procedimientos_medicos")]
        public async Task<IActionResult> GetProcedimientosMedicos()
        {
            var data = await _postgresql.GetProcedimientosMedicosAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("procedimiento_medico/({nombre})")]
        public async Task<IActionResult> GetProcedimientoMedico(string nombre)
        {
            var data = await _postgresql.GetProcedimientoMedicoPorNombreAsync(nombre);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


    }
}
